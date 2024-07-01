using DrinkService.Data.ViewModels;
using DrinkService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
using DrinkService.Data.Models;
using SafeNeeds.DySmat;
using SafeNeeds.DySmat.Logic;

namespace DrinkService.Data.Logics
{
    public class CollectionLogic : LogicBase
    {
        public CollectionLogic(EntityRequest enreq)
            : base(enreq)
        {
            _enreq = enreq;
        }

        public PagedResult GetCollectionList(string shopCD, string StartDate, string EndDate, string staffCD, string pageNumber)
        {
            return GetModels(shopCD, StartDate, EndDate, staffCD, pageNumber);
         }


        private PagedResult GetModels(string shopCD, string StartDate, string EndDate, string staffCD, string pageNumber)
        {


            DateTime startDate = new DateTime();
            DateTime endDate = new DateTime();
            if (string.IsNullOrEmpty(StartDate) == false)
            {
                startDate = DateTime.ParseExact(StartDate, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
            }
            if (string.IsNullOrEmpty(EndDate) == false)
            {
                endDate = DateTime.ParseExact(EndDate, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
            }

            //var HoClient = db.HoClients.Where(e => e.HoDate >= startDate).ToList();
            //var Staff = db.Staffs.ToList();
            //var HoOrderClient = db.HoOrderClients.Where(e => e.HoDate >= startDate).ToList();

            //var models = from hoClient in HoClient

            //             join staff in Staff
            //             on new { shop = hoClient.ShopCD, staffname = hoClient.TantoCD } equals new { shop = staff.ShopCD, staffname = staff.StaffCD } into client_item

            //             from c_item in client_item.DefaultIfEmpty(new M_Staff())
            //             join orderClient in HoOrderClient
            //             on new { hoClient.ShopCD, hoClient.ClientCD, hoClient.Seq } equals new { orderClient.ShopCD, orderClient.ClientCD, orderClient.Seq } into client_order

            //             from c_order in client_order.DefaultIfEmpty(new T_HoOrderClient())
            //             where c_order.DoneFlag == "1" || c_order.DoneFlag == "3"

            //             group hoClient by new { ShopCD = hoClient.ShopCD, HoDate = hoClient.HoDate, TantoCD = hoClient.TantoCD, StaffName = c_item.StaffName } into cg

            //             orderby cg.Key.HoDate, cg.Key.TantoCD

            //             select new CollectionViewModel
            //             {
            //                 ShopCD = cg.Key.ShopCD,
            //                 HoDate = cg.Key.HoDate == null ? "" : string.Format("{0:yyyy/MM/dd}", cg.Key.HoDate),
            //                 date = cg.Key.HoDate,
            //                 StaffCD = cg.Key.TantoCD,
            //                 StaffName = cg.Key.StaffName,
            //                 GetMoney = DataUtil.CStr(cg.Sum(g => g.GetMoney))
            //             };

            //if (string.IsNullOrEmpty(shopCD) == false)
            //{
            //    models = models.Where(p => p.ShopCD == shopCD);
            //}

            //if (string.IsNullOrEmpty(EndDate) == false)
            //{
            //    models = models.Where(p => p.date <= endDate);
            //}

            //if (string.IsNullOrEmpty(staffCD) == false)
            //{
            //    models = models.Where(p => p.StaffCD == staffCD);
            //}

            //return models;


            int pNumber = string.IsNullOrEmpty(pageNumber) ? 1 : int.Parse(pageNumber);

            T_HoClientAdapter logic = new T_HoClientAdapter(_enreq);

            PageViewRequest req = new PageViewRequest();
            req.PageNo = pNumber;
            req.GetPageCount = true;
            req.FilterDic.Add(Y_EntityFilterData.ShopFilter, new string[] { shopCD });
            req.FilterDic.Add(Y_EntityFilterData.HoDateFromFilter, new string[] { string.IsNullOrEmpty(StartDate) ? "" : startDate.ToString() });
            req.FilterDic.Add(Y_EntityFilterData.HoDateToFilter, new string[] { string.IsNullOrEmpty(EndDate) ? "" : endDate.ToString() });
            req.FilterDic.Add(Y_EntityFilterData.StaffFilter, new string[] { staffCD });
            req.FilterDic.Add(Y_EntityFilterData.DoneFlagFilter, new string[] {"1"});
            

            //一覧データの取得
            PageViewResult result = logic.GetCollectionList(req);

            DyEntityLogic dyLogic = new DyEntityLogic();

            return new PagedResult(pageSize, result.PageCount, pNumber, dyLogic.DataTableToDic(result.DataTable));
        }
    }
}
