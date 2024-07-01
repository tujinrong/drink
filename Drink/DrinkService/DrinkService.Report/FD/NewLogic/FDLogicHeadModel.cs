using DrinkService.Utils;
using SafeNeeds.DySmat.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkService.Report.FD.Model
{
    public class FDLogicHeadModel
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

        /// <summary>
        /// GetMoney
        /// </summary>
        public int GetMoney { get; set; }


        /// <summary>
        /// レコード区分 : 顧客レコードの場合、”１”固定
        /// </summary>
        public string RecoeCD { get; set; }

        /// <summary>
        ///契約番号 : シャトル採番の契約番号コード 
        /// </summary>
        public string OrderCD { get; set; }



        public List<FDLogicDetailModel> DetailList = new List<FDLogicDetailModel>();

        public FDLogicHeadModel(FDDbClientModel client, FDDbGroupModel group)
        {
            int tax = group.TaxRate;

            this.Type = "Client";

            this.ShopCD = DataUtil.CStr(group.ShopCD);

            //固定値　"007800"
            this.ShuttleCD = "00" + "7800";
            this.ReportDate = DataUtil.CDate(group.HoDate).ToString("yyyyMMdd");

            this.ClientCD = DataUtil.CStr(client.KanriClientCD);

            //店舗単位の連番（前ゼロ付与）
            //this.SlipNO = DataUtil.CStr(dbModel.Seq);
            //int slipNo = DataUtil.CInt(client.SlipNO);
            //if (slipNo > 0)
            //{
            //    this.SlipNO = DataUtil.CStr(slipNo);
            //    this.IsNew = false;
            //}
            //else
            //{
            //    this.SlipNO = DataUtil.CStr(++lastSlipNo);
            //    if (lastSlipNo > 999999)
            //    {
            //        lastSlipNo = 1;
            //        this.SlipNO = "1";
            //    }
            //    this.IsNew = true;
            //    if (clientSlipNos.ContainsKey(this.ClientCD) == false)
            //    {
            //        clientSlipNos.Add(this.ClientCD, lastSlipNo);
            //    }
            //}

            //固定値　"000"
            this.Seq = "000";
            this.SubCD = "0";
            this.ManageCD = "0";
            this.PreUnGetMoneyCD = "0";
            this.PreUnGetMoney = "0";
            int indexItem = 1;
            this.TransactionType = DataUtil.CStr(client.TransactionType);

            this.ThisSoldMoneyCD = group.ThisSoldMoneyCD;
            this.ThisSoldMoney = group.ThisSoldMoney;
            this.ThisGetMoneyCD = group.ThisGetMoneyCD;
            this.ThisGetMoney = group.ThisGetMoney;
            this.TaxCD = group.TaxCD;
            this.Tax = group.Tax;
            this.SoldMoney = group.SoldMoney;
            this.GetMoney = group.GetMoney;
            this.TaxTypeCD = group.TaxTypeCD;
            //課税種別 : 1：総額
            this.KsTaxTypeCD = "1";

           

            //課税額
            //this.TaxCD = "0";
            //this.Tax = "0";

            //実績区分
            this.PositiveID = "00";

            this.FreeRate = "0";

            this.Preliminary = " ";

            DateTime dateNow = CommonUtils.GetDateTimeNow();
            this.Year = dateNow.ToString("yyyy");
            this.Month = dateNow.ToString("MM");
            this.Day = dateNow.ToString("dd");


            this.RecoeCD = "1";
            if (client.SysTypeCD == "0")
            {
                this.ManageCD = client.KanriClientCD;
            }
            
            this.OrderCD = "000000000";

            //明細処理
            foreach (var item in group.ItemList)
            {
                var detail = new FDLogicDetailModel(client, group, item, indexItem++);
                DetailList.Add(detail);
            }
        }

        public void SetSlipNO(FDDbClientModel client, ref int lastSlipNo, ref Dictionary<string, int> clientSlipNos,int addIndex)
        {
            
            int slipNo = DataUtil.CInt(client.SlipNO);
            if (slipNo > 0)
            {
                this.SlipNO = DataUtil.CStr(slipNo + addIndex);
                this.IsNew = false;
            }
            else
            {
                this.SlipNO = DataUtil.CStr(++lastSlipNo);
                if (lastSlipNo > 999999)
                {
                    lastSlipNo = 1;
                    this.SlipNO = "1";
                }
                this.IsNew = true;
                if (clientSlipNos.ContainsKey(this.ClientCD) == false)
                {
                    clientSlipNos.Add(this.ClientCD, lastSlipNo);
                }
            }

        }


        public int ItemUsedCount()
        {
            if (DetailList.Count == 0) return 0;
            return DetailList.Sum(e => e.UsedCount);
        }

        /// <summary>
        /// 導入
        /// </summary>
        /// <returns></returns>
        public ShopWithSysClientModel ToSysModel()
        {
            var m = new ShopWithSysClientModel();

            m.ShopCD = this.ShopCD;
            m.ShuttleCD = this.ShuttleCD;
            m.ReportDate = this.ReportDate;
            m.SlipNO = this.SlipNO;
            m.ClientCD = this.ClientCD;
            m.Seq = this.Seq;
            m.SubCD = this.SubCD;
            m.ManageCD = this.ManageCD;
            m.PreUnGetMoneyCD = this.PreUnGetMoneyCD;
            m.PreUnGetMoney = this.PreUnGetMoney;
            m.ThisSoldMoneyCD = this.ThisSoldMoneyCD;
            m.ThisSoldMoney = this.ThisSoldMoney;
            m.Type = this.Type;
            m.ThisGetMoneyCD = this.ThisGetMoneyCD;
            m.ThisGetMoney = this.ThisGetMoney;
            m.TaxCD = this.TaxCD;
            m.Tax = this.Tax;
            m.PositiveID = this.PositiveID;
            m.FreeRate = this.FreeRate;
            m.TaxTypeCD = this.TaxTypeCD;
            m.KsTaxTypeCD = this.KsTaxTypeCD;
            m.Preliminary = this.Preliminary;
            m.Year = this.Year;
            m.Month = this.Month;
            m.Day = this.Day;
            m.IsNew = this.IsNew;
            m.TransactionType = this.TransactionType;
            m.UsedCount = this.UsedCount;
            m.SoldMoney = this.SoldMoney;

            return m;
        }

        /// <summary>
        /// 未導入
        /// </summary>
        /// <returns></returns>
        public ShopNoSysClientModel ToNoSysModel()
        {
            var m = new ShopNoSysClientModel();

            m.Type = this.Type;
            m.RecoeCD = "1";    
            m.ReportDate = this.ReportDate;
            m.SlipNO = this.SlipNO;
            m.ClientCD = this.ClientCD;
            m.PreUnGetMoney = this.PreUnGetMoney;
            m.ThisSoldMoney = this.ThisSoldMoney;
            if (this.ThisSoldMoneyCD == "-")
            {
                m.ThisSoldMoney = "-" + this.ThisSoldMoney;
            }
            m.Tax = this.Tax;
            m.ThisGetMoney = this.ThisGetMoney;
            if (this.ThisGetMoneyCD == "-")
            {
                m.ThisGetMoney = "-" + this.ThisGetMoney;
            }
            m.PositiveID = this.PositiveID;
            m.ShuttleCD = this.ShuttleCD;
            m.ManageCD = this.ManageCD;
            m.OrderCD = "000000000";
            m.TaxTypeCD = this.TaxTypeCD;
            m.KsTaxTypeCD = this.KsTaxTypeCD;
            m.Preliminary = this.Preliminary;
            m.IsNew = this.IsNew;
            m.TransactionType = this.TransactionType;
            m.UsedCount = this.UsedCount;
            m.SoldMoney = this.SoldMoney;

            return m;
        }


    }
}
