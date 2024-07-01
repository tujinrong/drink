using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using WebEvaluation.Models;
using WebEvaluation.DAL;
using WebEvaluation.Controllers.Filters;
using WebEvaluation.ViewModels;
using WebEvaluation.Utils;
using WebEvaluation.Common;
using WebEvaluation.DataModels;

namespace WebEvaluation.Controllers
{
    [AuthenticationFilter(Order = 1)]
    [ExceptionFilter(Order = 2)]
    public class StaffController : Controller
    {
        private EvaluationContext db = new EvaluationContext();

        // GET: /Staff/
        public ActionResult Index()
        {
            return View(db.Staffs.ToList());
        }

        [WebAuthorizeAttribute(Roles = "02,04,09")]
        public ActionResult MasterManager(string UnitCD, string staffCd, string name, string kanaName,string isPostBack,string msg,string msgType, int? page)
        {
            ViewBag.msg = msg;
            ViewBag.msgType = msgType;
            return GetResultModels(UnitCD, staffCd, name, kanaName,isPostBack, page);
        }

        private ActionResult GetResultModels(string UnitCD, string staffCd, string name, string kanaName, string isPostBack, int? page)
        {
            var models = GetModels(UnitCD, staffCd, name, kanaName);

            if (isPostBack != null && isPostBack == "1")
            {
                ViewBag.isPostBack = "1";

                if (models.ToList().Count == 0)
                {
                    ViewBag.msg = "データがありません。";
                    ViewBag.msgType = "info";
                }
                GC.Collect();
                int pageSize = 100;
                int pageNumber = (page ?? 1);
                return View(models.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                ViewBag.isPostBack = "1";
                return View();
            }
        }

        private IEnumerable<StaffViewModel> GetModels(string UnitCD , string staffCd, string name, string kanaName)
        {
            db.Database.CommandTimeout = 600000;

            ViewBag.name = name;
            ViewBag.staffCd = staffCd;
            ViewBag.UnitCD = UnitCD;
            ViewBag.kanaName = kanaName;

            if (UnitCD == "") {
                ViewBag.UnitCD = " ";
            }

            if (UnitCD == " ")
            {
                UnitCD = "";
            }

            var Staffs = db.Staffs.ToList();
            var Users = db.Users.ToList();
            var Units = db.Units.ToList();
            var Codes = db.Codes.ToList();

            var models = from staff in Staffs
                         join user in Users on staff.StaffCD equals user.StaffCD into g_staff_user
                         from s_user in g_staff_user.DefaultIfEmpty(new M_User())
                         join units in Units on staff.UnitCD equals units.UnitCD into g_staff_units
                         from s_unit in g_staff_units.DefaultIfEmpty(new S_Unit())
                         join code in Codes on new { key = s_user.RoleCD, kind = "RoleCD" } equals new { key = code.CD, kind = code.Kind } into g_user_code
                         from u_code in g_user_code.DefaultIfEmpty(new S_Code())
                         select new StaffViewModel
                         {
                             StaffCD = staff.StaffCD,
                             StaffName = staff.StaffName,
                             StaffKana = staff.StaffKana,
                             Sex = staff.Sex,
                             SexName = GetSexName(staff.Sex),
                             EnrollmentDate = staff.EnrollmentDate,
                             Duty = staff.Duty,
                             Yakusyoku = staff.Yakusyoku,
                             UnitCD = staff.UnitCD,
                             RoleCD = u_code.Name,
                             EMail = staff.Email,
                             UnitName = s_unit.UnitName
                         };

            if (!string.IsNullOrEmpty(name))
            {
                models = models.Where(p => CommonUtils.isContains(p.StaffName,name));//p.StaffName.Contains(name)
            }

            if (!string.IsNullOrEmpty(kanaName))
            {
                models = models.Where(p => CommonUtils.isContains(p.StaffKana,kanaName)); // p.StaffKana.Contains(kanaName)
            }

            if (!string.IsNullOrEmpty(staffCd))
            {
                models = models.Where(p => p.StaffCD == staffCd);
            }

            if (!string.IsNullOrEmpty(UnitCD))
            {
                models = models.Where(p => p.UnitCD == UnitCD);
            }

            models = models.OrderBy(u => u.UnitCD);


            return models;
        }

        private String GetSexName(String sex) 
        {
            if (sex == "1")
            {
                return "男";
            }
            else if (sex == "2")
            {
                return "女";
            }

            return "";
        }


        // GET: /Staff/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            M_Staff m_staff = db.Staffs.Find(id);
            if (m_staff == null)
            {
                return HttpNotFound();
            }
            return View(m_staff);
        }

