//*****************************************************************************
// [システム]  T&G テレフォンレポート管理
// 
// [機能概要]  モデルベース。
//             モデル共通内容、可変部分の定義
//
// [作成履歴]　2014/06/25  屠錦栄　初版 
//
// [レビュー]　2014/07/17  屠錦栄　 
//*****************************************************************************

namespace WebEvaluation.Models
{
    /// <summary>
    /// `モデルベース
    /// </summary>
    public class ModelBase
    {
        //ユーザIDの文字数
        public const int USERID_LEN = 8;

        //グループコードの文字数
        public const int GROUPCD_LEN = 2;
        
        //社員コードの文字数
        public const int STAFFCD_LEN = 8;
        
        //店舗コードの文字数
        public const int SHOPCD_LEN = 5;

        //事業部コードの文字数
        public const int DIVCD_LEN=1;

        //エリアコードの文字数
    //    public const int AREACD_LEN = 5;
    
        //電話番号の文字数
        public const int TEL_LEN = 13;

        //新郎、新婦名前文字数
        public const int CUSNAME_LEN = 30;

        //組織
        public const int UNITCD_LEN = 11;
    }
}