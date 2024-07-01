//*****************************************************************************
// ユーティリティのライブラリ
//
// 概要　　：　ファイル関連のユーティリティクラス
//               
// 作成　　：屠
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
	/// ファイル関連のユーティリティクラス
	/// </summary>
	public class FileUtil 
	{ 
		/// <summary>
		/// フォルダの存在判定
		/// </summary>
        /// <param name="path">パス</param>
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
		/// ドライブの番号判定
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
		/// ファイルの存在判定
		/// </summary>
        /// <param name="fileName">ファイル名</param>
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