        // GET: /Staff/Create
        [WebAuthorizeAttribute(Roles = "04,09")]
        public ActionResult Create(string UnitCD, string staffCd, string name, string kanaName,string _staffCD,string _staffName, string isPostBack,string ShopCD)
        {
            ViewBag.name = name;
            ViewBag._staffCd = staffCd;
            ViewBag._UnitCD = UnitCD;
            ViewBag.kanaName = kanaName;

            M_Staff staff = new M_Staff();
            staff.StaffCD = _staffCD;
            staff.StaffName = _staffName;
            ViewBag.isPostBack = isPostBack;

            if (ShopCD != null && String.IsNullOrEmpty(ShopCD) == false)
            {

                var shopQuery = from unit in db.Units
                                where unit.ShopCD == ShopCD && unit.IsShop == true
                                select unit;

                if (shopQuery.Count() > 0)
                {
                    ViewBag._UnitCD = shopQuery.First().UnitCD;
                    staff.UnitCD = shopQuery.First().UnitCD;
                }

                ViewBag.EnrollmentDate = DateTime.Now.ToString("yyyy/MM/dd");

                ViewBag.RoleCD = "01";
                ViewBag.psw = _staffCD;
                ViewBag.psw_c = _staffCD;
            }

            return View(staff);
        }

        [WebAuthorizeAttribute(Roles = "04,09")]
        public ActionResult IframeCreate(string UnitCD, string staffCd, string name, string kanaName, string _staffCD, string _staffName, string isPostBack, string ShopCD)
        {
            ViewBag.name = name;
            ViewBag._staffCd = staffCd;
            ViewBag._UnitCD = UnitCD;
            ViewBag.kanaName = kanaName;

            M_Staff staff = new M_Staff();
            staff.StaffCD = _staffCD;
            staff.StaffName = _staffName;
            ViewBag.isPostBack = isPostBack;

            if (ShopCD != null && String.IsNullOrEmpty(ShopCD) == false)
            {

                var shopQuery = from unit in db.Units
                                where unit.ShopCD == ShopCD && unit.IsShop == true
                                select unit;

                if (shopQuery.Count() > 0)
                {
                    ViewBag._UnitCD = shopQuery.First().UnitCD;
                    staff.UnitCD = shopQuery.First().UnitCD;
                }

                ViewBag.EnrollmentDate = DateTime.Now.ToString("yyyy/MM/dd");

                ViewBag.RoleCD = "01";
                ViewBag.psw = _staffCD;
                ViewBag.psw_c = _staffCD;
            }

            return View(staff);
        }


