using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkService.Report.FD.Model
{
    public class ShopWithSysClientModel
    {
        public string Type { get; set; }

        /// <summary>
        /// 加盟店外部コード : 支店コード（７桁）
        /// </summary>
        public string ShopCD { get; set; }

        /// <summary>
        /// シャトルコード : ＦＤ提供元シャトルコード　(先頭'00'＋４桁のシャトルコード）
        /// </summary>
        public string ShuttleCD { get; set; }

        /// <summary>
        /// 実績日付 : 実績日付（YYYYMMDD形式）
        /// </summary>
        public string ReportDate { get; set; }

        /// <summary>
        /// 納品書伝票番号 : お客様納品書伝票番号（シャトルで採番した番号）
        /// </summary>
        public string SlipNO { get; set; }

        /// <summary>
        /// お客さまコード : 加盟店殿採番のお客さまコード
        /// </summary>
        public string ClientCD { get; set; }

        /// <summary>
        /// シーケンス番号 : 伝票番号内のデータ連番０から連番に採番
        /// </summary>
        public string Seq { get; set; }

        /// <summary>
        /// サーヴコード : ALL ZERO （サーヴ機能廃止に伴う対応）
        /// </summary>
        public string SubCD { get; set; }

        /// <summary>
        /// 管理コード : シャトル採番の顧客管理コード
        /// </summary>
        public string ManageCD { get; set; }

        /// <summary>
        /// 前回未収額符号 : 前回未収額がﾏｲﾅｽ："-"、以外："0"
        /// </summary>
        public string PreUnGetMoneyCD { get; set; }

        /// <summary>
        /// 前回未収額 : 当該顧客の前回未収額（符号なし編集）
        /// </summary>
        public string PreUnGetMoney { get; set; }

        /// <summary>
        /// 今回売上額符号 : 今回売上額がﾏｲﾅｽ："-"、以外："0"
        /// </summary>
        public string ThisSoldMoneyCD { get; set; }

        /// <summary>
        /// 今回売上額 : 当該顧客の今回売上額（符号なし編集）　税込み金額('03.12.15UPD)
        /// </summary>
        public string ThisSoldMoney { get; set; }

        /// <summary>
        /// 今回入金額符号 : 今回入金額がﾏｲﾅｽ："-"、以外："0"
        /// </summary>
        public string ThisGetMoneyCD { get; set; }

        /// <summary>
        /// 今回入金額 : 当該顧客の今回入金額（符号なし編集） 前回未収額+今回売上額('03.12.15UPD)
        /// </summary>
        public string ThisGetMoney { get; set; }

        /// <summary>
        /// 課税額符号 : 課税額がﾏｲﾅｽ："-"、以外："0"
        /// </summary>
        public string TaxCD { get; set; }

        /// <summary>
        /// 課税額 : 今回売上額に対する内消費税額（符号なし編集）
        /// </summary>
        public string Tax { get; set; }

        /// <summary>
        /// 実績区分 : 当該顧客全体に対する実績区分　('00'実績、'01'後日、'02'今ｽﾄ)
        /// </summary>
        public string PositiveID { get; set; }

        /// <summary>
        /// 代行手数料率 : 通常、ﾅｲﾄ、その他の条件によって手数料率を設定する(0埋め)
        /// </summary>
        public string FreeRate { get; set; }

        /// <summary>
        ///消費税種別 : 0：新税率　1：軽減税率　2：旧税率（経過措置）
        /// </summary>
        public string TaxTypeCD { get; set; }


        /// <summary>
        ///課税種別 : 1：総額　2：外税　3:免税
        /// </summary>
        public string KsTaxTypeCD { get; set; }

        /// <summary>
        /// 予備 : ＳＰＡＣＥ
        /// </summary>
        public string Preliminary { get; set; }

        /// <summary>
        ///作成年 
        /// </summary>
        public string Year { get; set; }

        /// <summary>
        /// 作成月
        /// </summary>
        public string Month { get; set; }

        /// <summary>
        /// 作成日
        /// </summary>
        public string Day { get; set; }


        /// <summary>
        /// 新ダウンロード
        /// </summary>
        public bool IsNew { get; set; }
        
        /// <summary>
        /// 取引種別
        /// </summary>
        public string TransactionType { get; set; }

        /// <summary>
        /// 使用数
        /// </summary>
        public int UsedCount { get; set; }

        /// <summary>
        /// SoldMoney
        /// </summary>
        public int SoldMoney { get; set; }

        

    }
}
