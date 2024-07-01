//*****************************************************************************
// ユーティリティのライブラリ
//
// 概要　　：　システム関連のユーティリティクラス
//               
// 作成　　：屠
//            2006/05/16
//
//*****************************************************************************
using System.Globalization; 
using System.Threading;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System;

namespace SafeNeeds.DySmat.Util
{ 
	/// <summary>
	/// 
	/// </summary>
    public class SystemApp
	{ 
		private static Mutex _objMutex;		// '二重起動防止用
		
		/// <summary>
		/// 標準のカルチャ情報の取得
		/// </summary>
		/// <returns></returns>
		public static CultureInfo GetStandardCulture() 
		{ 
			CultureInfo cul = new CultureInfo("ja-JP", false); 
			cul.DateTimeFormat.FullDateTimePattern = "yyyy'年'MM'月'dd'日' HH:mm:ss"; 
			cul.DateTimeFormat.LongDatePattern = "yyyy'年'M'月'd'日'"; 
			cul.DateTimeFormat.ShortDatePattern = "yyyy/MM/dd"; 
			cul.DateTimeFormat.YearMonthPattern = "yyyy'年'MM'月'"; 
			
			return cul; 
		} 

		
		/// <summary>
		/// 二重起動の判定
		/// </summary>
		/// <param name="sAppName">アプリ名</param>
		/// <returns></returns>
		public static bool HasPrevInstance(string sAppName) 
		{ 
			_objMutex = new Mutex(false, sAppName); 
			
			if (_objMutex.WaitOne(0, false)) 
			{ 
				return false; 
			} 
			else 
			{ 
				_objMutex.Close(); 
				
				return true; 
			} 
		}

        /// <summary>
        /// Winform中自定义控件判断是否处于IDE设计模式
        /// </summary>
        /// <returns></returns>
        public static bool IsDesignMode()
        {
            bool returnFlag = false;
#if DEBUG
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                returnFlag = true;
            }
            else if (Process.GetCurrentProcess().ProcessName == "devenv")
            {
                returnFlag = true;
            }
#endif
            return returnFlag;

        }

        /// <summary>
        /// イベントより、メソッドを実行する
        /// </summary>
        /// <param name="name"></param>
        public static void MethodInvoke(object obj, string methodName)
        {
            if (obj == null) return;

            MethodInfo method;
            try
            {
                method = obj.GetType().GetMethod(methodName);
            }
            catch (Exception ex)
            {
                //エラー処理
                throw new ApplicationException(ex.ToString());
            }

            if (method == null)
            {
                throw new ApplicationException(string.Format("Methodが存在しない{0}\n", methodName));
            }
            else
            {
                method.Invoke(obj, new object[] { });
            }
        }
	} 
}