        // POST: /Staff/Create
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [WebAuthorizeAttribute(Roles = "04,09")]
        public ActionResult Create([Bind(Include = "StaffCD,StaffName,StaffKana,Sex,EnrollmentDate,Duty,UnitCD,UpdateUserID,UpdateTime")] M_Staff m_staff, string _UnitCD, string _staffCd, string name, string kanaName, string RoleCD, string psw, string psw_c, string isPostBack)
        {
            ViewBag.name = name;
            ViewBag._staffCd = _staffCd;
            ViewBag._UnitCD = _UnitCD;
            ViewBag.kanaName = kanaName;
            ViewBag.isPostBack = isPostBack;

            ViewBag.RoleCD = RoleCD;
            ViewBag.psw = psw;
            ViewBag.psw_c = psw_c;

            if (m_staff.EnrollmentDate != null)
            {
                ViewBag.EnrollmentDate = m_staff.EnrollmentDate.Value.ToString("yyyy/MM/dd");
            }

            if (ModelState.IsValid)
            {
                if (m_staff.StaffCD.Length < 5)
                {
                    ViewBag.msg = "社員番号が５桁不足です！";
                    ViewBag.msgType = "error";
                    return View();
                }

                M_Staff oldstaff = db.Staffs.Find(m_staff.StaffCD);

                if (oldstaff != null) {
                    ViewBag.msg = "該当社員番号がすでに存在しています。";
                    ViewBag.msgType = "error";
                    return View();
                }

                if (RoleCD == "01" && String.IsNullOrEmpty(m_staff.UnitCD) == false) 
                {
                    S_Unit unit = db.Units.Find(m_staff.UnitCD);
                    if (unit == null || unit.IsShop == false) 
                    {
                        ViewBag.msg = "権限は店舗ですが、所属が店舗ではありません、ご確認ください。";
                        ViewBag.msgType = "error";
                        return View();
                    }
                }

                if (String.IsNullOrEmpty(RoleCD) == false)
                {
                    if (psw != psw_c)
                    {
                        ViewBag.msg = "パスワードが一致していません。";
                        ViewBag.msgType = "error";
                        return View();
                    }

                    M_User user = new M_User();
                    user.RoleCD = RoleCD;
                    user.UserID = m_staff.StaffCD;
                    user.Password = psw;
                    user.StaffCD = m_staff.StaffCD;

                    db.Users.Add(user);
                }

                db.Staffs.Add(m_staff);
                db.SaveChanges();

                return RedirectToAction("MasterManager", new { UnitCD = _UnitCD, staffCd = _staffCd, name = name, kanaName = kanaName, isPostBack = isPostBack });
            }

            return View(m_staff);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [WebAuthorizeAttribute(Roles = "04,09")]
        public ActionResult IframeCreate([Bind(Include = "StaffCD,StaffName,StaffKana,Sex,EnrollmentDate,Duty,UnitCD,UpdateUserID,UpdateTime")] M_Staff m_staff, string _UnitCD, string _staffCd, string name, string kanaName, string RoleCD, string psw, string psw_c, string isPostBack)
        {
            ViewBag.name = name;
            ViewBag._staffCd = _staffCd;
            ViewBag._UnitCD = _UnitCD;
            ViewBag.kanaName = kanaName;
            ViewBag.isPostBack = isPostBack;

            ViewBag.RoleCD = RoleCD;
            ViewBag.psw = psw;
            ViewBag.psw_c = psw_c;

            if (m_staff.EnrollmentDate != null)
            {
                ViewBag.EnrollmentDate = m_staff.EnrollmentDate.Value.ToString("yyyy/MM/dd");
            }

            if (ModelState.IsValid)
            {
                if (m_staff.StaffCD.Length < 5)
                {
                    ViewBag.msg = "社員番号が５桁不足です！";
                    ViewBag.msgType = "error";
                    return View();
                }

                M_Staff oldstaff = db.Staffs.Find(m_staff.StaffCD);

                if (oldstaff != null)
                {
                    ViewBag.msg = "該当社員番号がすでに存在しています。";
                    ViewBag.msgType = "error";
                    return View();
                }

                if (RoleCD == "01" && String.IsNullOrEmpty(m_staff.UnitCD) == false)
                {
                    S_Unit unit = db.Units.Find(m_staff.UnitCD);
                    if (unit == null || unit.IsShop == false)
                    {
                        ViewBag.msg = "権限は店舗ですが、所属が店舗ではありません、ご確認ください。";
                        ViewBag.msgType = "error";
                        return View();
                    }
                }

                if (String.IsNullOrEmpty(RoleCD) == false)
                {
                    if (psw != psw_c)
                    {
                        ViewBag.msg = "パスワードが一致していません。";
                        ViewBag.msgType = "error";
                        return View();
                    }

                    M_User user = new M_User();
                    user.RoleCD = RoleCD;
                    user.UserID = m_staff.StaffCD;
                    user.Password = psw;
                    user.StaffCD = m_staff.StaffCD;

                    db.Users.Add(user);
                }

                db.Staffs.Add(m_staff);
                db.SaveChanges();

                ViewBag.isClose = "true";
            }

            return View(m_staff);
        }

        // GET: /Staff/Edit/5
        [WebAuthorizeAttribute(Roles = "04,09")]
        public ActionResult Edit(string id,string page, string UnitCD, string staffCd, string name, string kanaName)
        {
            ViewBag.page = page;
            ViewBag.name = name;
            ViewBag._staffCd = staffCd;
            ViewBag._UnitCD = UnitCD;
            ViewBag.kanaName = kanaName;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            M_Staff m_staff = db.Staffs.Find(id);
            if (m_staff == null)
            {
                return HttpNotFound();
            }

            ViewBag.StaffCDForDisplay = m_staff.StaffCD;

            if (m_staff.EnrollmentDate != null)
            {
                ViewBag.EnrollmentDate = m_staff.EnrollmentDate.Value.ToString("yyyy/MM/dd");
            }

            M_User user = db.Users.Find(id);
            if (user != null)
            {
                ViewBag.RoleCD = user.RoleCD;
                ViewBag.psw = user.Password;
                ViewBag.psw_c = user.Password;
            }
            
            return View(m_staff);
        }

        // POST: /Staff/Edit/5
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [WebAuthorizeAttribute(Roles = "04,09")]
        public ActionResult Edit([Bind(Include = "StaffCD,StaffName,StaffKana,Sex,EnrollmentDate,Duty,UnitCD,UpdateUserID,UpdateTime,Email,Yakusyoku")] M_Staff m_staff, string _UnitCD, string _staffCd, string page, string name, string kanaName, string RoleCD, string psw, string psw_c)
        {
            ViewBag.page = page;
            ViewBag.name = name;
            ViewBag._staffCd = _staffCd;
            ViewBag._UnitCD = _UnitCD;
            ViewBag.kanaName = kanaName;

            ViewBag.RoleCD = RoleCD;
            ViewBag.psw = psw;
            ViewBag.psw_c = psw_c;

            ViewBag.StaffCDForDisplay = m_staff.StaffCD;

            if (m_staff.EnrollmentDate != null)
            {
                ViewBag.EnrollmentDate = m_staff.EnrollmentDate.Value.ToString("yyyy/MM/dd");
            }

            if (string.IsNullOrEmpty(m_staff.Email) == false) {
                if (this.IsEmail(m_staff.Email) == false) {
                    ViewBag.msg = "Emailアドレスのフォーマットが正しくありません、ご確認ください。";
                    ViewBag.msgType = "error";
                    return View(m_staff);
                }
            }

            if (ModelState.IsValid)
            {
                if (psw != psw_c)
                {
                    ViewBag.msg = "二度入力したパスワードが一致しない。";
                    ViewBag.msgType = "error";
                    return View();
                }

                if (RoleCD == "01" && String.IsNullOrEmpty(m_staff.UnitCD) == false)
                {
                    S_Unit unit = db.Units.Find(m_staff.UnitCD);
                    if (unit == null || unit.IsShop == false)
                    {
                        ViewBag.msg = "権限は店舗ですが、所属が店舗ではありません、ご確認ください。";
                        ViewBag.msgType = "error";
                        return View(m_staff);
                    }
                }

                M_User user = db.Users.Find(m_staff.StaffCD);
                if (user == null)
                {
                    user = new M_User();
                    user.UserID = m_staff.StaffCD;
                    user.StaffCD = m_staff.StaffCD;
                    user.RoleCD = RoleCD;
                    user.Password = psw;
                    db.Users.Add(user);
                }
                else
                {
                    user.StaffCD = m_staff.StaffCD;
                    user.RoleCD = RoleCD;
                    user.Password = psw;
                    db.Entry(user).State = EntityState.Modified;
                }

                db.Entry(m_staff).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("MasterManager", new {page = page, UnitCD = _UnitCD, staffCd = _staffCd, name = name, kanaName = kanaName, isPostBack = "1" });
            }
            return View(m_staff);
        }

        // GET: /Staff/Delete/5
        public ActionResult Delete(string id, string UnitCD, string staffCd, string name, string kanaName, int? page)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            M_Staff m_staff = db.Staffs.Find(id);
            if (m_staff == null)
            {
                return RedirectToAction("MasterManager", new { isPostBack = "1", UnitCD = UnitCD, staffCd = staffCd, name = name, kanaName = kanaName, msg = "社員№：" + id + "　は存在しません！", msgType = "error", page = page });
            }

            var Partys = db.Partys.ToList();
            var EvaByStaff= db.EvaByStaffs.ToList();
            var EvaByLeader = db.EvaByLeaders.ToList();

            var partyModel = Partys.Where(p=>p.TantoCD  == id);
            var evaByStaffModel = EvaByStaff.Where(e => e.Eva1StaffCD == id || e.Eva2StaffCD == id || e.Eva3StaffCD == id);
            var evaByLeaderModel = EvaByLeader.Where(e => e.UpdateUserID == id);

            if (partyModel.ToList().Count > 0 || evaByStaffModel.ToList().Count > 0 || evaByLeaderModel.ToList().Count > 0)
            {
                return RedirectToAction("MasterManager", new { isPostBack = "1", UnitCD = UnitCD, staffCd = staffCd, name = name, kanaName = kanaName, msg = "社員№：" + id + "　関連データは存在、削除不可！", msgType = "error", page = page });
            }

            db.Staffs.Remove(m_staff);

            List<M_User> users = db.Users.Where(u => u.StaffCD == id).ToList();
            db.Users.RemoveRange(users);
            db.SaveChanges();

            db.SaveChanges();
            return RedirectToAction("MasterManager", new { isPostBack = "1", UnitCD = UnitCD, staffCd = staffCd, name = name, kanaName = kanaName, msg = "社員№：" + id + "　の社員を削除しました!", page = page });
        }

