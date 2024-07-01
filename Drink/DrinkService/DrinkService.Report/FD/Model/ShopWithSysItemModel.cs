using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkService.Report.FD.Model
{
    public class ShopWithSysItemModel
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
        /// 納品書伝票番号 : 加盟店殿採番のお客さまコード
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
        /// 事業コード : 事業コード（３桁前ＺＥＲＯ編集）
        /// </summary>
        public string WorkCD { get; set; }

        /// <summary>
        /// 品コード１ : 品コード１　（契約商品コード１）
        /// </summary>
        public string ItemCD1 { get; set; }

        /// <summary>
        /// 品コード２ : 品コード2　（契約商品コード2）
        /// </summary>
        public string ItemCD2 { get; set; }

        /// <summary>
        /// 納品商品コード : 実績納品商品コード　（商品コード１＋商品コード２）
        /// </summary>
        public string ItemCD { get; set; }

        /// <summary>
        /// 予備 : ＳＰＡＣＥ
        /// </summary>
        public string ItemPreliminary { get; set; }

        /// <summary>
        /// マットＮＯ : ＯＭＭマット番号　（'0'＋マットNo７桁　　ＯＭＭ以外　ALL ZERO）
        /// </summary>
        public string MatNO { get; set; }

        /// <summary>
        /// CTメンテ料金符号 : CTのﾒﾝﾃ料金がﾏｲﾅｽ："-"、以外："0"
        /// </summary>
        public string CTMoneyCD { get; set; }

        /// <summary>
        /// CTメンテ料金 : CTのﾒﾝﾃ料金（符号なし編集）　基本週の場合に台数×基本料金を設定基本料金は税込み金額('03.12.15UPD)
        /// </summary>
        public string CTMoney { get; set; }

        /// <summary>
        /// 予定納品数符号 : 納品予定数がﾏｲﾅｽ："-"、以外："0"
        /// </summary>
        public string ScheduleDeliveryCD { get; set; }

        /// <summary>
        /// 予定納品数 : 納品予定数（符号なし編集）
        /// </summary>
        public string ScheduleDelivery { get; set; }

        /// <summary>
        /// 納品予定金額符号 : 納品予定金額がﾏｲﾅｽ："-"、以外："0"
        /// </summary>
        public string ScheduleDeliveryMoneyCD { get; set; }

        /// <summary>
        /// 納品予定金額 : 納品予定金額（符号なし編集）　　税込み金額('03.12.15UPD)
        /// </summary>
        public string ScheduleDeliveryMoney { get; set; }

        /// <summary>
        /// 実績区分 : 当該顧客全体に対する実績区分　('00'実績、'01'後日、'02'今ｽﾄ)
        /// </summary>
        public string PositiveID { get; set; }

        /// <summary>
        /// 実績納品数符号 : 実績納品数がﾏｲﾅｽ："-"、以外："0"
        /// </summary>
        public string PositiveDeliveryCD { get; set; }

        /// <summary>
        /// 実績納品数 : 実績納品数（符号なし編集）
        /// </summary>
        public string PositiveDelivery { get; set; }

        /// <summary>
        /// 実績回収数符号 : 実績回収数がﾏｲﾅｽ："-"、以外："0"
        /// </summary>
        public string PositiveBackCD { get; set; }

        /// <summary>
        /// 実績回収数 : 実績回収数（符号なし編集）
        /// </summary>
        public string PositiveBack { get; set; }

        /// <summary>
        /// 実績客中残数符号 : 実績客中残がﾏｲﾅｽ："-"、以外："0"
        /// </summary>
        public string PositiveRemainCD { get; set; }

        /// <summary>
        /// 実績客中残 : 実績客中残（符号なし編集）
        /// </summary>
        public string PositiveRemain { get; set; }

        /// <summary>
        /// 実績売上符号 : 実績売上がﾏｲﾅｽ："-"、以外："0"
        /// </summary>
        public string PositiveSaleCD { get; set; }

        /// <summary>
        /// 実績売上 : 当該商品に対する売上実績（符号なし編集）　税込み金額('03.12.15UPD)
        /// </summary>
        public string PositiveSale { get; set; }

        /// <summary>
        /// メンテ区分 : "0":単価制、"1":基本制、"2":メンテ制、　ＣＴ以外の時は"0"
        /// </summary>
        public string MaintenanceCD { get; set; }

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
        ///消費税種別 : 0：新税率　1：軽減税率　2：旧税率（経過措置）
        /// </summary>
        public string TaxTypeCD { get; set; }


        /// <summary>
        /// 使用数
        /// </summary>
        public int UsedCount { get; set; }

    }
}
