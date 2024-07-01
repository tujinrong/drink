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
using WebEvaluation.Controllers.Filters;
using WebEvaluation.DataModels;

namespace WebEvaluation.Controllers//namespace名前がちょっと変な感じです。2014/07/03
{
    [AuthenticationFilter(Order = 1)]
    [ExceptionFilter(Order = 2)]
    public class UserController : Controller
    {
        private EvaluationContext db = new EvaluationContext();
        //LILIANG :/User/ 2014/07/03===========↓

        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (null != model.OldPassowrd)
            {
                UserSession _user = Session["user"] as UserSession;
                M_User user = db.Users.Find(_user.UserID);
                if (null != user)
                {  
                    if (model.OldPassowrd != user.Password)
                    {
                        ViewBag.msg = "パスワードの入力エラー!";
                        ViewBag.msgType = "error";
                        return View();
                    }
                    else 
                    {
                        if (model.NewPassWord != model.NewPasswordConfirm)
                        {
                            ViewBag.msg = "パスワードが一致しません!";
                            ViewBag.msgType = "error";
                            return View();
                        }
                        else
                        {
                            user.Password = model.NewPassWord;
                            db.Entry(user).State = EntityState.Modified;
                            db.SaveChanges();
                            ViewBag.msg = "パスワードの変更に成功!";
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
                else
                {
                    return RedirectToAction("Login", "Home");
                }
            }else
            {
                return View();
            }
        }
        [WebAuthorizeAttribute(Roles = "02,04,09")]
        public ActionResult MasterManager(string name, string divitionCD, string groupCD, int? page)
        {
            return GetResultModels(name, divitionCD, groupCD, page);
        }
        //LILIANG :/User/ 2014/07/03===========↑
        // GET: /User/
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: /User/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            M_User m_user = db.Users.Find(id);
            if (m_user == null)
            {
                return HttpNotFound();
            }
            return View(m_user);
        }

        // GET: /User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /User/Create
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,Password,StaffCD,UpdateUserID,UpdateTime")] M_User m_user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(m_user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(m_user);
        }

        // GET: /User/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            M_User m_user = db.Users.Find(id);
            if (m_user == null)
            {
                return HttpNotFound();
            }
            return View(m_user);
        }

        // POST: /User/Edit/5
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,Password,StaffCD,UpdateUserID,UpdateTime")] M_User m_user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(m_user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(m_user);
        }

        // GET: /User/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            M_User m_user = db.Users.Find(id);
            if (m_user == null)
            {
                return HttpNotFound();
            }
            return View(m_user);
        }

        // POST: /User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            M_User m_user = db.Users.Find(id);
            db.Users.Remove(m_user);
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

        //店舗データ取得
        private ActionResult GetResultModels(string name, string divitionCD, string groupCD, int? page)
        {
            var models = GetModels(name, divitionCD, groupCD);

            int pageSize = 100;
            int pageNumber = (page ?? 1);
            return View(models.ToPagedList(pageNumber, pageSize));
        }

        //店舗全データ取得
        private IEnumerable<StaffViewModel> GetModels(string name, string divitionCD, string groupCD)
        {
            ViewBag.name = name;
            ViewBag.divitionCD = divitionCD;
            ViewBag.groupCD = groupCD;

            var Users = db.Users.ToList();
            var Staffs = db.Staffs.ToList();
            var Codes = db.Codes.ToList();

            var models = from user in Users
                         join staff in Staffs on user.StaffCD equals staff.StaffCD into g_user_staff
                         from u_staff in g_user_staff.DefaultIfEmpty()
                         join code in Codes on new { key = user.RoleCD, kind = "RoleCD" } equals new { key = code.CD,kind = code.Kind } into g_user_code
                         from u_code in g_user_code.DefaultIfEmpty()
                         select new StaffViewModel
                         {
                             StaffCD = u_staff.StaffCD,
                             StaffName = u_staff.StaffName,
                             StaffKana = u_staff.StaffKana,
                             Sex = u_staff.Sex,
                             EnrollmentDate = u_staff.EnrollmentDate,
                             Duty = u_staff.Duty,
                             UnitCD = u_staff.UnitCD,
                             RoleCD = u_code.Name
                         };

            models.Where(u => doField(u));

            models = models.OrderBy(u => u.UnitCD);


            return models;
        }

        private bool doField(StaffViewModel staff)
        {

            return true;
        }

    }
}
