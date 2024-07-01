//*****************************************************************************
// ユーティリティのライブラリ
//
// 概要　　：　ログファイルの管理クラス
//               
// 作成　　：屠
//            2006/05/11 
//
//*****************************************************************************
using System; 
using System.Collections;
using System.IO; 
using System.Text;

namespace SafeNeeds.DySmat.Util
{ 
	/// <summary>
	/// ログファイルの管理クラス
	/// </summary>
	public class LogWriter 
	{ 
		/// <summary>
        /// ログレベルタイプ
		/// </summary>
		public enum LogLevelType 
		{ 
			/// <summary>
            /// ログを出力しない
			/// </summary>
            None = 0,
			
            /// <summary>
            /// 例外を出力する
            /// </summary>
            Exception = 1,
			
            /// <summary>
            /// データ処理部分まで出力する
            /// </summary>
            DBAccess = 2,
			
            /// <summary>
            /// メッセージを出力する
            /// </summary>
            Message = 3,
			
            /// <summary>
            /// ディバグ情報を出力
            /// </summary>
            Debug = 4,
			
            /// <summary>
            /// すべて出力する
            /// </summary>
            All = 5
		}


		/// <summary>
        /// ログファイルのバックアップタイプ
		/// </summary>
		public enum LogBackupType
		{
			/// <summary>
			/// 
			/// </summary>
            Size = 0,

            /// <summary>
            /// 
            /// </summary>
			Date = 1
		}

		private TextWriter _stream;

		/// <summary>
		/// ログファイルの管理対象をクリエートする
		/// </summary>
		public LogWriter() 
		{ 
			if (_primaryInstance == null) 
			{ 
				_primaryInstance = this; 
			} 
		} 


		/// <summary>
		/// ログファイルの管理対象をクリエートする
		/// </summary>
		/// <param name="sFileName">ログファイル名</param>
		public LogWriter(string sFileName)
		{
			if (_primaryInstance == null) 
			{ 
				_primaryInstance = this; 
			} 

			_sFileName = sFileName;
		}

		
		#region	　プロパティ　

		private string _sDatetimeFormat = "yyyy\\/MM\\/dd HH:mm:ss";
		/// <summary>
		/// 日付時刻のフォーマットを取得または設定する
		/// </summary>
		public string DatetimeFormat
		{
			get
			{
				return _sDatetimeFormat;
			}
			set
			{
				_sDatetimeFormat = value;
			}
		}


		private bool _bKeepStream = true;
		/// <summary>
		/// 
		/// </summary>
		public bool KeepStream 
		{ 
			get 
			{ 
				return _bKeepStream; 
			} 

			set 
			{ 
				if (!(value) && !(_stream == null)) 
				{ 
					Close();
				} 
				_bKeepStream = value; 
			} 
		} 

		
		private string _sFileName = "";
		/// <summary>
		/// ログファイル名を取得または設定する
		/// </summary>
		public string LogFile 
		{ 
			get 
			{ 
				return _sFileName; 
			} 
			set 
			{ 
				_sFileName = value;

				string sPath = string.Empty;
				int iPos = _sFileName.LastIndexOf("\\");

				if(iPos > 0)
				{
					sPath = _sFileName.Substring(0, iPos);

					if (!Directory.Exists(sPath))
					{
						Directory.CreateDirectory(sPath);
					}
				}
			} 
		} 


		private LogLevelType _logLevel = LogLevelType.All;
		/// <summary>
		/// ログ出力のレベルを取得または設定する
		/// </summary>
		public LogLevelType LogLevel 
		{ 
			get 
			{ 
				return _logLevel; 
			} 
			set 
			{ 
				_logLevel = value; 
			} 
		} 


