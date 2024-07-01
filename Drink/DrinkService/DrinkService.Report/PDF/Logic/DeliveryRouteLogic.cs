using DrinkService.Report.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DrinkService.Models;
using System.ComponentModel;
using DrinkService.Data;
using DrinkService.Report.Common;
using DrinkService.Data.Logics;
using DrinkService.Utils;
using SafeNeeds.DySmat;
using SafeNeeds.DySmat.Util;

namespace DrinkService.Report.Logic
{
    public class DeliveryRouteLogic :LogicBase
    {
        public DeliveryRouteLogic(EntityRequest enreq)
            : base(enreq)
        {
            _enreq = enreq;
        }
        public List<DeliveryRouteModel> GetModes(List<T_HoClient> hoClientList)
        {

            string sql = @"
                        select
	                    T1.ShopCD,
	                    T2.Route,
	                    T1.Seq,  
	                    T1.ClientCD,
	                    T3.ClientName,
	                    T2.HoDate,
	                    T4.StaffCD,
	                    T4.StaffName,
	                    T2.SoldMoney,
	                    T2.GetMoney,
	                    T2.DiffMoney,
                        T2.Memo,
	                    T1.ShelfNo,
                        M_Code.Name AS ShelfName,
	                    T1.ShelfSubNo,
	                    T5.ShortName,
	                    T1.Price,
	                    T1.BeforeNum,
	                    T1.PrevNum,
	                    T1.ThisNum,
	                    T1.UsedNum,
	                    T1.AddNum,
	                    T1.AfterNum,
	                    T1.FreshDate,
						T1.ItemAddFlag
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
                    Left JOIN M_Code on M_Code.Kind='Shelf' and M_Code.CD = T1.ShelfNo
                    where 1=1 and ({0})
                    order by 
						T1.ShopCD,
						T1.ClientCD,
                        T2.HoDate,
						T1.Seq,
                        T1.ItemAddFlag,
						T1.ShelfNo,
	                    T1.ShelfSubNo,
                        T1.ItemCD
            ";

            List<string> whereArr = new List<string>();

            foreach (var hoClientItem in hoClientList)
            {
                whereArr.Add(string.Format("(T1.ShopCD = '{0}' and T1.ClientCD = '{1}' and T1.Seq = {2} and (T1.ItemAddFlag != 2 or T1.ItemAddFlag IS NULL) )", hoClientItem.ShopCD, hoClientItem.ClientCD, hoClientItem.Seq));
            }
            string where = string.Join(" or ", whereArr);

            sql = string.Format(sql, where);

            DataTable dt = SQLHelper.GetDataTable(sql);

            return GetModelList(dt);
        }

