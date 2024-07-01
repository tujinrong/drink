using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkService.Report.FD.Report
{
    public class FDReportShopWithSys : FDReport
    {
        public FDReportShopWithSys() {
            this.ItemTypeFieldName = "Type";

            this.Defines = new Dictionary<string, List<FDReportItemDefined>>();

            #region defineClient

            List<FDReportItemDefined> defineClient = new List<FDReportItemDefined>();

            //　加盟店外部コード : 支店コード（７桁）
            defineClient.Add(new FDReportItemDefined { Field = "ShopCD", ByteLength = 7 });

            //　シャトルコード : ＦＤ提供元シャトルコード　(先頭'00'＋４桁のシャトルコード）
            defineClient.Add(new FDReportItemDefined { Field = "ShuttleCD", ByteLength = 6 });

            //　実績日付 : 実績日付（YYYYMMDD形式）
            defineClient.Add(new FDReportItemDefined { Field = "ReportDate", ByteLength = 8 });

            //  納品書伝票番号 : お客様納品書伝票番号（シャトルで採番した番号）
            defineClient.Add(new FDReportItemDefined { Field = "SlipNO", ByteLength = 6, PadDirection = 1 });

            //　お客さまコード : 加盟店殿採番のお客さまコード
            defineClient.Add(new FDReportItemDefined { Field = "ClientCD", ByteLength = 8, PadDirection = 1 });

            //　シーケンス番号 : 伝票番号内のデータ連番０から連番に採番
            defineClient.Add(new FDReportItemDefined { Field = "Seq", ByteLength = 3, PadDirection = 1 });

            //　サーヴコード : ALL ZERO （サーヴ機能廃止に伴う対応）
            defineClient.Add(new FDReportItemDefined { Field = "SubCD", ByteLength = 8 });

            //　管理コード : シャトル採番の顧客管理コード
            defineClient.Add(new FDReportItemDefined { Field = "ManageCD", ByteLength = 8 });

            //　前回未収額符号 : 前回未収額がﾏｲﾅｽ："-"、以外："0"
            defineClient.Add(new FDReportItemDefined { Field = "PreUnGetMoneyCD", ByteLength = 1 });

            //　前回未収額 : 当該顧客の前回未収額（符号なし編集）
            defineClient.Add(new FDReportItemDefined { Field = "PreUnGetMoney", ByteLength = 8, PadDirection = 1});

            //　今回売上額符号 : 今回売上額がﾏｲﾅｽ："-"、以外："0"
            defineClient.Add(new FDReportItemDefined { Field = "ThisSoldMoneyCD", ByteLength = 1 });

            //　今回売上額 : 当該顧客の今回売上額（符号なし編集）　税込み金額('03.12.15UPD)
            defineClient.Add(new FDReportItemDefined { Field = "ThisSoldMoney", ByteLength = 8, PadDirection = 1 });

            //　今回入金額符号 : 今回入金額がﾏｲﾅｽ："-"、以外："0"
            defineClient.Add(new FDReportItemDefined { Field = "ThisGetMoneyCD", ByteLength = 1 });

            //  今回入金額 : 当該顧客の今回入金額（符号なし編集） 前回未収額+今回売上額('03.12.15UPD)
            defineClient.Add(new FDReportItemDefined { Field = "ThisGetMoney", ByteLength = 8, PadDirection = 1 });

            //  課税額符号 : 課税額がﾏｲﾅｽ："-"、以外："0"
            defineClient.Add(new FDReportItemDefined { Field = "TaxCD", ByteLength = 1 });

            //  課税額 : 今回売上額に対する内消費税額（符号なし編集）
            defineClient.Add(new FDReportItemDefined { Field = "Tax", ByteLength = 8, PadDirection = 1 });

            //  実績区分 : 当該顧客全体に対する実績区分　('00'実績、'01'後日、'02'今ｽﾄ)
            defineClient.Add(new FDReportItemDefined { Field = "PositiveID", ByteLength = 2 });

            //  代行手数料率 : 通常、ﾅｲﾄ、その他の条件によって手数料率を設定する(0埋め)
            defineClient.Add(new FDReportItemDefined { Field = "FreeRate", ByteLength = 4 });

            //　消費税種別 : 0：新税率　1：軽減税率　2：旧税率（経過措置）
            defineClient.Add(new FDReportItemDefined { Field = "TaxTypeCD", ByteLength = 1, PadChar = ' ' });

            //　課税種別 : 1：総額　2：外税　3:免税
            defineClient.Add(new FDReportItemDefined { Field = "KsTaxTypeCD", ByteLength = 1, PadChar = ' ' });

            //  予備 : ＳＰＡＣＥ
            defineClient.Add(new FDReportItemDefined { Field = "Preliminary", ByteLength = 57, PadChar = ' ' });

            //  作成年
            defineClient.Add(new FDReportItemDefined { Field = "Year", ByteLength = 4 });

            //  作成月
            defineClient.Add(new FDReportItemDefined { Field = "Month", ByteLength = 2 });

            //  作成日
            defineClient.Add(new FDReportItemDefined { Field = "Day", ByteLength = 2 });


            this.Defines.Add("Client", defineClient);

            #endregion


            #region defineClient

            List<FDReportItemDefined> defineItem = new List<FDReportItemDefined>();

            //　加盟店外部コード : 支店コード（７桁）
            defineItem.Add(new FDReportItemDefined { Field = "ShopCD", ByteLength = 7 });

            //　シャトルコード : ＦＤ提供元シャトルコード　(先頭'00'＋４桁のシャトルコード）
            defineItem.Add(new FDReportItemDefined { Field = "ShuttleCD", ByteLength = 6 });

            //　実績日付 : 実績日付（YYYYMMDD形式）
            defineItem.Add(new FDReportItemDefined { Field = "ReportDate", ByteLength = 8 });

            //  納品書伝票番号 : 加盟店殿採番のお客さまコード
            defineItem.Add(new FDReportItemDefined { Field = "SlipNO", ByteLength = 6, PadDirection = 1 });

            //　お客さまコード : 加盟店殿採番のお客さまコード
            defineItem.Add(new FDReportItemDefined { Field = "ClientCD", ByteLength = 8, PadDirection = 1 });

            //　シーケンス番号 : 伝票番号内のデータ連番０から連番に採番
            defineItem.Add(new FDReportItemDefined { Field = "Seq", ByteLength = 3, PadDirection = 1 });



            //　事業コード : 事業コード（３桁前ＺＥＲＯ編集）
            defineItem.Add(new FDReportItemDefined { Field = "WorkCD", ByteLength = 3 });

            //　品コード１ : 品コード１　（契約商品コード１）
            defineItem.Add(new FDReportItemDefined { Field = "ItemCD1", ByteLength = 4 });

            //　品コード２ : 品コード2　（契約商品コード2）
            defineItem.Add(new FDReportItemDefined { Field = "ItemCD2", ByteLength = 2 });

            //　納品商品コード : 実績納品商品コード　（商品コード１＋商品コード２）
            defineItem.Add(new FDReportItemDefined { Field = "ItemCD", ByteLength = 6 });

            //　予備 : ＳＰＡＣＥ
            defineItem.Add(new FDReportItemDefined { Field = "ItemPreliminary", ByteLength = 1, PadChar = ' ' });

            //　マットＮＯ : ＯＭＭマット番号　（'0'＋マットNo７桁　　ＯＭＭ以外　ALL ZERO）
            defineItem.Add(new FDReportItemDefined { Field = "MatNO", ByteLength = 7 });

            //　CTメンテ料金符号 : CTのﾒﾝﾃ料金がﾏｲﾅｽ："-"、以外："0"
            defineItem.Add(new FDReportItemDefined { Field = "CTMoneyCD", ByteLength = 1 });

            //　CTメンテ料金 : CTのﾒﾝﾃ料金（符号なし編集）　基本週の場合に台数×基本料金を設定基本料金は税込み金額('03.12.15UPD)
            defineItem.Add(new FDReportItemDefined { Field = "CTMoney", ByteLength = 8, PadDirection = 1 });

            //  予定納品数符号 : 納品予定数がﾏｲﾅｽ："-"、以外："0"
            defineItem.Add(new FDReportItemDefined { Field = "ScheduleDeliveryCD", ByteLength = 1 });

            //  予定納品数 : 納品予定数（符号なし編集）
            defineItem.Add(new FDReportItemDefined { Field = "ScheduleDelivery", ByteLength = 6, PadDirection = 1 });

            //  納品予定金額符号 : 納品予定金額がﾏｲﾅｽ："-"、以外："0"
            defineItem.Add(new FDReportItemDefined { Field = "ScheduleDeliveryMoneyCD", ByteLength = 1 });

            //  納品予定金額 : 納品予定金額（符号なし編集）　　税込み金額('03.12.15UPD)
            defineItem.Add(new FDReportItemDefined { Field = "ScheduleDeliveryMoney", ByteLength = 8, PadDirection = 1 });


            //  実績区分 : 当該顧客全体に対する実績区分　('00'実績、'01'後日、'02'今ｽﾄ)
            defineItem.Add(new FDReportItemDefined { Field = "PositiveID", ByteLength = 2 });

            //　実績納品数符号 : 実績納品数がﾏｲﾅｽ："-"、以外："0"
            defineItem.Add(new FDReportItemDefined { Field = "PositiveDeliveryCD", ByteLength = 1 });

            //　実績納品数 : 実績納品数（符号なし編集）
            defineItem.Add(new FDReportItemDefined { Field = "PositiveDelivery", ByteLength = 6, PadDirection = 1 });

            //  実績回収数符号 : 実績回収数がﾏｲﾅｽ："-"、以外："0"
            defineItem.Add(new FDReportItemDefined { Field = "PositiveBackCD", ByteLength = 1 });

            //　実績回収数 : 実績回収数（符号なし編集）
            defineItem.Add(new FDReportItemDefined { Field = "PositiveBack", ByteLength = 6, PadDirection = 1 });

            //　実績客中残数符号 : 実績客中残がﾏｲﾅｽ："-"、以外："0"
            defineItem.Add(new FDReportItemDefined { Field = "PositiveRemainCD", ByteLength = 1 });

            //　実績客中残 : 実績客中残（符号なし編集）
            defineItem.Add(new FDReportItemDefined { Field = "PositiveRemain", ByteLength = 6, PadDirection = 1 });

            //  実績売上符号 : 実績売上がﾏｲﾅｽ："-"、以外："0"
            defineItem.Add(new FDReportItemDefined { Field = "PositiveSaleCD", ByteLength = 1 });

            //　実績売上 : 当該商品に対する売上実績（符号なし編集）　税込み金額('03.12.15UPD)
            defineItem.Add(new FDReportItemDefined { Field = "PositiveSale", ByteLength = 8, PadDirection = 1 });

            //  メンテ区分 : "0":単価制、"1":基本制、"2":メンテ制、　ＣＴ以外の時は"0"
            defineItem.Add(new FDReportItemDefined { Field = "MaintenanceCD", ByteLength = 1 });

            //  予備 : ＳＰＡＣＥ
            defineItem.Add(new FDReportItemDefined { Field = "Preliminary", ByteLength = 36, PadChar = ' ' });

            //  作成年
            defineItem.Add(new FDReportItemDefined { Field = "Year", ByteLength = 4 });

            //  作成月
            defineItem.Add(new FDReportItemDefined { Field = "Month", ByteLength = 2 });

            //  作成日
            defineItem.Add(new FDReportItemDefined { Field = "Day", ByteLength = 2 });


            this.Defines.Add("Item", defineItem);

            #endregion
        }
    }
}