		private LogBackupType _logBackup = LogBackupType.Size;
		/// <summary>
		/// ログファイルのバックアップタイプを取得または設定する
		/// </summary>
		public LogBackupType LogBackup
		{
			get 
			{ 
				return _logBackup; 
			} 
			set 
			{ 
				_logBackup = value; 
			} 
		}

		
		private int _iMaxFileNo = 999;
		/// <summary>
		/// ログファイルの最大番号を取得または設定する
		/// </summary>
		public int MaxFileNo 
		{ 
			get 
			{ 
				return _iMaxFileNo; 
			} 
			set 
			{ 
				if (value < 1) 
				{ 
					value = 1; 
				} 
				_iMaxFileNo = value; 
			} 
		} 


		private int _iMaxFileSize = 1024;
		/// <summary>
		/// ファイル最大サイズを取得または設定する（単位：KB）
		/// </summary>
		public int MaxFileSize 
		{ 
			get 
			{ 
				return _iMaxFileSize; 
			} 
			set 
			{ 
				if (value < 1) 
				{ 
					value = 1; 
				} 
				_iMaxFileSize = value; 
			} 
		} 


		private int _iMaxFileDay = 7;
		/// <summary>
		/// ファイル存在の最大時間を取得または設定する（単位：日）
		/// </summary>
		public int MaxFileDay
		{
			get
			{
				return _iMaxFileDay;
			}
			set
			{
				_iMaxFileDay = value;
			}
		}

		
		private static LogWriter _primaryInstance;
		/// <summary>
		/// 
		/// </summary>
		public static LogWriter PrimaryInstance 
		{ 
			get 
			{ 
				return _primaryInstance; 
			} 
		} 


		#endregion

		#region 　メソッド(Public)　

		/// <summary>
		/// ファイルのオープン
		/// </summary>
		public void Open() 
		{ 
			if (_sFileName == "") 
			{ 
				return; 
			} 

			if (_bKeepStream) 
			{ 
				Close();
			} 

			BackupFile();

			if (_bKeepStream) 
			{ 
				_stream = new StreamWriter(_sFileName, true, Encoding.Default); 
			} 
		} 
		

		/// <summary>
		/// ファイルのクローズ
		/// </summary>
		public void Close() 
		{ 
			if (this._stream != null)
			{
				this._stream.Close();
				this._stream = null;
			}
		}


		/// <summary>
		/// メッセージのファイルへの書き込み
		/// </summary>
        /// <param name="msg">メッセージ</param>
		public void WriteMsg(string msg) 
		{ 
			string sMessage = DataUtil.DateToString(DateTime.Now, _sDatetimeFormat) + 
								StringUtil.SPACE + msg; 
			
			AddLog2Debug(sMessage); 
			
			if (_logLevel > LogLevelType.None) 
			{ 
				WriteLogFile(sMessage); 
			} 
		} 


		/// <summary>
		/// ログタイプとテーブル名とメッセージのファイルへの書き込みとSQL文の作成・実行
		/// </summary>
        /// <param name="userID">UserID</param>
        /// <param name="tableName">テーブル名</param>
        /// <param name="msg">メッセージ</param>
		public void WriteTableMsg(string userID, string tableName, string msg) 
		{ 
			string sMessage = DataUtil.DateToString(DateTime.Now, _sDatetimeFormat) + StringUtil.SPACE + 
							userID + StringUtil.SPACE + StringUtil.SPACE + tableName + StringUtil.SPACE + msg; 

			AddLog2Debug(sMessage); 
			
			if (_logLevel == LogLevelType.DBAccess || _logLevel == LogLevelType.All) 
			{ 
				WriteLogFile(sMessage); 
			} 
		} 


		/// <summary>
		/// デバッグ処理時のファイルへの書き込み
		/// </summary>
        /// <param name="msg">メッセージ</param>
		public void WriteDebug(string msg) 
		{ 
			string sMessage = DataUtil.DateToString(DateTime.Now, _sDatetimeFormat) + StringUtil.SPACE + msg; 

			AddLog2Debug(sMessage); 
			
			if (_logLevel == LogLevelType.Debug || _logLevel == LogLevelType.All) 
			{ 
				WriteLogFile(sMessage); 
			} 
		} 