        public List<DeliveryRouteModel> GetEmptyModes(string shopCD, string staffCD, string route, string hoDate)
        {

            T_HoDayAdapter logic = new T_HoDayAdapter(_enreq);

            PageViewRequest req = new PageViewRequest();

            req.GetPageCount = false;

            req.FilterDic.Add(Y_EntityFilterData.ShopFilter, new string[] { shopCD });
            req.FilterDic.Add(Y_EntityFilterData.StaffFilter, new string[] { staffCD });
            req.FilterDic.Add(Y_EntityFilterData.OrderTantoFilter, new string[] { route });
            req.FilterDic.Add(Y_EntityFilterData.HoDateFilter, new string[] { hoDate });

            PageViewResult result = logic.GetOrderList(req);

            List<string> whereFirstSeq = new List<string>();
            List<string> wherePrevSeq = new List<string>();

            foreach (DataRow row in result.DataTable.Rows)
            {

                if (DataUtil.CStr(row["初回"]) == "1")
                {
                    whereFirstSeq.Add(string.Format("(T1.ShopCD = '{0}' and T1.ClientCD = '{1}')", DataUtil.CStr(row["ShopCD"]), DataUtil.CStr(row["ClientCD"])));
                }
                else
                {
                    if (DataUtil.CInt(row["補充SEQ"]) == 0)
                    {
                        wherePrevSeq.Add(string.Format("(T1.ShopCD = '{0}' and T1.ClientCD = '{1}' and T1.Seq = {2})", DataUtil.CStr(row["ShopCD"]), DataUtil.CStr(row["ClientCD"]), DataUtil.CInt(row["LastSeq"])));
                    }
                    else
                    {
                        wherePrevSeq.Add(string.Format("(T1.ShopCD = '{0}' and T1.ClientCD = '{1}' and T1.Seq = {2})", DataUtil.CStr(row["ShopCD"]), DataUtil.CStr(row["ClientCD"]), DataUtil.CInt(row["補充SEQ"]) - 1));
                    }
                }
            }
            string whereFirst = string.Join(" or ", whereFirstSeq);
            string wherePrev = string.Join(" or ", wherePrevSeq);

            if (whereFirst.Length > 0)
            {
                whereFirst = " and (" + whereFirst + ")";
            }
            else
            {
                whereFirst = " and 1 != 1";
            }


            if (wherePrev.Length > 0)
            {
                wherePrev = " and (" + wherePrev + ")";
            }
            else
            {
                wherePrev = " and 1 != 1";
            }

            string routeSql = string.IsNullOrEmpty(route) ? "" : "AND T_HoOrderClient.Route = '" + route + "'";

            string sql = @"
                    select T.*,M_Code.Name AS ShelfName from (
                        select 
	                        T1.ShopCD,
	                        T5.Route as Route,
	                        1 as Seq,  
	                        T1.ClientCD,
	                        T2.ClientName,
	                        T5.HoDate as HoDate,
	                        T5.StaffCD,
                            T5.StaffName,
	                        '' as SoldMoney,
	                        '' as GetMoney,
	                        '' as DiffMoney,
	                        '' as Memo,
	                        T1.ShelfCD as ShelfNo,
	                        '0' as ShelfSubNo,
	                        T3.ShortName,
	                        T1.Price as Price,
	                        '' as BeforeNum,
	                        '' as PrevNum,
	                        str(T1.ThisNum) as ThisNum,
	                        '' as UsedNum,
	                        '' as AddNum,
	                        '' as AfterNum,
	                        null as FreshDate,
                            0 as ItemAddFlag,
                            T1.ItemCD
                        from 
                        (select T.ShopCD, T.ClientCD,T.ItemCD,sum(T.Num) as ThisNum,min(T.ShelfCD) as ShelfCD ,max(Price) as Price  from M_ClientInitItems T group by T.ShopCD , T.ClientCD,T.ItemCD) T1

                        left join M_Client T2
                        on T1.ShopCD = T2.ShopCD
                        and T1.ClientCD = T2.ClientCD
                        left join M_Item T3
                        on T1.ItemCD = T3.ItemCD
                        left join M_Staff T4
                        on T2.ShopCD = T4.ShopCD
                        and T2.TantoCD = T4.StaffCD
                        left join (
                            SELECT
                              T_HoOrderClient.ClientCD AS ClientCD
                              , T_HoOrderClient.ShopCD AS ShopCD
                              , T_HoOrderClient.HoDate AS HoDate
                              , T_HoOrderClient.FirstFlag AS FirstFlag
                              , T_HoOrderClient.Route AS Route 
                              , M_Staff.StaffCD AS StaffCD
                              , M_Staff.StaffName AS StaffName 
                            FROM
                              ( 
                                ( 
                                  T_HoOrderClient 
                                    LEFT JOIN M_Staff 
                                      ON T_HoOrderClient.ShopCD = M_Staff.ShopCD 
                                      AND T_HoOrderClient.TantoCD = M_Staff.StaffCD
                                ) 
                                  LEFT JOIN M_Client 
                                    ON T_HoOrderClient.ShopCD = M_Client.ShopCD 
                                    AND T_HoOrderClient.ClientCD = M_Client.ClientCD
                              ) 
                            WHERE
                              T_HoOrderClient.ShopCD = '{2}' 
                              {3}
                              AND T_HoOrderClient.HoDate = '{4}'
                        ) T5
                        on T1.ShopCD = T5.ShopCD
                        and T1.ClientCD = T5.ClientCD
                        where 1=1
                         {0} and T5.FirstFlag = 1

                        union all 

                        select
                        T1.ShopCD,
                        T6.Route,
                        T1.Seq + 1 as Seq,  
                        T1.ClientCD,
                        T3.ClientName,
                        T6.HoDate as HoDate,
                        T6.StaffCD,
                        T6.StaffName,
                        '' as SoldMoney,
                        '' as GetMoney,
                        '' as DiffMoney,
                        '' as Memo,
                        T1.ShelfNo,
                        cast(T1.ShelfSubNo as varchar(20)) ShelfSubNo,
                        T5.ShortName,
                        T1.NextPrice as Price,
                        str(T1.AddNum) as BeforeNum,
                        str(T1.AfterNum) as PrevNum,
                        '' as ThisNum,
                        '' as UsedNum,
                        '' as AddNum,
                        '' as AfterNum,
                        T1.FreshDate as FreshDate,
                        0 as ItemAddFlag,
                        T1.ItemCD
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
                        left join (
                            SELECT
                              T_HoOrderClient.ClientCD AS ClientCD
                              , T_HoOrderClient.ShopCD AS ShopCD
                              , T_HoOrderClient.HoDate AS HoDate
                              , T_HoOrderClient.Route AS Route 
                              , M_Staff.StaffCD AS StaffCD
                              , M_Staff.StaffName AS StaffName 
                            FROM
                              ( 
                                ( 
                                  T_HoOrderClient 
                                    LEFT JOIN M_Staff 
                                      ON T_HoOrderClient.ShopCD = M_Staff.ShopCD 
                                      AND T_HoOrderClient.TantoCD = M_Staff.StaffCD
                                ) 
                                  LEFT JOIN M_Client 
                                    ON T_HoOrderClient.ShopCD = M_Client.ShopCD 
                                    AND T_HoOrderClient.ClientCD = M_Client.ClientCD
                              ) 
                            WHERE
                              T_HoOrderClient.ShopCD = '{2}' 
                              {3} 
                              AND T_HoOrderClient.HoDate = '{4}'
                        ) T6
                        on T1.ShopCD = T6.ShopCD
                        and T1.ClientCD = T6.ClientCD
                        where 1=1
                         {1} and T1.NextStopFlag = 0  and T1.AfterNum >0
                    ) T
                        Left JOIN M_Code on M_Code.Kind='Shelf' and M_Code.CD = T.ShelfNo
			         order by 
			        T.ShopCD,
                    T.StaffCD,
			        T.ClientCD,
			        T.ShelfNo,
	                T.ShelfSubNo,
	                T.ItemCD
            ";

            sql = string.Format(sql, whereFirst, wherePrev, shopCD, routeSql, hoDate);

            DataTable dt = SQLHelper.GetDataTable(sql);

            return GetModelList(dt);
        }

