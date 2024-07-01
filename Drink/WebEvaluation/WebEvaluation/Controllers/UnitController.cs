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
    public class UnitController : BaseController
    {
        private EvaluationContext db = new EvaluationContext();
        //<summary> UnitImport
        //[WebAuthorizeAttribute(Roles = "02,04,09")]
        //public ActionResult UnitDataImport(HttpPostedFileBase datafile, string path)
        //{
        //    if ((datafile != null && datafile.ContentLength > 0) || path != null)
        //    {
        //        String filePath = "";
        //        string importFileName = "";
        //        if (path == null)
        //        {
        //            string basePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\UPLOAD\\";
        //            String date = string.Format("{0:yyyyMMddHHmmssfff}", DateTime.Now);
        //            String fileName = string.Format("組織_{0}.csv", date);
        //            filePath = basePath + fileName;
        //            importFileName = datafile.FileName;

        //            if (!Directory.Exists(basePath))
        //            {
        //                Directory.CreateDirectory(basePath);
        //            }
        //            datafile.SaveAs(filePath);
        //        }
        //        else
        //        {
        //            filePath = path;
        //        }

        //        List<ImportErrorViewModel> errorList = new List<ImportErrorViewModel>();
        //        List<UnitData> addList = new List<UnitData>();
        //        List<UnitData> updateList = new List<UnitData>();
        //        List<UnitData> deleteList = new List<UnitData>();
        //        List<S_Unit> data = db.Units.ToList();
        //        List<M_Staff> satffs = db.Staffs.ToList();
        //        List<M_Shop> shops = db.Shops.ToList();
        //        List<ICSVChecker<UnitData>> checkers = new List<ICSVChecker<UnitData>>();

        //        checkers.Add(new UnitValueCheck(shops));

        //        List<UnitData> csvList = CsvUtils.CsvToModel<UnitData>(filePath, ref errorList, checkers);//csvList

        //        if (errorList.Count > 0)
        //        {
        //            return ImportIndex<UnitData>(importFileName, "UnitDataImport", errorList, csvList, addList, updateList, deleteList, new { path = filePath });
        //        }


        //        #region 　変化有のデータを　更新する。
        //        var updataQuery = from unit in data

        //                          join unitUnitCD in csvList on unit.UnitCD equals unitUnitCD.UnitCD into u_import_unitcd
        //                          from u_unitcd in u_import_unitcd

        //                          //join unitShopCD in shops on u_unitcd.ShopCD equals unitShopCD.ShopCD into u_import_unitshopcd
        //                          //from u_shopcd in u_import_unitshopcd

        //                          //join importUnitName in csvList on unit.UnitCD equals importUnitName.UnitCD into u_import_unitname
        //                          //from u_name in u_import_unitname

        //                          //join importUnitShopCD in csvList on unit.UnitCD equals importUnitShopCD.UnitCD into u_import_unitshopcd
        //                          //from u_shopcd in u_import_unitshopcd
                                         
        //                          //where (
        //                          //   unit.UnitName != u_unitcd.UnitName
        //                          //|| unit.ShopCD != u_unitcd.ShopCD
        //                          //|| unit.IsShop != bool.Parse(u_unitcd.IsShop)
        //                          //|| unit.IsCusCenter != bool.Parse(u_unitcd.IsCusCenter)
        //                          //)

        //                          where(checkShopUpdate(u_unitcd,unit))

        //                          select new
        //                          {
        //                              unit = unit,
        //                              u_unitcd = u_unitcd,
        //                              //u_name = u_name,
        //                              //u_shopcd = u_shopcd,
        //                              //u_isshop = u_isshop,
        //                              //u_iscuscenter = u_iscuscenter
        //                          };

        //        foreach (var item in updataQuery)
        //        {
        //            S_Unit unit = item.unit;
        //            unit.UnitName = item.u_unitcd.UnitName;

        //            //if (item.u_unitcd != null)
        //            //{
        //            //    unit.UnitName = item.u_unitcd.UnitName;
        //            //}
        //            if (item.u_unitcd.ShopCD != null || item.u_unitcd.ShopCD.Equals("-") == false)
        //            {
        //                unit.ShopCD = item.u_unitcd.ShopCD;

        //            }
        //            unit.IsCusCenter = bool.Parse(item.u_unitcd.IsCusCenter);
        //            unit.IsShop = bool.Parse(item.u_unitcd.IsShop);

        //            //if (item.u_isshop != null)
        //            //{
        //            //    unit.IsShop = item.u_isshop.IsShop;
        //            //}
        //            //if (item.u_iscuscenter != null)
        //            //{
        //            //    unit.IsCusCenter = item.u_iscuscenter.IsCusCenter;
        //            //}

        //            db.Entry(unit).State = EntityState.Modified;

        //            updateList.Add(item.u_unitcd);
        //        }
        //        #endregion
        //        #region 差分削除
        //        if (csvList.Count >= data.Count / 2)
        //        {
        //            var delQuery = from unit in data

        //                           join importUnit in csvList on unit.UnitCD equals importUnit.UnitCD into u_import_unit
        //                           from u_import in u_import_unit.DefaultIfEmpty()
        //                           //join importShop in shops on unit.ShopCD equals importShop.ShopCD into u_import_shop
        //                           //from s_import in u_import_shop.DefaultIfEmpty()                                   
        //                           join importStaff in satffs on unit.UnitCD equals importStaff.UnitCD into u_import_staff
        //                           from st_import in u_import_staff.DefaultIfEmpty()

        //                           where u_import == null && st_import == null//&&s_import == null

        //                           select new
        //                           {
        //                               unit = unit,
        //                               u_import = u_import
        //                           };

        //            foreach (var item in delQuery)
        //            {
        //                S_Unit unit = item.unit;
        //                db.Entry(unit).State = EntityState.Deleted;

        //                UnitData importData = new UnitData();
        //                importData.UnitCD = unit.UnitCD;
        //                importData.UnitName = unit.UnitName;
        //                importData.ShopCD = unit.ShopCD;
        //                importData.IsCusCenter = unit.IsCusCenter.ToString();
        //                importData.IsShop = unit.IsShop.ToString();
        //                if(null != unit.ShopCD){
        //                    List<M_Shop> shp = shops.Where(g => g.ShopCD == unit.ShopCD).ToList();
        //                    if(shp.Count>0){
        //                        importData.ShopCD = shp[0].ShopCD;
        //                    }
        //                }                        
        //                deleteList.Add(importData);
        //            }

        //        }


        //        #endregion
        //        #region 差分追加

        //        var addQuery = from importUnit in csvList

        //                       join unit in data on importUnit.UnitCD equals unit.UnitCD into u_import_unit
        //                       from u_import in u_import_unit.DefaultIfEmpty()

        //                       //join shopcd in shops on importUnit.ShopCD equals shopcd.ShopCD into u_import_shop
        //                       //from s_import in u_import_shop.DefaultIfEmpty(new M_Shop())                               

        //                       where u_import == null

        //                       select new
        //                       {
        //                           importUnit = importUnit,
        //                           //u_import = u_import,
        //                           //s_import = s_import
        //                       };

        //        List<S_Unit> newdata = new List<S_Unit>();
        //        var a = 1;
        //        foreach (var item in addQuery)
        //        {
        //            a++;
        //            S_Unit unit = new S_Unit();

        //            unit.UnitCD = item.importUnit.UnitCD;
        //            unit.UnitName = item.importUnit.UnitName;
        //            unit.ShopCD = item.importUnit.ShopCD;
        //            unit.IsCusCenter = bool.Parse(item.importUnit.IsCusCenter);                    
        //            unit.IsShop = bool.Parse(item.importUnit.IsShop);   
                
        //            newdata.Add(unit);

        //            addList.Add(item.importUnit);
        //            //db.Units.AddRange(newdata);//
        //            //db.SaveChanges();//
        //        }
        //        db.Units.AddRange(newdata);
        //        #endregion
        //        if (path == null || errorList.Count > 0)
        //        {
        //            return ImportIndex<UnitData>(importFileName, "UnitDataImport", errorList, csvList, addList, updateList, deleteList, new { path = filePath });
        //        }
        //        else
        //        {
        //            try
        //            {
        //                db.SaveChanges();
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.Write(ex.ToString());
        //                throw;
        //            }
        //            ViewBag.msg = "ファイルが正常にアップロードされました。";
        //        }
        //    }
        //    return View();
        //}
        //private bool checkShopUpdate(UnitData u_unitcd, S_Unit unit)
        //{
        //    string importStr = u_unitcd.UnitCD + "," + u_unitcd.UnitName + "," + u_unitcd.ShopCD + "," + bool.Parse(u_unitcd.IsCusCenter) + "," + bool.Parse(u_unitcd.IsShop);
        //    string dataStr = unit.UnitCD + "," + unit.UnitName + "," + unit.ShopCD + "," + unit.IsCusCenter + "," + unit.IsShop;
        //    if (importStr == dataStr)
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        return true;
        //    }
        //}
        /// <summary>
        ///組織情報インポート チェック
        /// </summary>
        //public class UnitValueCheck : WebEvaluation.Utils.ICSVChecker<UnitData>
        //{
        //    private List<S_Unit> Units;
        //    private List<M_Shop> Shops;            
        //    private List<M_Staff> satffs;
        //    //private List<S_Unit> Units;

        //    public UnitValueCheck(List<M_Shop> _shops)
        //    {
        //        Shops = _shops;
        //    }

        //    public UnitValueCheck(List<S_Unit> data)
        //    {
        //        // TODO: Complete member initialization
        //        Units = data;
        //    }

        //    public UnitValueCheck(List<M_Staff> satffs)
        //    {
        //        // TODO: Complete member initialization
        //        this.satffs = satffs;
        //    }

        //    List<UnitData> us =new List<UnitData>();
        //    int x = 0;
        //    int y = 0;
        //    public void doCheck(ref List<ImportErrorViewModel> errorList, UnitData data, int rowNum)
        //    {
        //        //CSVファイル主鍵のチェック
        //        if (!string.IsNullOrEmpty(data.UnitCD))
        //        {
        //            int flag = 0;
        //            us.Add(data);
        //            int count1 = us.Count-1;
        //            int count2 = us.Count;
        //            for (int i = 0; i < count1;i++ )
        //            {                        
        //                for (int j = i+1; j < count2;j++ )
        //                {
        //                    string us1 = us[i].UnitCD.ToString();
        //                    string us2 = us[j].UnitCD.ToString();
        //                    if (us1 == us2)
        //                    {
        //                        flag += 1;
        //                        x = i;
        //                        y = j;
        //                    }
        //                }
        //            }
        //            if (flag>0)
        //            {
        //                CommonUtils.AddError(ref errorList, new ImportErrorViewModel
        //                {
        //                    ErrorField = "組織コード",
        //                    ErrorType = "UnitValueError",
        //                    ErrorDetail = "「組織コード」：" + us[y].UnitCD + ","+(x+2)+"行目：「組織コード」：" + us[x].UnitCD + "列のデータが複数存在します。",
        //                    ErrorRow = rowNum
        //                });
        //            }
        //        }  
        //        if (string.IsNullOrEmpty(data.UnitCD))
        //        {
        //            CommonUtils.AddError(ref errorList, new ImportErrorViewModel
        //            {
        //                ErrorField = "組織コード",
        //                ErrorType = "UnitValueError",
        //                ErrorDetail = "「組織コード」列のデータが必ず入力してください。",
        //                ErrorRow = rowNum
        //            });
        //        }               
        //        if (string.IsNullOrEmpty(data.UnitName))
        //        {
        //            CommonUtils.AddError(ref errorList, new ImportErrorViewModel
        //            {
        //                ErrorField = "組織名",
        //                ErrorType = "UnitValueError",
        //                ErrorDetail = "「組織名」列のデータが必ず入力してください。",
        //                ErrorRow = rowNum
        //            });
        //        }

        //        //if (!string.IsNullOrEmpty(data.ShopCD))
        //        //{
        //        //    List<M_Shop> uns = Shops.Where(s => s.ShopCD == data.ShopCD).ToList();
        //        //    if (uns.Count == 0)
        //        //    {
        //        //        CommonUtils.AddError(ref errorList, new ImportErrorViewModel
        //        //        {
        //        //            ErrorField = "店舗略称",
        //        //            ErrorType = "UnitValueError",
        //        //            ErrorDetail = "「店舗名」[" + data.ShopCD + "]列のデータがUNIT中に存在しません。",
        //        //            ErrorRow = rowNum
        //        //        });
        //        //    }
        //        //}
        //        if (string.IsNullOrEmpty(data.IsCusCenter))
        //        {
        //            CommonUtils.AddError(ref errorList, new ImportErrorViewModel
        //            {
        //                ErrorField = "IsCusCenter",
        //                ErrorType = "UnitValueError",
        //                ErrorDetail = "「IsCusCenter」列のデータが必ず入力してください。",
        //                ErrorRow = rowNum
        //            });
        //        }
        //        if (string.IsNullOrEmpty(data.IsShop))
        //        {
        //            CommonUtils.AddError(ref errorList, new ImportErrorViewModel
        //            {
        //                ErrorField = "IsShop",
        //                ErrorType = "UnitValueError",
        //                ErrorDetail = "「IsShop」列のデータが必ず入力してください。",
        //                ErrorRow = rowNum
        //            });
        //        }
        //        else  if(data.IsShop!="TRUE"&&data.IsShop!="FALSE")
        //        {
        //            CommonUtils.AddError(ref errorList, new ImportErrorViewModel
        //            {
        //                ErrorField = "IsShop",
        //                ErrorType = "UnitValueError",
        //                ErrorDetail = "「IsShop」列のデータが必TRUEまたはFALSE入力してください。",
        //                ErrorRow = rowNum
        //            });
        //        }
        //    }
        //}
        //</summary>
        // GET: /Unit/

        //public ActionResult Index(){
        //  return View(db.Units.ToList());
        //}
        [WebAuthorizeAttribute(Roles = "02,04,09")]
        public ActionResult Index(string ispostback,string id,string msg,string msgType)
        {
            ViewBag.msg = msg;
            ViewBag.msgType = msgType;
            return View(db.Units.ToList());
        }


        // GET: /Unit/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            S_Unit s_unit = db.Units.Find(id);
            if (s_unit == null)
            {
                return HttpNotFound();
            }
            return View(s_unit);
        }

        // GET: /Unit/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Unit/Create
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UnitCD,UnitName,ShopCD,IsCusCenter,IsShop")] S_Unit s_unit)
        {
            var isCusCenter = db.Systems.ToList();
            var units = db.Units.ToList();
            var shops = db.Shops.ToList();
            if (s_unit.ShopCD == null) {
                s_unit.ShopCD = "";
            }
            var isCenter = isCusCenter.Where(p => p.CusCenterUnitCD == s_unit.UnitCD);
            var checkUnits = units.Where(p => p.UnitCD == s_unit.UnitCD);
            var checkShops = shops.Where(p=> p.ShopCD == s_unit.ShopCD);

            if (isCenter.Count() > 0 && s_unit.IsCusCenter == false)
            {
                ViewBag.msg = "組織コード：" + s_unit.UnitCD + "　がカスタマセンタですので『カスタマーセンタフラグ』をチックしてください。";
                ViewBag.msgType = "error";
                return View();
                //return RedirectToAction("Create", new { isPostBack = "1", UnitCD = s_unit.UnitCD, msg = "組織コード：" + s_unit.UnitCD + "　がカスタマセンタので「カスタマーセンタフラグをチックしてください。」", msgType = "error" });
            }
            if (checkUnits.Count() > 0) {
                ViewBag.msg = "組織コード：" + s_unit.UnitCD + "　が存在しているので、登録登録することができません。";
                ViewBag.msgType = "error";
                return View();
            }
            if (checkShops.Count() > 0 && s_unit.IsShop == false) {
                ViewBag.msg = "組織コード：" + s_unit.UnitCD + "　が店舗ですので『店舗フラグ』をチックしてください。";
                ViewBag.msgType = "error";
                return View();
            }
            if(checkShops.Count()==0 && s_unit.ShopCD.Length>0){
                ViewBag.msg = "店舗コード：" + s_unit.ShopCD + " 　　該当店舗が存在しないので、登録することができません。";
                ViewBag.msgType = "error";
                return View();
            }
            if (ModelState.IsValid)
            {
                db.Units.Add(s_unit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(s_unit);
        }

        // GET: /Unit/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            S_Unit s_unit = db.Units.Find(id);
            if (s_unit == null)
            {
                return HttpNotFound();
            }
            return View(s_unit);
        }

        // POST: /Unit/Edit/5
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UnitCD,UnitName,ShopCD,IsCusCenter,IsShop")] S_Unit s_unit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(s_unit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(s_unit);
        }

        // GET: /Unit/Delete/5
        public ActionResult Delete(string id)
        {
            //List<M_Staff> staffs = db.Staffs.ToList();
            //var num = from staff in staffs
            //          where staff.UnitCD==id
            //          select new
            //          {
            //              nums = staff.UnitCD.Count()
            //          };            
            //ViewBag.nums = num.Count();
            var staffs = db.Staffs.ToList();
            var staffsModel = staffs.Where(p => p.UnitCD == id);
            if(staffsModel.Count()>0){
                return RedirectToAction("Index", new { isPostBack = "1", UnitCD = id, msg = "組織コード：" + id + "　関連データ"+staffsModel.Count()+"件が存在しているのど、削除不可！",msgType = "error"});
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            S_Unit s_unit = db.Units.Find(id);
            if (s_unit == null)
            {
                return HttpNotFound();
            }
            S_Unit unit = db.Units.Find(id);
            db.Units.Remove(unit);
            db.SaveChanges();
            return RedirectToAction("Index");//, new { isPostBack = "1", UnitCD = id}
            //return View(s_unit);
        }

        // POST: /Unit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            S_Unit s_unit = db.Units.Find(id);
            db.Units.Remove(s_unit);
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
        //[WebAuthorizeAttribute(Roles = "02,04,09")]
        //public ActionResult detailCsv()
        //{
        //    //List<ShopViewModel> models = GetModels(null, null,false).ToList();            
        //    //List<UnitData> detailModels = new List<UnitData>().ToList();
        //    // List<S_Code> codes = db.Codes.ToList();
        //    //if (models.Count == 0)
        //    //{
        //    //   return Json(new { ResultType = EnumResultType.NoData, Message = "データがありません。", });
        //    //}

        //    string basePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

        //    String date = string.Format("{0:yyyyMMddHHmmssfff}", DateTime.Now);

        //    String fileName = string.Format("\\CSV\\組織_{0}.csv", date);

        //    CsvUtils.ModlesToCsv<S_Unit>(basePath + fileName, db.Units.ToList());////

        //    return Json(new { Path = string.Format("/CSV/組織_{0}.csv", date), ResultType = EnumResultType.Success });
        //}
    }
}
