using DrinkService.Data;
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
    public class DeliveryRouteStorageLogic : LogicBase
    {
        public DeliveryRouteStorageLogic(EntityRequest enreq)
            : base(enreq)
        {
            _enreq = enreq;
        }

        public List<DeliveryRouteStorageModel> GetModes(string shopCD, string staffCD, string route, string hoDate)
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
                        wherePrevSeq.Add(string.Format("(T_HoClientItem.ShopCD = '{0}' and T_HoClientItem.ClientCD = '{1}' and T_HoClientItem.Seq = {2})", DataUtil.CStr(row["ShopCD"]), DataUtil.CStr(row["ClientCD"]), DataUtil.CInt(row["LastSeq"])));
                    }
                    else
                    {
                        wherePrevSeq.Add(string.Format("(T_HoClientItem.ShopCD = '{0}' and T_HoClientItem.ClientCD = '{1}' and T_HoClientItem.Seq = {2})", DataUtil.CStr(row["ShopCD"]), DataUtil.CStr(row["ClientCD"]), DataUtil.CInt(row["補充SEQ"]) - 1));
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
                        select
                          T.ShopCD
                        , T.ShopName
                        , T.StaffCD
                        , T.StaffName
                        , T.HoDate
                        , T.Route
                        , T.ItemCD
                        , M_Item.ShortName
                        , sum(T.AddNum) AddNum
                        , floor(sum(T.AddNum) / M_Item.InNum) CaseNum
                        , sum(T.AddNum) % M_Item.InNum as FractionNum
                        from
                          ( 
                            select
                                T1.ShopCD
                              , T5.ShopName
                              , T6.StaffCD
                              , T6.StaffName
                              , T6.HoDate as HoDate
                              , T6.Route as Route
                              , T1.ItemCD
                              , T1.AddNum 
                            from
                              ( 
                                select
                                  T.ShopCD
                                  , T.ClientCD
                                  , T.ItemCD
                                  , sum(T.Num) as AddNum 
                                from
                                  M_ClientInitItems T 
                                group by
                                  T.ShopCD
                                  , T.ClientCD
                                  , T.ItemCD
                              ) T1 
                              left join M_Client T2 
                                on T1.ShopCD = T2.ShopCD 
                                and T1.ClientCD = T2.ClientCD 
                              left join M_Staff T4 
                                on T2.ShopCD = T4.ShopCD 
                                and T2.TantoCD = T4.StaffCD 
                              left join M_Shop T5 
                                on T1.ShopCD = T5.ShopCD
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
                            ) T6
                            on T1.ShopCD = T6.ShopCD
                            and T1.ClientCD = T6.ClientCD 
                            where
                              1 = 1 
                              and T1.AddNum >= 0 
                              and T6.FirstFlag = 1
                              {0}

                            union all 

                            select
                              T_HoClientItem.ShopCD
                            , M_Shop.ShopName
                            , T5.StaffCD
                            , T5.StaffName
                            , T5.HoDate
                            , T5.Route
                            , T_HoClientItem.ItemCD
                            , T_HoClientItem.AddNum AddNum 
                            from
                              T_HoClientItem 
                              left join T_HoClient 
                                on T_HoClientItem.ShopCD = T_HoClient.ShopCD 
                                and T_HoClientItem.ClientCD = T_HoClient.ClientCD 
                                and T_HoClientItem.Seq = T_HoClient.Seq 
                              left join M_Staff 
                                on M_Staff.ShopCD = T_HoClient.ShopCD 
                                and M_Staff.StaffCD = T_HoClient.TantoCD 
                              left join M_Shop 
                                on M_Shop.ShopCD = T_HoClientItem.ShopCD 
                              left join M_Client 
                                on M_Client.ShopCD = T_HoClientItem.ShopCD 
                                and M_Client.ClientCD = T_HoClientItem.ClientCD
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
                            ) T5 
                            on T_HoClientItem.ShopCD = T5.ShopCD
                            and T_HoClientItem.ClientCD = T5.ClientCD 
                             
                              left join T_HoClientItem TP 
                                on T_HoClientItem.ShopCD = TP.ShopCD 
                                and T_HoClientItem.ClientCD = TP.ClientCD 
                                and T_HoClientItem.ItemCD = TP.ItemCD 
                                and T_HoClientItem.Seq = (TP.Seq+1) 


                            where
                              1 = 1 
                              --and T_HoClientItem.AddNum > 0 
                              and (T_HoClientItem.AddNum > 0 or (T_HoClientItem.AddNum = 0 and TP.AddNum > 0))
                              and T_HoClientItem.NextStopFlag = 0
                              {1}
                          ) T 
                        left join M_Item 
                            on M_Item.ItemCD = T.ItemCD 
                        group by
                            T.ShopCD
                            , T.ShopName
                            , T.StaffCD
                            , T.StaffName
                            , T.HoDate
                            , T.Route
                            , T.ItemCD
                            , M_Item.ShortName
                            , M_Item.InNum
                        order by
                            T.ShopCD
                            , T.StaffCD
                            , T.HoDate
                            , T.Route
                            , T.ItemCD
            ";

            sql = string.Format(sql, whereFirst, wherePrev, shopCD, routeSql, hoDate);

            DataTable dt = SQLHelper.GetDataTable(sql);

            List<DeliveryRouteStorageModel> masterList = new List<DeliveryRouteStorageModel>();
            DeliveryRouteStorageModel master = null;
            string date = CommonUtils.GetDateTimeNow().ToString("yyyy/MM/dd HH:mm:ss");

            string tKey = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];

                string key = DataUtil.CStr(dr["ShopCD"]) + "|" + DataUtil.CStr(dr["HoDate"]) + "|" + DataUtil.CStr(dr["StaffCD"]) + "|" + DataUtil.CStr(dr["Route"]);

                if (key != tKey)
                {
                    if (master != null)
                    {
                        masterList.Add(master);
                    }
                    tKey = key;
                    master = new DeliveryRouteStorageModel();
                    master.shopName = DataUtil.CStr(dr["ShopCD"]) + "　" + DataUtil.CStr(dr["ShopName"]);
                    master.outdate = DataUtil.CDate(dr["HoDate"]).ToString("yyyy/MM/dd");
                    master.masterName = DataUtil.CStr(dr["StaffCD"]) + "　" + DataUtil.CStr(dr["StaffName"]);
                    master.route = DataUtil.CStr(dr["Route"]);
                    master.date = date;
                }

                DeliveryRouteStorageItem item = new DeliveryRouteStorageItem();
                item.productCode = CommonUtils.FormatItemCD(DataUtil.CStr(dr["ItemCD"]));
                item.productName = DataUtil.CStr(dr["ShortName"]);
                item.realNum = DataUtil.CInt(dr["AddNum"]).ToString("N0");
                item.caseNum = DataUtil.CInt(dr["CaseNum"]).ToString("N0");
                item.fractionNum = DataUtil.CInt(dr["FractionNum"]).ToString("N0");

                master.details.Add(item);

                if (i == dt.Rows.Count - 1)
                {
                    masterList.Add(master);
                }
            }

            int pageCount = 1;
            string clientPageKey = string.Empty;
            List<DeliveryRouteStorageModel> modelList = new List<DeliveryRouteStorageModel>();

            foreach (var masterItem in masterList)
            {
                for (int i = 0; i < masterItem.details.Count; i = i + 20)
                {
                    DeliveryRouteStorageModel model = new DeliveryRouteStorageModel();
                    model.shopName = masterItem.shopName;
                    model.outdate = masterItem.outdate;
                    model.masterName = masterItem.masterName;
                    model.route = masterItem.route;
                    model.date = date;
                    model.page = "Page: " + (pageCount++);

                    if (i + 20 > masterItem.details.Count)
                    {
                        model.details.AddRange(masterItem.details.GetRange(i, masterItem.details.Count - i));
                    }
                    else
                    {
                        model.details.AddRange(masterItem.details.GetRange(i, 20));
                    }

                    modelList.Add(model);
                }
            }

            return modelList;
        }
    }
}
