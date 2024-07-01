using DrinkService.Utils;
using SafeNeeds.DySmat.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkService.Report.FD.Model
{
    public class FDLogicDetailModel
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


      

        /// <summary>
        /// レコード区分 : 商品レコードの場合、”２”固定
        /// </summary>
        public string RecoeCD { get; set; }


        /// <summary>
        /// 基本料金 : ＣＴメンテナンス料　（ＣＴの場合に表示　ＣＴ以外　ALL ZERO）
        /// </summary>
        public string BaseMoney { get; set; }




        public FDLogicDetailModel(FDDbClientModel client, FDDbGroupModel group, FDDbItemModel item, int indexItem)
        {

            //ShopWithSysItemModel model = new ShopWithSysItemModel();

            this.Type = "Item";

            this.UsedCount = System.Math.Abs(DataUtil.CInt(item.UsedNum));

            this.ShopCD = DataUtil.CStr(item.ShopCD);
            this.TaxTypeCD = DataUtil.CStr(item.TaxTypeCD);

            //固定値　"007800"
            this.ShuttleCD = "00" + "7800";


            this.ReportDate = DataUtil.CDate(group.HoDate).ToString("yyyyMMdd");

            this.ClientCD = DataUtil.CStr(client.KanriClientCD);

            //商品明細単位に連番（使用商品が３明細であれば001,002,003）
            this.Seq = DataUtil.CStr(indexItem);

            this.WorkCD = "0";

            if (DataUtil.CStr(item.ItemCD).Length > 1)
            {
                this.WorkCD = "0" + DataUtil.CStr(item.ItemCD).Substring(0, 2);
            }
            else
            {
                this.WorkCD = "0";
            }


            if (DataUtil.CStr(item.ItemCD).Length > 5)
            {
                this.ItemCD1 = DataUtil.CStr(item.ItemCD).Substring(2, 4);
            }
            else
            {
                this.ItemCD1 = "0";
            }

            if (DataUtil.CStr(item.ItemCD).Length == 8)
            {
                this.ItemCD2 = DataUtil.CStr(item.ItemCD).Substring(6, 2);
                this.ItemCD = DataUtil.CStr(item.ItemCD).Substring(2, 6);
            }
            else
            {
                this.ItemCD2 = "0";
                this.ItemCD = DataUtil.CStr(item.ItemCD);
            }

            //固定値　"0"
            this.ItemPreliminary = "0";

            this.MatNO = "0";
            this.CTMoneyCD = "0";
            this.CTMoney = "0";
            //納品予定数
            if (DataUtil.CDec(item.UsedNum) < 0)
            {
                this.ScheduleDeliveryCD = "-";
                this.ScheduleDelivery = DataUtil.CStr(DataUtil.CDec(item.UsedNum) * -1);
            }
            else
            {
                this.ScheduleDeliveryCD = "0";
                this.ScheduleDelivery = DataUtil.CStr(item.UsedNum);
            }
            //予定売上
            if (DataUtil.CDec(item.Money) < 0)
            {
                this.ScheduleDeliveryMoneyCD = "-";
                if (client.SysTypeCD == "0")
                {
                    this.ScheduleDeliveryMoney = "-" + DataUtil.CStr(DataUtil.CDec(item.Money) * -1);
                }
                else
                {
                    this.ScheduleDeliveryMoney = DataUtil.CStr(DataUtil.CDec(item.Money) * -1);
                }
            }
            else
            {
                this.ScheduleDeliveryMoneyCD = "0";
                this.ScheduleDeliveryMoney = DataUtil.CStr(item.Money);
            }

            this.PositiveID = "0";

            //実績納品数符号
            if (DataUtil.CDec(item.UsedNum) < 0)
            {
                this.PositiveDeliveryCD = "-";
                if (client.SysTypeCD == "0")
                {
                    this.PositiveDelivery = "-" + DataUtil.CStr(DataUtil.CDec(item.UsedNum) * -1);
                }
                else
                {
                    this.PositiveDelivery = DataUtil.CStr(DataUtil.CDec(item.UsedNum) * -1);
                }
            }
            else
            {
                this.PositiveDeliveryCD = "0";
                this.PositiveDelivery = DataUtil.CStr(item.UsedNum);
            }

            //実績回収数符号
            this.PositiveBackCD = "0";
            this.PositiveBack = "0";

            this.PositiveRemainCD = "0";
            this.PositiveRemain = "0";

            //実績売上
            if (DataUtil.CDec(item.Money) < 0)
            {
                this.PositiveSaleCD = "-";
                if (client.SysTypeCD == "0")
                {
                    this.PositiveSale = "-" + DataUtil.CStr(DataUtil.CDec(item.Money) * -1);
                }
                else
                {
                    this.PositiveSale = DataUtil.CStr(DataUtil.CDec(item.Money) * -1);
                }
            }
            else
            {
                this.PositiveSaleCD = "0";
                this.PositiveSale = DataUtil.CStr(item.Money);
            }

            //メンテ区分
            this.MaintenanceCD = "0";

            //実績区分
            this.PositiveID = "00";

            this.Preliminary = " ";

            DateTime dateNow = CommonUtils.GetDateTimeNow();
            this.Year = dateNow.ToString("yyyy");
            this.Month = dateNow.ToString("MM");
            this.Day = dateNow.ToString("dd");



            this.RecoeCD = "2";

            //固定値"00000000"
            if (client.SysTypeCD == "0")
            {
                this.MatNO = "00000000";
            }
           
            this.BaseMoney = "      0";

        }

        public ShopWithSysItemModel ToSysModel(String SlipNO)
        {
            var m = new ShopWithSysItemModel();
            m.Type = this.Type;
            m.ShopCD = this.ShopCD;
            m.ShuttleCD = this.ShuttleCD;
            m.ReportDate = this.ReportDate;
            m.SlipNO = SlipNO;
            m.ClientCD = this.ClientCD;
            m.Seq = this.Seq;
            m.WorkCD = this.WorkCD;
            m.ItemCD1 = this.ItemCD1;
            m.ItemCD2 = this.ItemCD2;
            m.ItemCD = this.ItemCD;
            m.ItemPreliminary = this.ItemPreliminary;
            m.MatNO = this.MatNO;
            m.CTMoneyCD = this.CTMoneyCD;
            m.CTMoney = this.CTMoney;
            m.ScheduleDeliveryCD = this.ScheduleDeliveryCD;
            m.ScheduleDelivery = this.ScheduleDelivery;
            m.ScheduleDeliveryMoneyCD = this.ScheduleDeliveryMoneyCD;
            m.ScheduleDeliveryMoney = this.ScheduleDeliveryMoney;
            m.PositiveID = this.PositiveID;
            m.PositiveDeliveryCD = this.PositiveDeliveryCD;
            m.PositiveDelivery = this.PositiveDelivery;
            m.PositiveBackCD = this.PositiveBackCD;
            m.PositiveBack = this.PositiveBack;
            m.PositiveRemainCD = this.PositiveRemainCD;
            m.PositiveRemain = this.PositiveRemain;
            m.PositiveSaleCD = this.PositiveSaleCD;
            m.PositiveSale = this.PositiveSale;
            m.MaintenanceCD = this.MaintenanceCD;
            m.Preliminary = this.Preliminary;
            m.Year = this.Year;
            m.Month = this.Month;
            m.Day = this.Day;
            m.TaxTypeCD = this.TaxTypeCD;
            m.UsedCount = this.UsedCount;
            return m;
        }

        public ShopNoSysItemModel ToNoSysModel(String SlipNO)
        {
            var m = new ShopNoSysItemModel();

            m.Type = this.Type;
            m.RecoeCD = "2";
            m.ReportDate = this.ReportDate;
            m.SlipNO = SlipNO;
            m.ClientCD = this.ClientCD;
            m.WorkCD = this.WorkCD;
            m.ItemCD1 = this.ItemCD1;
            m.ItemCD2 = this.ItemCD2;
            m.ItemCD = this.ItemCD;
            m.MatNO = this.MatNO;
            m.PositiveDelivery = this.PositiveDelivery;
            m.PositiveBack = this.PositiveBack;
            m.PositiveRemain = this.PositiveRemain;
            m.PositiveSale = this.PositiveSale;
            m.BaseMoney = "      0";
            m.PositiveID = this.PositiveID;
            m.ShuttleCD = this.ShuttleCD;
            m.ScheduleDelivery = this.ScheduleDelivery;
            m.ScheduleDeliveryMoney = this.ScheduleDeliveryMoney;
            m.MaintenanceCD = this.MaintenanceCD;
            m.Preliminary = this.Preliminary;
            m.TaxTypeCD = this.TaxTypeCD;
            m.UsedCount = this.UsedCount;

            return m;
        }
    }
}