        // POST: /Staff/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            M_Staff m_staff = db.Staffs.Find(id);
            db.Staffs.Remove(m_staff);
            return RedirectToAction("MasterManager");
        }

        /// <summary>
        /// 参照画面
        /// </summary>
        /// <returns></returns>
        public ActionResult Refer(string UnitCD,string shopCD,  string staffCd, string name, string kanaName,string initCenter, int? page)
        {
            //2014．10．03　李梁　検索条件格式化
            if (name == null) {
                name = "";
            }
            var staffname = name.Trim();
            //2014．10．03　李梁
            List<S_Unit> units = db.Units.ToList();

            if (UnitCD == null && initCenter == "1")
            {
                List<S_Unit> unitList = units.Where(u => u.IsCusCenter == true).ToList();
                if (unitList.Count > 0)
                {
                    UnitCD = unitList[0].UnitCD;
                }

            }

            if (UnitCD == " ")
            {
                UnitCD = "";
            }
            

            if(shopCD != null)
            {
                 List<S_Unit> list =  units.Where(u=>u.ShopCD == shopCD).ToList();
                 if (list.Count > 0)
                 {
                     UnitCD = list[0].UnitCD;
                 }
            }

            if (UnitCD != null && UnitCD.Trim().Length > 0)
            {
                List<S_Unit> list = units.Where(u => u.UnitCD == UnitCD).ToList();
                if (list.Count > 0)
                {
                    shopCD = list[0].ShopCD;
                }
            }

            ViewBag.shopCD = shopCD; 
            return GetResultModels(UnitCD, staffCd, staffname, kanaName,"1", page);
        }

