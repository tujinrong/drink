//*****************************************************************************
// ユーティリティのライブラリ
//
// 概要　　：　CSVファイル関連のユーティリティクラス
//               
// 作成　　：屠
//            2006/05/16
//
//*****************************************************************************
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SafeNeeds.DySmat.Util
{ 
	/// <summary>
	/// 
	/// </summary>
    public class CsvFile
	{ 
		private bool _bInQuotMasks = true;	
		/// <summary>
		/// CSVの項目ごとは引用符を利用する必要があるか
		/// </summary>
		public bool IsInQuotMasks
		{
			get
			{
				return _bInQuotMasks;
			}
			set
			{
				_bInQuotMasks = value;
			}
		}


		private bool _bReplaceCRLF = false;
		/// <summary>
		/// 項目内容中のエンターと改行コードは置き換える必要があるか
		/// </summary>
		public bool IsReplaceCRLF
		{
			get
			{
				return _bReplaceCRLF;
			}
			set
			{
				_bReplaceCRLF = value;
			}
		}

		
		/// <summary>
		/// CSVファイルの読み込み
		/// </summary>
        /// <param name="fileName">ファイル名</param>
        /// <param name="content">内容</param>
		/// <returns>true:成功 ;　false:失敗</returns>
		public bool ReadCsv(string fileName, ref List<string[]> content) 
		{ 
			string[] sResult = new string[]{string.Empty};
			bool bRet = true;

			StreamReader sr = new StreamReader(fileName, Encoding.GetEncoding("Shift_JIS")); 

			bool bContentEnd = true;

			while ((sr.Peek() != -1)) 
			{ 
				string[] sBuffers = sr.ReadLine().Trim().Split(','); 
				
				CombineContent(sBuffers, ref sResult, ref bContentEnd); 

				if (bContentEnd)
				{
					for (int i = 0; i <= sResult.Length - 1; i++) 
					{ 
						if (_bReplaceCRLF)
						{ 
							sResult[i] = sResult[i].Replace("\\n", StringUtil.CRLF);
						}
					}

					content.Add(sResult); 
				}
			} 

			sr.Close(); 

			return bRet; 
		} 
		
		/// <summary>
		/// CSV形式のファイルの出力
		/// </summary>
        /// <param name="fileName">ファイル名</param>
        /// <param name="content">出力内容</param>
        /// <param name="append">ファイル出力形式(false: ファイル上書, true: ファイル末の行追加)</param>
		/// <returns></returns>
		public bool WriteCsv(string fileName, List<string[]> content, bool append) 
		{
            StreamWriter sw = new StreamWriter(fileName, append, Encoding.Default);
			bool bRet = true;

			//-----------------------------------------------------------------
			// CSV出力処理
			//-----------------------------------------------------------------
			foreach (string[] sContents in content) 
			{ 
				string sRowContent = string.Empty;	// 行ごとの内容

				for (int i = 0; i <= sContents.Length - 1; i++) 
				{ 
					string sBuffer = sContents[i];	// 指定列のデータ
                    bool bFlag = false;

                    // エンターと改行コードは置き換える必要がある場合
                    if (_bReplaceCRLF == true)
                    {
                        string sTmp = sBuffer;

                        // エンターコードを空白に置き換える
                        sBuffer = sBuffer.Replace(StringUtil.CRLF, StringUtil.CR);

                        if (sTmp.Length != sBuffer.Length)
                        {
                            bFlag = true;
                        }
                    }
                    else
                    {
                        if (sBuffer.IndexOf(StringUtil.CRLF) > -1)
                        {
                            bFlag = true;
                        }
                    }

					// 内容中の「"」を「""」に置き換える
                    if (sBuffer.IndexOf("\"") > -1)
                    {
                        sBuffer = sBuffer.Replace("\"", "\"\"");

                        // 引用符を利用する必要がない場合、引用符を追加
                        if (_bInQuotMasks == false)
                        {
                            sBuffer = "\"" + sBuffer + "\"";
                        }
                    }
                    else if (sBuffer.IndexOf(",") > -1 && _bInQuotMasks == false)
                    {
                        // 内容中の「,」が存在して、しかも引用符を利用する必要がない場合、引用符を追加
                        sBuffer = "\"" + sBuffer + "\"";
                    }
                    else if (bFlag == true)
                    {
                        if (_bInQuotMasks == false)
                        {
                            sBuffer = "\"" + sBuffer + "\"";
                        }
                    }

                    // 引用符を利用する必要がある場合
                    if (_bInQuotMasks == true)
                    {
                        sBuffer = "\"" + sBuffer + "\"";
                    }

					sRowContent += sBuffer + ","; 
				} 
				
				if (sRowContent != "")
				{ 
					sRowContent = sRowContent.Substring(0, sRowContent.Length - 1); 
				} 

				sw.WriteLine(sRowContent); 
			} 

			sw.Flush(); 
			sw.Close(); 

			return bRet; 
		}

        /// <summary>
        /// CSV形式のファイルの出力
        /// </summary>
        /// <param name="fileName">ファイル名</param>
        /// <param name="content">出力内容</param>
        /// <returns></returns>
        public bool WriteCsv(string fileName, List<string[]> content)
        {
            return WriteCsv(fileName, content, false);
        }

		/// <summary>
		/// 項目内容の組み合わせ
		/// </summary>
		/// <param name="sOrigins">入力内容</param>
		/// <param name="sDests">出力内容</param>
        /// <param name="bContentEnd"></param>
		private void CombineContent(string[] sOrigins, ref string[] sDests, ref bool bContentEnd)
		{ 
			List<string> alResult = new List<string>(); 
			
			string sContent = string.Empty; 

			bool bComma = true;

			if (!bContentEnd)
			{
				alResult.AddRange(sDests);
				
				sContent = sDests[sDests.Length - 1] + StringUtil.CRLF;

				alResult.RemoveAt(sDests.Length - 1);

				bComma = false;
			}

			for (int i = 0; i <= sOrigins.Length - 1; i++) 
			{ 
				if (bContentEnd)
				{
					int iCnt = 0;

					//if (ContainsOddQuot(sOrigins[i].Trim(), ref iCnt) && sOrigins[i].Trim().Length == iCnt)
                    if (ContainsOddQuot(sOrigins[i].Trim(), ref iCnt))
                    {
                        if (sOrigins[i].StartsWith("\"") && sOrigins[i].EndsWith("\""))
                        {
                            sContent = sOrigins[i].Substring(1, sContent.Length - 2);

                            alResult.Add(sContent.Replace("\"\"", "\""));

                            bContentEnd = true;

                            bComma = true;

                            continue;
                        }
                        else if ((!sOrigins[i].StartsWith("\"") && !sOrigins[i].EndsWith("\"")) || (!sOrigins[i].StartsWith("\"") && sOrigins[i].EndsWith("\"")))
                        {
                            sContent = sOrigins[i];

                            alResult.Add(sContent);

                            bContentEnd = true;

                            bComma = true;

                            continue;
                        }

                        sContent = sOrigins[i];

                        bContentEnd = false;
                    }
                    else
                    {
                        if (sOrigins[i].StartsWith("\"") && sOrigins[i].EndsWith("\""))
                        {
                            sContent = sOrigins[i];

                            if (sContent.StartsWith("\"") && sContent.EndsWith("\""))
                            {
                                sContent = sContent.Substring(1, sContent.Length - 2);
                            }

                            alResult.Add(sContent.Replace("\"\"", "\""));
                        }
                        else if (sOrigins[i].StartsWith("\"") && !sOrigins[i].EndsWith("\""))
                        {
                            //sContent = sOrigins[i].Substring(1, sOrigins[i].Length - 1);

                            //if (sContent.StartsWith("\""))
                            //{
                            //    sContent = sOrigins[i];

                            //    alResult.Add(sContent.Replace("\"\"", "\""));
                            //}
                            //else
                            //{
                                sContent = sOrigins[i];

                                bContentEnd = false;
                            //}
                        }
                        else
                        {
                            sContent = sOrigins[i];

                            if (sContent.StartsWith("\"") && sContent.EndsWith("\""))
                            {
                                sContent = sContent.Substring(1, sContent.Length - 2);
                            }

                            alResult.Add(sContent.Replace("\"\"", "\""));
                        }
                    }
					
				}
				else 
				{ 
					if (!bComma)
					{
						sContent += sOrigins[i];

						if (sContent.StartsWith("\"") && sContent.EndsWith("\""))
						{
                            //if (_bInQuotMasks == false)
                            //{
                                sContent = sContent.Substring(1, sContent.Length - 2);
                            //}
                            
                            alResult.Add(sContent.Replace("\"\"", "\""));

							bContentEnd = true;
						
							bComma = true;

							continue;
						}
					}
					else
					{
						sContent += "," + sOrigins[i];
					}
				
					//if (!sOrigins[i].StartsWith("\"") && sOrigins[i].EndsWith("\""))
                    if (sOrigins[i].EndsWith("\""))
					{
                        //-------------- 2006/12/22 余 追加 S------------------
                        //if (_bInQuotMasks == false)
                        //{
                            sContent = sContent.Substring(1, sContent.Length - 2);
                        //}
                        //-------------- 2006/12/22 余 追加 E------------------
                        
                        alResult.Add(sContent.Replace("\"\"", "\""));

						bContentEnd = true;
						
						bComma = true;
					}
				} 
			} 

			if (!bContentEnd)
			{
				alResult.Add(sContent);
			}
			
			sDests = (string[])alResult.ToArray();
		} 
		
		/// <summary>
		/// 指定される文字列に「"」が奇数個含まれているかどうかを判定する
		/// </summary>
		/// <param name="sContent">指定される文字列</param>
		/// <param name="iCount">「"」の実際個数</param>
		/// <returns>true: 奇数個; false: 偶数個</returns>
		private bool ContainsOddQuot(string sContent, ref int iCount) 
		{ 
			bool bRet = true;
			int iPos = -1; 
			int iCnt = 0;

			do 
			{ 
				bRet = !(bRet);

				iPos = sContent.IndexOf("\"", iPos + 1); 

				iCnt++;
			} while (iPos >= 0); 

			iCount = iCnt - 1;

			return bRet; 
		}
       
	} 
}
