using ASMAT.Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASMAT.Demo.Controllers
{
    public class GridController : Controller
    {

        public ActionResult Basic_usage(string did, string aid)
        {
            ViewBag.did = did;

            ViewBag.aid = aid;

            return View();
        }

        public ActionResult Edit(string did, string aid)
        {
            ViewBag.did = did;

            ViewBag.aid = aid;

            return View();
        }

        public ActionResult RowDetail(string did, string aid)
        {
            ViewBag.did = did;

            ViewBag.aid = aid;

            return View();
        }

        public JsonResult DetailData(string Id)
        {
            var res = new JsonResult();
            //var value = "actionValue";  

            res.Data = GetPersonList();

            res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;//允许使用GET方式获取，否则用GET获取是会报错。  
            return res;
        }

        private IList<Person> GetPersonList()
        {
            IList<Person> personList = new List<Person>();
            personList.Add(new Person { Id = 1, FirstName = "William", LastName = "Adama", Episodes = 73 });
            personList.Add(new Person { Id = 2, FirstName = "Laura", LastName = "Roslin", Episodes = 73 });
            personList.Add(new Person { Id = 3, FirstName = "Gaius", LastName = "Baltar", Episodes = 73 });
            personList.Add(new Person { Id = 4, FirstName = "Number", LastName = "Six", Episodes = 73 });
            personList.Add(new Person { Id = 5, FirstName = "Saul", LastName = "Tigh", Episodes = 69 });
            personList.Add(new Person { Id = 6, FirstName = "Kara", LastName = "Thrace", Episodes = 71 });
            personList.Add(new Person { Id = 7, FirstName = "Lee", LastName = "Adama", Episodes = 73 });
            return personList;
        }
    }
}