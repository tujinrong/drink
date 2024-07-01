using DrinkService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DrinkService.Data.Logics;
using DrinkService.Data.Models;
using DrinkService.Data.ViewModels;
using DrinkService.Utils;
using PagedList;
using DrinkService.Filters;
using System.IO;
using System.Data.Entity.Migrations;
using System.Data.Entity;
using System.Web.Configuration;
using System.Reflection;
using System.Diagnostics;
using SafeNeeds.DySmat;
using SafeNeeds.DySmat.Util;
using SafeNeeds.DySmat.Logic;
using System.Data;
using System.Threading;
using System.Transactions;

namespace DrinkService.Controllers
{
    public class MainController : BaseController
    {
        private DrinkServiceContext db = new DrinkServiceContext();

        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult Home()
        {
            return View();
        }

        public JsonResult GetMsgNewMark()
        {
            Boolean NewMark = false;
            String BeginDate = "";

            TransactionOptions opt = new TransactionOptions();
            opt.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, opt))
            {
                DyEntityLogic logic = new DyEntityLogic();

                DynamicsViewRequest req = new DynamicsViewRequest();
                req.ProjID = 1;
                req.EntityName = "T_Message";
                req.ViewName = "お知らせメッセージ一覧画面";
                req.GetPageCount = false;

                req.FilterDic = new Dictionary<string, string>();

                req.FilterDic.Add("BeginDateReangeFilter", DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss"));
                req.FilterDic.Add("DraftFlgFilter", "1");


                PageViewResult list = logic.GetList(req);

                
                foreach (DataRow row in list.DataTable.Rows)
                {
                    if (BeginDate.Length == 0)
                    {
                        BeginDate = DataUtil.CDate(row["BeginDate"]).ToString("yyyy/MM/dd");
                    }
                    if (DataUtil.CStr(row["NewMark"]) == "1")
                    {
                        //NewMark = DataUtil.CStr(row["BeginDate"]).Replace(" 0:00:00", "");


                        NewMark = true;
                        break;
                    }
                }
            }
            return Json(new { NewMark = NewMark, BeginDate = BeginDate }, JsonRequestBehavior.AllowGet); ;
        }

        //static void test()
        // {
        //     Console.WriteLine("Starting...");
        //     for (int i = 1; i < 10; i++)
        //     {
        //         DyEntityLogic logic = new DyEntityLogic();

        //         EntityRequest enreq = new EntityRequest(1, "111", "");
        //         T_HoClientAdapter adapter = new T_HoClientAdapter(enreq);

        //         ShopLogic shopLogic = new ShopLogic(new EntityRequest(1, "111", ""));
        //         List<ShopListViewModel> data = shopLogic.GetShopList("", "", "");
        //         shopLogic.Dispose();
        //         foreach (ShopListViewModel m in data)
        //         {
        //             PageViewResult list = adapter.GetClientItemLimitList(m.ShopCD, DateTime.Now.ToString("yyyy/MM/dd"), true, false, 1);
        //         }
        //         adapter.Dispose();

        //     }
        // }

