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
    public class ItemLogic : LogicBase
    {
        public ItemLogic(EntityRequest enreq)
            : base(enreq)
        {
            _enreq = enreq;
        }
        /// <summary>
        /// 商品リストを取得
        /// </summary>
        /// <returns></returns>
        public List<M_Item> GetAllItems(string IgnoreItems)
        {
            List<M_Item> ItemsList = db.Items.ToList();

            var Items = from Item in ItemsList

                        select Item;

            if (String.IsNullOrEmpty(IgnoreItems) == false)
            {
                List<string> ignores = new List<string>(IgnoreItems.Split(','));
                Items = Items.Where(p => ignores.Contains(p.ItemCD) == false);
            }

            return Items.ToList();


            //List<string> ignores = new List<string>(IgnoreItems.Split(','));
            //ignores.ForEach(i => i = "'" + i + "'");

            //EntityRequest enreq = new EntityRequest(1, "test");

            //M_ItemAdapter logic = new M_ItemAdapter(enreq);

            //PageViewRequest req = new PageViewRequest();

            //req.FilterDic.Add(Y_EntityFilterData.ItemIgnoreFilter, new string[] { string.Join(",", ignores.ToArray()) });
            ////一覧データの取得
            //PageViewResult result = logic.GetItemRefer(req);

            //DyEntityLogic dyLogic = new DyEntityLogic();

            //return new PagedResult(pageSize, result.PageCount, 1, dyLogic.DataTableToDic(result.DataTable));
        }

        /// <summary>
        /// 商品ページリストを取得
        /// </summary>
        /// <param name="ItemKey">キー</param>
        /// <param name="pageNumber">ページ番号</param>
        /// <param name="MakerCD">MakerCD</param>
        /// <returns></returns>
        public PagedResult GetItemPagedListByKeyAndPageNumber(string ItemKey, string IgnoreItems, string ShopCD, string MakerCD, string pageNumber)
        {
            List<string> ignores = new List<string>(IgnoreItems.Split(','));
            ignores.ForEach(i => i = "'" + i + "'");

            int pNumber = String.IsNullOrEmpty(pageNumber) ? 1 : int.Parse(pageNumber);

            M_ItemAdapter logic = new M_ItemAdapter(_enreq);

            PageViewRequest req = new PageViewRequest();
            req.PageNo = pNumber;
            req.GetPageCount = true;

            req.FilterDic.Add(Y_EntityFilterData.ItemIgnoreFilter, new string[] { string.Join(",", ignores.ToArray()) });
            req.FilterDic.Add(Y_EntityFilterData.ReferItemFilter, new string[] { ItemKey});
            req.FilterDic.Add(Y_EntityFilterData.ItemMakerFilter, new string[] { MakerCD });


            //研修資格
            if (string.IsNullOrEmpty(ShopCD) == false)
            {
                req.FilterDic.Add("ShopCDFilter", new string[] { ShopCD });
            }
            

            string today = CommonUtils.GetDateTimeNow().ToString();
            req.FilterDic.Add("SaleDateFilter", new string[] { today });
            //一覧データの取得
            PageViewResult result = logic.GetItemRefer(req);

            DyEntityLogic dyLogic = new DyEntityLogic();

            return new PagedResult(pageSize,result.PageCount,pNumber,dyLogic.DataTableToDic(result.DataTable));
        }

        /// <summary>
        /// 商品リストを取得
        /// </summary>
        /// <param name="itemCD">商品コード</param>
        /// <param name="itemName">商品名</param>
        /// <param name="pageNumber">ページ番号</param>
        /// <returns></returns>
        public PagedResult GetPagedItemList(string itemCD, string itemName, bool searchAll, string MakerCD, string pageNumber)
        {
            int pNumber = String.IsNullOrEmpty(pageNumber) ? 1 : int.Parse(pageNumber);
            return GetModels(itemCD, itemName, searchAll, MakerCD, pNumber);
        }

        /// <summary>
        /// 商品リストを取得
        /// </summary>
        /// <param name="itemCD">商品コード</param>
        /// <param name="itemName">商品名</param>
        /// <param name="pageNumber">ページ番号</param>
        /// <returns></returns>
        public List<ItemListViewModel> GetItemList(string itemCD, string itemName, string MakerCD, bool searchAll)
        {
            List<ItemListViewModel> modelList = new List<ViewModels.ItemListViewModel>();
            PagedResult data = GetModels(itemCD, itemName, searchAll, MakerCD, null);

            //商品種別取得
            List<M_Code> itemTypeCodes = CommonLogic.CodeKindList("ItemTypeCD");
            Dictionary<String, M_Code> codeMap = new Dictionary<string, M_Code>();
            foreach (M_Code code in itemTypeCodes)
            {
                codeMap.Add(code.CD, code);
            }

            //軽減税率区分
            List<M_Code> taxTypeCDCodes = CommonLogic.CodeKindList("TaxTypeCD");
            Dictionary<String, M_Code> taxCodeMap = new Dictionary<string, M_Code>();
            foreach (M_Code code in taxTypeCDCodes)
            {
                taxCodeMap.Add(code.CD, code);
            }

            data.pageData.ForEach(
                l =>
                {
                    ItemListViewModel model = new ItemListViewModel();
                    model.ItemCD = DataUtil.CStr(l["ItemCD"]);
                    model.ItemName = DataUtil.CStr(l["ItemName"]);
                    if (DataUtil.CStr(l["SaleEndDay"]) != "")
                    {
                        model.SaleEndDay = DataUtil.DateToString(DataUtil.CDate(l["SaleEndDay"]),"yyyy/MM/dd");
                    }
                    model.StandardPrice = DataUtil.CDec(l["StandardPrice"]);
                    model.ShopPrice = DataUtil.CDec(l["ShopPrice"]);
                    model.InNum = DataUtil.CDec(l["InNum"]);
                    model.QualifiedCD = DataUtil.CStr(l["QualifiedCD"]);
                    model.MakerCD = DataUtil.CStr(l["MakerCD"]);
                    model.QualifiedName = DataUtil.CStr(l["QualifiedName"]);
                    model.MakerName = DataUtil.CStr(l["MakerName"]);
                    model.ItemTypeCD = DataUtil.CStr(l["ItemTypeCD"]);
                    model.TaxTypeCD = DataUtil.CStr(l["TaxTypeCD"]);


                    model.ShortName = DataUtil.CStr(l["ShortName"]);
                    if (DataUtil.CStr(l["SaleStartDay"]) != "")
                    {
                        model.SaleStartDay = DataUtil.DateToString(DataUtil.CDate(l["SaleStartDay"]), "yyyy/MM/dd");
                    }
                    if (DataUtil.CStr(l["FreezingDay"]) != "")
                    {
                        model.FreezingDay = DataUtil.DateToString(DataUtil.CDate(l["FreezingDay"]), "yyyy/MM/dd");
                    }
                    if (!DataUtil.IsNullOrEmpty(model.ItemTypeCD) && codeMap.ContainsKey(model.ItemTypeCD))
                    {
                        model.ItemTypeName = DataUtil.CStr(codeMap[model.ItemTypeCD].Name);
                    }
                    if (!DataUtil.IsNullOrEmpty(model.TaxTypeCD) && taxCodeMap.ContainsKey(model.TaxTypeCD))
                    {
                        model.TaxTypeName = DataUtil.CStr(taxCodeMap[model.TaxTypeCD].Name);
                    }
                    modelList.Add(model);
                });
            return modelList;
        }

        /// <summary>
        /// 商品リストを取得
        /// </summary>
        /// <param name="itemCD">商品コード</param>
        /// <param name="itemName">商品名</param>
        /// <param name="pageNumber">ページ番号</param>
        /// <returns></returns>
        private PagedResult GetModels(string itemCD, string itemName, bool searchAll, string MakerCD, int? pageNumber)
        {
            //var Items = db.Items.ToList();

            //var models = from item in Items
            //             select new ItemListViewModel
            //             {
            //                 ItemCD = item.ItemCD,
            //                 ItemName = item.ShortName,
            //                 StandardPrice = item.StandardPrice,
            //                 ShopPrice = item.ShopPrice,
            //                 InNum = item.InNum,
            //             };

            //if (string.IsNullOrEmpty(itemCD) == false)
            //{
            //    models = models.Where(m => CommonUtils.StartsWith(m.ItemCD, itemCD));
            //}

            //if (string.IsNullOrEmpty(itemName) == false)
            //{
            //    models = models.Where(m => CommonUtils.isContains(m.ItemName, itemName));
            //}

            //return models;

            int pNumber = pageNumber == null ? 1 : pageNumber.Value;

            bool getPageCount = pageNumber == null ? false : true;

            M_ItemAdapter logic = new M_ItemAdapter(_enreq);

            PageViewRequest req = new PageViewRequest();
            req.PageNo = pNumber;
            req.GetPageCount = getPageCount;
            req.FilterDic.Add(Y_EntityFilterData.ItemCDFilter, new string[] { itemCD });
            req.FilterDic.Add(Y_EntityFilterData.ItemNameFilter, new string[] { itemName });
            req.FilterDic.Add(Y_EntityFilterData.ItemMakerFilter, new string[] { MakerCD });

            if (searchAll == false)
            {
                string today = CommonUtils.GetDateTimeNow().ToString();
                req.FilterDic.Add("SaleDateFilter", new string[] { today });
            }

            //一覧データの取得
            PageViewResult result = logic.GetItemList(req);

            DyEntityLogic dyLogic = new DyEntityLogic();

            return new PagedResult(pageSize, result.PageCount, pNumber, dyLogic.DataTableToDic(result.DataTable));
        }
        
        /// <summary>
        /// 商品を取得
        /// </summary>
        /// <param name="itemCD">商品コード</param>
        /// <returns>商品</returns>
        public M_Item GetItemByItemCD(string itemCD)
        {
            M_Item item = db.Items.Find(itemCD);
            if (item == null)
                return new M_Item();
            else
                return item;
        }

        /// <summary>
        /// 商品を保存する
        /// </summary>
        /// <param name="_item">商品</param>
        public void Save(M_Item _item)
        {
            _item.UpdateTime = DateTime.Now;
            _item.UpdateUser = "test";

            db.Items.AddOrUpdate(_item);
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
        /// 商品リストを取得
        /// </summary>
        /// <param name="itemName">商品名</param>
        /// <returns>商品</returns>
        public List<M_Item> GetItemByItemName(string itemName)
        {
            var Items = db.Items.ToList();


            if (!string.IsNullOrEmpty(itemName))
            {
                return Items.Where(s => s.ItemName == itemName).ToList();
            }
            else
            {
                return Items;
            }
        }

        public PagedResult GetAutoComplete(string itemKey, string IgnoreItems)
        {
            List<string> ignores = new List<string>();
            if (!string.IsNullOrEmpty(IgnoreItems))
            {
                ignores.AddRange(IgnoreItems.Split(','));
                ignores.ForEach(i => i = "'" + i + "'");
            }

            int pNumber = 1;

            M_ItemAdapter logic = new M_ItemAdapter(_enreq);

            PageViewRequest req = new PageViewRequest();
            req.PageNo = pNumber;
            req.GetPageCount = true;
            req.PageRows = 10;
            req.FilterDic.Add(Y_EntityFilterData.ItemIgnoreFilter, new string[] { string.Join(",", ignores.ToArray()) });
            req.FilterDic.Add(Y_EntityFilterData.ReferItemFilter, new string[] { itemKey });
            //一覧データの取得
            PageViewResult result = logic.GetItemRefer(req);

            DyEntityLogic dyLogic = new DyEntityLogic();

            return new PagedResult(pageSize, result.PageCount, pNumber, dyLogic.DataTableToDic(result.DataTable));
        }
    }
}
