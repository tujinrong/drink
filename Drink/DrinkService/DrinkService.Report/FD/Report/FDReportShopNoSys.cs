using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkService.Report.FD.Report
{
    public class FDReportShopNoSys : FDReport
    {
        public FDReportShopNoSys()
        {
            this.ItemTypeFieldName = "Type";

            this.Defines = new Dictionary<string, List<FDReportItemDefined>>();

            #region defineClient

            List<FDReportItemDefined> defineClient = new List<FDReportItemDefined>();


            //　レコード区分 : 顧客レコードの場合、”１”固定
            defineClient.Add(new FDReportItemDefined { Field = "RecoeCD", ByteLength = 1 });


            //　実績日付 : 実績日付（YYYYMMDD形式）
            defineClient.Add(new FDReportItemDefined { Field = "ReportDate", ByteLength = 8 });

            //  納品書伝票番号 : お客様納品書伝票番号（シャトルで採番した番号）
            defineClient.Add(new FDReportItemDefined { Field = "SlipNO", ByteLength = 6, PadDirection = 1 });

            //　お客さまコード : 加盟店殿採番のお客さまコード
            defineClient.Add(new FDReportItemDefined { Field = "ClientCD", ByteLength = 8, PadDirection = 1 });


            //　前回未収額 : 当該顧客の前回未収額（ﾏｲﾅｽ編集）
            defineClient.Add(new FDReportItemDefined { Field = "PreUnGetMoney", ByteLength = 7, PadDirection = 1, PadChar = ' ' });


            //　今回売上額 : 当該顧客の今回売上額（ﾏｲﾅｽ編集）　税込み金額('03.12.15UPD)
            defineClient.Add(new FDReportItemDefined { Field = "ThisSoldMoney", ByteLength = 7, PadDirection = 1, PadChar = ' ' });

            //  課税額 : 今回売上額に対する内消費税額（符号なし編集）
            defineClient.Add(new FDReportItemDefined { Field = "Tax", ByteLength = 7, PadDirection = 1, PadChar = ' ' });

            //  今回入金額 : 当該顧客の今回売上額（ﾏｲﾅｽ編集）　税込み金額('03.12.15UPD)
            defineClient.Add(new FDReportItemDefined { Field = "ThisGetMoney", ByteLength = 7, PadDirection = 1, PadChar = ' ' });


            //  実績区分 : 当該顧客全体に対する実績区分　('00'実績、'01'後日、'02'今ｽﾄ)
            defineClient.Add(new FDReportItemDefined { Field = "PositiveID", ByteLength = 2, PadChar = ' ' });

            //　シャトルコード : ＦＤ提供元シャトルコード　(先頭'00'＋４桁のシャトルコード）
            defineClient.Add(new FDReportItemDefined { Field = "ShuttleCD", ByteLength = 6, PadChar = ' ' });

            //　管理コード : シャトル採番の顧客管理コード
            defineClient.Add(new FDReportItemDefined { Field = "ManageCD", ByteLength = 8, PadDirection = 1 });

            //　契約番号 : シャトル採番の契約番号コード
            defineClient.Add(new FDReportItemDefined { Field = "OrderCD", ByteLength = 9, PadChar = ' ' });

            //　消費税種別 : 0：新税率　1：軽減税率　2：旧税率（経過措置）
            defineClient.Add(new FDReportItemDefined { Field = "TaxTypeCD", ByteLength = 1, PadChar = ' ' });

            //　課税種別 : 1：総額　2：外税　3:免税
            defineClient.Add(new FDReportItemDefined { Field = "KsTaxTypeCD", ByteLength = 1, PadChar = ' ' });

            //  予備 : ＳＰＡＣＥ
            defineClient.Add(new FDReportItemDefined { Field = "Preliminary", ByteLength = 21, PadChar = ' ' });


            this.Defines.Add("Client", defineClient);

            #endregion


            #region defineClient

            List<FDReportItemDefined> defineItem = new List<FDReportItemDefined>();


            //　レコード区分 : 商品レコードの場合、”２”固定
            defineItem.Add(new FDReportItemDefined { Field = "RecoeCD", ByteLength = 1 });

            //　実績日付 : 実績日付（YYYYMMDD形式）
            defineItem.Add(new FDReportItemDefined { Field = "ReportDate", ByteLength = 8 });

            //  納品書伝票番号 : 加盟店殿採番のお客さまコード
            defineItem.Add(new FDReportItemDefined { Field = "SlipNO", ByteLength = 6, PadDirection = 1 });

            //　お客さまコード : 加盟店殿採番のお客さまコード
            defineItem.Add(new FDReportItemDefined { Field = "ClientCD", ByteLength = 8, PadDirection = 1 });


            //　事業コード : 事業コード（３桁前ＺＥＲＯ編集）
            defineItem.Add(new FDReportItemDefined { Field = "WorkCD", ByteLength = 3, PadChar = ' ' });

            //　品コード１ : 品コード１　（契約商品コード１）
            defineItem.Add(new FDReportItemDefined { Field = "ItemCD1", ByteLength = 4, PadChar = ' ' });

            //　品コード２ : 品コード2　（契約商品コード2）
            defineItem.Add(new FDReportItemDefined { Field = "ItemCD2", ByteLength = 2, PadChar = ' ' });

            //　納品商品コード : 実績納品商品コード　（商品コード１＋商品コード２）
            defineItem.Add(new FDReportItemDefined { Field = "ItemCD", ByteLength = 6, PadChar = ' ' });

            //　マットＮＯ : ＯＭＭマット番号　（'0'＋マットNo７桁　　ＯＭＭ以外　ALL ZERO）
            defineItem.Add(new FDReportItemDefined { Field = "MatNO", ByteLength = 8, PadChar = ' ' });


            //　実績納品数 : 実績納品数（ﾏｲﾅｽ編集）
            defineItem.Add(new FDReportItemDefined { Field = "PositiveDelivery", ByteLength = 4, PadDirection = 1, PadChar = ' ' });


            //　実績回収数 : 実績回収数（ﾏｲﾅｽ編集）
            defineItem.Add(new FDReportItemDefined { Field = "PositiveBack", ByteLength = 4, PadDirection = 1, PadChar = ' ' });

            //　実績客中残 : 実績客中残（ﾏｲﾅｽ編集）
            defineItem.Add(new FDReportItemDefined { Field = "PositiveRemain", ByteLength = 4, PadDirection = 1, PadChar = ' ' });


            //　実績売上 : 当該商品に対する売上実績（ﾏｲﾅｽ編集）　税込み金額('03.12.15UPD)
            defineItem.Add(new FDReportItemDefined { Field = "PositiveSale", ByteLength = 7, PadDirection = 1, PadChar = ' ' });


            //  基本料金 : ＣＴメンテナンス料　（ＣＴの場合に表示　ＣＴ以外　ALL ZERO）
            defineItem.Add(new FDReportItemDefined { Field = "BaseMoney", ByteLength = 7, PadDirection = 1, PadChar = ' ' });

            //  実績区分 : 当該顧客全体に対する実績区分　('00'実績、'01'後日、'02'今ｽﾄ)
            defineItem.Add(new FDReportItemDefined { Field = "PositiveID", ByteLength = 2, PadChar = ' ' });

            //　シャトルコード : ＦＤ提供元シャトルコード　(先頭'00'＋４桁のシャトルコード）
            defineItem.Add(new FDReportItemDefined { Field = "ShuttleCD", ByteLength = 6, PadChar = ' ' });

            //  予定納品数 : 納品予定数（ﾏｲﾅｽ編集）
            defineItem.Add(new FDReportItemDefined { Field = "ScheduleDelivery", ByteLength = 4, PadDirection = 1, PadChar = ' ' });

            //  納品予定金額 : 納品予定金額（ﾏｲﾅｽ編集）　　税込み金額('03.12.15UPD)
            defineItem.Add(new FDReportItemDefined { Field = "ScheduleDeliveryMoney", ByteLength = 7, PadDirection = 1, PadChar = ' ' });

            //  メンテ区分 : "0":単価制、"1":基本制、"2":メンテ制、　ＣＴ以外の時は"0"
            defineItem.Add(new FDReportItemDefined { Field = "MaintenanceCD", ByteLength = 1, PadChar = ' ' });

            //  予備 : ＳＰＡＣＥ
            defineItem.Add(new FDReportItemDefined { Field = "Preliminary", ByteLength = 7, PadChar = ' ' });

            this.Defines.Add("Item", defineItem);

            #endregion
        }
    }
}
