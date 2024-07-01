//*****************************************************************************
// ユーティリティのライブラリ
//
// 概要　　：　データの種別
//               
// 作成　　：屠
//            2015.3
//
//*****************************************************************************

namespace SafeNeeds.DySmat.DB
{ 
	/// <summary>
	/// データの種別
	/// </summary>
	public enum EnumDbDataType 
	{
        /// <summary>
        /// 文字列
        /// </summary>
        UNICODE,

		/// <summary>
        /// 文字列
		/// </summary>
        STRING,
		
        ///// <summary>
        ///// 数値
        /// 十進数
        /// </summary>
        NUMBER,

        INT64,

        INT32,

        INT16,

        INT8,

        /// <summary>
        /// 論理型
        /// </summary>
		BOOLEAN,
		
        /// <summary>
        /// 日付型
        /// </summary>
        DATE,
		
        /// <summary>
        /// 浮動小数点
        /// </summary>
        FLOAT,
		

		
        /// <summary>
        /// 长文本
        /// </summary>
        LONGTEXT,

        /// <summary>
        /// 字节数组
        /// </summary>
        BLOB,

        /// <summary>
        /// 字符大对象
        /// </summary>
        CLOB
	} 
}
