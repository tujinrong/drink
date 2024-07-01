using WebEvaluation.DAL;
using WebEvaluation.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using WebEvaluation.Controllers.Filters;
using WebEvaluation.Models;
using WebEvaluation.DataModels;
using System.Data.Entity;
using System.Text.RegularExpressions;

namespace WebEvaluation.Controllers
{
    public class HomeController : Controller
    {
        private EvaluationContext db = new EvaluationContext();

        [AuthenticationFilter(Order = 1)]
        [ExceptionFilter(Order = 2)]
        public ActionResult Index()
        {
            UserSession _user = Session["user"] as UserSession;

            HomeViewModel model = new HomeViewModel();
            model.StaffName = _user.StaffName;
            model.UnitName = _user.UnitName;

            
            List<M_Message> msgs = db.Messages.ToList();

            if (msgs.Count > 0) {
                M_Message msg = msgs.Last();

                model.MessageID = msg.MessageID;
                model.Message = msg.Message;
                if (msg.UpdateUserID != null) 
                {
                    M_Staff staff = db.Staffs.Find(msg.UpdateUserID);
                    if (staff != null) {
                        model.UpdateUser = staff.StaffName;
                    }
                }

                if (msg.UpdateTime != null) 
                {
                    model.UpdateTime = msg.UpdateTime.Value.ToString("MM/dd HH:mm");
                }
            }


            return View(model);
        }

        
        public ActionResult Main()
        {
            UserSession _user = new UserSession(); ;

            HomeViewModel model = new HomeViewModel();
            Session["user"] = _user;


            List<M_Message> msgs = db.Messages.ToList();

            if (msgs.Count > 0)
            {
                M_Message msg = msgs.Last();

                model.MessageID = msg.MessageID;
                model.Message = msg.Message;
                if (msg.UpdateUserID != null)
                {
                    M_Staff staff = db.Staffs.Find(msg.UpdateUserID);
                    if (staff != null)
                    {
                        model.UpdateUser = staff.StaffName;
                    }
                }

                if (msg.UpdateTime != null)
                {
                    model.UpdateTime = msg.UpdateTime.Value.ToString("MM/dd HH:mm");
                }
            }


            return View(model);
        }

        [HttpGet]
        public ActionResult Login()
        {
            string dbConnectionString = ConfigurationManager.ConnectionStrings["EvaluationContext"].ToString();
            Regex regex = new Regex("Initial\\s{1,}Catalog\\s{0,}=\\s{0,}TG\\s{0,};", RegexOptions.IgnoreCase);
            if (regex.IsMatch(dbConnectionString))
            {
                ViewBag.isTestVersion = "false";
            }
            else
            {
                ViewBag.isTestVersion = "true";
            }
            return View();
        }

        [HttpPost]
        [ExceptionFilter(Order = 2)]
        public ActionResult Login(LoginViewModel loginUser)
        {
            List<M_User> list = (from u in db.Users where u.StaffCD == loginUser.LoginName && u.Password == loginUser.PassWord select u).ToList() as List<Models.M_User>;
            if (null != list && list.Count > 0)
            {

                M_User _user = list[0];
                //01:店舗  02:カスタマセンター 03:閲覧 04:カスタマセンター上長 09:システム管理者
                if (_user.RoleCD == "01" 
                    || _user.RoleCD == "02" 
                    || _user.RoleCD == "03" 
                    || _user.RoleCD == "04"
                    || _user.RoleCD == "05" //20151025 「業務管理者」権限を追加。
                    || _user.RoleCD == "09")
                {
                    UserSession user = new UserSession();
                    user.UserID = _user.UserID;
                    user.StaffCD = _user.StaffCD;
                    user.RoleCD = _user.RoleCD;

                    M_Staff staff = db.Staffs.Find(_user.StaffCD);
                    if (null != staff)
                    {
                        user.StaffName = staff.StaffName;
                        S_Unit unit = db.Units.Find(staff.UnitCD);
                        if (null != unit)
                        {
                            user.UnitCD = unit.UnitCD;
                            user.UnitName = unit.UnitName;
                            if (unit.IsShop)
                            {
                                M_Shop shop = db.Shops.Find(unit.ShopCD);
                                if (null != shop)
                                {
                                    user.ShopCD = shop.ShopCD;
                                    user.ShopName = shop.ShopName;
                                }
                            }
                        }
                    }

                    //-----------
                    //List<T_EvaByStaff> evas = db.EvaByStaffs.ToList();
                    //foreach (T_EvaByStaff _evaByStaffData in evas)
                    //{
                    //    if (!string.IsNullOrEmpty(_evaByStaffData.Eva1Result) || !string.IsNullOrEmpty(_evaByStaffData.Eva2Result) || !string.IsNullOrEmpty(_evaByStaffData.Eva3Result))
                    //    {
                    //        if (_evaByStaffData.Eva1Result == "0" || _evaByStaffData.Eva2Result == "0" || _evaByStaffData.Eva3Result == "0")
                    //        {
                    //            _evaByStaffData.EvaResultFlag = "0";
                    //        }
                    //        else
                    //        {
                    //            _evaByStaffData.EvaResultFlag = "1";
                    //        }
                    //    }
                    //    else
                    //    {
                    //        _evaByStaffData.EvaResultFlag = null;
                    //    }
                    //    db.Entry(_evaByStaffData).State = EntityState.Modified;

                    //}
                    //try
                    //{
                    //    db.SaveChanges();
                    //}
                    //catch (Exception ex)
                    //{
                    //    Console.Write(ex.ToString());
                    //    throw;
                    //}
                    //-----------
                    

                    Session["user"] = user;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.msg = "ログイン権限がありません。";
                    ViewBag.msgType = "error";
                    return View();
                }
            }
            else
            {
                ViewBag.msg = "ログイン名またはパスワードが正しくありません。";
                ViewBag.msgType = "error";
            }

            string dbConnectionString = ConfigurationManager.ConnectionStrings["EvaluationContext"].ToString();
            Regex regex = new Regex("Initial\\s{1,}Catalog\\s{0,}=\\s{0,}TG\\s{0,};", RegexOptions.IgnoreCase);
            if (regex.IsMatch(dbConnectionString))
            {
                ViewBag.isTestVersion = "false";
            }
            else
            {
                ViewBag.isTestVersion = "true";
            }

            return View();
        }

