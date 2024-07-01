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
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkService.Report.Logic
{
    public class DeliveryRoutePlacementLogic : LogicBase
    {
        public DeliveryRoutePlacementLogic(EntityRequest enreq)
            : base(enreq)
        {
            _enreq = enreq;
        }

        public List<DeliveryRoutePlacementModel> GetModes(string shopCD)
        {
            string shopSql = "";

            if (shopCD != null && shopCD.Length > 0)
            {
                shopSql = string.Format("and M_Shop.ShopCD = '{0}'", shopCD);
            }

            string sql = @"
                 select M_Shop.ShopCD,M_Shop.ShopName,T_HoClientItem.ItemCD,M_Item.ShortName 

                ,sum(isnull(T_HoClientItem.AfterNum,0)) as AfterNum
            
                from T_HoClientItem 
            
                left join M_Shop 
                on T_HoClientItem.ShopCD = M_Shop.ShopCD
            
                left join M_Item
                on T_HoClientItem.ItemCD = M_Item.ItemCD
            
                left join ( 
                    select
                        ShopCD
                        , ClientCD
                        , max(Seq) MaxSeq 
                    from
                        T_HoOrderClient 
                    where
                        DoneFlag = 1 
                        or DoneFlag = 3 
                    group by
                        ShopCD
                        , ClientCD
                ) T4 
                on T_HoClientItem.ShopCD = T4.ShopCD 
                and T_HoClientItem.ClientCD = T4.ClientCD
            
                where 1=1 {0} and isnull(T_HoClientItem.AfterNum,0) > 0 and T_HoClientItem.Seq = T4.MaxSeq 
            
                group by M_Shop.ShopCD,M_Shop.ShopName,T_HoClientItem.ItemCD,M_Item.ShortName
                order by M_Shop.ShopCD,T_HoClientItem.ItemCD
            ";

            sql = string.Format(sql, shopSql);

            DataTable dt = SQLHelper.GetDataTable(sql);

            List<DeliveryRoutePlacementModel> masterList = new List<DeliveryRoutePlacementModel>();
            DeliveryRoutePlacementModel master = null;
            string date = CommonUtils.GetDateTimeNow().ToString("yyyy/MM/dd HH:mm:ss");

            string tKey = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];

                string key = DataUtil.CStr(dr["ShopCD"]);

                if (key != tKey)
                {
                    if (master != null)
                    {
                        masterList.Add(master);
                    }
                    tKey = key;
                    master = new DeliveryRoutePlacementModel();
                    master.shopName =DataUtil.CStr(dr["ShopCD"]) + "　" + DataUtil.CStr(dr["ShopName"]);
                    master.date = date;
                }

                DeliveryRoutePlacementDetailItem item = new DeliveryRoutePlacementDetailItem();
                item.productCode = CommonUtils.FormatItemCD(DataUtil.CStr(dr["ItemCD"]));
                item.productName = DataUtil.CStr(dr["ShortName"]);
                item.placeNum = DataUtil.CInt(dr["AfterNum"]).ToString("N0");

                master.details.Add(item); 
                
                if (i == dt.Rows.Count - 1)
                {
                    masterList.Add(master);
                }
            }

            int pageCount = 1;
            List<DeliveryRoutePlacementModel> modelList = new List<DeliveryRoutePlacementModel>();

            foreach (var masterItem in masterList)
            {
                for (int i = 0; i < masterItem.details.Count; i = i + 40)
                {
                    DeliveryRoutePlacementModel model = new DeliveryRoutePlacementModel();
                    model.shopName = masterItem.shopName;
                    model.date = masterItem.date;
                    model.page = "Page: " + (pageCount++);

                    if (i + 40 > masterItem.details.Count)
                    {
                        model.details.AddRange(masterItem.details.GetRange(i, masterItem.details.Count - i));
                    }
                    else
                    {
                        model.details.AddRange(masterItem.details.GetRange(i, 40));
                    }

                    modelList.Add(model);
                }
            }

            return modelList;
        }
    }
}
