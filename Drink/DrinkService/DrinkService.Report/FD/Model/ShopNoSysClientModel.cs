using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkService.Report.FD.Model
{
    public class ShopNoSysClientModel
    {
        public string Type { get; set; }

        /// <summary>
        /// レコード区分 : 顧客レコードの場合、”１”固定
        /// </summary>
        public string RecoeCD { get; set; }


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
        /// 前回未収額 : 当該顧客の前回未収額（ﾏｲﾅｽ編集）
        /// </summary>
        public string PreUnGetMoney { get; set; }


        /// <summary>
        /// 今回売上額 : 当該顧客の今回売上額（ﾏｲﾅｽ編集）　税込み金額('03.12.15UPD)
        /// </summary>
        public string ThisSoldMoney { get; set; }

        /// <summary>
        /// 課税額 : 今回売上額に対する内消費税額（符号なし編集）
        /// </summary>
        public string Tax { get; set; }

        /// <summary>
        /// 今回入金額 : 当該顧客の今回売上額（ﾏｲﾅｽ編集）　税込み金額('03.12.15UPD)
        /// </summary>
        public string ThisGetMoney { get; set; }


        /// <summary>
        /// 実績区分 : 当該顧客全体に対する実績区分　('00'実績、'01'後日、'02'今ｽﾄ)
        /// </summary>
        public string PositiveID { get; set; }

        /// <summary>
        /// シャトルコード : ＦＤ提供元シャトルコード　(先頭'00'＋４桁のシャトルコード）
        /// </summary>
        public string ShuttleCD { get; set; }

        /// <summary>
        /// 管理コード : シャトル採番の顧客管理コード
        /// </summary>
        public string ManageCD { get; set; }

        /// <summary>
        ///契約番号 : シャトル採番の契約番号コード 
        /// </summary>
        public string OrderCD { get; set; }


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

    public class ShopNoSysItemModel
    {
        public string Type { get; set; }

        /// <summary>
        /// レコード区分 : 商品レコードの場合、”２”固定
        /// </summary>
        public string RecoeCD { get; set; }

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
        /// マットＮＯ : ＯＭＭマット番号　（'0'＋マットNo７桁　　ＯＭＭ以外　ALL ZERO）
        /// </summary>
        public string MatNO { get; set; }


        /// <summary>
        /// 実績納品数 : 実績納品数（ﾏｲﾅｽ編集）
        /// </summary>
        public string PositiveDelivery { get; set; }


        /// <summary>
        /// 実績回収数 : 実績回収数（ﾏｲﾅｽ編集）
        /// </summary>
        public string PositiveBack { get; set; }

        /// <summary>
        /// 実績客中残 : 実績客中残（ﾏｲﾅｽ編集）
        /// </summary>
        public string PositiveRemain { get; set; }


        /// <summary>
        /// 実績売上 : 当該商品に対する売上実績（ﾏｲﾅｽ編集）　税込み金額('03.12.15UPD)
        /// </summary>
        public string PositiveSale { get; set; }


        /// <summary>
        /// 基本料金 : ＣＴメンテナンス料　（ＣＴの場合に表示　ＣＴ以外　ALL ZERO）
        /// </summary>
        public string BaseMoney { get; set; }

        /// <summary>
        /// 実績区分 : 当該顧客全体に対する実績区分　('00'実績、'01'後日、'02'今ｽﾄ)
        /// </summary>
        public string PositiveID { get; set; }

        /// <summary>
        /// シャトルコード : ＦＤ提供元シャトルコード　(先頭'00'＋４桁のシャトルコード）
        /// </summary>
        public string ShuttleCD { get; set; }

        /// <summary>
        /// 予定納品数 : 納品予定数（ﾏｲﾅｽ編集）
        /// </summary>
        public string ScheduleDelivery { get; set; }

        /// <summary>
        /// 納品予定金額 : 納品予定金額（ﾏｲﾅｽ編集）　　税込み金額('03.12.15UPD)
        /// </summary>
        public string ScheduleDeliveryMoney { get; set; }

        /// <summary>
        /// メンテ区分 : "0":単価制、"1":基本制、"2":メンテ制、　ＣＴ以外の時は"0"
        /// </summary>
        public string MaintenanceCD { get; set; }

        /// <summary>
        /// 予備 : ＳＰＡＣＥ
        /// </summary>
        public string Preliminary { get; set; }

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
