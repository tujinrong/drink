//*****************************************************************************
// [システム]  ダスキン・配置ドリンク
// 
// [機能概要]  モデルベース。
//             モデル共通内容、可変部分の定義
//
// [作成履歴]　2014/06/25  屠錦栄　初版 
//
// [レビュー]　2014/07/17  屠錦栄　 
//*****************************************************************************

namespace DrinkService.Models
{
    /// <summary>
    /// `モデルベース
    /// </summary>
    public class ModelBase
    {
        public const string CN区分 = "CodeType";
        public const string CN地域 = "Region";
        public const string CNエリア = "Area";
        public const string CN役割 = "Role";
        public const string CN棚="Shelf";
        public const string CN取引種別 = "TransactionType";
//        public const string CN商品種別 = "06"; 
        public const string CN済="DoneFlag";
        public const string CN初回 = "FirstFlag";
        public const string CN店舗区分 = "ShopType";
        public const string CN店舗業務区分 = "SysTypeCD";

        public const string CN役割_システム管理者 = "1";
        public const string CN役割_本部管理者 = "2";
        public const string CN役割_ドリンクマネジメント = "3";
        public const string CN役割_店舗管理者 = "4";
        public const string CN役割_店舗担当者 = "5";
        public const string CN役割_本部参照 = "6";

        public const int ITEMCD_LEN = 8;

        public const int CLIENTCD_LEN = 7;
        
        //社員コードの文字数
        public const int STAFFCD_LEN = 5;
        public const int STAFFNAME_LEN = 10;
        //店舗コードの文字数
        public const int SHOPCD_LEN = 7;

        public const int ROUTE_LEN =4;

        //電話番号の文字数
        public const int TEL_LEN = 16;

        public const int NUM_LEN = 3;

        public const int PRICE_LEN = 4;

        public const int MONEY_LEN = 6;

        public const int SLIP_LEN = 6;

        public const int FlAG_LEN = 1;
        

    }
}