//*****************************************************************************
// ���[�e�B���e�B�̃��C�u����
//
// �T�v�@�@�F�@���O�t�@�C���̊Ǘ��N���X
//               
// �쐬�@�@�F�j
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
	/// ���O�t�@�C���̊Ǘ��N���X
	/// </summary>
	public class LogWriter 
	{ 
		/// <summary>
        /// ���O���x���^�C�v
		/// </summary>
		public enum LogLevelType 
		{ 
			/// <summary>
            /// ���O���o�͂��Ȃ�
			/// </summary>
            None = 0,
			
            /// <summary>
            /// ��O���o�͂���
            /// </summary>
            Exception = 1,
			
            /// <summary>
            /// �f�[�^���������܂ŏo�͂���
            /// </summary>
            DBAccess = 2,
			
            /// <summary>
            /// ���b�Z�[�W���o�͂���
            /// </summary>
            Message = 3,
			
            /// <summary>
            /// �f�B�o�O�����o��
            /// </summary>
            Debug = 4,
			
            /// <summary>
            /// ���ׂďo�͂���
            /// </summary>
            All = 5
		}


		/// <summary>
        /// ���O�t�@�C���̃o�b�N�A�b�v�^�C�v
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
		/// ���O�t�@�C���̊Ǘ��Ώۂ��N���G�[�g����
		/// </summary>
		public LogWriter() 
		{ 
			if (_primaryInstance == null) 
			{ 
				_primaryInstance = this; 
			} 
		} 


		/// <summary>
		/// ���O�t�@�C���̊Ǘ��Ώۂ��N���G�[�g����
		/// </summary>
		/// <param name="sFileName">���O�t�@�C����</param>
		public LogWriter(string sFileName)
		{
			if (_primaryInstance == null) 
			{ 
				_primaryInstance = this; 
			} 

			_sFileName = sFileName;
		}

		
		#region	�@�v���p�e�B�@

		private string _sDatetimeFormat = "yyyy\\/MM\\/dd HH:mm:ss";
		/// <summary>
		/// ���t�����̃t�H�[�}�b�g���擾�܂��͐ݒ肷��
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
		/// ���O�t�@�C�������擾�܂��͐ݒ肷��
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
		/// ���O�o�͂̃��x�����擾�܂��͐ݒ肷��
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
		/// ���O�t�@�C���̃o�b�N�A�b�v�^�C�v���擾�܂��͐ݒ肷��
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
		/// ���O�t�@�C���̍ő�ԍ����擾�܂��͐ݒ肷��
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
		/// �t�@�C���ő�T�C�Y���擾�܂��͐ݒ肷��i�P�ʁFKB�j
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
		/// �t�@�C�����݂̍ő厞�Ԃ��擾�܂��͐ݒ肷��i�P�ʁF���j
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

		#region �@���\�b�h(Public)�@

		/// <summary>
		/// �t�@�C���̃I�[�v��
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
		/// �t�@�C���̃N���[�Y
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
		/// ���b�Z�[�W�̃t�@�C���ւ̏�������
		/// </summary>
        /// <param name="msg">���b�Z�[�W</param>
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
		/// ���O�^�C�v�ƃe�[�u�����ƃ��b�Z�[�W�̃t�@�C���ւ̏������݂�SQL���̍쐬�E���s
		/// </summary>
        /// <param name="userID">UserID</param>
        /// <param name="tableName">�e�[�u����</param>
        /// <param name="msg">���b�Z�[�W</param>
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
		/// �f�o�b�O�������̃t�@�C���ւ̏�������
		/// </summary>
        /// <param name="msg">���b�Z�[�W</param>
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
		/// �ُ픭�����̃t�@�C���ւ̏�������
		/// </summary>
        /// <param name="msg">���b�Z�[�W</param>
		/// <param name="ex">���������ُ�</param>
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
		/// �ُ픭�����̃t�@�C���ւ̏�������
		/// </summary>
		/// <param name="ex">���������ُ�</param>
		public void WriteException(Exception ex) 
		{ 
			const string MSG = "�ُ�𔭐����܂������A�ڍׂ͉��L�̒ʂ�ł��B";

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
        /// ���b�Z�[�W���t�@�C���ɏ�������
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

                // ���b�Z�[�W�̏�������
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

		#region �@���\�b�h(Private)�@


		/// <summary>
		/// �f�o�b�O���鎞�A���b�Z�[�W��Console�ɕ\������
		/// </summary>
		/// <param name="sMsg"></param>
		private void AddLog2Debug(string sMsg) 
		{ 
#if DEBUG
			System.Console.WriteLine(sMsg); 
#endif
		} 


		/// <summary>
		/// ���O�t�@�C���̃o�b�N�A�b�v
		/// </summary>
		private void BackupFile()
		{
			//-----------------------------------------------------------------
			// �t�@�C�������݂���ꍇ�A�t�@�C���̃o�b�N�A�b�v���s��
			//-----------------------------------------------------------------
			if (File.Exists(_sFileName))
			{
				// �t�@�C�����̎擾
				FileInfo fileinfo = new FileInfo(_sFileName);

				switch(_logBackup)
				{
					case LogBackupType.Size:  
						// �ő�T�C�Y�𒴂���ꍇ�A�o�b�N�A�b�v����
						if ( fileinfo.Length > _iMaxFileSize * 1024)
//						if (fileinfo.Length > 1)
						{
							BackupFileSub(_sFileName, _iMaxFileNo.ToString().Length);
						}
						break;

					case LogBackupType.Date: 
						// �w�肳�������𒴂���ꍇ�A�o�b�N�A�b�v����
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
		/// �o�b�N�A�b�v�t�@�C�����쐬����
		/// </summary>
        /// <param name="fileName">�t�@�C����</param>
        /// <param name="fileNoLen"></param>
		private void BackupFileSub(string fileName, int fileNoLen) 
		{ 
			// �t�@�C���̃p�X�A�t�@�C�����A�g���q���擾����
			int iPos = fileName.LastIndexOf("\\");
			string sPath = fileName.Substring(0, iPos + 1);
			string sName = fileName.Substring(iPos + 1, fileName.LastIndexOf(".") - iPos - 1);
			string sExt =  fileName.Substring(fileName.LastIndexOf(".") + 1);
			
			// �t�@�C���ԍ����擾����
			string sFileNo = GetFileNo(sPath, sName, sExt, fileNoLen);

			// �V�����t�@�C������ݒ肷��
			string sNewFileName = sPath + sName + sFileNo + "." + sExt;

			// ���̃t�@�C����V�����t�@�C���ɃR�s�[����
			File.Copy(_sFileName, sNewFileName);

			// ���̃t�@�C�����폜����
			File.Delete(_sFileName);
		} 

	
		/// <summary>
		/// �o�b�N�A�b�v���鎞�A���O�t�@�C���̔ԍ����擾����
		/// </summary>
		/// <param name="sPath">�t�@�C���p�X</param>
		/// <param name="sName">�t�@�C����(�g���q���܂߂Ȃ�)</param>
		/// <param name="sExt">�t�@�C���g���q</param>
		/// <param name="iFileNoLen">�t�@�C���ԍ��̌���</param>
		/// <returns></returns>
		private string GetFileNo(string sPath, string sName, string sExt, int iFileNoLen) 
		{ 
			//-----------------------------------------------------------------
			// �t�@�C���ԍ��̃t�H�[�}�b�g�̐ݒ�
			//-----------------------------------------------------------------
			string sFileNoFormat = string.Empty;
			for (int i = 0; i < iFileNoLen; i++)
			{
				sFileNoFormat += "0";
			}

			//-----------------------------------------------------------------
			// �t�@�C���ԍ��̎擾
			//-----------------------------------------------------------------
			string[] sFiles = Directory.GetFiles(sPath, "*" + sName + "*." + sExt); 
			string sFileNo; 
			string sRet = string.Empty; 
			int iMaxFileNo; 
			SortedList slFileNo = new SortedList();

			// ���݂����֘A�t�@�C���ɂ��A�ԍ����擾���ASortList�Ɋi�[����
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
				// �t�@�C���ԍ������݂���ꍇ
				// ------------------------------------------------------------
				// �ԍ��̍ő�l���擾
				iMaxFileNo = DataUtil.CInt(slFileNo.GetKey(slFileNo.Count - 1));

				// �擾�����ő�ԍ��͎��ۂ̍ő�ԍ���菬�����ꍇ�A�����̔�
				if (iMaxFileNo < _iMaxFileNo)
				{ 
					iMaxFileNo += 1;
					sRet = iMaxFileNo.ToString(sFileNoFormat);

					return sRet; 
				} 

				// ���ۂ̍ő�ԍ��𒴂��Ă��܂����ꍇ�A�󂫔Ԃ�T���A���蓖�Ă�
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

				// �S�Ė��܂��Ă����ꍇ�A��ԌÂ��ԍ����擾����
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
				// �t�@�C���ԍ������݂��Ȃ��ꍇ
				iMaxFileNo = 1;

				sRet = iMaxFileNo.ToString(sFileNoFormat);
			}

			return sRet; 
		} 
		

		#endregion
	} 
}