        /// <summary>
        /// 参照
        /// </summary>
        /// <returns></returns>
        public JsonResult ReferJson(string StaffCD)
        {
            M_Staff staff = db.Staffs.Find(StaffCD);
            if (null == staff)
            {
                staff = new M_Staff();
            }
            return Json(staff); 
        }

        /// <summary>
        /// CSV出力
        /// </summary>
        /// <returns></returns>
        public ActionResult csv(string UnitCD, string staffCd, string name, string kanaName)
        {
            List<StaffViewModel> models = GetModels(UnitCD, staffCd, name, kanaName).ToList();

            if (models.Count == 0)
            {
                return Json(new { ResultType = EnumResultType.NoData, Message = "データがありません。", });
            }

            string basePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

            String date = string.Format("{0:yyyyMMddHHmmssfff}", DateTime.Now);

            String fileName = string.Format("\\CSV\\社員_{0}.csv", date);

            CsvUtils.ModlesToCsv<StaffViewModel>(basePath + fileName, models);
            GC.Collect();
            return Json(new { Path = string.Format("/CSV/社員_{0}.csv", date), ResultType = EnumResultType.Success });
        }

        public ActionResult detailCsv()
        {
            List<S_Unit> units = db.Units.ToList();

            List<StaffViewModel> models = GetModels(null, null, null, null).ToList();
            List<StaffDetailModel> detailModels = new List<StaffDetailModel>();

            foreach (StaffViewModel item in models)
            {
                StaffDetailModel detail = new StaffDetailModel();

                detail.StaffCD = item.StaffCD;
                detail.StaffName = item.StaffName;
                detail.StaffKana = item.StaffKana;
                detail.Sex = item.SexName;
                if (null != item.EnrollmentDate)
                {
                    detail.EnrollmentDate = item.EnrollmentDate.HasValue ? item.EnrollmentDate.Value.ToString("yyyyMMdd") : null;
                }
                detail.SosikiCD = item.UnitCD;
                if (null != item.UnitCD)
                { 
                    List<S_Unit> unitList = units.Where(u=>u.UnitCD == item.UnitCD).ToList();
                    if (unitList.Count > 0)
                    {
                        detail.SosikiName = unitList[0].UnitName;
                    }
                }
               
                detail.Yakusyoku = item.Yakusyoku;
                detail.Duty = item.Duty;
                detail.Email = item.EMail;

                detailModels.Add(detail);
            }

            if (models.Count == 0)
            {
                return Json(new { ResultType = EnumResultType.NoData, Message = "データがありません。", });
            }

            string basePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

            String date = string.Format("{0:yyyyMMddHHmmssfff}", DateTime.Now);

            String fileName = string.Format("\\CSV\\社員_{0}.csv", date);

            CsvUtils.ModlesToCsv<StaffDetailModel>(basePath + fileName, detailModels);
            GC.Collect();
            return Json(new { Path = string.Format("/CSV/社員_{0}.csv", date), ResultType = EnumResultType.Success });
        }

