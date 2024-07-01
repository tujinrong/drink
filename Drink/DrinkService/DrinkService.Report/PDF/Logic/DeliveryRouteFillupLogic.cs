using DrinkService.Data;
using DrinkService.Data.ViewModels;
using DrinkService.Models;
using DrinkService.Report.Common;
using DrinkService.Report.Model;
using DrinkService.Utils;
using SafeNeeds.DySmat;
using SafeNeeds.DySmat.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkService.Report.Logic
{
    public class DeliveryRouteFillupLogic : LogicBase
    {
        public DeliveryRouteFillupLogic(EntityRequest enreq)
            : base(enreq)
        {
            _enreq = enreq;
        }
        public List<DeliveryRouteFillupModel> GetModes(List<CollectionViewModel> hoClientList)
        {
            string sql = @"
                    select
		                T1.ShopCD,
		                T6.ShopName,
		                T2.HoDate,
		                T4.StaffCD,
		                T4.StaffName,
		                T1.ClientCD,
		                T3.ClientName,
		                T1.ItemCD,
		                T5.ShortName,
		                T1.Price,
		                T1.UsedNum,
		                T1.AfterNum,
                        T1.Money,
		                T2.GetMoney,
		                T5.TaxTypeCD
	                from
			                T_HoClientItem T1
	                left join T_HoClient T2
		                on T1.ShopCD = T2.ShopCD
		                and T1.ClientCD = T2.ClientCD
		                and T1.Seq = T2.Seq
	                left join M_Client T3
		                on T1.ShopCD =T3.ShopCD 
		                and T1.ClientCD =T3.ClientCD
	                left join M_Staff T4
		                on T2.ShopCD = T4.ShopCD
		                and T2.TantoCD = T4.StaffCD 
	                left join M_Item T5
		                on T1.ItemCD = T5.ItemCD
	                left join M_Shop T6
		                on T1.ShopCD = T6.ShopCD
                    where 1=1 and ({0})
                     order by 
						T1.ShopCD,
		                T2.HoDate,
                        T4.StaffCD,
						T1.ClientCD,
						T1.Seq,
                        T1.ItemAddFlag,
                        T1.ShelfNo,
                        T1.ShelfSubNo,
                        T1.ItemCD
            ";

            List<string> whereArr = new List<string>();

            foreach (var hoClientItem in hoClientList)
            {
                whereArr.Add(string.Format("(T1.ShopCD = '{0}' and (T1.ItemAddFlag != 2 or T1.ItemAddFlag IS NULL) and T2.TantoCD = '{1}' and T2.HoDate = '{2}')", hoClientItem.ShopCD, hoClientItem.StaffCD, DateTime.ParseExact(hoClientItem.HoDateStr, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture).ToString()));
            }
            string where = string.Join(" or ", whereArr);

            sql = string.Format(sql, where);

            DataTable dt = SQLHelper.GetDataTable(sql);

            List<DeliveryRouteFillupModel> modelList = new List<DeliveryRouteFillupModel>();
            DeliveryRouteFillupModel model = null;
            DeliveryRouteFillupDetailItem dItem = null;
            string tKey = "";
            string tKey2 = "";
            int getMoneyCount = 0;
            int lastMoney = 0;
            int amount = 0;
            string date = CommonUtils.GetDateTimeNow().ToString("yyyy/MM/dd HH:mm:ss");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];

                string key = DataUtil.CStr(dr["ShopCD"]) + "|" + DataUtil.CStr(dr["StaffCD"]) + "|" + DataUtil.CStr(dr["HoDate"]);
                string dKey = DataUtil.CStr(dr["ShopCD"]) + "|" + DataUtil.CStr(dr["StaffCD"]) + "|" + DataUtil.CStr(dr["HoDate"]) + "|" + DataUtil.CStr(dr["ClientCD"]);

                if (dKey != tKey)
                {
                    tKey = dKey;
                    if (dItem != null)
                    {
                        //if (dItem.customerDetail.Count > 0)
                        //{
                        //    dItem.customerDetail.Last().money = (amount - (getMoneyCount - lastMoney)).ToString("N0");
                        //}
                        model.details.Add(dItem);
                        getMoneyCount = 0;
                        lastMoney = 0;
                    }
                    dItem = new DeliveryRouteFillupDetailItem();
                    dItem.customer = DataUtil.CStr(dr["ClientCD"]) + "　" + DataUtil.CStr(dr["ClientName"]);
                    dItem.amount = DataUtil.CStr(dr["GetMoney"]) == "" ? "0" : DataUtil.CInt(dr["GetMoney"]).ToString("N0");
                    amount = DataUtil.CInt(dr["GetMoney"]);
                }

                if (key != tKey2)
                {
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                    tKey2 = key;
                    model = new DeliveryRouteFillupModel();
                    model.date = date;
                    model.shopName = DataUtil.CStr(dr["ShopCD"]) + "　" + DataUtil.CStr(dr["ShopName"]);
                    model.fillDate = DataUtil.CDate(dr["HoDate"]).ToString("yyyy/MM/dd");
                    model.masterName = DataUtil.CStr(dr["StaffCD"]) + "　" + DataUtil.CStr(dr["StaffName"]);
                }

                DeliveryRouteFillupCustomerDetailItem item = new DeliveryRouteFillupCustomerDetailItem();
                item.productCode = CommonUtils.FormatItemCD(DataUtil.CStr(dr["ItemCD"]));
                item.productName = DataUtil.CStr(dr["ShortName"]);
                item.price = DataUtil.CInt(dr["Price"]).ToString("N0");
                item.usedNum = DataUtil.CInt(dr["UsedNum"]).ToString("N0");
                item.fillNum = DataUtil.CInt(dr["AfterNum"]).ToString("N0");
                item.money = DataUtil.CInt(dr["Money"]).ToString("N0");
                getMoneyCount += DataUtil.CInt(dr["Money"]);
                lastMoney = DataUtil.CInt(dr["Money"]);
                item.taxTypeCD = DataUtil.CStr(dr["TaxTypeCD"]);
                dItem.customerDetail.Add(item);

                if (i == dt.Rows.Count - 1)
                {
                    //if (dItem.customerDetail.Count > 0)
                    //{
                    //    dItem.customerDetail.Last().money = (amount - (getMoneyCount - lastMoney)).ToString("N0");
                    //}
                    model.details.Add(dItem);
                    modelList.Add(model);
                }
            }

            return modelList;
        }

        public List<DeliveryRouteFillupNotYetModel> GetNoYetModels(List<CollectionViewModel> hoClientList)
        {
            string sql = @"
                    select
                      T1.ShopCD
                      , T2.ShopName
                      , T1.HoDate
                      , T4.StaffCD
                      , T4.StaffName
                      , T1.ClientCD
                      , T3.ClientName
                      , T1.AfterStopFlag
                      , T1.AfterDate 
                    from
                      T_HoOrderClient T1 
                      left join M_Shop T2 
                        on T1.ShopCD = T2.ShopCD 
                      left join M_Client T3 
                        on T1.ShopCD = T3.ShopCD 
                        and T1.ClientCD = T3.ClientCD 
                      left join M_Staff T4 
                        on T1.ShopCD = T4.ShopCD 
                        and T1.TantoCD = T4.StaffCD
                    where (AfterStopFlag = 1  or AfterStopFlag = 2) and ({0})
                     order by
                      T1.ShopCD
                      , T1.HoDate
                      , T4.StaffCD
                      , T1.ClientCD
            ";

            List<string> whereArr = new List<string>();

            foreach (var hoClientItem in hoClientList)
            {
                whereArr.Add(string.Format("(T1.ShopCD = '{0}' and T1.TantoCD = '{1}' and T1.HoDate = '{2}')", hoClientItem.ShopCD, hoClientItem.StaffCD, DateTime.ParseExact(hoClientItem.HoDateStr, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture).ToString()));
            }
            string where = string.Join(" or ", whereArr);

            sql = string.Format(sql, where);

            DataTable dt = SQLHelper.GetDataTable(sql);

            List<DeliveryRouteFillupNotYetModel> modelList = new List<DeliveryRouteFillupNotYetModel>();
            DeliveryRouteFillupNotYetModel model = null;
            DeliveryRouteFillupNotYetDetailItem dItem = null;
            string tKey = "";
            string tKey2 = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];

                string key = DataUtil.CStr(dr["ShopCD"]) + "|" + DataUtil.CStr(dr["HoDate"]);
                string dKey = DataUtil.CStr(dr["ShopCD"]) + "|" + DataUtil.CStr(dr["HoDate"]) + "|" + DataUtil.CStr(dr["StaffCD"]);

                if (dKey != tKey)
                {
                    tKey = dKey;
                    if (dItem != null)
                    {
                        model.details.Add(dItem);
                    }
                    dItem = new DeliveryRouteFillupNotYetDetailItem();
                    dItem.masterName = DataUtil.CStr(dr["StaffCD"]) + "　" + DataUtil.CStr(dr["StaffName"]);
                    dItem.customerDetail = new List<DeliveryRouteFillupNotYetCustomerDetailItem>();
                }

                if (key != tKey2)
                {
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                    tKey2 = key;
                    model = new DeliveryRouteFillupNotYetModel();
                    model.shopName = DataUtil.CStr(dr["ShopCD"]) + "　" + DataUtil.CStr(dr["ShopName"]);
                    model.fillDate = DataUtil.CDate(dr["HoDate"]).ToString("yyyy/MM/dd");
                    model.details = new List<DeliveryRouteFillupNotYetDetailItem>();
                }

                DeliveryRouteFillupNotYetCustomerDetailItem item = new DeliveryRouteFillupNotYetCustomerDetailItem();
                item.customer = DataUtil.CStr(dr["ClientCD"]) + "　" + DataUtil.CStr(dr["ClientName"]);
                item.afterStopFlag = DataUtil.CStr(dr["AfterStopFlag"]) == "1" ? "今ｽﾄ" : "後日ﾌｫﾛｰ";
                item.afterDate = DataUtil.CStr(dr["AfterStopFlag"]) == "1" ? "" : DataUtil.CDate(dr["AfterDate"]).ToString("yyyy/MM/dd");

                dItem.customerDetail.Add(item);

                if (i == dt.Rows.Count - 1)
                {
                    model.details.Add(dItem);
                    modelList.Add(model);
                }
            }

            //modelList = new List<DeliveryRouteFillupNotYetModel>();
            //for (int i = 0; i < 10; i++)
            //{
            //    model = new DeliveryRouteFillupNotYetModel();
            //    model.shopName = "8000331　垂水支店" + i;
            //    model.fillDate = "2015/12/19";
            //    model.details = new List<DeliveryRouteFillupNotYetDetailItem>();
            //    for (int j = i; j < 10; j++)
            //    {
            //        DeliveryRouteFillupNotYetDetailItem masterDetail = new DeliveryRouteFillupNotYetDetailItem();
            //        masterDetail.masterName = "01109　三谷　徹也" + j;
            //        masterDetail.customerDetail = new List<DeliveryRouteFillupNotYetCustomerDetailItem>();
            //        for (int k = j; k < 10; k++)
            //        {
            //            DeliveryRouteFillupNotYetCustomerDetailItem item = new DeliveryRouteFillupNotYetCustomerDetailItem();
            //            item.customer = "顧客：000000" + k + "　顧客顧客顧客顧客顧客顧客顧客顧客顧客顧客顧客顧客顧客顧客顧客" + k;
            //            item.afterStopFlag = k % 2 == 0 ? "今ｽﾄ" : "後日ﾌｫﾛｰ";
            //            item.afterDate = k % 2 == 0 ? "" : "2012/11/19";
            //            masterDetail.customerDetail.Add(item);
            //        }
            //        model.details.Add(masterDetail);
            //    }
            //    modelList.Add(model);
            //}

            return modelList;
        }
    }
}