        public JsonResult GetItemLimit()
        {
            Boolean HasLimit = false;

            TransactionOptions opt = new TransactionOptions();
            opt.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, opt))
            {
                DyEntityLogic logic = new DyEntityLogic();

                EntityRequest enreq = new EntityRequest(1, loginUser.StaffName, "");
                T_HoClientAdapter adapter = new T_HoClientAdapter(enreq);

                PageViewResult list = adapter.GetClientItemLimitList(loginUser.ShopCD, DateTime.Now.ToString("yyyy/MM/dd"), true, false, 1);


                ////
                //for (int i = 0; i < 10; i++)
                //{
                //    Thread t = new Thread(test);
                //    t.Start();
                //}

                if (list.DataTable.Rows.Count > 0 && DataUtil.CInt(list.DataTable.Rows[0][0]) > 0)
                {
                    HasLimit = true;
                }
                adapter.Dispose();
            }
            return Json(new { HasLimit = HasLimit }, JsonRequestBehavior.AllowGet); ;
        }

        [WebAuthorizeAttribute(Roles = "1,2,3,4,5,6")]
        public ActionResult PDFView(string Path,string PDF)
        {
            if (string.IsNullOrEmpty(Path))
            {
                ViewBag.PDFPath = "";
            }
            else {
                ViewBag.PDFPath = System.Web.HttpUtility.UrlDecode(Path);
            }

            if (string.IsNullOrEmpty(PDF))
            {
                ViewBag.PDF = "";
            }
            else
            {
                ViewBag.PDF = System.Web.HttpUtility.UrlDecode(PDF);
            }


            return View();
        }

        public ActionResult Login()
        {
            ViewBag.ver = GetVersion();
            return View();
        }

        public ActionResult BusinessAnalysis()
        {
            return View();
        }

        public ActionResult Upload(String callBack,String resourceType)
        {
            ViewBag.callBack = callBack;
            ViewBag.resourceType = resourceType;
            return View();
        }
        

        [HttpPost]
        public ActionResult Login(string shopCD, string userName, string passWord)
        {
            //==============================temp====================================
            //DyEntityLogic logic = new DyEntityLogic();

            //DynamicsSaveRequest flowStepStaffReq = new DynamicsSaveRequest();
            //flowStepStaffReq.ProjID = 1;
            //flowStepStaffReq.EntityName = "T_Period";
            //flowStepStaffReq.SaveData = new List<Dictionary<string, object>>();

            //DateTime start = new DateTime(2013, 3, 31);

            ////DateTime currentDate = new DateTime(2013, 3, 31);
            ////DateTime endDate = new DateTime(2014, 3, 29);

            ////DateTime currentDate = new DateTime(2014, 3, 30);
            ////DateTime endDate = new DateTime(2017, 3, 29);

            ////DateTime currentDate = new DateTime(2017, 3, 30);
            ////DateTime endDate = new DateTime(2020, 3, 29);

            ////DateTime currentDate = new DateTime(2020, 3, 30);
            ////DateTime endDate = new DateTime(2023, 3, 29);


            //DateTime currentDate = new DateTime(2023, 3, 30);
            //DateTime endDate = new DateTime(2026, 3, 29);

            //while (DateTime.Compare(currentDate, endDate) <= 0)
            //{

            //    Dictionary<string, object> staffDataItem = new Dictionary<string, object>();

            //    int d = (int)currentDate.Subtract(start).TotalDays;

            //    int year = d / 364 + start.Year;

            //    int month = ((d % 364) / 28) + 1;

            //    int week = (d % 28) / 7 + 1;

            //    string weekStr = "";
            //    if (week == 1)
            //    {
            //        weekStr = "A";
            //    }
            //    else if (week == 2)
            //    {
            //        weekStr = "B";

            //    }
            //    else if (week == 3)
            //    {
            //        weekStr = "C";
            //    }
            //    else if (week == 4)
            //    {
            //        weekStr = "D";
            //    }


            //    string weekDayStr = "";
            //    if (currentDate.DayOfWeek == DayOfWeek.Monday)
            //    {
            //        weekDayStr = "2";
            //    }
            //    else if (currentDate.DayOfWeek == DayOfWeek.Tuesday)
            //    {
            //        weekDayStr = "3";

            //    }
            //    else if (currentDate.DayOfWeek == DayOfWeek.Wednesday)
            //    {
            //        weekDayStr = "4";

            //    }
            //    else if (currentDate.DayOfWeek == DayOfWeek.Thursday)
            //    {
            //        weekDayStr = "5";

            //    }
            //    else if (currentDate.DayOfWeek == DayOfWeek.Friday)
            //    {
            //        weekDayStr = "6";

            //    }
            //    else if (currentDate.DayOfWeek == DayOfWeek.Saturday)
            //    {
            //        weekDayStr = "7";

            //    }
            //    else if (currentDate.DayOfWeek == DayOfWeek.Sunday)
            //    {
            //        weekDayStr = "1";

            //    }

            //    staffDataItem.Add("DyTableName", "T_Period");
            //    staffDataItem.Add("PeriodDate", currentDate);
            //    staffDataItem.Add("PeriodDayOnly", currentDate.Day);
            //    staffDataItem.Add("PeriodMonthOnly", currentDate.Month);
            //    staffDataItem.Add("PeriodMonth", DataUtil.CStr(currentDate.Year) + DataUtil.CStr(currentDate.Month).PadLeft(2, '0'));
            //    staffDataItem.Add("PeriodYear", DataUtil.CStr(currentDate.Year));
            //    staffDataItem.Add("PeriodCycleYear", DataUtil.CStr(year));
            //    staffDataItem.Add("PeriodCycle", month);
            //    staffDataItem.Add("PeriodCycleWeek", weekStr);
            //    staffDataItem.Add("PeriodCycleWeekDay", weekDayStr);
            //    flowStepStaffReq.SaveData.Add(staffDataItem);

            //    currentDate = currentDate.AddDays(1);
            //}

            //logic.Save(flowStepStaffReq);
            //==============================temp====================================




            StaffLogic staffLogic = new StaffLogic(new EntityRequest(1, "", ""));
            ShopLogic shopLogic = new ShopLogic(new EntityRequest(1, "", ""));
            M_Staff staff = staffLogic.GetStaffByKey(shopCD, userName);
            if (staff != null && staff.Password == passWord)
            {
                DateTime timeNow = CommonUtils.GetDateTimeNow();
                DateTime lockTimeS = DateTime.Today;
                DateTime lockTimeE = DateTime.Today.AddHours(5);

                if (timeNow >= lockTimeS && timeNow <= lockTimeE && staff.RoleCD != ModelBase.CN役割_システム管理者)
                {
                    ViewBag.msg = "「0時〜5時」はシステム保守時間であり、ご利用できません。";
                    ViewBag.msgType = "error";

                    ViewBag.ver = GetVersion();

                    return View();
                }

                if (staff.RoleCD != ModelBase.CN役割_システム管理者)
                {
                    M_Code code = db.Codes.Find("SystemSet", "NoLogin");
                    if (code != null && code.RefCD == "1") {
                        ViewBag.msg = "現在、システム保守作業中です。　　ご利用できません。";
                        ViewBag.msgType = "error";

                        ViewBag.ver = GetVersion();

                        return View();
                    }
                }

                UserSession user = new UserSession();
                user.ShopCD = staff.ShopCD;
                M_Shop shop = shopLogic.GetShopByShopCD(user.ShopCD);
                if (shop != null)
                {
                    user.ShopTypeCD = shop.ShopTypeCD;
                    user.ShopName = shop.ShopName;
                }

                user.StaffCD = staff.StaffCD;
                user.StaffName = staff.StaffName;
                user.RoleCD = staff.RoleCD;

                Dictionary<string, object> keys = (HttpContext.Application["user_session_keys"] as Dictionary<string, object>);

                //if (keys.ContainsKey(user.ShopCD + "_" + user.StaffCD))
                //{
                //    ViewBag.ver = typeof(MainController).Assembly.GetName().Version.ToString();
                //    ViewBag.msg = "店舗【" + user.ShopCD + "】の担当者【" + user.StaffCD + "】は既にログイン済、再ログインできません。";
                //    ViewBag.msgType = "error";
                //    return View();
                //}
                //else
                //{
                //    keys.Add(user.ShopCD + "_" + user.StaffCD, user);
                //}

                Session["user"] = user;
                ViewBag.LoginUser = user;

                staffLogic.Dispose();
                shopLogic.Dispose();
                return Redirect("Home");
            }
            else
            {
                ViewBag.msg = "ログイン名またはパスワードが正しくありません。";
                ViewBag.msgType = "error";

                ViewBag.ver = GetVersion();

                staffLogic.Dispose();
                shopLogic.Dispose();
                return View();
            }
        }

        public string GetVersion()
        {
            //FileVersionInfo fv = System.Diagnostics.FileVersionInfo.GetVersionInfo
            //                               (Assembly.GetExecutingAssembly().Location); 
            //string ver = fv.FileVersion;

            Assembly thisAssem = typeof(MainController).Assembly;
            AssemblyName thisAssemName = thisAssem.GetName();

            Version version = thisAssemName.Version;
            string ver = version.ToString();
            return ver;
        }

        public ActionResult Logout()
        {
            UserSession user = Session["user"] as UserSession;
            Dictionary<string, object> keys = (HttpContext.Application["user_session_keys"] as Dictionary<string, object>);
            if (keys.ContainsKey(user.ShopCD + "_" + user.StaffCD))
            {
                keys.Remove(user.ShopCD + "_" + user.StaffCD);
            }

            Session["user"] = null;
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            return RedirectToAction("Login", "Main");
        }

        [WebAuthorizeAttribute(Roles = "1,2,3,4,5,6")]
        public ActionResult PasswordChange()
        {
            return View();
        }
        [WebAuthorizeAttribute(Roles = "1,2,3,4,5,6")]
        public JsonResult SavePassword(string oldPwd,string newPwd)
        {
            StaffLogic staffLogic = new StaffLogic(new EntityRequest(1, loginUser.StaffName , ""));
            string userID = loginUser.StaffCD;
            string shop = loginUser.ShopCD;

            Result result = new Result();
            M_Staff staff = staffLogic.GetStaffByKey(shop, userID);

            if (oldPwd == staff.Password)
            {
                if (newPwd == staff.Password)
                {
                    result.ReturnValue = EnumResult.Error;
                    result.Message = "新しいパスワードは、現在のパスワードを一致させることはできません。";
                    result.ErrorKey = "newPwd";
                }
                else if (newPwd == staff.OldPassword1 || newPwd == staff.OldPassword2)
                {
                    result.ReturnValue = EnumResult.Error;
                    result.Message = "新しいパスワードは二回前のパスワードと一致することはできません。";
                    result.ErrorKey = "newPwd";
                }
                else
                {
                    staff.OldPassword2 = staff.OldPassword1;
                    staff.OldPassword1 = staff.Password;
                    staff.Password = newPwd;
                    staffLogic.Save(staff,loginUser.StaffName);
                    result.ReturnValue = EnumResult.OK;
                    result.Message = "パスワードに変更されました。";
                }
            }
            else
            {
                result.ReturnValue = EnumResult.Error;
                result.Message = "新しいパスワードと古いパスワードが一致しません。";
                result.ErrorKey = "oldPwd";
            }
            staffLogic.Dispose();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaleDataList()
        {
            return View();
        }

        [WebAuthorizeAttribute(Roles = "1,2,3,4,5,6")]
        public JsonResult MainSaleDataSearch(string ShopCD, string handingFlag, string StartDate, string EndDate, string pageNumber)
        {
            PagedResult data = null;
            TransactionOptions opt = new TransactionOptions();
            opt.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, opt))
            {
                SaleDataLogic saleDataLogic = new SaleDataLogic(new EntityRequest(1, loginUser.StaffName, ""));
                data = saleDataLogic.GetSaleDataList(ShopCD, handingFlag, StartDate, EndDate, pageNumber);
                saleDataLogic.Dispose();
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        [WebAuthorizeAttribute(Roles = "1,2,3,4,5,6")]
        public ActionResult ZipcodeImport()
        {
            return View();
        }

        [WebAuthorizeAttribute(Roles = "1,2,3,4,5,6")]
        public ActionResult ZipcodeSave(HttpPostedFileBase datafile)
        {
            

            try
            {
                String filePath = "";
                string importFileName = "";

                string errorMsg = "";

                if (datafile == null || datafile.ContentLength < 10240000) {
                    errorMsg = "ファイルサイズは１０M以下。";
                    ViewBag.msg = errorMsg;
                    ViewBag.msgType = "error";
                    return View("ZipcodeImport");
                }

                if ((datafile != null && datafile.ContentLength > 0))
                {
                    string basePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\UPLOAD\\";
                    String date = string.Format("{0:yyyyMMddHHmmssfff}", DateTime.Now);
                    String fileName = string.Format("KEN_ALL_{0}.csv", date);
                    filePath = basePath + fileName;
                    importFileName = datafile.FileName;

                    if (!Directory.Exists(basePath))
                    {
                        Directory.CreateDirectory(basePath);
                    }

                    datafile.SaveAs(filePath);
                }

                List<M_PostCode> data = db.PostCodes.ToList();

                List<ImportErrorViewModel> errorList = new List<ImportErrorViewModel>();
                List<ICSVChecker<PostImportModel>> checkers = new List<ICSVChecker<PostImportModel>>();

                List<PostImportModel> list = CsvUtils.CsvToModel<PostImportModel>(filePath, ref errorList, checkers, false);

                foreach (PostImportModel p in list) {
                    p.PostCD = p.PostCD.Replace("\"", "");
                    p.Adress1 = p.Adress1.Replace("\"", "");
                    p.Adress2 = p.Adress2.Replace("\"", "");
                    p.Adress3 = p.Adress3.Replace("\"", "");

                    if (p.PostCD.Length != 7)
                    {
                        errorMsg = "郵便番号桁数エラー";
                        ViewBag.msg = errorMsg;
                        ViewBag.msgType = "error";
                        return View("ZipcodeImport");
                    }
                    if (DataUtil.CDec(p.PostCD) <= 0) {
                        errorMsg = "郵便番号数字エラー";

                        ViewBag.msg = errorMsg;
                        ViewBag.msgType = "error";
                        return View("ZipcodeImport");
                    }

                    p.PostCD = p.PostCD.Substring(0, 3) + "-" + p.PostCD.Substring(3, 4);
                }

                var gList =  from c in list

                             group c by c.PostCD into g

                             select new M_PostCode
                             {
                                 PostCD = g.First().PostCD,
                                 Adress = g.First().Adress1  + g.First().Adress2 + g.First().Adress3
                             };

                List<M_PostCode> csvCodes = gList.ToList();

                var addQuery = from impotPost in csvCodes

                               join postCD in data on impotPost.PostCD equals postCD.PostCD into i_post
                               from post_CD in i_post.DefaultIfEmpty(null)

                               where post_CD == null

                               group impotPost by impotPost.PostCD into g

                               select new M_PostCode
                               {
                                   PostCD = g.First().PostCD,
                                   Adress = g.First().Adress
                               };

                M_PostCode[] addCodes = addQuery.ToArray();
                db.PostCodes.AddRange(addCodes);

                var updateQuery = from impotPost in csvCodes

                                  join postCD in data on impotPost.PostCD equals postCD.PostCD into i_post
                                  from post_CD in i_post.DefaultIfEmpty(null)

                                  where post_CD != null && (impotPost.Adress != post_CD.Adress)

                                  select new
                                  {
                                      impotPost = impotPost,
                                      post_CD = post_CD
                                  };

                foreach (var impotPost in updateQuery)
                {
                    M_PostCode pos = impotPost.post_CD;
                    pos.Adress = impotPost.impotPost.Adress;
                    db.Entry(pos).State = EntityState.Modified;
                }

                db.SaveChanges();

                //string basePath2 = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

                //String date2 = string.Format("{0:yyyyMMddHHmmssfff}", CommonUtils.GetDateTimeNow());

                //String fileName2 = string.Format("\\SQL\\SQL_{0}.csv", date2);

                //CsvUtils.ModlesToSql<M_PostCode>(basePath2 + fileName2, csvCodes, "M_PostCode");

                ViewBag.msg = "取込完了。";
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                ViewBag.msg = "取込失敗、ご確認ください。";
                ViewBag.msgType = "error";
            }


            return View("ZipcodeImport");
        }

        [WebAuthorizeAttribute(Roles = "1,2,3,4,5,6")]
        public JsonResult GetZipByCode(string PostCD)
        {
            M_PostCode code = db.PostCodes.Find(PostCD);


            //return Json(new { pageSize = 10, totalSize = tsize, pageNumber = pNumber, pageData = codes }, JsonRequestBehavior.AllowGet);
            return Json(new { PostInfo = code }, JsonRequestBehavior.AllowGet); ;
        }

        [WebAuthorizeAttribute(Roles = "4")]
        public ActionResult ShopEdit(string shopCD)
        {
            M_Shop shop = new M_Shop();
            TransactionOptions opt = new TransactionOptions();
            opt.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, opt))
            {
                ShopLogic shopLogic = new ShopLogic(new EntityRequest(1, loginUser.StaffName, ""));
                
                if (!string.IsNullOrEmpty(shopCD))
                {
                    shop = shopLogic.GetShopByShopCD(shopCD);
                }
                shopLogic.Dispose();
            }
            return View(shop);
        }
    }
}