using DrinkService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using PagedList;
using System.Data.Entity.Migrations;
using DrinkService.Data.Models;
using DrinkService.Data.ViewModels;
using DrinkService.Utils;
using System.Data.Entity;
using SafeNeeds.DySmat;
using SafeNeeds.DySmat.Logic;
using SafeNeeds.DySmat.Util;

namespace DrinkService.Data.Logics
{
    public class ShopLogic : LogicBase
    {

        public ShopLogic(EntityRequest enreq)
            : base(enreq)
        {
            _enreq = enreq;
        }
        /// <summary>
        /// 店舗リストを取得
        /// </summary>
        /// <returns></returns>
        public List<M_Shop> GetAllShops(string shopTypeCD)
        {
            List<M_Shop> shopsList;
            if (shopTypeCD == null || shopTypeCD == "")
            {
                shopTypeCD = "1";
                shopsList = db.Shops.Where(s => s.ShopTypeCD == shopTypeCD).ToList();
            }
            else
            {
                List<string> shopTypeCDs = new List<string>(shopTypeCD.Split(','));

                shopsList = db.Shops.Where(s => shopTypeCDs.Contains(s.ShopTypeCD)).ToList();
            }
            return shopsList;
        }

        /// <summary>
        /// 店舗ページリストを取得
        /// </summary>
        /// <param name="shopKey">キー</param>
        /// <param name="pageNumber">ページ番号</param>
        /// <returns></returns>
        public PagedResult GetShopPagedListByKeyAndPageNumber(string shopTypeCD, string shopKey, string pageNumber)
        {
            //List<M_Shop> shopsList;
            //if (shopTypeCD == null || shopTypeCD == "")
            //{
            //    shopTypeCD = "1";
            //    shopsList = db.Shops.Where(s => s.ShopTypeCD == shopTypeCD).ToList();
            //}
            //else
            //{
            //    List<string> shopTypeCDs = new List<string>(shopTypeCD.Split(','));

            //    shopsList = db.Shops.Where(s => shopTypeCDs.Contains(s.ShopTypeCD)).ToList();
            //}

            //var shops = from shop in shopsList

            //            select shop;

            //if (String.IsNullOrEmpty(shopKey) == false)
            //{
            //    shops = shops.Where(p => (p.ShopCD.Contains(shopKey)) || (p.ShopName.Contains(shopKey)));
            //}

            //int totalSize = shops.Count();
            //int pNumber = String.IsNullOrEmpty(pageNumber) ? 1 : int.Parse(pageNumber);

            //return new PagedResult<M_Shop>(pageSize, shops.Count(),pNumber, shops.ToPagedList(pNumber, pageSize));

            int pNumber = String.IsNullOrEmpty(pageNumber) ? 1 : int.Parse(pageNumber);

            M_ShopAdapter logic = new M_ShopAdapter(_enreq);

            PageViewRequest req = new PageViewRequest();
            req.PageNo = pNumber;
            req.GetPageCount = true;
            req.FilterDic.Add(Y_EntityFilterData.ReferShopTypeFilter, new string[] { string.IsNullOrEmpty(shopTypeCD) ? "1" : shopTypeCD });
            req.FilterDic.Add(Y_EntityFilterData.ReferShopKeyFilter, new string[] {shopKey });
            //一覧データの取得
            PageViewResult result = logic.GetShopRefer(req);

            DyEntityLogic dyLogic = new DyEntityLogic();

            return new PagedResult(pageSize, result.PageCount, pNumber, dyLogic.DataTableToDic(result.DataTable));
        }

        /// <summary>
        /// 店舗リストを取得
        /// </summary>
        /// <param name="regionCD">地域コード</param>
        /// <param name="shopName">店舗名</param>
        /// <param name="pageNumber">ページ番号</param>
        /// <returns></returns>
        public PagedResult GetPagedShopList(string shopCD, string shopType, string regionCD, string shopName, string pageNumber)
        {
            int pNumber = String.IsNullOrEmpty(pageNumber) ? 1 : int.Parse(pageNumber);
            return GetModels(shopCD,shopType, regionCD, shopName, pNumber);
        }

        /// <summary>
        /// 店舗リストを取得
        /// </summary>
        /// <param name="regionCD">地域コード</param>
        /// <param name="shopName">店舗名</param>
        /// <param name="pageNumber">ページ番号</param>
        /// <returns></returns>
        public List<ShopListViewModel> GetShopList(string shopType, string regionCD, string shopName)
        {
            List<ShopListViewModel> modelList = new List<ShopListViewModel>();
            PagedResult data = GetModels(null,shopType, regionCD, shopName, null);
            data.pageData.ForEach(
                l =>
                {
                    ShopListViewModel model = new ShopListViewModel();
                    model.ShopCD = DataUtil.CStr(l["ShopCD"]);
                    model.ShopName = DataUtil.CStr(l["ShopName"]);
                    model.RegionCDName = DataUtil.CStr(l["RegionCDName"]);
                    model.AreaCDName = DataUtil.CStr(l["AreaCDName"]);
                    model.SysTypeCDName = DataUtil.CStr(l["SysTypeCDName"]);
                    modelList.Add(model);
                });
            return modelList;
        }

