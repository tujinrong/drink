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
using System.Data;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Data.Entity.Migrations;

using SafeNeeds.DySmat;
using DrinkService.Utils;

namespace DrinkService.Models
{
    /// <summary>
    /// 店舗マスタ
    /// </summary>
    public class M_StaffAdapter : HoEntityAdapterBase
    {
        public M_StaffAdapter(EntityRequest request)
            : base(request, typeof(M_Staff).Name)
        {
        }


        /// <summary>
        /// 担当者一覧
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public PageViewResult GetList(PageViewRequest req)
        {
            return base.GetList(req, Y_EntityViewData.担当者一覧);
        }


        /// <summary>
        /// 追加処理
        /// </summary>
        /// <param name="row"></param>
        public Result Add(M_Staff row)
        {
            dbContext.Staffs.Add(row);
            dbContext.SaveChanges();

            return new Result();
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        /// <param name="row"></param>
        public Result Edit(M_Staff row)
        {
            dbContext.Entry(row).State = EntityState.Modified;
            dbContext.SaveChanges();

            return new Result();

        }

        /// <summary>
        /// 削除処理
        /// </summary>
        /// <param name="_staff"></param>
        public Result Delete(M_Staff _staff)
        {
            TableDeleteRequest dreq = new TableDeleteRequest(_staff.ShopCD, _staff.StaffCD);
            return base.Delete(dreq);
        }

        public Result Save(M_Staff _staff, bool newMode)
        {
            Result result = new Result();

            if (newMode)
            {
                string[] key = { "ShopCD", "StaffCD" };
                string[] value = { _staff.ShopCD, _staff.StaffCD };

                if (this.HasData(key, value))
                {
                    result.Message = "このデータはすでに存在しています。";
                    result.ReturnValue = EnumResult.Error;
                    result.ErrorKey = "key";
                    return result;
                }
            }
            else
            {
                M_Staff staff  = dbContext.Staffs.Find(_staff.ShopCD, _staff.StaffCD);
                if (staff != null)
                {
                    _staff.OldPassword1 = staff.OldPassword1;
                    _staff.OldPassword2 = staff.OldPassword2;
                }
            }

            _staff.UpdateUser = this._entityRequest.User;
            _staff.UpdateTime = CommonUtils.GetDateTimeNow();
            dbContext.Staffs.AddOrUpdate(_staff);
            dbContext.SaveChanges();
            result.Message = "担当者保存完了しました。";
            result.ReturnValue = EnumResult.OK;
            return result;
        }

        internal PageViewResult GetStaffList(PageViewRequest req)
        {
            ViewAdapter view = new ViewAdapter(_entityRequest, typeof(M_Staff).Name, Y_EntityViewData.担当者一覧);

            return view.GetPageView(req);
        }
    }
}