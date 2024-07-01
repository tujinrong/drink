using DrinkService.Data;
using DrinkService.Data.ViewModels;
using DrinkService.Report.Common;
using DrinkService.Report.Model;
using DrinkService.Utils;
using SafeNeeds.DySmat.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkService.Report.Logic
{
    public class DeliveryRouteFillupListLogic
    {
        public static List<DeliveryRouteFillupListModel> GetModes(List<CollectionViewModel> hoClientList)
        {
            string sql = @"
                   
                    select
		                T1.ShopCD,
		                T4.ShopName,
						T2.HoDate,
						T2.TantoCD,
						T5.StaffName,
		                T1.ItemCD,
		                T3.ShortName,
		                sum(T1.AddNum) AddNumCount
	                from
			                T_HoClientItem T1
	                left join T_HoClient T2
		                on T1.ShopCD = T2.ShopCD
		                and T1.ClientCD = T2.ClientCD
		                and T1.Seq = T2.Seq
	                left join M_Item T3
		                on T1.ItemCD = T3.ItemCD
	                left join M_Shop T4
		                on T1.ShopCD = T4.ShopCD
					left join M_Staff T5
						on T2.ShopCD = T5.ShopCD
						and T2.TantoCD = T5.StaffCD
                    where 1=1 and T1.AddNum <> 0 and T1.AddNum is not null and ({0})
					group by
		            T1.ItemCD,
					T1.ShopCD,
					T2.HoDate,
					T2.TantoCD,
					T5.StaffName,
		            T4.ShopName,
		            T3.ShortName

having sum(T1.AddNum) <>0

					order by 
					T1.ShopCD,
					T2.HoDate,
					T2.TantoCD,
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

            List<DeliveryRouteFillupListModel> masterList = new List<DeliveryRouteFillupListModel>();
            DeliveryRouteFillupListModel master = null;

            string tKey = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];

                string key = DataUtil.CStr(dr["ShopCD"]) + "|" + DataUtil.CStr(dr["HoDate"]) + "|" + DataUtil.CStr(dr["TantoCD"]);

                if (key != tKey)
                {
                    if (master != null)
                    {
                        masterList.Add(master);
                    }
                    tKey = key;
                    master = new DeliveryRouteFillupListModel();
                    master.shopName = DataUtil.CStr(dr["ShopCD"]) + "　" + DataUtil.CStr(dr["ShopName"]);
                    master.fillDate = DataUtil.CDate(dr["HoDate"]).ToString("yyyy/MM/dd");
                    master.masterName = DataUtil.CStr(dr["TantoCD"]) + "　" + DataUtil.CStr(dr["StaffName"]);
                }

                DeliveryRouteFillupListDetailItem item = new DeliveryRouteFillupListDetailItem();
                item.productCode = CommonUtils.FormatItemCD(DataUtil.CStr(dr["ItemCD"]));
                item.productName = DataUtil.CStr(dr["ShortName"]);
                item.fillupNum = DataUtil.CInt(dr["AddNumCount"]).ToString("N0");
   
                master.details.Add(item);

                if (i == dt.Rows.Count - 1)
                {
                    masterList.Add(master);
                }
            }

            string date = CommonUtils.GetDateTimeNow().ToString("yyyy/MM/dd HH:mm:ss");
            int pageCount = 0;
            List<DeliveryRouteFillupListModel> modelList = new List<DeliveryRouteFillupListModel>();

            foreach (var masterItem in masterList)
            {
                for (int i = 0; i < masterItem.details.Count; i = i + 20)
                {
                    DeliveryRouteFillupListModel model = new DeliveryRouteFillupListModel();

                    model.shopName = masterItem.shopName;
                    model.fillDate = masterItem.fillDate;
                    model.masterName = masterItem.masterName;
                    model.date = date;
                    model.page = "Page:" +(++pageCount);

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
