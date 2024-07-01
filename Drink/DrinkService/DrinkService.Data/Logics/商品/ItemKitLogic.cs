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
using SafeNeeds.DySmat.Logic;
using System.Transactions;
using SafeNeeds.DySmat.Util;
using SafeNeeds.DySmat;

namespace DrinkService.Data.Logics
{
    public class ItemKitLogic : LogicBase
    {
        public ItemKitLogic(EntityRequest enreq)
            : base(enreq)
        {
            _enreq = enreq;
        }

        public PagedResult GetPagedItemKitList(string shopCD, int? kitID, string kitName, string pageNumber)
        {
            int pNumber = String.IsNullOrEmpty(pageNumber) ? 1 : int.Parse(pageNumber);
            return GetModels(shopCD, kitID, kitName, pNumber);
        }

        public List<ItemKitListViewModel> GetItemKitList(string shopCD, int? kitID, string kitName)
        {
            List<ItemKitListViewModel> modelList = new List<ItemKitListViewModel>();
            PagedResult data = GetModels(shopCD, kitID, kitName, null);
            data.pageData.ForEach(
                s =>
                {
                    ItemKitListViewModel model = new ItemKitListViewModel();
                    model.ShopCD = DataUtil.CStr(s["ShopCD"]);
                    model.ShopName = DataUtil.CStr(s["ShopName"]);
                    model.KitID = DataUtil.CInt(s["KitID"]);
                    model.KitName = DataUtil.CStr(s["KitName"]);
                    modelList.Add(model);
                });

            return modelList;
        }

        private PagedResult GetModels(string shopCD, int? kitID, string kitName, int? pageNumber)
        {
            //var ItemKits = db.ItemKits.ToList();
            //var Shops = db.Shops.ToList();
            //var models = from itemKit in ItemKits
            //             join shop in Shops on itemKit.ShopCD equals shop.ShopCD into g_itemKit_shop
            //             from s_shop in g_itemKit_shop.DefaultIfEmpty(new M_Shop())
            //             select new ItemKitListViewModel
            //             {
            //                 ShopCD = itemKit.ShopCD,
            //                 ShopName = s_shop.ShopName,
            //                 KitID = itemKit.KitID,
            //                 KitName = itemKit.KitName,
            //             };

            //if (string.IsNullOrEmpty(shopCD) == false)
            //{
            //    models = models.Where(m => m.ShopCD == shopCD);
            //}

            //if (kitID != null)
            //{
            //    models = models.Where(m => m.KitID == kitID.Value);
            //}

            //if (string.IsNullOrEmpty(kitName) == false)
            //{
            //    models = models.Where(m => m.KitName.Contains(kitName));
            //}

            //return models;

            int pNumber = pageNumber == null ? 1 : pageNumber.Value;

            bool getPageCount = pageNumber == null ? false : true;

            M_ItemAdapter logic = new M_ItemAdapter(_enreq);

            PageViewRequest req = new PageViewRequest();
            req.PageNo = pNumber;
            req.GetPageCount = getPageCount;
            req.FilterDic.Add(Y_EntityFilterData.ShopFilter, new string[] { shopCD });
            req.FilterDic.Add(Y_EntityFilterData.ItemKitFilter, new string[] { kitID == null?"":kitID.Value.ToString() });
            req.FilterDic.Add(Y_EntityFilterData.ItemKitNameFilter, new string[] { kitName });
            //一覧データの取得
            PageViewResult result = logic.GetItemKitList(req);

            DyEntityLogic dyLogic = new DyEntityLogic();

            return new PagedResult(pageSize, result.PageCount, pNumber, dyLogic.DataTableToDic(result.DataTable));
        }

        public M_ItemKit GetItemKitByShopCD(string shopCD, int? kitID)
        {
            return db.ItemKits.Find(shopCD, kitID);
        }

        public object Save(M_ItemKit _itemKit, List<M_ItemKitDetail> detailList,string updateUser)
        {

            //string Message = "このデータはすでに存在しています。";
            //EnumResult ReturnValue = EnumResult.OK;
            //string ErrorKey = "Kit";

