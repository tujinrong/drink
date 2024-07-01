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
    public class DeliveryRoutePaymentLogic:LogicBase
    {
        public DeliveryRoutePaymentLogic(EntityRequest enreq)
            : base(enreq)
        {
            _enreq = enreq;
        }
        public List<DeliveryRoutePaymentModel> GetModes(List<CollectionViewModel> hoClientList)
        {
            string sql = @"
                     select 
	                    T1.ShopCD,
	                    T4.ShopName,
	                    T1.HoDate,
	                    T1.TantoCD,
	                    T3.StaffName,
	                   sum(T1.GetMoney) GetMoney
                    from
		                    T_HoClient T1
                    left join M_Staff T3
	                    on T1.ShopCD = T3.ShopCD
	                    and T1.TantoCD = T3.StaffCD 
                    left join M_Shop T4
	                    on T1.ShopCD = T4.ShopCD
                    where 1=1 and ({0})
                    group by 
                    T1.TantoCD,
                    T3.StaffName,
                    T1.ShopCD,
                    T4.ShopName,
                    T1.HoDate
                    order by
                    T1.ShopCD,
                    T1.HoDate,
                    T1.TantoCD
                ";

            List<string> whereArr = new List<string>();

            foreach (var hoClientItem in hoClientList)
            {
                whereArr.Add(string.Format("(T1.ShopCD = '{0}' and T1.TantoCD = '{1}' and T1.HoDate = '{2}')", hoClientItem.ShopCD, hoClientItem.StaffCD, DateTime.ParseExact(hoClientItem.HoDateStr, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture).ToString()));
            }
            string where = string.Join(" or ", whereArr);

            sql = string.Format(sql, where);

            DataTable dt = SQLHelper.GetDataTable(sql);

            List<DeliveryRoutePaymentModel> modelList = new List<DeliveryRoutePaymentModel>();

            string date = CommonUtils.GetDateTimeNow().ToString("yyyy/MM/dd HH:mm:ss");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                DeliveryRoutePaymentModel model = new DeliveryRoutePaymentModel();
                model.shopName = DataUtil.CStr(dr["ShopCD"]) + "　" + DataUtil.CStr(dr["ShopName"]);
                model.masterName = DataUtil.CStr(dr["TantoCD"]) + "　" + DataUtil.CStr(dr["StaffName"]);
                model.outputTime = date;
                model.fillDate = DataUtil.CDate(dr["HoDate"]).ToString("yyyy/MM/dd");
                model.page = "Page: " + (i + 1);
                model.totalAmount = DataUtil.CInt(dr["GetMoney"]).ToString("N0");
                model.cash = DataUtil.CInt(dr["GetMoney"]).ToString("N0");
                modelList.Add(model);
            }

            return modelList;
        }
    }
}