        private List<DeliveryRouteModel> GetModelList(DataTable dt)
        {
            List<DeliveryRouteModel> masterList = new List<DeliveryRouteModel>();
            DeliveryRouteModel master = null;
            HoOrderClientLogic hoOrderClientLogic = new HoOrderClientLogic(this._enreq);

            string date = CommonUtils.GetDateTimeNow().ToString("yyyy/MM/dd HH:mm:ss");

            string tKey = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];

                string key = DataUtil.CStr(dr["ShopCD"]) + "|" + DataUtil.CStr(dr["ClientCD"]) + "|" + DataUtil.CStr(dr["Seq"]);

                if (key != tKey)
                {
                    if (master != null)
                    {
                        masterList.Add(master);
                    }
                    tKey = key;
                    master = new DeliveryRouteModel();

                    master.times = DataUtil.CStr(dr["Seq"]);

                    master.kaishaName =  DataUtil.CStr(dr["ClientName"]) + "　様";
                    master.addDate = dr["HoDate"] == DBNull.Value ? CommonUtils.GetDateTimeNow().ToString("yyyy/MM/dd") : DataUtil.CDate(dr["HoDate"]).ToString("yyyy/MM/dd");

                    //GetNextHoDate 
                    master.accessDate = hoOrderClientLogic.GetNextHoDate(DataUtil.CStr(dr["ShopCD"]), DataUtil.CStr(dr["ClientCD"]), DataUtil.CStr(dr["Route"]), DataUtil.CDate(master.addDate)).ToString("yyyy/MM/dd");
                    master.master = DataUtil.CStr(dr["StaffCD"]) + "　" + DataUtil.CStr(dr["StaffName"]);

                    master.sale = DataUtil.CStr(dr["SoldMoney"]) == "" ? "" : DataUtil.CInt(dr["SoldMoney"]).ToString("N0");
                    master.collect = DataUtil.CStr(dr["GetMoney"]) == "" ? "" : DataUtil.CInt(dr["GetMoney"]).ToString("N0");
                    master.deficiency = DataUtil.CStr(dr["DiffMoney"]) == "" ? "" : DataUtil.CInt(dr["DiffMoney"]).ToString("N0");
                    master.memo = DataUtil.CStr(dr["Memo"]);

                    T_HoClientAdapter logic = new T_HoClientAdapter(this._enreq);
                    T_Sign hoSign = null;
                    logic.GetSign(new T_HoClient.Key { ShopCD = DataUtil.CStr(dr["ShopCD"]), ClientCD = DataUtil.CStr(dr["ClientCD"]), Seq = DataUtil.CInt(dr["Seq"]) }, out hoSign);
                    master.signData = hoSign.SignData;
                }

