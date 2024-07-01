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
        public const string CN区分 = "00";
        public const string CN地域 = "01";
        public const string CNエリア = "02";
        public const string CN役割 = "03";
        public const string CN棚="05";
        public const string CN商品種別 = "06"; 
        public const string CN済="08";
        public const string CN初回 = "09";
        public const string CN店舗区分 = "10";
        public const string CN店舗業務区分 = "11";


        public const int ITEMCD_LEN = 8;

        public const int CLIENTCD_LEN = 8;
        
        //社員コードの文字数
        public const int STAFFCD_LEN = 5;
        
        //店舗コードの文字数
        public const int SHOPCD_LEN = 7;

        public const int ROUTE_LEN =4;

        //電話番号の文字数
        public const int TEL_LEN = 15;

        public const int NUM_LEN = 3;

        public const int PRICE_LEN = 3;

        public const int MONEY_LEN = 6;

    }
}