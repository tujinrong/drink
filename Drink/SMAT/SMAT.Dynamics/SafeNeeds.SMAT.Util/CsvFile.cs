//*****************************************************************************
// ���[�e�B���e�B�̃��C�u����
//
// �T�v�@�@�F�@CSV�t�@�C���֘A�̃��[�e�B���e�B�N���X
//               
// �쐬�@�@�F�j
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
		/// CSV�̍��ڂ��Ƃ͈��p���𗘗p����K�v�����邩
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
		/// ���ړ��e���̃G���^�[�Ɖ��s�R�[�h�͒u��������K�v�����邩
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
		/// CSV�t�@�C���̓ǂݍ���
		/// </summary>
        /// <param name="fileName">�t�@�C����</param>
        /// <param name="content">���e</param>
		/// <returns>true:���� ;�@false:���s</returns>
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
		/// CSV�`���̃t�@�C���̏o��
		/// </summary>
        /// <param name="fileName">�t�@�C����</param>
        /// <param name="content">�o�͓��e</param>
        /// <param name="append">�t�@�C���o�͌`��(false: �t�@�C���㏑, true: �t�@�C�����̍s�ǉ�)</param>
		/// <returns></returns>
		public bool WriteCsv(string fileName, List<string[]> content, bool append) 
		{
            StreamWriter sw = new StreamWriter(fileName, append, Encoding.Default);
			bool bRet = true;

			//-----------------------------------------------------------------
			// CSV�o�͏���
			//-----------------------------------------------------------------
			foreach (string[] sContents in content) 
			{ 
				string sRowContent = string.Empty;	// �s���Ƃ̓��e

				for (int i = 0; i <= sContents.Length - 1; i++) 
				{ 
					string sBuffer = sContents[i];	// �w���̃f�[�^
                    bool bFlag = false;

                    // �G���^�[�Ɖ��s�R�[�h�͒u��������K�v������ꍇ
                    if (_bReplaceCRLF == true)
                    {
                        string sTmp = sBuffer;

                        // �G���^�[�R�[�h���󔒂ɒu��������
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

					// ���e���́u"�v���u""�v�ɒu��������
                    if (sBuffer.IndexOf("\"") > -1)
                    {
                        sBuffer = sBuffer.Replace("\"", "\"\"");

                        // ���p���𗘗p����K�v���Ȃ��ꍇ�A���p����ǉ�
                        if (_bInQuotMasks == false)
                        {
                            sBuffer = "\"" + sBuffer + "\"";
                        }
                    }
                    else if (sBuffer.IndexOf(",") > -1 && _bInQuotMasks == false)
                    {
                        // ���e���́u,�v�����݂��āA���������p���𗘗p����K�v���Ȃ��ꍇ�A���p����ǉ�
                        sBuffer = "\"" + sBuffer + "\"";
                    }
                    else if (bFlag == true)
                    {
                        if (_bInQuotMasks == false)
                        {
                            sBuffer = "\"" + sBuffer + "\"";
                        }
                    }

                    // ���p���𗘗p����K�v������ꍇ
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
        /// CSV�`���̃t�@�C���̏o��
        /// </summary>
        /// <param name="fileName">�t�@�C����</param>
        /// <param name="content">�o�͓��e</param>
        /// <returns></returns>
        public bool WriteCsv(string fileName, List<string[]> content)
        {
            return WriteCsv(fileName, content, false);
        }

		/// <summary>
		/// ���ړ��e�̑g�ݍ��킹
		/// </summary>
		/// <param name="sOrigins">���͓��e</param>
		/// <param name="sDests">�o�͓��e</param>
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
                        //-------------- 2006/12/22 �] �ǉ� S------------------
                        //if (_bInQuotMasks == false)
                        //{
                            sContent = sContent.Substring(1, sContent.Length - 2);
                        //}
                        //-------------- 2006/12/22 �] �ǉ� E------------------
                        
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
		/// �w�肳��镶����Ɂu"�v����܂܂�Ă��邩�ǂ����𔻒肷��
		/// </summary>
		/// <param name="sContent">�w�肳��镶����</param>
		/// <param name="iCount">�u"�v�̎��ی�</param>
		/// <returns>true: ���; false: ������</returns>
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