        /// <summary>
        /// 店舗リストを取得
        /// </summary>
        /// <param name="regionCD">地域コード</param>
        /// <param name="shopName">店舗名</param>
        /// <param name="pageNumber">ページ番号</param>
        /// <returns></returns>
        private PagedResult GetModels(string shopCD, string shopType, string regionCD, string shopName, int? pageNumber)
        {
            //var Shops = db.Shops.ToList();
            //var Codes = db.Codes.ToList();

            //var models = from shop in Shops

            //             join codeShopTypeCD in Codes on new { key = shop.ShopTypeCD, kind = ModelBase.CN店舗区分 } equals new { key = codeShopTypeCD.CD, kind = codeShopTypeCD.Kind } into g_ShopTypeCD_code
            //             from s_codeShopTypeCD in g_ShopTypeCD_code.DefaultIfEmpty(new M_Code())

            //             join codeRegionCD in Codes on new { key = shop.RegionCD, kind = ModelBase.CN地域 } equals new { key = codeRegionCD.CD, kind = codeRegionCD.Kind } into g_RegionCD_code
            //             from s_codeRegionCD in g_RegionCD_code.DefaultIfEmpty(new M_Code())

            //             join codeAreaCD in Codes on new { key = shop.AreaCD, kind = ModelBase.CNエリア } equals new { key = codeAreaCD.CD, kind = codeAreaCD.Kind } into g_AreaCD_code
            //             from s_codeAreaCD in g_AreaCD_code.DefaultIfEmpty(new M_Code())

            //             join codeSysTypeCD in Codes on new { key = shop.SysTypeCD, kind = ModelBase.CN店舗業務区分 } equals new { key = codeSysTypeCD.CD, kind = codeSysTypeCD.Kind } into g_SysTypeCD_code
            //             from s_codeSysTypeCD in g_SysTypeCD_code.DefaultIfEmpty(new M_Code())
            //             select new ShopListViewModel
            //             {
            //                 ShopCD = shop.ShopCD,
            //                 ShopName = shop.ShopName,
            //                 ShopTypeCD = shop.ShopTypeCD,
            //                 RegionCD = shop.RegionCD,
            //                 AreaCD = shop.AreaCD,
            //                 AreaCDName = s_codeAreaCD.Name,
            //                 SysTypeCD = shop.SysTypeCD,
            //                 ShopTypeCDName = s_codeShopTypeCD.Name,
            //                 RegionCDName = s_codeRegionCD.Name,
            //                 SysTypeCDName = s_codeSysTypeCD.Name,
            //             };

            //if (string.IsNullOrEmpty(shopType) == false)
            //{
            //    models = models.Where(m => m.ShopTypeCD == shopType);
            //}

            //if (string.IsNullOrEmpty(regionCD) == false)
            //{
            //    models = models.Where(m => m.RegionCD == regionCD);
            //}

            //if (string.IsNullOrEmpty(shopName) == false)
            //{
            //    models = models.Where(m => CommonUtils.isContains(m.ShopName, shopName));
            //}

            //return models;


            int pNumber = pageNumber == null ? 1 : pageNumber.Value;

            bool getPageCount = pageNumber == null ? false : true;

            M_ShopAdapter logic = new M_ShopAdapter(_enreq);

            PageViewRequest req = new PageViewRequest();
            req.PageNo = pNumber;
            req.GetPageCount = getPageCount;
            req.FilterDic.Add(Y_EntityFilterData.ShopFilter, new string[] { shopCD });
            req.FilterDic.Add(Y_EntityFilterData.ShopTypeFilter, new string[] { shopType });
            req.FilterDic.Add(Y_EntityFilterData.RegionFilter, new string[] { regionCD });
            req.FilterDic.Add(Y_EntityFilterData.ShopNameFilter, new string[] { shopName });

            
            //一覧データの取得
            PageViewResult result = logic.GetShopList(req);

            DyEntityLogic dyLogic = new DyEntityLogic();

            return new PagedResult(pageSize, result.PageCount, pNumber, dyLogic.DataTableToDic(result.DataTable));

        }

        /// <summary>
        /// 店舗を取得
        /// </summary>
        /// <param name="shopCD">店舗コード</param>
        /// <returns>店舗</returns>
        public M_Shop GetShopByShopCD(string shopCD)
        {
            M_Shop shop = db.Shops.Find(shopCD);
            if (shop == null)
                return new M_Shop();
            else
                return shop;
        }

        /// <summary>
        /// 店舗を保存する
        /// </summary>
        /// <param name="_shop">店舗</param>
        public void Save(M_Shop _shop)
        {
            _shop.UpdateTime = DateTime.Now;
            //_shop.UpdateUserID = "test";

            db.Shops.AddOrUpdate(_shop);
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                throw;
            }
        }

        /// <summary>
        /// 店舗を削除する
        /// </summary>
        /// <param name="_shop">店舗</param>
        public void Del(M_Shop _shop)
        {
            _shop = GetShopByShopCD(_shop.ShopCD);
            db.Shops.Remove(_shop);
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                throw;
            }
        }

        public object GetShopAutoComplete(string shopTypeCD, string shopKey)
        {
            int pNumber = 1;

            M_ShopAdapter logic = new M_ShopAdapter(_enreq);

            PageViewRequest req = new PageViewRequest();
            req.PageNo = pNumber;
            req.PageRows = 10;
            req.GetPageCount = true;
            req.FilterDic.Add(Y_EntityFilterData.ReferShopTypeFilter, new string[] { string.IsNullOrEmpty(shopTypeCD) ? "1" : shopTypeCD });
            req.FilterDic.Add(Y_EntityFilterData.ReferShopKeyFilter, new string[] { shopKey });
            //一覧データの取得
            PageViewResult result = logic.GetShopRefer(req);

            DyEntityLogic dyLogic = new DyEntityLogic();

            return new PagedResult(pageSize, result.PageCount, pNumber, dyLogic.DataTableToDic(result.DataTable));
        }
    }
}