		/// <summary>
		/// 異常発生時のファイルへの書き込み
		/// </summary>
        /// <param name="msg">メッセージ</param>
		/// <param name="ex">発生した異常</param>
		public void WriteException(string msg, Exception ex) 
		{ 
			if (_logLevel == LogLevelType.Exception || 
				_logLevel == LogLevelType.Debug || 
				_logLevel == LogLevelType.All) 
			{ 
				WriteLogFile(DataUtil.DateToString(DateTime.Now, _sDatetimeFormat) + StringUtil.SPACE + msg);
				WriteLogFile("--------------------------------------------------------------------------------");
				WriteLogFile(ex.Message + StringUtil.CRLF + ex.StackTrace);
				WriteLogFile("--------------------------------------------------------------------------------");
			} 
		}


		/// <summary>
		/// 異常発生時のファイルへの書き込み
		/// </summary>
		/// <param name="ex">発生した異常</param>
		public void WriteException(Exception ex) 
		{ 
			const string MSG = "異常を発生しましたが、詳細は下記の通りです。";

			if (_logLevel == LogLevelType.Exception || 
				_logLevel == LogLevelType.Debug || 
				_logLevel == LogLevelType.All) 
			{ 
				WriteLogFile(DataUtil.DateToString(DateTime.Now, _sDatetimeFormat) + StringUtil.SPACE + MSG);
				WriteLogFile("--------------------------------------------------------------------------------");
				WriteLogFile(ex.Message + StringUtil.CRLF + ex.StackTrace);
				WriteLogFile("--------------------------------------------------------------------------------");
			} 
		}

        /// <summary>
        /// メッセージをファイルに書き込む
        /// </summary>
        /// <param name="strMsg"></param>
        public void WriteLogFile(string strMsg)
        {
            lock (typeof(LogWriter))
            {
                if (!(_bKeepStream))
                {
                    BackupFile();

                    _stream = new StreamWriter(_sFileName, true, Encoding.Default);
                }

                if (_stream == null)
                {
                    Open();
                }

                // メッセージの書き込み
                _stream.WriteLine(strMsg);

                if (!(_bKeepStream))
                {
                    Close();
                }
                else
                {
                    _stream.Flush();
                }
            }
        } 
		
		#endregion

		#region 　メソッド(Private)　


		/// <summary>
		/// デバッグする時、メッセージをConsoleに表示する
		/// </summary>
		/// <param name="sMsg"></param>
		private void AddLog2Debug(string sMsg) 
		{ 
#if DEBUG
			System.Console.WriteLine(sMsg); 
#endif
		} 


		/// <summary>
		/// ログファイルのバックアップ
		/// </summary>
		private void BackupFile()
		{
			//-----------------------------------------------------------------
			// ファイルが存在する場合、ファイルのバックアップを行う
			//-----------------------------------------------------------------
			if (File.Exists(_sFileName))
			{
				// ファイル情報の取得
				FileInfo fileinfo = new FileInfo(_sFileName);

				switch(_logBackup)
				{
					case LogBackupType.Size:  
						// 最大サイズを超える場合、バックアップする
						if ( fileinfo.Length > _iMaxFileSize * 1024)
//						if (fileinfo.Length > 1)
						{
							BackupFileSub(_sFileName, _iMaxFileNo.ToString().Length);
						}
						break;

					case LogBackupType.Date: 
						// 指定される日数を超える場合、バックアップする
						TimeSpan ts = DateTime.Now - fileinfo.LastWriteTime;
						if (ts.Days > _iMaxFileDay)
						{
							BackupFileSub(_sFileName, _iMaxFileNo.ToString().Length);
						}
						break;
				}
			}
		}


