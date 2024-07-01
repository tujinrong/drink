//*****************************************************************************
// [システム]  配置ドリンク
// 
// [機能概要]  店舗マスタ。
//
// [作成履歴]　2015/06/25  屠錦栄　初版 
//
// [レビュー]　2015/07/17  屠錦栄　 
//*****************************************************************************

using System;
using System.Data.Entity;
using System.Linq;
using System.Data;
using System.Collections.Generic;
using System.Data.Entity.Migrations;

using SafeNeeds.DySmat;
using DrinkService.Utils;


namespace DrinkService.Models
{
    /// <summary>
    /// 店舗マスタ
    /// </summary>
    public class M_ShopAdapter : HoEntityAdapterBase
    {
        public M_ShopAdapter(EntityRequest request): base(request, typeof(M_Shop).Name)
        {
        }



        public PageViewResult GetList(PageViewRequest req)
        {
            return base.GetList(req, Y_EntityViewData.店舗一覧);
        }

        /// <summary>
        /// 追加処理
        /// </summary>
        /// <param name="row"></param>
        public Result Add(M_Shop row)
        {
            dbContext.Shops.Add(row);
            dbContext.SaveChanges();

            return new Result();
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        /// <param name="row"></param>
        public Result Edit(M_Shop row)
        {
            dbContext.Entry(row).State = EntityState.Modified;
            dbContext.SaveChanges();

            return new Result();

        }

        /// <summary>
        /// 削除処理
        /// </summary>
        /// <param name="id"></param>
        public Result Delete(M_Shop _shop)
        {
            TableDeleteRequest dreq = new TableDeleteRequest(_shop.ShopCD);
            return base.Delete(dreq);
        }

        public Result Save(M_Shop _shop, bool newMode)
        {
            Result result = new Result();

            string[] keys = { _shop.ShopCD };

            if (newMode)
            {
                string[] keyFields = { "ShopCD" };

                if (this.HasData(keyFields, keys))
                {
                    result.Message = "このデータはすでに存在しています。";
                    result.ReturnValue = EnumResult.Error;
                    result.ErrorKey = "key";
                    return result;
                }
            }

            string[] shopNameField = { "ShopName" };
            string[] shopNameValue = { _shop.ShopName };
            if (this.UniqueCheck(keys, shopNameField, shopNameValue))
            {
                result.Message = "このデータはすでに存在しています。";
                result.ReturnValue = EnumResult.Error;
                result.ErrorKey = "ShopName";
                return result;
            }

            _shop.UpdateUser = this._entityRequest.User;
            _shop.UpdateTime = CommonUtils.GetDateTimeNow();
            dbContext.Shops.AddOrUpdate(_shop);
            dbContext.SaveChanges();
            result.Message = "店舗保存完了しました。";
            result.ReturnValue = EnumResult.OK;
            return result;
        }

        internal PageViewResult GetShopRefer(PageViewRequest req)
        {
            ViewAdapter view = new ViewAdapter(_entityRequest, typeof(M_Shop).Name, Y_EntityViewData.店舗参照);

            return view.GetPageView(req);
        }

        internal PageViewResult GetShopList(PageViewRequest req)
        {
            ViewAdapter view = new ViewAdapter(_entityRequest, typeof(M_Shop).Name, Y_EntityViewData.店舗一覧);

            return view.GetPageView(req);
        }
    }
}