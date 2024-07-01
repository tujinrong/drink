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
    public class StaffLogic : LogicBase
    {
        public StaffLogic(EntityRequest enreq)
            : base(enreq)
        {
            _enreq = enreq;
        }
        /// <summary>
        /// 担当者リストを取得
        /// </summary>
        /// <param name="roleKbn">所属区分</param>
        /// <param name="shopCD">店舗コード</param>
        /// <param name="staffName">担当者名</param>
        /// <param name="pageNumber">ページ番号</param>
        /// <returns>担当者リスト</returns>
        public PagedResult GetPagedStaffList(string roleKbn, string shopCD, string staffName, string pageNumber)
        {
            int pNumber = String.IsNullOrEmpty(pageNumber) ? 1 : int.Parse(pageNumber);
            return GetModels(roleKbn, shopCD, staffName, pNumber);
          }

        /// <summary>
        /// 担当者リストを取得
        /// </summary>
        /// <param name="roleKbn">所属区分</param>
        /// <param name="shopCD">店舗コード</param>
        /// <param name="staffName">担当者名</param>
        /// <param name="pageNumber">ページ番号</param>
        /// <returns>担当者リスト</returns>
        public List<StaffListViewModel> GetStaffList(string roleKbn, string shopCD, string staffName)
        {
            List<StaffListViewModel> modelList = new List<StaffListViewModel>();
            PagedResult data = GetModels(roleKbn, shopCD, staffName, null);
            data.pageData.ForEach(
                s => {
                    StaffListViewModel model = new StaffListViewModel();
                    model.ShopName = DataUtil.CStr(s["ShopName"]);
                    model.StaffCD = DataUtil.CStr(s["StaffCD"]);
                    model.StaffName = DataUtil.CStr(s["StaffName"]);
                    model.RoleCD = DataUtil.CStr(s["RoleCD"]);
                    modelList.Add(model);
                });

            return modelList;
        }

        /// <summary>
        /// 担当者リストを取得
        /// </summary>
        /// <param name="roleKbn">所属区分</param>
        /// <param name="shopCD">店舗コード</param>
        /// <param name="staffName">担当者名</param>
        /// <param name="pageNumber">ページ番号</param>
        /// <returns>担当者リスト</returns>
        private PagedResult GetModels(string shopType, string shopCD, string staffName, int? pageNumber)
        {
            //var Staffs = db.Staffs.ToList();
            //var Shops = db.Shops.ToList();
            //var Codes = db.Codes.ToList();

            //var models = from staff in Staffs
            //             join shop in Shops on staff.ShopCD equals shop.ShopCD into g_staff_shop
            //             from s_shop in g_staff_shop.DefaultIfEmpty(new M_Shop())
            //             join code in Codes on new { key = staff.RoleCD, kind = ModelBase.CN役割 } equals new { key = code.CD, kind = code.Kind } into g_staff_code
            //             from s_code in g_staff_code.DefaultIfEmpty(new M_Code())
            //             select new StaffListViewModel
            //             {
            //                 ShopTypeCD = s_shop.ShopTypeCD,
            //                 ShopCD = s_shop.ShopCD,
            //                 ShopName = s_shop.ShopName,
            //                 StaffCD = staff.StaffCD,
            //                 StaffName = staff.StaffName,
            //                 RoleCD = s_code.Name
            //             };

            //if (string.IsNullOrEmpty(shopType) == false)
            //{
            //    models = models.Where(m => m.ShopTypeCD == shopType);
            //}

            //if (string.IsNullOrEmpty(shopCD) == false)
            //{
            //    models = models.Where(p => p.ShopCD == shopCD);
            //}

            //if (string.IsNullOrEmpty(staffName) == false)
            //{
            //    models = models.Where(p => CommonUtils.isContains(p.StaffName, staffName));
            //}

            //return models;

            int pNumber = pageNumber == null ? 1 : pageNumber.Value;

            bool getPageCount = pageNumber == null ? false : true;

            M_StaffAdapter logic = new M_StaffAdapter(_enreq);

            PageViewRequest req = new PageViewRequest();
            req.PageNo = pNumber;
            req.GetPageCount = getPageCount;
            req.FilterDic.Add(Y_EntityFilterData.ShopTypeFilter, new string[] { shopType });
            req.FilterDic.Add(Y_EntityFilterData.ShopFilter, new string[] { shopCD});
            req.FilterDic.Add(Y_EntityFilterData.StaffNameFilter, new string[] { staffName });
            //一覧データの取得
            PageViewResult result = logic.GetStaffList(req);

            DyEntityLogic dyLogic = new DyEntityLogic();

            return new PagedResult(pageSize, result.PageCount, pNumber, dyLogic.DataTableToDic(result.DataTable));
        }

        /// <summary>
        /// 担当者を取得
        /// </summary>
        /// <param name="staffCD">担当者コード</param>
        /// <returns>担当者</returns>
        public M_Staff GetStaffByKey(string shopCD, string staffCD)
        {

            M_Staff staff = null;
            try 
	        {
                staff = db.Staffs.Find(shopCD, staffCD);
	        }
	        catch (Exception ex)
	        {
                Console.WriteLine(ex.StackTrace);
		        throw;
	        }

            return staff;
        }

        /// <summary>
        /// 担当者を保存する
        /// </summary>
        /// <param name="_staff">担当者</param>
        public void Save(M_Staff _staff,string updateUser)
        {
            _staff.UpdateTime = DateTime.Now;
            _staff.UpdateUser = updateUser;

            db.Staffs.AddOrUpdate(_staff);
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
        /// 担当者を削除する
        /// </summary>
        /// <param name="_staff">担当者</param>
        public void Del(M_Staff _staff)
        {
            _staff = GetStaffByKey(_staff.ShopCD, _staff.StaffCD);
            db.Staffs.Remove(_staff);
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
        /// 担当者リストを取得
        /// </summary>
        /// <param name="shopCD">店舗コード</param>
        public List<M_Staff> GetStaffByShopCD(string shopCD)
        {

            try
            {
                var Staffs = db.Staffs.ToList();


                if (!string.IsNullOrEmpty(shopCD))
                {
                    return Staffs.Where(s => s.ShopCD == shopCD).ToList();
                }
                else
                {
                    return Staffs;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                throw;
            }

            
        }
    }
}