		/// <summary>
		/// バックアップファイルを作成する
		/// </summary>
        /// <param name="fileName">ファイル名</param>
        /// <param name="fileNoLen"></param>
		private void BackupFileSub(string fileName, int fileNoLen) 
		{ 
			// ファイルのパス、ファイル名、拡張子を取得する
			int iPos = fileName.LastIndexOf("\\");
			string sPath = fileName.Substring(0, iPos + 1);
			string sName = fileName.Substring(iPos + 1, fileName.LastIndexOf(".") - iPos - 1);
			string sExt =  fileName.Substring(fileName.LastIndexOf(".") + 1);
			
			// ファイル番号を取得する
			string sFileNo = GetFileNo(sPath, sName, sExt, fileNoLen);

			// 新しいファイル名を設定する
			string sNewFileName = sPath + sName + sFileNo + "." + sExt;

			// 元のファイルを新しいファイルにコピーする
			File.Copy(_sFileName, sNewFileName);

			// 元のファイルを削除する
			File.Delete(_sFileName);
		} 

	
		/// <summary>
		/// バックアップする時、ログファイルの番号を取得する
		/// </summary>
		/// <param name="sPath">ファイルパス</param>
		/// <param name="sName">ファイル名(拡張子を含めない)</param>
		/// <param name="sExt">ファイル拡張子</param>
		/// <param name="iFileNoLen">ファイル番号の桁数</param>
		/// <returns></returns>
		private string GetFileNo(string sPath, string sName, string sExt, int iFileNoLen) 
		{ 
			//-----------------------------------------------------------------
			// ファイル番号のフォーマットの設定
			//-----------------------------------------------------------------
			string sFileNoFormat = string.Empty;
			for (int i = 0; i < iFileNoLen; i++)
			{
				sFileNoFormat += "0";
			}

			//-----------------------------------------------------------------
			// ファイル番号の取得
			//-----------------------------------------------------------------
			string[] sFiles = Directory.GetFiles(sPath, "*" + sName + "*." + sExt); 
			string sFileNo; 
			string sRet = string.Empty; 
			int iMaxFileNo; 
			SortedList slFileNo = new SortedList();

			// 存在した関連ファイルにより、番号を取得し、SortListに格納する
			for (int i = 0; i <= sFiles.Length - 1; i++) 
			{ 
				sFileNo = sFiles[i].Substring(sFiles[i].Length - iFileNoLen - 4, iFileNoLen); 
				if ((DataUtil.IsNumeric(sFileNo))) 
				{ 
					slFileNo.Add(DataUtil.CInt(sFileNo), sFiles[i]); 
				} 
			} 

			if ((slFileNo.Count != 0)) 
			{ 
				// ------------------------------------------------------------
				// ファイル番号が存在する場合
				// ------------------------------------------------------------
				// 番号の最大値を取得
				iMaxFileNo = DataUtil.CInt(slFileNo.GetKey(slFileNo.Count - 1));

				// 取得した最大番号は実際の最大番号より小さい場合、自動採番
				if (iMaxFileNo < _iMaxFileNo)
				{ 
					iMaxFileNo += 1;
					sRet = iMaxFileNo.ToString(sFileNoFormat);

					return sRet; 
				} 

				// 実際の最大番号を超えてしまった場合、空き番を探し、割り当てる
				int j = 0; 
				if (slFileNo.Count < _iMaxFileNo)
				{
					foreach (int iNo in slFileNo.Keys) 
					{ 
						if ((j + 1 != iNo)) 
						{ 
							iMaxFileNo = j + 1;
							sRet = iMaxFileNo.ToString(sFileNoFormat);

							return sRet; 
						} 

						j = j + 1; 
					}
				}

				// 全て埋まっていた場合、一番古い番号を取得する
				DateTime oldestTime = DateTime.Now; 
				foreach (string sFileName in slFileNo.Values) 
				{ 
					FileInfo fileInfo = new FileInfo(sFileName); 
					if (fileInfo.LastWriteTime < oldestTime) 
					{ 
						oldestTime = fileInfo.LastWriteTime; 
						sRet = sFileName.Substring(sFileName.Length - iFileNoLen - 4, iFileNoLen);
					} 
				} 
			} 
			else 
			{ 
				// ファイル番号が存在しない場合
				iMaxFileNo = 1;

				sRet = iMaxFileNo.ToString(sFileNoFormat);
			}

			return sRet; 
		} 
		

		#endregion
	} 
}
