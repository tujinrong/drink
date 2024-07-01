//*****************************************************************************
// ���[�e�B���e�B�̃��C�u����
//
// �T�v�@�@�F�@�t�@�C���֘A�̃��[�e�B���e�B�N���X
//               
// �쐬�@�@�F�j
//            2006/05/12 
//
//*****************************************************************************
using System; 
using System.Net; 
using System.IO; 
using System.Text;


namespace SafeNeeds.DySmat.Util
{ 
	/// <summary>
	/// �t�@�C���֘A�̃��[�e�B���e�B�N���X
	/// </summary>
	public class FileUtil 
	{ 
		/// <summary>
		/// �t�H���_�̑��ݔ���
		/// </summary>
        /// <param name="path">�p�X</param>
		/// <returns></returns>
		public static bool IsExistPath(string path) 
		{ 
			try 
			{ 
				if (Directory.Exists(path)) 
				{ 
					return true; 
				} 
			} 
			catch
			{ 
			}

			return false; 
		} 


		/// <summary>
		/// �h���C�u�̔ԍ�����
		/// </summary>
        /// <param name="driver"></param>
		/// <returns></returns>
		public static bool IsDrive(string driver) 
		{ 
			string sTmp; 

			try 
			{ 
				sTmp = driver.Trim(); 

				if (sTmp.Length < 2) 
				{ 
					return false; 
				} 

				sTmp = sTmp.Substring(0, 2); 
				if (sTmp == "\\\\") 
				{ 
					return true; 
				} 
				
				sTmp = sTmp.ToUpper(); 
				if (sTmp.ToCharArray(0, 1)[0] >= 'C' && sTmp.ToCharArray(0, 1)[0] <= 'Z' && sTmp.ToCharArray(1, 1)[0] >= ':') 
				{ 
					return true; 
				} 
			} 
			catch
			{ 
			}

			return false; 
		} 


		/// <summary>
		/// �t�@�C���̑��ݔ���
		/// </summary>
        /// <param name="fileName">�t�@�C����</param>
		/// <returns></returns>
		public static bool IsExistFile(string fileName) 
		{ 
			try 
			{ 
				if (File.Exists(fileName)) 
				{ 
					return true; 
				}
			}
			catch 
			{ 
			} 

			return false; 
		} 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
		public static string RenameCopyFile(string fileName) 
		{ 
			return string.Empty;
		} 
	} 
}
