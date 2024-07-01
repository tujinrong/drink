//*****************************************************************************
// [システム]  T&G テレフォンレポート管理
// 
// [機能概要]  店舗マスタ。
//
// [作成履歴]　2014/06/25  屠錦栄　初版 
//
// [レビュー]　2014/07/17  屠錦栄　 
//*****************************************************************************

using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using DrinkService.Models;


namespace DrinkService.Models
{
    /// <summary>
    /// 店舗マスタ
    /// </summary>
    public class M_CodeData 
    {
                /// <summary>
        /// テストデータ
        /// </summary>
        /// <returns></returns>
        public static M_Code[] GetData()
        {
            return new M_Code[] {
                new M_Code {  Kind = ModelBase.CN区分, CD= ModelBase.CN地域, Name="地域", RefNo=1, Memo="編集可能"}, 
                new M_Code {  Kind = ModelBase.CN区分, CD= ModelBase.CNエリア, Name="エリア", RefNo=1, Memo="編集可能"}, 
                new M_Code {  Kind = ModelBase.CN区分, CD= ModelBase.CN役割, Name="役割", RefNo=0, Memo="編集不可"},  
                new M_Code {  Kind = ModelBase.CN区分, CD= ModelBase.CN棚, Name="棚",RefNo=2,Memo="編集可能"}, 
                new M_Code {  Kind = ModelBase.CN区分, CD= ModelBase.CN商品種別, Name="商品種別", RefNo=1, Memo="編集可能"}, 
                new M_Code {  Kind = ModelBase.CN区分, CD= ModelBase.CN初回, Name="初回", RefNo=0, Memo="編集不可"}, 
                new M_Code {  Kind = ModelBase.CN区分, CD= ModelBase.CN済, Name="済", RefNo=0, Memo="編集不可"}, 

                new M_Code {  Kind = ModelBase.CN地域, CD= "001", Name="北海道"}, 
                new M_Code {  Kind = ModelBase.CN地域, CD= "002", Name="東北"}, 
                new M_Code {  Kind = ModelBase.CN地域, CD= "003", Name="北関東"}, 
                new M_Code {  Kind = ModelBase.CN地域, CD= "005", Name="東京"	}, 
                new M_Code {  Kind = ModelBase.CN地域, CD= "006", Name="東海"	}, 
                new M_Code {  Kind = ModelBase.CN地域, CD= "007", Name="北陸"	}, 
                new M_Code {  Kind = ModelBase.CN地域, CD= "008", Name="近畿"	}, 
                new M_Code {  Kind = ModelBase.CN地域, CD= "009", Name="中国"	}, 
                new M_Code {  Kind = ModelBase.CN地域, CD= "010", Name="四国"	}, 
                new M_Code {  Kind = ModelBase.CN地域, CD= "011", Name="九州"	}, 
                new M_Code {  Kind = ModelBase.CN地域, CD= "195", Name="本社"	}, 

            new M_Code {  Kind = ModelBase.CNエリア, CD= "001", Name="道央", RefCD="001"}, 
        new M_Code {  Kind = ModelBase.CNエリア, CD= "002", Name="道央南", RefCD="001"}, 
        new M_Code {  Kind = ModelBase.CNエリア, CD= "003", Name="道東", RefCD="001"}, 
        new M_Code {  Kind = ModelBase.CNエリア, CD= "004", Name="道南", RefCD="001"}, 
        new M_Code {  Kind = ModelBase.CNエリア, CD= "005", Name="道北", RefCD="001"}, 
        new M_Code {  Kind = ModelBase.CNエリア, CD= "007", Name="青森", RefCD="002"}, 
        new M_Code {  Kind = ModelBase.CNエリア, CD= "008", Name="岩手", RefCD="002"}, 
        new M_Code {  Kind = ModelBase.CNエリア, CD= "009", Name="宮城", RefCD="002"}, 
        new M_Code {  Kind = ModelBase.CNエリア, CD= "011", Name="秋田", RefCD="002"}, 
        new M_Code {  Kind = ModelBase.CNエリア, CD= "012", Name="山形", RefCD="002"}, 
        new M_Code {  Kind = ModelBase.CNエリア, CD= "013", Name="福島", RefCD="002"}, 
        new M_Code {  Kind = ModelBase.CNエリア, CD= "014", Name="茨城", RefCD="003"}, 
        new M_Code {  Kind = ModelBase.CNエリア, CD= "016", Name="栃木", RefCD="003"}, 
        new M_Code {  Kind = ModelBase.CNエリア, CD= "017", Name="群馬", RefCD="003"}, 
        new M_Code {  Kind = ModelBase.CNエリア, CD= "019", Name="埼玉東", RefCD="003"}, 
        new M_Code {  Kind = ModelBase.CNエリア, CD= "020", Name="埼玉中央", RefCD="003"}, 
        new M_Code {  Kind = ModelBase.CNエリア, CD= "021", Name="埼玉西", RefCD="003"}, 
        new M_Code {  Kind = ModelBase.CNエリア, CD= "041", Name="新潟", RefCD="003"}, 
        new M_Code {  Kind = ModelBase.CNエリア, CD= "045", Name="山梨", RefCD="003"}, 
        new M_Code {  Kind = ModelBase.CNエリア, CD= "046", Name="長野", RefCD="003"}, 
        new M_Code {  Kind = ModelBase.CNエリア, CD= "023", Name="千葉東", RefCD="004"}, 
        new M_Code {  Kind = ModelBase.CNエリア, CD= "025", Name="千葉西", RefCD="004"}, 
        new M_Code {  Kind = ModelBase.CNエリア, CD= "036", Name="神奈川横浜", RefCD="004"}, 
        new M_Code {  Kind = ModelBase.CNエリア, CD= "037", Name="神奈川中央", RefCD="004"}, 
        new M_Code {  Kind = ModelBase.CNエリア, CD= "038", Name="神奈川県北", RefCD="004"}, 
        new M_Code {  Kind = ModelBase.CNエリア, CD= "039", Name="神奈川湘南", RefCD="004"}, 
        new M_Code {  Kind = ModelBase.CNエリア, CD= "027", Name="東京北", RefCD="005"}, 
        new M_Code {  Kind = ModelBase.CNエリア, CD= "028", Name="東京西", RefCD="005"}, 
        new M_Code {  Kind = ModelBase.CNエリア, CD= "029", Name="東京東", RefCD="005"}, 
        new M_Code {  Kind = ModelBase.CNエリア, CD= "030", Name="東京城東", RefCD="005"}, 
        new M_Code {  Kind = ModelBase.CNエリア, CD= "031", Name="東京中央", RefCD="005"}, 
        new M_Code {  Kind = ModelBase.CNエリア, CD= "032", Name="東京南", RefCD="005"}, 
        new M_Code {  Kind = ModelBase.CNエリア, CD= "034", Name="東京武蔵野", RefCD="005"}, 
        new M_Code {  Kind = ModelBase.CNエリア, CD= "035", Name="東京多摩", RefCD="005"}, 
        new M_Code {  Kind = ModelBase.CNエリア, CD= "047", Name="岐阜", RefCD="006"}, 
        new M_Code {  Kind = ModelBase.CNエリア, CD= "048", Name="静岡東", RefCD="006"}, 
        new M_Code {  Kind = ModelBase.CNエリア, CD= "049", Name="静岡西", RefCD="006"}, 
        new M_Code {  Kind = ModelBase.CNエリア, CD= "051", Name="愛知東", RefCD="006"}, 
        new M_Code {  Kind = ModelBase.CNエリア, CD= "052", Name="愛知西", RefCD="006"}, 
        new M_Code {  Kind = ModelBase.CNエリア, CD= "053", Name="愛知北", RefCD="006"}, 
        new M_Code {  Kind = ModelBase.CNエリア, CD= "055", Name="三重", RefCD="006"}, 
        new M_Code {  Kind = ModelBase.CNエリア, CD= "042", Name="富山", RefCD="007"}, 
        new M_Code {  Kind = ModelBase.CNエリア, CD= "043", Name="石川", RefCD="007"}, 
        new M_Code {  Kind = ModelBase.CNエリア, CD= "044", Name="福井", RefCD="007"}, 
        new M_Code {  Kind = ModelBase.CNエリア, CD= "057", Name="滋賀", RefCD="008"}, 
        new M_Code {  Kind = ModelBase.CNエリア, CD= "058", Name="京都", RefCD="008"}, 
        new M_Code {  Kind = ModelBase.CNエリア, CD= "059", Name="大阪中央", RefCD="008"}, 
        new M_Code {  Kind = ModelBase.CNエリア, CD= "061", Name="大阪北", RefCD="008"}, 
        new M_Code {  Kind = ModelBase.CNエリア, CD= "063", Name="大阪東", RefCD="008"}, 
        new M_Code {  Kind = ModelBase.CNエリア, CD= "065", Name="大阪南", RefCD="008"}, 
        new M_Code {  Kind = ModelBase.CNエリア, CD= "067", Name="兵庫東", RefCD="008"}, 
        new M_Code {  Kind = ModelBase.CNエリア, CD= "068", Name="兵庫中央", RefCD="008"}, 
        new M_Code {  Kind = ModelBase.CNエリア, CD= "069", Name="兵庫西", RefCD="008"}, 
        new M_Code {  Kind = ModelBase.CNエリア, CD= "070", Name="奈良", RefCD="008"}, 
        new M_Code {  Kind = ModelBase.CNエリア, CD= "071", Name="和歌山", RefCD="008"}, 
        new M_Code {  Kind = ModelBase.CNエリア, CD= "072", Name="山陰", RefCD="009"}, 
        new M_Code {  Kind = ModelBase.CNエリア, CD= "074", Name="岡山", RefCD="009"}, 
        new M_Code {  Kind = ModelBase.CNエリア, CD= "075", Name="広島東", RefCD="009"}, 
        new M_Code {  Kind = ModelBase.CNエリア, CD= "076", Name="広島", RefCD="009"}, 
        new M_Code {  Kind = ModelBase.CNエリア, CD= "077", Name="山口", RefCD="009"}, 
        new M_Code {  Kind = ModelBase.CNエリア, CD= "078", Name="四国東", RefCD="010"}, 
        new M_Code {  Kind = ModelBase.CNエリア, CD= "081", Name="愛媛", RefCD="010"}, 
        new M_Code {  Kind = ModelBase.CNエリア, CD= "082", Name="福岡北", RefCD="011"}, 
        new M_Code {  Kind = ModelBase.CNエリア, CD= "083", Name="福岡中央", RefCD="011"}, 
        new M_Code {  Kind = ModelBase.CNエリア, CD= "084", Name="福岡南", RefCD="011"}, 
        new M_Code {  Kind = ModelBase.CNエリア, CD= "085", Name="佐賀", RefCD="011"}, 
        new M_Code {  Kind = ModelBase.CNエリア, CD= "086", Name="長崎", RefCD="011"}, 
        new M_Code {  Kind = ModelBase.CNエリア, CD= "087", Name="熊本", RefCD="011"}, 
        new M_Code {  Kind = ModelBase.CNエリア, CD= "088", Name="大分", RefCD="011"}, 
        new M_Code {  Kind = ModelBase.CNエリア, CD= "089", Name="宮崎", RefCD="011"}, 
        new M_Code {  Kind = ModelBase.CNエリア, CD= "090", Name="鹿児島", RefCD="011"}, 
        new M_Code {  Kind = ModelBase.CNエリア, CD= "091", Name="沖縄", RefCD="011"}, 
        new M_Code {  Kind = ModelBase.CNエリア, CD= "095", Name="本社", RefCD="195"}, 



                new M_Code {  Kind = ModelBase.CN役割, CD= "1", Name="システム管理者"}, 
                new M_Code {  Kind = ModelBase.CN役割, CD= "2", Name="本部管理者"}, 
                new M_Code {  Kind = ModelBase.CN役割, CD= "3", Name="店舗管理者"}, 
                new M_Code {  Kind = ModelBase.CN役割, CD= "4", Name="店舗担当者"}, 

                new M_Code {  Kind = ModelBase.CN棚, CD= "0", Name="上", RefNo=0}, 
                new M_Code {  Kind = ModelBase.CN棚, CD= "1", Name="１", RefNo=1}, 
                new M_Code {  Kind = ModelBase.CN棚, CD= "2", Name="２", RefNo=2}, 
                new M_Code {  Kind = ModelBase.CN棚, CD= "3", Name="３", RefNo=3}, 
                new M_Code {  Kind = ModelBase.CN棚, CD= "4", Name="４", RefNo=4}, 

                new M_Code {  Kind = ModelBase.CN商品種別, CD= "10", Name="80円"}, 
                new M_Code {  Kind = ModelBase.CN商品種別, CD= "21", Name="お茶"}, 
                new M_Code {  Kind = ModelBase.CN商品種別, CD= "22", Name="コヒー"}, 
                new M_Code {  Kind = ModelBase.CN商品種別, CD= "23", Name="炭酸"}, 
                new M_Code {  Kind = ModelBase.CN商品種別, CD= "24", Name="水"}, 
                new M_Code {  Kind = ModelBase.CN商品種別, CD= "30", Name="スープ"}, 
                new M_Code {  Kind = ModelBase.CN商品種別, CD= "40", Name="ドリップ"}, 
                new M_Code {  Kind = ModelBase.CN商品種別, CD= "50", Name="カップ"}, 
                new M_Code {  Kind = ModelBase.CN商品種別, CD= "60", Name="健康"}, 
                new M_Code {  Kind = ModelBase.CN商品種別, CD= "70", Name="説明商品"}, 
                new M_Code {  Kind = ModelBase.CN商品種別, CD= "90", Name="その他"}, 

                new M_Code {  Kind = ModelBase.CN済, CD= "0", Name="未"}, 
                new M_Code {  Kind = ModelBase.CN済, CD= "1", Name="済"}, 

                new M_Code {  Kind = ModelBase.CN初回, CD= "0", Name=""}, 
                new M_Code {  Kind = ModelBase.CN初回, CD= "1", Name="○"}, 

                new M_Code {  Kind = ModelBase.CN店舗区分, CD= "1", Name="店舗"}, 
                new M_Code {  Kind = ModelBase.CN店舗区分, CD= "2", Name="管理部署"}, 

                new M_Code {  Kind = ModelBase.CN店舗業務区分, CD= "0", Name="未導入"}, 
                new M_Code {  Kind = ModelBase.CN店舗業務区分, CD= "1", Name="導入済"}, 

            };
        }

        public static void Seed(DrinkService.Models.DrinkServiceContext context)
        {
            context.Codes.AddOrUpdate(GetData());
            context.SaveChanges();
        }
    }
}