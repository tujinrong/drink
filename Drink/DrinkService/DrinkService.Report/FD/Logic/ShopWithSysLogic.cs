using DrinkService.Data;
using DrinkService.Data.ViewModels;
using DrinkService.Models;
using DrinkService.Report.FD.Model;
using DrinkService.Utils;
using SafeNeeds.DySmat;
using SafeNeeds.DySmat.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkService.Report.FD.Logic
{
    public partial class ShopWithSysLogic : LogicBase
    {
        private static string currentHoDate;

        private static int lastSlipNo;

        public ShopWithSysLogic(EntityRequest enreq)
            : base(enreq)
        {
            _enreq = enreq;
        }

        public static List<object> GetDatas(List<SaleDataViewModel> dates, string SysTypeCD, ref Dictionary<string, int> clientSlipNos
                                            , out int shopLastSlipNo)
        {

            //check and delete
            if (dates.Count() > 0)
            {

                string checkSql = @"

 select 
 ShopCD
 ,ClientCD
 ,Seq
 ,Route
 ,TantoCD
  from T_HoClient where ShopCD = '{0}'  and HoDate = '{1}'
  order by 
  ShopCD
  ,ClientCD
  ,Seq
            ";

                checkSql = string.Format(checkSql, dates[0].ShopCD, DateTime.ParseExact(dates[0].HoDate, "yyyy/MM/dd", System.Globalization.CultureInfo.InvariantCulture).ToString());

                DataTable dtCheck = SQLHelper.GetDataTable(checkSql);

                for (int i = 0; i < dtCheck.Rows.Count; i++)
                {
                    DataRow drC = dtCheck.Rows[i];
                    string clientCD = DataUtil.CStr(drC["ClientCD"]) + DataUtil.CStr(drC["Route"]) + DataUtil.CStr(drC["TantoCD"]);
                    string seq = DataUtil.CStr(drC["Seq"]);
                    string route = DataUtil.CStr(drC["Route"]);
                    string tantoCD = DataUtil.CStr(drC["TantoCD"]);

                    if (i < dtCheck.Rows.Count - 1)
                    {
                        string clientCDNext = DataUtil.CStr(dtCheck.Rows[i + 1]["ClientCD"]) + DataUtil.CStr(dtCheck.Rows[i + 1]["Route"]) + DataUtil.CStr(dtCheck.Rows[i + 1]["TantoCD"]);
                        if (clientCD == clientCDNext)
                        {
                            //delete seq
                            string delSql = @"delete from T_HoClient where ShopCD = '{0}' and ClientCD = '{1}'  and Seq = '{2}' and Route = '{3}' and TantoCD = '{4}';";
                            delSql = string.Format(delSql, dates[0].ShopCD, DataUtil.CStr(drC["ClientCD"]), seq, route, tantoCD);
                            SQLHelper.ExecuteNonQuery(delSql);

                            string delSql2 = @"delete from T_HoClientItem where ShopCD = '{0}' and ClientCD = '{1}'  and Seq = '{2}';";
                            delSql2 = string.Format(delSql2, dates[0].ShopCD, DataUtil.CStr(drC["ClientCD"]), seq);
                            SQLHelper.ExecuteNonQuery(delSql2);
                        }
                    }
                }


                string checkLastSeqSql = @"

select T1.* from
(

select
  T_HoOrderClient.ShopCD
  , T_HoOrderClient.ClientCD
  , MAX(T_HoOrderClient.HoDate) HoDate
  , MAX(T_HoOrderClient.Seq) Seq
  , MAX(M_Client.LastSeq) LastSeq
from
  T_HoOrderClient 
  left join M_Client 
    ON T_HoOrderClient.ShopCD = M_Client.ShopCD 
    AND T_HoOrderClient.ClientCD = M_Client.ClientCD 
	
	where T_HoOrderClient.shopCD = '{0}'
	
group by
  T_HoOrderClient.ShopCD
  , T_HoOrderClient.ClientCD

) T1
where T1.Seq <> T1.LastSeq
            ";

                checkLastSeqSql = string.Format(checkLastSeqSql, dates[0].ShopCD);

                DataTable dtCheck2 = SQLHelper.GetDataTable(checkLastSeqSql);

                for (int i = 0; i < dtCheck2.Rows.Count; i++)
                {
                    DataRow drC = dtCheck2.Rows[i];

                    string clientCD = DataUtil.CStr(drC["ClientCD"]);
                    string seq = DataUtil.CStr(drC["Seq"]);
                    string lastSeq = DataUtil.CStr(drC["LastSeq"]);

                    //delete seq
                    string updateSql = @"update M_Client set LastSeq = {2} where shopCD = '{0}' and ClientCD =  '{1}';";
                    updateSql = string.Format(updateSql, dates[0].ShopCD, clientCD, seq);
                    SQLHelper.ExecuteNonQuery(updateSql);
                }

            }




            clientSlipNos = new Dictionary<string, int>();
            //tax = GetFax();


            //shop
            lastSlipNo = GetLastSlipNo(dates[0].ShopCD);

            string sql = @"
                   
                    select 

                        T1.ShopCD
					    ,T1.ClientCD
					    ,T1.Seq
                        ,T1.ItemCD
                        ,T1.PrevNum
                        ,T1.ThisNum
                        ,T1.AddNum
                        ,T1.BeforeNum
                        ,T1.UsedNum
                        ,T1.AfterNum
                        ,T1.Price
                        ,T1.Money

                        ,T3.PrevNum as pre_PrevNum
                        ,T3.ThisNum as pre_ThisNum
                        ,T3.AddNum as pre_AddNum
                        ,T3.BeforeNum as pre_BeforeNum
                        ,T3.UsedNum as pre_UsedNum
                        ,T3.AfterNum as pre_AfterNum
                        ,T3.Price as pre_Price
                        ,T3.Money as pre_Money

                        ,T2.HoDate
                        ,T2.SoldMoney
                        ,T2.GetMoney
                        ,T2.DiffMoney

                        ,T5.SlipNO
						--,T4.HoDate as pre_HoDate
                        --,T4.SoldMoney as pre_SoldMoney
                        --,T4.GetMoney as pre_GetMoney
                        --,T4.DiffMoney as pre_DiffMoney
                        ,T6.TransactionType
                        ,T6.KanriClientCD
                        ,T7.TaxTypeCD
                        from 

                        T_HoClientItem T1

                        left join T_HoClient T2
                        on T1.ShopCD = T2.ShopCD
                        and T1.ClientCD = T2.ClientCD
                        and T1.Seq = T2.Seq

                        left join T_HoClientItem T3
                        on T1.ShopCD = T3.ShopCD
                        and T1.ClientCD = T3.ClientCD
                        and T1.ItemCD = T3.ItemCD
                        and (T1.Seq-1) = T3.Seq

						--left join T_HoClient T4
                        --on T1.ShopCD = T4.ShopCD
                        --and T1.ClientCD = T4.ClientCD
                        --and (T1.Seq-1) = T4.Seq

                        left join T_HoOrderClient T5
                        on T1.ShopCD = T5.ShopCD
                        and T1.ClientCD = T5.ClientCD
                        and T1.Seq = T5.Seq

	                    left join M_Client T6
	                    on T1.ClientCD = T6.ClientCD
                        and T1.ShopCD = T6.ShopCD

	                    left join M_Item T7
	                    on T1.ItemCD = T7.ItemCD

                    where 1=1 and T1.ShopCD ='{0}' and ({1}) 
					order by T1.ShopCD ,T5.SlipNO,T1.ClientCD,T1.Seq,
                        T1.ItemAddFlag,
                        T1.ShelfNo,
                        T1.ShelfSubNo,T1.ItemCD
                ";

            List<string> whereArr = new List<string>();

            foreach (SaleDataViewModel item in dates)
            {
                whereArr.Add(string.Format("(T2.HoDate = '{0}')", DateTime.ParseExact(item.HoDate, "yyyy/MM/dd", System.Globalization.CultureInfo.InvariantCulture).ToString()));
            }
            string where = string.Join(" or ", whereArr);

            sql = string.Format(sql, dates[0].ShopCD, where);

            DataTable dt = SQLHelper.GetDataTable(sql);

            if (TaxService.IsNewTax(DataUtil.CDate(dates[0].HoDate)))
            {
                //新
                return FDLogic.GetFDData(dt, SysTypeCD, ref clientSlipNos, lastSlipNo, out shopLastSlipNo);
            }
            else
            {
                //旧
                return GetFDDataOld(dt, SysTypeCD, ref clientSlipNos, out shopLastSlipNo);
            }
        }

        private static int GetLastSlipNo(string shopCd)
        {

            string sql = @"
                select *
                from M_Shop
                where ShopCD = '{0}'    
            ";
            DataTable dt = SQLHelper.GetDataTable(string.Format(sql, shopCd));

            if (dt.Rows.Count > 0)
            {
                lastSlipNo = DataUtil.CInt(dt.Rows[0]["LastSlipNo"]);
            }
            return lastSlipNo;
        }
    }
}
