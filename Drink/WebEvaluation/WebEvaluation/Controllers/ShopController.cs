using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebEvaluation.Models;
using WebEvaluation.DAL;
using WebEvaluation.ViewModels;
using PagedList;
using WebEvaluation.Utils;
using System.IO;
using WebEvaluation.Controllers.Filters;
using WebEvaluation.DataModels;
using System.Globalization;
using System.Data.Entity.Validation;
using System.Reflection;
using WebEvaluation.Common;


namespace WebEvaluation.Controllers
{
    [AuthenticationFilter(Order = 1)]
    [ExceptionFilter(Order = 2)]    
    public class ShopController : BaseController
    {
        private EvaluationContext db = new EvaluationContext();
        /*LILIANG====2017/07/03=========================↓*/
        [WebAuthorizeAttribute(Roles = "02,04,09")]
        public ActionResult ShopDataImport(HttpPostedFileBase datafile, string path)
        {
            if ((datafile != null && datafile.ContentLength > 0) || path != null)
            {
                String filePath = "";
                string importFileName = "";
                if (path == null)
                {
                    string basePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\UPLOAD\\";
                    String date = string.Format("{0:yyyyMMddHHmmssfff}", DateTime.Now);
                    String fileName = string.Format("店舗_{0}.csv", date);
                    filePath = basePath + fileName;
                    importFileName = datafile.FileName;

                    if (!Directory.Exists(basePath))
                    {
                        Directory.CreateDirectory(basePath);
                    }

                    datafile.SaveAs(filePath);
                }
                else
                {
                    filePath = path;
                }
            
                List<ImportErrorViewModel> errorList = new List<ImportErrorViewModel>();
                List<ShopData> addList = new List<ShopData>();
                List<ShopData> updateList = new List<ShopData>();
                List<ShopData> deleteList = new List<ShopData>();

                List<M_Shop> data = db.Shops.ToList();

                List<T_Party> partys = db.Partys.ToList();
                List<M_Division> divs = db.Divisions.ToList();
                List<M_Group> groups = db.Groups.ToList();
                List<S_Code> codes = db.Codes.ToList();

                List<ICSVChecker<ShopData>> checkers = new List<ICSVChecker<ShopData>>();

                checkers.Add(new ShopValueCheck(groups));

                List<ShopData> list = CsvUtils.CsvToModel<ShopData>(filePath, ref errorList, checkers);

                if (errorList.Count > 0)
                { 
                    return ImportIndex<ShopData>(importFileName, "ShopDataImport", errorList, list, addList, updateList, deleteList, new { path = filePath });
                }

                #region 変化有のデータを　更新する。

                var updateQuery = from shop in data

                                  join shopGroup in groups on shop.GroupCD equals shopGroup.GroupCD into g_shop_group
                                  from s_group in g_shop_group.DefaultIfEmpty(new M_Group())

                                  join importShop in list on shop.ShopCD equals importShop.ShopCD into g_import_shop
                                  from s_import in g_import_shop

                                  join import_Group in groups on s_import.GroupCD equals import_Group.GroupName into g_import_group
                                  from i_group in g_import_group.DefaultIfEmpty(new M_Group())

                                  join shopType in codes on new { key = shop.ShopType, kind = "ShopType" } equals new { key = shopType.CD,kind = shopType.Kind } into g_shop_type
                                  from s_type in g_shop_type.DefaultIfEmpty(new S_Code())

                                  //join importType in codes on new { key = s_import.ShopType, kind = "ShopType" } equals new { key = importType.Name, kind = importType.Kind } into g_import_type
                                  //from i_type in g_import_type.DefaultIfEmpty(new S_Code())

                                  //where (s_group.GroupName != s_import.GroupCD
                                  //      || shop.ShopName != s_import.ShopName
                                  //      || s_type.CD != i_type.CD)

                                  where(checkShopUpdate(s_import,shop,s_group,s_type))

                                  select new
                                  {
                                      shop = shop,
                                      i_group = i_group,
                                      importShop = s_import,
                                      //i_type = i_type
                                  };

                foreach (var item in updateQuery)
                {
                    M_Shop shop = item.shop;

                    shop.ShopName = item.importShop.ShopName;

                    if (item.i_group != null)
                    {
                        shop.GroupCD = item.i_group.GroupCD;
                    }

                    //if(item.i_type != null)
                    //{ 
                    //    shop.ShopType = item.i_type.CD;
                    //}

                    shop.UpdateTime = DateTime.Now;
                    shop.UpdateUserID = (Session["user"] as UserSession).StaffCD;

                    db.Entry(shop).State = EntityState.Modified;

                    updateList.Add(item.importShop);

                }

                #endregion

                #region 差分削除

                //import件数ＤＢ半分以上の場合：差分削除する
                if (list.Count >= data.Count / 2)
                {
                    var delQuery = from shop in data

                                   join party in partys on shop.ShopCD equals party.ShopCD into g_import_party
                                   from p_import in g_import_party.DefaultIfEmpty()

                                   join importShop in list on shop.ShopCD equals importShop.ShopCD into g_import_shop
                                   from s_import in g_import_shop.DefaultIfEmpty()

                                   where s_import == null && p_import == null

                                   select new
                                   {
                                       shop = shop
                                   };

                    foreach (var item in delQuery)
                    {
                        M_Shop shop = item.shop;

                        db.Entry(shop).State = EntityState.Deleted;

                        ShopData importData = new ShopData();
                        importData.ShopCD = shop.ShopCD;
                        importData.ShopName = shop.ShopName;
                        //importData.ShopType = shop.ShopType;

                        //if (null != shop.ShopType)
                        //{
                        //    List<S_Code> codeList = codes.Where(c => c.CD == shop.ShopType && c.Kind == "ShopType").ToList();
                        //    if (codeList.Count > 0)
                        //    {
                        //        importData.ShopType = codeList[0].Name;
                        //    }
                        //}

                        if (null != shop.GroupCD)
                        {
                            List<M_Group> grp = groups.Where(g => g.GroupCD == shop.GroupCD).ToList();
                            if (grp.Count > 0)
                            {
                                importData.GroupCD = grp[0].GroupName;
                            }
                        }
                        deleteList.Add(importData);
                    }
                }
                #endregion

                #region 差分追加

                var addQuery = from importShop in list

                               join import_Group in groups on importShop.GroupCD equals import_Group.GroupName into g_import_group
                               from i_group in g_import_group.DefaultIfEmpty(new M_Group())

                               //join importType in codes on new { key = importShop.ShopType, kind = "ShopType" } equals new { key = importType.Name, kind = importType.Kind } into g_import_type
                               //from i_type in g_import_type.DefaultIfEmpty(new S_Code())

                               join shop in data on importShop.ShopCD equals shop.ShopCD into g_shop
                               from s_shop in g_shop.DefaultIfEmpty()
                               where s_shop == null

                               select new
                               {
                                   importShop = importShop,
                                   i_group = i_group,
                                   //i_type = i_type
                               };

                List<M_Shop> newdata = new List<M_Shop>();
                foreach (var item in addQuery)
                {
                    M_Shop shop = new M_Shop();

                    shop.ShopCD = item.importShop.ShopCD;
                    shop.ShopName = item.importShop.ShopName;

                    if (item.i_group != null)
                    {
                        shop.GroupCD = item.i_group.GroupCD;
                    }

                    //if (item.i_type != null)
                    //{
                    //    shop.ShopType = item.i_type.CD;
                    //}

                    shop.UpdateTime = DateTime.Now;
                    shop.UpdateUserID = (Session["user"] as UserSession).StaffCD;

                    newdata.Add(shop);

                    addList.Add(item.importShop);
                }
                db.Shops.AddRange(newdata);

                #endregion

                if (path == null || errorList.Count > 0)
                {
                    return ImportIndex<ShopData>(importFileName, "ShopDataImport", errorList, list, addList, updateList, deleteList, new { path = filePath });
                }
                else
                {
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex.ToString());
                        throw;
                    }

                    ViewBag.msg = "ファイルが正常にアップロードされました。";   
                }
            }
            return View();
        }

        private bool checkShopUpdate(ShopData s_import, M_Shop shop, M_Group s_group, S_Code s_type)
        {
             //where (s_group.GroupName != s_import.GroupCD
            //      || shop.ShopName != s_import.ShopName
            //      || s_type.CD != i_type.CD)

            string importStr = s_import.GroupCD + "," + s_import.ShopCD + "," + s_import.ShopName;
            string dataStr = s_group.GroupName + "," + shop.ShopCD + "," + shop.ShopName;

            if (importStr == dataStr)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        [WebAuthorizeAttribute(Roles = "02,04,09")]
        public ActionResult StaffDataImport(HttpPostedFileBase datafile, string path)
        {

             if ((datafile != null && datafile.ContentLength > 0) || path != null)
            {
                String filePath = "";
                string importFileName = "";
                if (path == null)
                {
                    string basePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\UPLOAD\\";
                    String date = string.Format("{0:yyyyMMddHHmmssfff}", DateTime.Now);
                    String fileName = string.Format("社員_{0}.csv", date);
                    filePath = basePath + fileName;
                    importFileName = datafile.FileName;

                    if (!Directory.Exists(basePath))
                    {
                        Directory.CreateDirectory(basePath);
                    }

                    datafile.SaveAs(filePath);
                }
                else
                {
                    filePath = path;
                }
            
                List<ImportErrorViewModel> errorList = new List<ImportErrorViewModel>();
                List<StaffData> addList = new List<StaffData>();
                List<StaffData> updateList = new List<StaffData>();
                List<StaffData> deleteList = new List<StaffData>();

                List<ICSVChecker<StaffData>> checkers = new List<ICSVChecker<StaffData>>();

                 List<S_Unit> units = db.Units.ToList();

                checkers.Add(new StaffValueCheck(units));
                List<StaffData> list = CsvUtils.CsvToModel<StaffData>(filePath, ref errorList, checkers);

                if (errorList.Count > 0)
                {
                    return ImportIndex<StaffData>(importFileName, "StaffDataImport", errorList, list, addList, updateList, deleteList, new { path = filePath });
                }

                foreach (var item in list)
                {
                    item.EnrollmentDate = CommonUtils.CDateStr(item.EnrollmentDate);
                    item.StaffCD = CommonUtils.ParseZero(item.StaffCD, 5);
                    if (item.Duty == "空白")
                    {
                        item.Duty = "";
                    }
                }

                List<M_Staff> data = db.Staffs.ToList();

                //廖　2018.10.13 ↓　差分削除不要　
               // List<T_Party> partys = db.Partys.ToList();
               // List<T_EvaByStaff> evaByStaffs = db.EvaByStaffs.ToList();
                //廖　2018.10.13 ↑

                #region 変化有のデータを　更新する。
                var updateQuery = from staff in data

                                  join importStaff in list on staff.StaffCD equals importStaff.StaffCD into g_import_staff
                                  from s_import in g_import_staff

                                  //where (IsDateDiff(s_import.EnrollmentDate, staff.EnrollmentDate) 
                                  //      || ((staff.Sex == "1" && s_import.Sex == "男") == false && (staff.Sex == "2" && s_import.Sex == "女") == false)
                                  //      || staff.StaffKana != s_import.StaffKana
                                  //      || staff.StaffName != s_import.StaffName
                                  //      || (staff.Duty != s_import.Duty && !((staff.Duty == null || staff.Duty == "") && (s_import.Duty == null || s_import.Duty == "")))
                                  //      || staff.Yakusyoku != s_import.Yakusyoku
                                  //      || staff.UnitCD != s_import.SosikiCD)

                                  where (checkStaffUpdate(s_import, staff))

                                  select new
                                  {
                                      staff = staff,
                                      importStaff = s_import
                                  };

                List<M_User> users = db.Users.ToList();
                List<M_User> userdata = new List<M_User>();

                foreach (var item in updateQuery) 
                {
                    M_Staff staff = item.staff;
                    if (!string.IsNullOrEmpty(item.importStaff.EnrollmentDate))
                    {
                        staff.EnrollmentDate = DateTime.ParseExact(item.importStaff.EnrollmentDate, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                    }
                    if (item.importStaff.Sex == "男")
                    {
                        staff.Sex = "1";
                    }
                    else if (item.importStaff.Sex == "女")
                    {
                        staff.Sex = "2";
                    }
                    staff.StaffKana = item.importStaff.StaffKana;
                    staff.StaffName = item.importStaff.StaffName;
                    staff.UnitCD = item.importStaff.SosikiCD;
                    staff.Yakusyoku = item.importStaff.Yakusyoku;
                    staff.Email = item.importStaff.Email;
                    if (item.importStaff.Duty != "空白")
                    {
                        staff.Duty = item.importStaff.Duty;
                    }

                    staff.UpdateTime = DateTime.Now;
                    staff.UpdateUserID = (Session["user"] as UserSession).StaffCD;
                    db.Entry(staff).State = EntityState.Modified;

                    S_Unit unit = db.Units.Find(staff.UnitCD);

                    if (unit != null)
                    {
                        List<M_User> userList = users.Where(u => u.StaffCD == staff.StaffCD).ToList();
                        if (userList.Count > 0)
                        {
                            M_User user = userList[0];

                            if (unit.IsShop)
                            {
                                user.RoleCD = "01";
                            }
                            else if (unit.IsCusCenter)
                            {
                                user.RoleCD = "02";
                            }
                            else
                            {
                                user.RoleCD = "03";
                            }
                            user.UpdateTime = DateTime.Now;
                            user.UpdateUserID = (Session["user"] as UserSession).StaffCD;
                            db.Entry(user).State = EntityState.Modified;
                        }
                        else
                        {
                            M_User user = new M_User();
                            user.UserID = staff.StaffCD;
                            user.Password = staff.StaffCD;
                            user.StaffCD = staff.StaffCD;
                            if (unit.IsShop)
                            {
                                user.RoleCD = "01";
                            }
                            else if (unit.IsCusCenter)
                            {
                                user.RoleCD = "02";
                            }
                            else
                            {
                                user.RoleCD = "03";
                            }
                            user.UpdateTime = DateTime.Now;
                            user.UpdateUserID = (Session["user"] as UserSession).StaffCD;
                            userdata.Add(user); 
                        }
                    }

                    updateList.Add(item.importStaff);
                }

                #endregion

                #region 差分削除

                //廖　2018.10.13 ↓　差分削除不要　
                 /*
                //import件数ＤＢ半分以上の場合：差分削除する
                //if (list.Count >= data.Count / 2)
                //李梁　2014.09.12
                if (list.Count >= 1000)
                {
                    var delQuery = from staff in data

                                   join party in partys on staff.StaffCD equals party.TantoCD into g_party_staff
                                   from p_import in g_party_staff.DefaultIfEmpty()
                                   join eva1ByStaff in evaByStaffs on staff.StaffCD equals eva1ByStaff.Eva1StaffCD into g_eva1_staff
                                   from eva1_import in g_eva1_staff.DefaultIfEmpty()
                                   join eva2ByStaff in evaByStaffs on staff.StaffCD equals eva2ByStaff.Eva2StaffCD into g_eva2_staff
                                   from eva2_import in g_eva2_staff.DefaultIfEmpty()
                                   join eva3ByStaff in evaByStaffs on staff.StaffCD equals eva3ByStaff.Eva3StaffCD into g_eva3_staff
                                   from eva3_import in g_eva3_staff.DefaultIfEmpty()
                                   join importStaff in list on staff.StaffCD equals importStaff.StaffCD into g_import_staff
                                   from s_import in g_import_staff.DefaultIfEmpty()
                                   where s_import == null && p_import == null && eva1_import == null && eva2_import == null && eva3_import == null

                                   select new
                                   {
                                       staff = staff
                                   };

                    foreach (var item in delQuery)
                    {
                        M_Staff staff = item.staff;

                        db.Entry(staff).State = EntityState.Deleted;

                        List<M_User> userList = users.Where(u => u.StaffCD == staff.StaffCD).ToList();
                        if (userList.Count > 0)
                        {
                            M_User user = userList[0];
                            db.Entry(user).State = EntityState.Deleted;
                        }

                        StaffData importData = new StaffData();
                        importData.StaffCD = staff.StaffCD;
                        importData.StaffName = staff.StaffName;
                        importData.StaffKana = staff.StaffKana;
                        if (staff.Sex == "1")
                        {
                            importData.Sex = "男";
                        }
                        else if (staff.Sex == "2")
                        {
                            importData.Sex = "女";
                        }
                        if (null != staff.EnrollmentDate)
                        {
                            importData.EnrollmentDate = staff.EnrollmentDate.HasValue ? staff.EnrollmentDate.Value.ToString("yyyyMMdd") : null;
                        }
                        importData.SosikiCD = staff.UnitCD;
                        importData.Yakusyoku = staff.Yakusyoku;
                        importData.Duty = staff.Duty;

                        deleteList.Add(importData);
                    }
                }
                  */
                //廖　2018.10.13 ↑　差分削除不要　
                #endregion

                #region 差分追加
                var addQuery = from importStaff in  list
                               join staff in data on importStaff.StaffCD equals staff.StaffCD into g_staff
                               from s_staff in g_staff.DefaultIfEmpty()
                               where s_staff == null

                               select new
                               {
                                   import = importStaff
                               };

                List<M_Staff> newdata = new List<M_Staff>();
                
                foreach (var item in addQuery)
                {
                    M_Staff staff = new M_Staff();

                    staff.StaffCD = CommonUtils.ParseZero(item.import.StaffCD, 5);
                    staff.StaffName = item.import.StaffName;
                    staff.StaffKana = item.import.StaffKana;
                    if (item.import.Sex == "男")
                    {
                        staff.Sex = "1";
                    }
                    else if (item.import.Sex == "女")
                    {
                        staff.Sex = "2";
                    }
                   
                    if (item.import.EnrollmentDate.Trim().Length > 0)
                    {
                        staff.EnrollmentDate = DateTime.ParseExact(item.import.EnrollmentDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                    }

                    staff.UnitCD = item.import.SosikiCD;
                    staff.Yakusyoku = item.import.Yakusyoku;
                    if (item.import.Duty != "空白")
                    {
                        staff.Duty = item.import.Duty;
                    }

                    staff.UpdateTime = DateTime.Now;
                    staff.UpdateUserID = (Session["user"] as UserSession).StaffCD;
                    newdata.Add(staff);

                    S_Unit unit = db.Units.Find(staff.UnitCD);

                    if(null != unit)
                    {
                        M_User user = new M_User();
                        user.UserID = staff.StaffCD;
                        user.Password = staff.StaffCD;
                        user.StaffCD = staff.StaffCD;
                        if (unit.IsShop)
                        {
                            user.RoleCD = "01";
                        }
                        else if (unit.IsCusCenter)
                        {
                            user.RoleCD = "02";
                        }
                        else
                        {
                            user.RoleCD = "03";
                        }
                        user.UpdateTime = DateTime.Now;
                        user.UpdateUserID = (Session["user"] as UserSession).StaffCD;
                        userdata.Add(user);
                    }

                    addList.Add(item.import);
                }
                db.Staffs.AddRange(newdata);
                db.Users.AddRange(userdata);
                #endregion

                if (path == null || errorList.Count > 0)
                {
                    return ImportIndex<StaffData>(importFileName, "StaffDataImport", errorList, list, addList, updateList, deleteList, new { path = filePath });
                }
                else
                {
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex.ToString());
                        throw;
                    }

                    ViewBag.msg = "ファイルが正常にアップロードされました。";
                }
            }
            return View();
        }

        private bool checkStaffUpdate(StaffData s_import, M_Staff staff)
        {
            //where (IsDateDiff(s_import.EnrollmentDate, staff.EnrollmentDate) 
            //      || ((staff.Sex == "1" && s_import.Sex == "男") == false && (staff.Sex == "2" && s_import.Sex == "女") == false)
            //      || staff.StaffKana != s_import.StaffKana
            //      || staff.StaffName != s_import.StaffName
            //      || (staff.Duty != s_import.Duty && !((staff.Duty == null || staff.Duty == "") && (s_import.Duty == null || s_import.Duty == "")))
            //      || staff.Yakusyoku != s_import.Yakusyoku
            //      || staff.UnitCD != s_import.SosikiCD)

            string importStr = CommonUtils.CDateStr(s_import.EnrollmentDate) + "," + s_import.Sex + "," + s_import.StaffKana + "," + s_import.StaffName + "," + s_import.Duty + "," + s_import.Yakusyoku + "," + s_import.SosikiCD + "," +  s_import.Email;
            string EnrollmentDate = staff.EnrollmentDate == null ? "" : staff.EnrollmentDate.Value.ToString("yyyyMMdd");
            string Sex = staff.Sex == "1" ? "男" : "女";
            string email = string.IsNullOrEmpty(staff.Email) ? "" : staff.Email;
            string dataStr = EnrollmentDate + "," + Sex + "," + staff.StaffKana + "," + staff.StaffName + "," + staff.Duty + "," + staff.Yakusyoku + "," + staff.UnitCD + "," + email;

            if (importStr == dataStr)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool IsDateDiff(string p, DateTime? nullable)
        {
            if (string.IsNullOrEmpty(p) && nullable == null)
            {
                return false;
            }

            if (string.IsNullOrEmpty(p) || nullable == null)
            {
                return true;
            }


            return (DateTime.ParseExact(p, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture).CompareTo(nullable) != 0);
        }

        [WebAuthorizeAttribute(Roles = "02,04,09")]
        public ActionResult PartyDataImport(HttpPostedFileBase datafile, string PartyMonths, string UpdateType,string path)
        {
            if (PartyMonths == null)
            {
                DateTime today = DateTime.Now;
                if (today.Day > 20)
                {
                    today = today.AddMonths(1);
                }

                ViewBag.PartyDateFrom = today.ToString("yyyy/MM");
            }

            if ((datafile != null && datafile.ContentLength > 0) || path != null)
            {
                String filePath = "";
                string importFileName = "";

                if (path == null)
                {
                    string basePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\UPLOAD\\";
                    String date = string.Format("{0:yyyyMMddHHmmssfff}", DateTime.Now);
                    String fileName = string.Format("挙式インポート_{0}.csv", date);
                    filePath = basePath + fileName;
                    importFileName = datafile.FileName;

                    if (!Directory.Exists(basePath))
                    {
                        Directory.CreateDirectory(basePath);
                    }

                    datafile.SaveAs(filePath);
                }
                else
                {
                    filePath = path; 
                }
               

                List<ImportErrorViewModel> errorList = new List<ImportErrorViewModel>();
                List<PartyData> addList = new List<PartyData>();
                List<PartyData> updateList = new List<PartyData>();
                List<PartyData> deleteList = new List<PartyData>();


                List<T_Party> partys1 = db.Partys.ToList();
                //2014．10．03　李梁　当月挙式データを取る
                var partys = partys1.Where(p => p.PartyDate.ToString("yyyy/MM").Equals(PartyMonths));
                //2014．10．03　李梁
                List<M_Shop> shops = db.Shops.ToList();
                List<M_Staff> staffs = db.Staffs.ToList();
                List<T_EvaByStaff> staffEvas = db.EvaByStaffs.ToList();
                List<T_EvaByLeader> leaderEvas = db.EvaByLeaders.ToList();

                List<ICSVChecker<PartyData>> checkers = new List<ICSVChecker<PartyData>>();

                if(path == null)
                {
                    string checkMonth;

                    if (PartyMonths != null && PartyMonths.Length > 0)
                    {
                        checkMonth = PartyMonths;//.Substring(0, 5) + int.Parse(PartyMonths.Substring(5, 2));
                    }
                    else
                    {
                        checkMonth = DateTime.Now.AddMonths(1).ToString("yyyy/MM");
                    }

                    checkers.Add(new PartyMonthCheck(checkMonth));
                }

                checkers.Add(new PartyValueCheck(shops,staffs));

                List<PartyData> list = CsvUtils.CsvToModel<PartyData>(filePath, ref errorList, checkers);

                if (errorList.Count > 0)
                {
                    return ImportIndex<PartyData>(importFileName, "PartyDataImport", errorList, list, addList, updateList, deleteList, new { PartyMonths = PartyMonths, path = filePath });
                }

                foreach (PartyData item in list)
                {
                    item.PartyDate = CommonUtils.CDateStr(item.PartyDate);
                    item.TantoCD = CommonUtils.ParseZero(item.TantoCD, 5);
                    item.PartyNo = CommonUtils.ParseZero(item.PartyNo, 10);
                    if (item.StartTime !=null && item.StartTime.Length >= 5)
                    {
                        item.StartTime = item.StartTime.Substring(0, 5);
                    }
                }

                //List<PartyData> csvList = new List<PartyData>();

                //#region 年月指定

                string PartyMonthSub = PartyMonths.Replace("/", "");

                //if (PartyMonths != null && PartyMonths.Length > 0 && path == null)
                //{
                //    var monthQuery = from p in list
                //                     where p.PartyDate.Contains(PartyMonthSub)
                //                     select p;

                //    csvList = monthQuery.ToList();
                //}
                //else
                //{
                //    csvList = list;
                //}

                //#endregion

                //import件数ＤＢ半分以下の場合：追加する
                if (list.Count < partys.Count() / 2)//2014.10.03 李梁 当月データ件数判断
                {
                    UpdateType = "modify";
                }

                #region 変化有のデータを　更新する。
                var updateQuery = from party in partys

                                  join importParty in list on party.PartyNo equals importParty.PartyNo into g_import_party
                                  from s_import in g_import_party

                                  where (checkPartyUpdate(s_import, party))

                                  select new
                                  {
                                      party = party,
                                      importParty = s_import
                                  };

                List<M_User> users = db.Users.ToList();
                List<M_User> userdata = new List<M_User>();

                foreach (var item in updateQuery)
                {
                    T_Party party = item.party;

                    party.ShopCD = item.importParty.ShopCD;
                    party.PartyDate = DateTime.ParseExact(item.importParty.PartyDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                    party.TantoCD = item.importParty.TantoCD;
                    party.BrideName = item.importParty.BrideName;
                    party.BrideKana = item.importParty.BrideKana;
                    party.BrideHomeTel = item.importParty.BrideHomeTel;
                    party.BrideMobile = item.importParty.BrideMobile;
                    party.GroomName = item.importParty.GroomName;
                    party.GroomKana = item.importParty.GroomKana;
                    party.GroomHomeTel = item.importParty.GroomHomeTel;
                    party.GroomMobile = item.importParty.GroomMobile;
                    party.StartTime = item.importParty.StartTime;
                    party.HallType = item.importParty.HallType;

                    party.UpdateTime = DateTime.Now;
                    party.UpdateUserID = (Session["user"] as UserSession).StaffCD;
                    db.Entry(party).State = EntityState.Modified;


                    item.importParty.PartyDate = party.PartyDate.ToString("yyyy/MM/dd");

                    item.importParty.PartyNo = item.importParty.PartyNo.Replace("\"", "\\\"");
                    item.importParty.ShopCD = item.importParty.ShopCD.Replace("\"", "\\\"");
                    item.importParty.TantoCD = item.importParty.TantoCD.Replace("\"", "\\\"");
                    item.importParty.BrideHomeTel = item.importParty.BrideHomeTel.Replace("\"", "\\\"");
                    item.importParty.BrideMobile = item.importParty.BrideMobile.Replace("\"", "\\\"");
                    item.importParty.GroomHomeTel = item.importParty.GroomHomeTel.Replace("\"", "\\\"");
                    item.importParty.GroomMobile = item.importParty.GroomMobile.Replace("\"", "\\\"");
                    item.importParty.StartTime = item.importParty.StartTime.Replace("\"", "\\\"");
                    item.importParty.HallType = item.importParty.HallType.Replace("\"", "\\\"");
                    item.importParty.StaffName = item.importParty.StaffName.Replace("\"", "\\\"");
                    item.importParty.GroomName = item.importParty.GroomName.Replace("\"", "\\\"");
                    item.importParty.BrideKana = item.importParty.BrideKana.Replace("\"", "\\\"");
                    item.importParty.BrideName = item.importParty.BrideName.Replace("\"", "\\\"");
                    item.importParty.GroomKana = item.importParty.GroomKana.Replace("\"", "\\\"");

                    updateList.Add(item.importParty);
                }

                #endregion

                #region 　全件更新：評価無のデータ削除
                if (UpdateType == "all")
                {
                    var delQuery = from party in partys

                                   join staff in staffEvas on party.PartyID equals staff.PartyID into g_eva_staff
                                   from p_staff in g_eva_staff.DefaultIfEmpty()

                                   join leader in leaderEvas on party.PartyID equals leader.PartyID into g_eva_leader
                                   from p_leader in g_eva_leader.DefaultIfEmpty()

                                   join importParty in list on party.PartyNo equals importParty.PartyNo into g_import_party
                                   from s_import in g_import_party.DefaultIfEmpty()

                                   where p_staff == null && p_leader == null
                                         && party.PartyDate.ToString("yyyyMM") == PartyMonthSub && s_import == null
                                   select new
                                   {
                                       party = party,
                                       p_staff = p_staff,
                                       p_leader = p_leader
                                   };

                    foreach (var item in delQuery)
                    {
                        T_Party party = item.party;
                        //T_EvaByStaff staffEva = item.p_staff;
                        //T_EvaByLeader leaderEva = item.p_leader;

                        db.Entry(party).State = EntityState.Deleted;

                        //if (staffEva.PartyID != 0)
                        //{
                        //    db.Entry(staffEva).State = EntityState.Deleted;
                        //}

                        //if (leaderEva.PartyID != 0)
                        //{
                        //    db.Entry(leaderEva).State = EntityState.Deleted;
                        //}

                        PartyData importdata = new PartyData();

                        importdata.ShopCD = item.party.ShopCD;
                        importdata.PartyNo = item.party.PartyNo;
                        if(null != item.party.PartyDate)
                        {
                            importdata.PartyDate = item.party.PartyDate.ToString("yyyy/MM/dd");
                        }
                        importdata.TantoCD = item.party.TantoCD;
                        List<M_Staff> staffList = staffs.Where(s => s.StaffCD == item.party.TantoCD).ToList();
                        if (staffList.Count > 0)
                        {
                            importdata.StaffName = staffList[0].StaffName;
                        }
                        importdata.BrideName = item.party.BrideName;
                        importdata.BrideKana = item.party.BrideKana;
                        importdata.BrideHomeTel = item.party.BrideHomeTel;
                        importdata.BrideMobile = item.party.BrideMobile;
                        importdata.GroomName = item.party.GroomName;
                        importdata.GroomKana = item.party.GroomKana;
                        importdata.GroomHomeTel = item.party.GroomHomeTel;
                        importdata.GroomMobile = item.party.GroomMobile;
                        importdata.HallType = item.party.HallType;
                        importdata.StartTime = item.party.StartTime;

                        deleteList.Add(importdata);
                    }
                    if (delQuery.Count() > 0)
                    {
                        partys = db.Partys.ToList();
                        staffEvas = db.EvaByStaffs.ToList();
                        leaderEvas = db.EvaByLeaders.ToList();
                    }

                }
                #endregion

                #region 　データ追加

                var addQuery = from importParty in list

                               join party in partys on importParty.PartyNo equals party.PartyNo into g_party
                               from db_party in g_party.DefaultIfEmpty()

                               where db_party == null

                               select importParty;

                List<T_Party> newdata = new List<T_Party>();

                foreach (PartyData item in addQuery)
                {
                    T_Party party = new T_Party();
                    party.PartyNo = CommonUtils.ParseZero(item.PartyNo, 10);
                    party.PartyDate = DateTime.ParseExact(item.PartyDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture); 
                    party.Year = party.PartyDate.Year;
                    party.ShopCD = item.ShopCD;
                    party.TantoCD = item.TantoCD;
                    party.BrideName = item.BrideName;
                    party.BrideKana = item.BrideKana;
                    party.BrideHomeTel = item.BrideHomeTel;
                    party.BrideMobile = item.BrideMobile;
                    party.GroomName = item.GroomName;
                    party.GroomKana = item.GroomKana;
                    party.GroomHomeTel = item.GroomHomeTel;
                    party.GroomMobile = item.GroomMobile;
                    party.HallType = item.HallType;

                    if (item.StartTime !=null && item.StartTime.Length >= 5)
                    {
                        party.StartTime = item.StartTime.Substring(0, 5);
                    }

                    party.UpdateTime = DateTime.Now;
                    party.UpdateUserID = (Session["user"] as UserSession).StaffCD;
                    newdata.Add(party);

                    item.PartyDate = party.PartyDate.ToString("yyyy/MM/dd");

                    item.PartyNo = item.PartyNo.Replace("\"", "\\\"");
                    item.ShopCD = item.ShopCD.Replace("\"", "\\\"");
                    item.TantoCD = item.TantoCD.Replace("\"", "\\\"");
                    item.BrideHomeTel = item.BrideHomeTel.Replace("\"", "\\\"");
                    item.BrideMobile = item.BrideMobile.Replace("\"", "\\\"");
                    item.GroomHomeTel = item.GroomHomeTel.Replace("\"", "\\\"");
                    item.GroomMobile = item.GroomMobile.Replace("\"", "\\\"");
                    item.StartTime = item.StartTime.Replace("\"", "\\\"");
                    item.HallType = item.HallType.Replace("\"", "\\\"");
                    item.StaffName = item.StaffName.Replace("\"", "\\\"");
                    item.GroomName = item.GroomName.Replace("\"", "\\\"");
                    item.BrideKana = item.BrideKana.Replace("\"", "\\\"");
                    item.BrideName = item.BrideName.Replace("\"", "\\\"");
                    item.GroomKana = item.GroomKana.Replace("\"", "\\\"");

                    addList.Add(item);
                }

                db.Partys.AddRange(newdata);
                #endregion

                if (path == null || errorList.Count > 0)
                {
                    return ImportIndex<PartyData>(importFileName, "PartyDataImport", errorList, list, addList, updateList, deleteList, new { PartyMonths = PartyMonths,path = filePath,UpdateType = UpdateType });
                }
                else
                {
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex.ToString());
                        throw;
                    }


                    DateTime today = DateTime.Now;
                    if (today.Day > 20)
                    {
                        today = today.AddMonths(1);
                    }

                    ViewBag.PartyDateFrom = today.ToString("yyyy/MM");
                    ViewBag.msg = "ファイルが正常にアップロードされました。";
                }
            }
            return View();
        }

        private bool checkPartyUpdate(PartyData s_import, T_Party party)
        {
            string importStr = s_import.PartyNo + "," + s_import.ShopCD + "," + s_import.PartyDate + "," + s_import.TantoCD + "," +
                               s_import.BrideName + "," + s_import.GroomName + "," + s_import.BrideKana + "," + s_import.GroomKana + "," +
                               s_import.BrideHomeTel + "," + s_import.GroomHomeTel + "," + s_import.BrideMobile + "," + s_import.GroomMobile + "," +
                               s_import.StartTime + "," + s_import.HallType;

            string dataStr = party.PartyNo + "," + party.ShopCD + "," + party.PartyDate.ToString("yyyyMMdd") + "," + party.TantoCD + "," +
                               party.BrideName + "," + party.GroomName + "," + party.BrideKana + "," + party.GroomKana + "," +
                               party.BrideHomeTel + "," + party.GroomHomeTel + "," + party.BrideMobile + "," + party.GroomMobile + "," +
                               party.StartTime + "," + party.HallType;

            if (importStr == dataStr)
            {
                return false;
            }
            else
            {
                return true;
            }


            throw new NotImplementedException();
        }

        [WebAuthorizeAttribute(Roles = "02,04,09")]
        public ActionResult MasterManager(string divisionCD, string groupCD,string isPostBack, int? page)
        {
            return GetResultModels(divisionCD, groupCD,isPostBack,false, page);
        }
        /*LILIANG====2017/07/03=========================↑*/
        // GET: /Shop/
        public ActionResult Index()
        {
            return View(db.Shops.ToList());
        }

        // GET: /Shop/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            M_Shop shop = db.Shops.Find(id);
            if (shop == null)
            {
                return HttpNotFound();
            }
            return View(shop);
        }

        // GET: /Shop/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Shop/Create
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ShopCD,ShopName,Address,AreaCD,PlaceCD,DivCD")] M_Shop shop)
        {
            if (ModelState.IsValid)
            {
                db.Shops.Add(shop);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(shop);
        }

        // GET: /Shop/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            M_Shop shop = db.Shops.Find(id);
            if (shop == null)
            {
                return HttpNotFound();
            }
            return View(shop);
        }

        // POST: /Shop/Edit/5
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ShopCD,ShopName,Address,AreaCD,PlaceCD,DivCD")] M_Shop shop)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shop).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(shop);
        }

        // GET: /Shop/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            M_Shop shop = db.Shops.Find(id);
            if (shop == null)
            {
                return HttpNotFound();
            }
            return View(shop);
        }

        // POST: /Shop/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            M_Shop shop = db.Shops.Find(id);
            db.Shops.Remove(shop);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        /// <summary>
        /// 参照画面
        /// </summary>
        /// <returns></returns>
        public ActionResult Refer(string divisionCD, int? page)
        {
            return GetResultModels(divisionCD, null, "1", true, page);
        }

        /// <summary>
        /// 参照
        /// </summary>
        /// <returns></returns>
        public JsonResult ReferJson(string ShopCD) 
        {
            M_Shop shop = db.Shops.Find(ShopCD);
            if (null == shop)
            {
                shop = new M_Shop();
            }

            return Json(shop);
        }

        //店舗データ取得
        private ActionResult GetResultModels(string divisionCD, string groupCD,string isPostBack,bool isRefer, int? page)
        {
            var models = GetModels(divisionCD, groupCD, isRefer);
            if (isPostBack != null && isPostBack == "1")
            {
                ViewBag.isPostBack = "1";
                if (models.ToList().Count == 0)
                {
                    ViewBag.msg = "データがありません。";
                    ViewBag.msgType = "info";
                }
                GC.Collect();
                //int pageSize = 100;
                int pageNumber = (page ?? 1);
                //return View(models.ToPagedList(pageNumber, pageSize));
                return View(models);
            }
            else
            {
                ViewBag.isPostBack = "1";
                return View();
            }
        }

        //店舗全データ取得
        private IEnumerable<ShopViewModel> GetModels( string divisionCD, string groupCD, bool isRefer)
        {
            db.Database.CommandTimeout = 600000;

            ViewBag.divisionCD = divisionCD;
            ViewBag.groupCD = groupCD;

            var Shops = db.Shops.ToList();
            var Divisions = db.Divisions.ToList();
            var Groups = db.Groups.ToList();

            var models = from shop in Shops
                         join grp in Groups on shop.GroupCD equals grp.GroupCD into g_shop_grp
                         from s_grp in g_shop_grp.DefaultIfEmpty(new M_Group())
                         join divsion in Divisions on s_grp.DivCD equals divsion.DivCD into g_shop_div
                         from s_div in g_shop_div.DefaultIfEmpty(new M_Division())
                        
                         select new ShopViewModel
                         {
                             ShopType = shop.ShopType,
                             DivCD = s_div.DivCD,
                             DivName = s_div.DivName,
                             GroupCD = s_grp.GroupCD,
                             GroupName = s_grp.GroupName,
                             ShopCD = shop.ShopCD,
                             ShopName = shop.ShopName
                         };

            if (!string.IsNullOrEmpty(divisionCD))
            {
                if (divisionCD.StartsWith("g_"))
                {
                    divisionCD = divisionCD.Replace("g_", "");
                    models = models.Where(s => s.GroupCD == divisionCD);
                }
                else
                {
                    models = models.Where(s => s.DivCD == divisionCD);
                }
            }

            if (isRefer)
            {
                models = models.OrderBy(p => p.ShopCD);
            }
            else 
            {
                models = models.OrderBy(p => p.DivCD).ThenBy(p => p.GroupCD).ThenBy(p => p.ShopCD);
            }
            

            return models;
        }

        /// <summary>
        /// CSV出力
        /// </summary>
        /// <returns></returns>
        [WebAuthorizeAttribute(Roles = "02,04,09")]
        public ActionResult csv(string divisionCD, string groupCD)
        {
            List<ShopViewModel> models = GetModels(divisionCD, groupCD,false).ToList();

            if (models.Count == 0)
            {
                return Json(new { ResultType = EnumResultType.NoData, Message = "データがありません。", });
            }

            int index = 0;

            foreach (ShopViewModel item in models)
            {
                item.No = ++index;
            }

            string basePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

            String date = string.Format("{0:yyyyMMddHHmmssfff}", DateTime.Now);

            String fileName = string.Format("\\CSV\\店舗_{0}.csv", date);

            CsvUtils.ModlesToCsv<ShopViewModel>(basePath + fileName, models);
            GC.Collect();
            return Json(new { Path = string.Format("/CSV/店舗_{0}.csv", date), ResultType = EnumResultType.Success });
        }

        [WebAuthorizeAttribute(Roles = "02,04,09")]
        public ActionResult detailCsv()
        {
            List<ShopViewModel> models = GetModels(null, null,false).ToList();
            List<ShopDetailModel> detailModels = new List<ShopDetailModel>();

            List<S_Code> codes = db.Codes.ToList();

            foreach (ShopViewModel item in models)
            {
                ShopDetailModel detail = new ShopDetailModel();
                //detail.ShopType = item.ShopType;

                //if (null != item.ShopType)
                //{
                //    List<S_Code> codeList = codes.Where(c => c.CD == item.ShopType && c.Kind == "ShopType").ToList();
                //    if (codeList.Count > 0)
                //    {
                //        detail.ShopType = codeList[0].Name;
                //    }
                //}

                detail.DivName = item.DivName;
                detail.GroupName = item.GroupName;
                detail.ShopCD = item.ShopCD;
                detail.ShopName = item.ShopName;
                detailModels.Add(detail);
            }

            if (models.Count == 0)
            {
                return Json(new { ResultType = EnumResultType.NoData, Message = "データがありません。", });
            }

            string basePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

            String date = string.Format("{0:yyyyMMddHHmmssfff}", DateTime.Now);

            String fileName = string.Format("\\CSV\\店舗_{0}.csv", date);

            CsvUtils.ModlesToCsv<ShopDetailModel>(basePath + fileName, detailModels);
            GC.Collect();
            return Json(new { Path = string.Format("/CSV/店舗_{0}.csv", date), ResultType = EnumResultType.Success });
        }


    }

    /// <summary>
    ///店舗情報インポート チェック
    /// </summary>
    public class ShopValueCheck : WebEvaluation.Utils.ICSVChecker<ShopData> {

        private List<M_Group> Groups;

        public ShopValueCheck(List<M_Group> _groups)
        {
            Groups = _groups;
        }

        public void doCheck(ref List<ImportErrorViewModel> errorList, ShopData data,int rowNum)
        {
            //if (!string.IsNullOrEmpty(data.ShopType))
            //{
            //    if (data.ShopType != "HW" && data.ShopType != "ﾚｽﾄﾗﾝ" && data.ShopType != "ﾎﾃﾙ" && data.ShopType != "PD" && data.ShopType != "BW")
            //    {
            //        CommonUtils.AddError(ref errorList, new ImportErrorViewModel
            //        {
            //            ErrorField = "状態",
            //            ErrorType = "ShopTypeError",
            //            ErrorDetail = "店舗状態のデータは必ずHW/ﾚｽﾄﾗﾝ/ﾎﾃﾙ/PD/BW 中の一つに指定してください。",
            //            ErrorRow = rowNum
            //        });
            //    }
            //}
            //else
            //{
            //    if (data.ShopType != null)
            //    {
            //        CommonUtils.AddError(ref errorList, new ImportErrorViewModel
            //        {
            //            ErrorField = "状態",
            //            ErrorType = "ShopValueError",
            //            ErrorDetail = "「状態」列のデータが必ず入力してください。",
            //            ErrorRow = rowNum
            //        });
            //    }

            //}

            if (!string.IsNullOrEmpty(data.GroupCD))
            {
                List<M_Group> grps = Groups.Where(g => g.GroupName == data.GroupCD).ToList();
                if(grps.Count == 0)
                {
                    CommonUtils.AddError(ref errorList, new ImportErrorViewModel
                    {
                        ErrorField = "グループ",
                        ErrorType = "ShopGroupNotFindError",
                        ErrorDetail = "「グループ」列のデータ「" + data.GroupCD + "」はグループ表の中に存在しません。",
                        ErrorRow = rowNum
                    });
                }
            }
            else
            {
                if (data.GroupCD != null)
                {
                    CommonUtils.AddError(ref errorList, new ImportErrorViewModel
                    {
                        ErrorField = "グループ",
                        ErrorType = "ShopValueError",
                        ErrorDetail = "「グループ」列のデータが必ず入力してください。",
                        ErrorRow = rowNum
                    });
                }
            }

            if (string.IsNullOrEmpty(data.ShopCD))
            {
                CommonUtils.AddError(ref errorList, new ImportErrorViewModel
                {
                    ErrorField = "店舗略称",
                    ErrorType = "ShopValueError",
                    ErrorDetail = "「店舗略称」列のデータが必ず入力してください。",
                        ErrorRow = rowNum
                    });
                }

            if (string.IsNullOrEmpty(data.ShopName))
            {
                CommonUtils.AddError(ref errorList, new ImportErrorViewModel
                {
                    ErrorField = "店舗名称",
                    ErrorType = "ShopValueError",
                    ErrorDetail = "「店舗名称」列のデータが必ず入力してください。",
                    ErrorRow = rowNum
                });
            }

        }
    }

    /// <summary>
    /// 社員情報インポート チェック
    /// </summary>
    public class StaffValueCheck : WebEvaluation.Utils.ICSVChecker<StaffData>
    {
       // private EvaluationContext db = new EvaluationContext();

        private List<S_Unit> Units;

        public StaffValueCheck(List<S_Unit> _units)
        {
            Units = _units;
        }

        public void doCheck(ref List<ImportErrorViewModel> errorList, StaffData data,int rowNum)
        {
            if (string.IsNullOrEmpty(data.StaffCD))
            {
                CommonUtils.AddError(ref errorList, new ImportErrorViewModel
                {
                    ErrorField = "社員コード",
                    ErrorType = "StaffValueError",
                    ErrorDetail = "「社員コード」列のデータが必ず入力してください。",
                    ErrorRow = rowNum
                });
            }

            if (string.IsNullOrEmpty(data.StaffName))
            {
                CommonUtils.AddError(ref errorList, new ImportErrorViewModel
                {
                    ErrorField = "氏名漢字",
                    ErrorType = "StaffValueError",
                    ErrorDetail = "「氏名漢字」列のデータが必ず入力してください。",
                    ErrorRow = rowNum
                });
            }

            if (!string.IsNullOrEmpty(data.Sex) )
            {
                if (data.Sex != "男" && data.Sex != "女")
                {
                    CommonUtils.AddError(ref errorList, new ImportErrorViewModel
                    {
                        ErrorField = "性別",
                        ErrorType = "StaffSexError",
                        ErrorDetail = "「性別」列のデータは必ず男/女中の一つに指定してください。",
                        ErrorRow = rowNum
                    });
                }  
            }
            else
            {
                if (data.Sex != null)
                {
                    CommonUtils.AddError(ref errorList, new ImportErrorViewModel
                    {
                        ErrorField = "性別",
                        ErrorType = "StaffValueError",
                        ErrorDetail = "「性別」列のデータが必ず入力してください。",
                        ErrorRow = rowNum
                    });
                }
            }

            if (!string.IsNullOrEmpty(data.EnrollmentDate))
            {
                DateTime dateValue;
                if (!DateTime.TryParseExact(CommonUtils.CDateStr(data.EnrollmentDate), "yyyyMMdd",
                             new CultureInfo("ja-jp"),
                             DateTimeStyles.None,
                             out dateValue))
                {
                    CommonUtils.AddError(ref errorList, new ImportErrorViewModel
                    {
                        ErrorField = "入社年月日",
                        ErrorType = "StaffValueError",
                        ErrorDetail = "「入社年月日」列日付の格式が違う。",
                        ErrorRow = rowNum
                    });  
                } 
            }

            if (!string.IsNullOrEmpty(data.SosikiCD))
            {
                List<S_Unit> units = Units.Where(u => u.UnitCD == data.SosikiCD).ToList();

                if (units.Count == 0)
                {
                    CommonUtils.AddError(ref errorList, new ImportErrorViewModel
                    {
                        ErrorField = "現在組織コード",
                        ErrorType = "StaffUnitNotFindError",
                        ErrorDetail = "「現在組織コード」列のデータ「" + data.SosikiCD + "」は組織表の中に存在しません。",
                        ErrorRow = rowNum
                    });
                }
            }
            else
            {
                if (data.SosikiCD != null)
                {
                    CommonUtils.AddError(ref errorList, new ImportErrorViewModel
                    {
                        ErrorField = "現在組織コード",
                        ErrorType = "StaffValueError",
                        ErrorDetail = "「現在組織コード」列のデータが必ず入力してください。",
                        ErrorRow = rowNum
                    });
                }
            }
        }
    }

    /// <summary>
    /// パーティ情報インポート チェック
    /// </summary>
    public class PartyValueCheck : WebEvaluation.Utils.ICSVChecker<PartyData>
    {
       // private EvaluationContext db = new EvaluationContext();

        private List<M_Shop> Shops;
        private List<M_Staff> Staffs;

        public PartyValueCheck(List<M_Shop> _shops, List<M_Staff> _staffs)
        {
            Shops = _shops;
            Staffs = _staffs;
        }

        public void doCheck(ref List<ImportErrorViewModel> errorList, PartyData data,int rowNum)
        {
            if (!string.IsNullOrEmpty(data.ShopCD))
            {
                List<M_Shop> shops = Shops.Where(s => s.ShopCD == data.ShopCD).ToList();
                if (shops.Count ==0)
                {
                    CommonUtils.AddError(ref errorList, new ImportErrorViewModel
                    {
                        ErrorField = "店舗略称",
                        ErrorType = "PartyShopNotFindError",
                        ErrorDetail = "「店舗略称」列のデータ「" + data.ShopCD + "」は店舗表の中に存在しません。",
                        ErrorRow = rowNum
                    });
                }
            }
            else
            {
                if (null != data.ShopCD)
                {
                    CommonUtils.AddError(ref errorList, new ImportErrorViewModel
                    {
                        ErrorField = "店舗略称",
                        ErrorType = "PartyValueError",
                        ErrorDetail = "「店舗略称」列のデータが必ず入力してください。",
                        ErrorRow = rowNum
                    });
                }
            }

            if (!string.IsNullOrEmpty(data.PartyNo))
            {
                int intValue;
                if (!int.TryParse(data.PartyNo, out intValue))
                {
                    CommonUtils.AddError(ref errorList, new ImportErrorViewModel
                    {
                        ErrorField = "パーティID",
                        ErrorType = "PartyValueError",
                        ErrorDetail = "「パーティID」列のデータは必ず数値である。",
                        ErrorRow = rowNum
                    });
                }
            }

            if (!string.IsNullOrEmpty(data.PartyDate))
            {
                DateTime dateValue;
                if (!DateTime.TryParseExact(CommonUtils.CDateStr(data.PartyDate), "yyyyMMdd",
                             new CultureInfo("ja-jp"),
                             DateTimeStyles.None,
                             out dateValue))
                {
                    CommonUtils.AddError(ref errorList, new ImportErrorViewModel
                    {
                        ErrorField = "パーティ年月日",
                        ErrorType = "PartyFormatError",
                        ErrorDetail = "「パーティ年月日」列日付の格式が違う。",
                        ErrorRow = rowNum
                    });
                }
            }
            else
            {
                if (data.PartyDate != null)
                {
                    CommonUtils.AddError(ref errorList, new ImportErrorViewModel
                    {
                        ErrorField = "パーティ年月日",
                        ErrorType = "PartyValueError",
                        ErrorDetail = "「パーティ年月日」列のデータが必ず入力してください。",
                        ErrorRow = rowNum
                    });
                }
            }

            if (!string.IsNullOrEmpty(data.TantoCD))
            {
                data.TantoCD = CommonUtils.ParseZero(data.TantoCD, 5);
                List<M_Staff> staff = Staffs.Where(s => s.StaffCD == data.TantoCD).ToList();
                if (staff.Count == 0)
                {
                    CommonUtils.AddError(ref errorList, new ImportErrorViewModel
                    {
                        ErrorField = "担当者コード",
                        ErrorType = "PartyStaffNotFindError",
                        ErrorDetail = "「担当者コード」列のデータ「" + data.TantoCD + "」は社員表の中に存在しません。",
                        ErrorRow = rowNum
                    });
                }
            }
            else
            {
                if (data.TantoCD != null)
                {
                    CommonUtils.AddError(ref errorList, new ImportErrorViewModel
                    {
                        ErrorField = "担当者コード",
                        ErrorType = "PartyValueError",
                        ErrorDetail = "「担当者コード」列のデータが必ず入力してください。",
                        ErrorRow = rowNum
                    });
                }
            }

            if (string.IsNullOrEmpty(data.BrideName))
            {
                CommonUtils.AddError(ref errorList, new ImportErrorViewModel
                {
                    ErrorField = "新郎氏名",
                    ErrorType = "PartyValueError",
                    ErrorDetail = "「新郎氏名」列のデータが必ず入力してください。",
                    ErrorRow = rowNum
                });
            }

            if (string.IsNullOrEmpty(data.GroomName))
            {
                CommonUtils.AddError(ref errorList, new ImportErrorViewModel
                {
                    ErrorField = "新婦氏名",
                    ErrorType = "PartyValueError",
                    ErrorDetail = "「新婦氏名」列のデータが必ず入力してください。",
                    ErrorRow = rowNum
                });
            }


            if (!string.IsNullOrEmpty(data.StartTime))
            {
                DateTime timeValue;
                if (!DateTime.TryParseExact(data.StartTime, "HH:mm:ss",
                             new CultureInfo("ja-jp"),
                             DateTimeStyles.None,
                             out timeValue) && !DateTime.TryParseExact(data.StartTime, "HH:mm",
                             new CultureInfo("ja-jp"),
                             DateTimeStyles.None,
                             out timeValue))
                {
                    CommonUtils.AddError(ref errorList, new ImportErrorViewModel
                    {
                        ErrorField = "開始時間",
                        ErrorType = "PartyValueError",
                        ErrorDetail = "「開始時間」列時間の格式が違う。",
                        ErrorRow = rowNum
                    });
                }
            }
        }
    }

    /// <summary>
    /// パーティ情報インポート（年月指定）チェック
    /// </summary>
    public class PartyMonthCheck : WebEvaluation.Utils.ICSVChecker<PartyData>
    {
        string checkMonth;

        public PartyMonthCheck(string _checkMonth)
        {
            checkMonth = _checkMonth;
        }

        public void doCheck(ref List<ImportErrorViewModel> errorList, PartyData data,int rowNum)
        {
            string checkMonthSub = checkMonth.Substring(0, 5) + checkMonth.Substring(6, 1);
            if (data.PartyDate != null && !data.PartyDate.Contains(checkMonth) && !data.PartyDate.Contains(checkMonthSub))
            {

                CommonUtils.AddError(ref errorList, new ImportErrorViewModel
                {
                    ErrorField = "パーティ年月日",
                    ErrorType = "PartyDateError",
                    ErrorDetail = "指定年月「" + checkMonth + "」と挙式日が一緻していません。",
                    ErrorRow = rowNum
                });
            }
        }
    }
}
