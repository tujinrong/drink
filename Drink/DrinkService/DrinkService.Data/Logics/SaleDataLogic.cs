using DrinkService.Data.Models;
using DrinkService.Data.ViewModels;
using DrinkService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
using SafeNeeds.DySmat;
using SafeNeeds.DySmat.Logic;
using DrinkService.Utils;

namespace DrinkService.Data.Logics
{
    public class SaleDataLogic : LogicBase
    {
        public SaleDataLogic(EntityRequest enreq)
            : base(enreq)
        {
            _enreq = enreq;
        }

        public PagedResult GetSaleDataList(string shopCD,string handingFlag, string StartDate, string EndDate, string pageNumber)
        {

            int pNumber = String.IsNullOrEmpty(pageNumber) ? 1 : int.Parse(pageNumber);
            return GetModels(shopCD, handingFlag, StartDate, EndDate, pNumber);
      }

        private PagedResult GetModels(string shopCD,string handingFlag, string StartDate, string EndDate, int? pageNumber)
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

            string downloadDate = "not null";

            if (handingFlag != "2")
            {
                downloadDate = "null";
                StartDate = "";
                EndDate = "1";
                endDate = CommonUtils.GetDateTimeNow();
            }

            int pNumber = pageNumber == null ? 1 : pageNumber.Value;

            bool getPageCount = pageNumber == null ? false : true;


            T_HoDayAdapter logic = new T_HoDayAdapter(_enreq);

            PageViewRequest req = new PageViewRequest();
            req.PageNo = pNumber;
            req.GetPageCount = getPageCount;
            req.FilterDic.Add(Y_EntityFilterData.ShopFilter, new string[] { shopCD });
            req.FilterDic.Add(Y_EntityFilterData.DownloadDateFilter, new string[] { downloadDate });
            req.FilterDic.Add(Y_EntityFilterData.HoDateFromFilter, new string[] { string.IsNullOrEmpty(StartDate) ? "" : startDate.ToString() });
            req.FilterDic.Add(Y_EntityFilterData.HoDateToFilter, new string[] { string.IsNullOrEmpty(EndDate) ? "" : endDate.ToString() });
            //一覧データの取得
            PageViewResult result = logic.GetSaleDataList(req);

            DyEntityLogic dyLogic = new DyEntityLogic();

            return new PagedResult(pageSize, result.PageCount, pNumber, dyLogic.DataTableToDic(result.DataTable));
 


        }
    }
}