        /// <summary>
        /// 参照画面
        /// </summary>
        /// <returns></returns>
        public ActionResult ReferEmail(string shopCD,string staffCD,string selectedStaffCD,string selectMode)
        {
            ViewBag.selectedStaffCD = selectedStaffCD;

            ViewBag.selectMode = selectMode;

            var Staffs = db.Staffs.ToList();
            var Units = db.Units.ToList();

            var models = from staff in Staffs
                         join units in Units on staff.UnitCD equals units.UnitCD into g_staff_units
                         from s_unit in g_staff_units.DefaultIfEmpty(new S_Unit())

                         where ((s_unit.ShopCD == shopCD
                            && (staff.Yakusyoku == "営業（支配人）"
                                || staff.Yakusyoku == "営業(支配人)"
                                || staff.Yakusyoku == "料飲（シェフ）"
                                || staff.Yakusyoku == "料飲(シェフ)"
                                || staff.Yakusyoku == "FC（チーフ）"
                                || staff.Yakusyoku == "FC(チーフ)"
                                || staff.Yakusyoku == "PDチーフ"
                                || staff.Yakusyoku == "プロデューサー"
                                || staff.Yakusyoku == "ﾌﾟﾛﾃﾞｭｰｻｰ"
                                || staff.Yakusyoku == "営業（プランナー）"
                                || staff.Yakusyoku == "営業(プランナー)"
                                || staff.Yakusyoku == "WP")) || staff.StaffCD == staffCD)
                                && string.IsNullOrEmpty(staff.Email) == false

                         select new StaffViewModel
                         {
                             StaffCD = staff.StaffCD,
                             StaffName = staff.StaffName,
                             StaffKana = staff.StaffKana,
                             Sex = staff.Sex,
                             EnrollmentDate = staff.EnrollmentDate,
                             Duty = staff.Duty,
                             Yakusyoku = staff.Yakusyoku,
                             UnitCD = staff.UnitCD,
                             EMail = staff.Email,
                             UnitName = s_unit.UnitName
                         };

            //

            if (models.Where(s => s.Yakusyoku == "営業（支配人）" || s.Yakusyoku == "営業(支配人)").Count() > 0)
            {
                models = models.Where(s => (s.Yakusyoku != "営業（プランナー）" && s.Yakusyoku != "営業(プランナー)") || s.StaffCD == staffCD);
            }

            if (models.ToList().Count == 0)
            {
                ViewBag.msg = "データがありません。";
                ViewBag.msgType = "info";
            }

            return View(models.ToList());
        }

        public bool IsEmail(string str_Email)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_Email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