                DeliveryRouteDetailItem item = new DeliveryRouteDetailItem();
                string newItem = DataUtil.CInt(dr["ItemAddFlag"]) == 1 ? "*" : "";
                item.shelf = newItem + DataUtil.CStr(dr["ShelfName"]);
                item.order = DataUtil.CStr(dr["ShelfSubNo"]);

                item.productName = DataUtil.CStr(dr["ShortName"]);
                item.price = DataUtil.CInt(dr["Price"]).ToString("N0");
                item.preNum = DataUtil.CStr(dr["BeforeNum"]).Trim();
                item.afterPreNum = DataUtil.CStr(dr["PrevNum"]).Trim();
                item.curStorage = DataUtil.CStr(dr["ThisNum"]).Trim();
                
                if (DataUtil.CInt(dr["Seq"]) == 1)
                {
                    item.used = "";
                    item.fill = "";

                }
                else
                {
                    item.used = DataUtil.CStr(dr["UsedNum"]);
                    item.fill = DataUtil.CStr(dr["AddNum"]);
                }

                item.afterFill = DataUtil.CStr(dr["AfterNum"]);
                item.expirationDate = dr["FreshDate"] == DBNull.Value ? "" : DataUtil.CDate(dr["FreshDate"]).ToString("yyyy/MM/dd");

                master.details.Add(item);

                if (i == dt.Rows.Count - 1)
                {
                    masterList.Add(master);
                }
            }

            int pageCount = 0;
            List<DeliveryRouteModel> modelList = new List<DeliveryRouteModel>();

            foreach (var masterItem in masterList)
            {
                for (int i = 0; i < masterItem.details.Count; i = i + 30)
                {
                    DeliveryRouteModel model = new DeliveryRouteModel();

                    model.times = masterItem.times;
                    model.kaishaName = masterItem.kaishaName;
                    model.addDate = masterItem.addDate;
                    model.accessDate = masterItem.accessDate;
                    model.master = masterItem.master;
                    model.sale = masterItem.sale;
                    model.collect = masterItem.collect;
                    model.deficiency = masterItem.deficiency;
                    model.memo = masterItem.memo;
                    model.signData = masterItem.signData;

                    model.page = DataUtil.CStr(++pageCount);
                    model.date = date;
                    if (i + 30 > masterItem.details.Count)
                    {
                        model.details.AddRange(masterItem.details.GetRange(i, masterItem.details.Count - i));
                    }
                    else
                    {
                        model.details.AddRange(masterItem.details.GetRange(i, 30));
                    }

                    modelList.Add(model);
                }
            }


            foreach (var model in modelList)
            {
                model.page += ("/" + pageCount);
            }

            return modelList;
        }
    }
}