        public JsonResult KeepSession()
        {
            return Json("");
        }

        public ActionResult Logout()
        {
            Session["user"] = null;
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            return RedirectToAction("Login", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult Download(string path)
        {
            ViewBag.path = path;

            return View();
        }

        [AuthenticationFilter(Order = 1)]
        [ExceptionFilter(Order = 2)]
        public ActionResult BusinessAnalysis()
        {
            ViewBag.entityName = "Y_EntityForm";
            ViewBag.pageName = "BusinessSearchFormList";
            return View();
        }

        public ActionResult UpdateMsg(HomeViewModel msg)
        {
            M_Message message = null;

            UserSession _user = Session["user"] as UserSession;

            if (msg.MessageID > 0) {
                message = db.Messages.Find(msg.MessageID);
            }

            DateTime date = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now, TimeZoneInfo.Local);
            date = TimeZoneInfo.ConvertTimeFromUtc(date, TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time"));

            if (message == null)
            {
                message = new M_Message();
                message.Message = msg.Message;
                message.UpdateTime = date;
                message.UpdateUserID = _user.StaffCD;

                db.Messages.Add(message);
            }
            else 
            {
                message.Message = msg.Message;
                message.UpdateTime = date;
                message.UpdateUserID = _user.StaffCD;

                db.Entry(message).State = EntityState.Modified;
            }

            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                throw;
            }

            return Json(new { Message = "保存完了しました。", MsgId = message.MessageID, UpdateTime = date.ToString("MM/dd HH:mm"), UpdateUser = _user.StaffName });
        }

        [AuthenticationFilter(Order = 1)]
        [ExceptionFilter(Order = 2)]
        public ActionResult DivList()
        {
            ViewBag.entityName = "M_Division";
            ViewBag.pageName = "事業部マスタ一覧画面";
            return View();
        }

        [AuthenticationFilter(Order = 1)]
        [ExceptionFilter(Order = 2)]
        public ActionResult GroupList()
        {
            ViewBag.entityName = "M_Group";
            ViewBag.pageName = "グループマスタ一覧画面";
            return View();
        }

        [AuthenticationFilter(Order = 1)]
        [ExceptionFilter(Order = 2)]
        public ActionResult PartTimeStaffList()
        {
            ViewBag.entityName = "M_PartTimeStaff";
            ViewBag.pageName = "アルバイトマスタ一覧画面";
            return View();
        }

        [AuthenticationFilter(Order = 1)]
        [ExceptionFilter(Order = 2)]
        public ActionResult DishList()
        {
            ViewBag.entityName = "M_Dish";
            ViewBag.pageName = "料理マスタ一覧画面";
            return View();
        }

        [AuthenticationFilter(Order = 1)]
        [ExceptionFilter(Order = 2)]
        public ActionResult OptionSetList()
        {
            ViewBag.entityName = "Y_OptionSet";
            ViewBag.pageName = "OptionClientList";
            return View();
        }

        [AuthenticationFilter(Order = 1)]
        [ExceptionFilter(Order = 2)]
        public ActionResult ReportRefer(string ShopCD, string PartyID)
        {
            ViewBag.ShopCD = ShopCD;
            ViewBag.PartyID = PartyID;
            return View();
        }
    }
}