            using (TransactionScope scope = new TransactionScope())
            {
                try
                {

                    var names = from kit in db.ItemKits
                                where kit.ShopCD == _itemKit.ShopCD && kit.KitName == _itemKit.KitName
                                select kit;

                    if (_itemKit.KitID == 0)
                    {


                        int count = db.ItemKits.Where(e => e.ShopCD == _itemKit.ShopCD).ToList().Count();
                        _itemKit.KitID = count + 1;

                    }
                    else
                    {
                        names = names.Where(m => m.KitID != _itemKit.KitID);
                    }

                    if (names.Count() > 0)
                    {
                        return new
                        {
                            Message = "このデータはすでに存在しています。",
                            ReturnValue = EnumResult.Error,
                            ErrorKey = "txtKitName",
                            KitID = _itemKit.KitID
                        };
                    }

                    if (detailList != null)
                    {
                        foreach (M_ItemKitDetail item in detailList)
                        {
                            item.KitID = _itemKit.KitID;
                        }
                    }

                    db.ItemKitDetails.RemoveRange(db.ItemKitDetails.Where(e => e.ShopCD == _itemKit.ShopCD && e.KitID == _itemKit.KitID));

                    if (detailList != null)
                    {
                        db.ItemKitDetails.AddRange(detailList.ToArray());
                    }


                    _itemKit.UpdateTime = CommonUtils.GetDateTimeNow();
                    _itemKit.UpdateUser = updateUser;

                    db.ItemKits.AddOrUpdate(_itemKit);
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        throw;
                    }

                    scope.Complete();

                    return new
                    {
                        ReturnValue = EnumResult.OK,
                        KitID = _itemKit.KitID
                    };
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    scope.Dispose();
                }

            }
        }

        public void Del(M_ItemKit _itemKit)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    _itemKit = GetItemKitByShopCD(_itemKit.ShopCD, _itemKit.KitID);
                    List<M_ItemKitDetail> details = db.ItemKitDetails.Where(d => d.ShopCD == _itemKit.ShopCD && d.KitID == _itemKit.KitID).ToList();
                    db.ItemKits.Remove(_itemKit);
                    db.ItemKitDetails.RemoveRange(details);
                    db.SaveChanges();
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    scope.Dispose();
                }

            }
        }

        public List<M_ItemKit> GetItemKitByKitName(string kitName)
        {
            var ItemKits = db.ItemKits.ToList();


            if (!string.IsNullOrEmpty(kitName))
            {
                return ItemKits.Where(s => s.KitName == kitName).ToList();
            }
            else
            {
                return ItemKits;
            }
        }

        public List<M_ItemKit> GetItemKitName(string shopCD)
        {
            List<M_ItemKit> ItemKits;
            if (shopCD == null || shopCD == "")
            {
                ItemKits = db.ItemKits.ToList();
            }
            else
            {
                ItemKits = db.ItemKits.Where(k=> k.ShopCD == shopCD).ToList();
            }

            var names = from k in ItemKits
                        group k by new { KitID = k.KitID, KitName = k.KitName } into g
                        select new M_ItemKit
                        {
                            KitID = g.Key.KitID,
                            KitName = g.Key.KitName,
                        };

            return names.ToList();
        }

        public List<ViewModels.KitDetailViewModel> KitItemList(string ShopCD, int? kitID)
        {
            if (kitID > 0)
            {
                var items = db.Items.ToList();

                var detailItems = from d in db.ItemKitDetails

                                  where d.ShopCD == ShopCD && d.KitID == kitID

                                  select d;

                int i = 0;
                var detail = from d in detailItems.ToList()

                             join item in items on d.ItemCD equals item.ItemCD into g_item
                             from t_item in g_item.DefaultIfEmpty(new M_Item())

                             select new KitDetailViewModel
                             {
                                 No = ++i,
                                 ShopCD = d.ShopCD,
                                 ItemCD = d.ItemCD,
                                 ItemName = t_item.ItemName,
                                 ShortName = t_item.ShortName,
                                 KitID = d.KitID,
                                 ShelfCD = d.ShelfCD,
                                 Num = d.Num,
                                 Price = d.Price
                             };

                List<ViewModels.KitDetailViewModel> result = detail.ToList();
                return result;
            }
            else 
            {
                return new List<ViewModels.KitDetailViewModel>();
            }
        }
    }
}
