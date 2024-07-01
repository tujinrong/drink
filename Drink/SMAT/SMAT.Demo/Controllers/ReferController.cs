using ASMAT.Demo.Models;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace ASMAT.Demo.Controllers
{
    public class ReferController : Controller
    {

        public ActionResult Basic_usage(string did, string aid)
        {
            ViewBag.did = did;

            ViewBag.aid = aid;

            return View();
        }

        public ActionResult PersonRefer()
        {
            return View(GetPersonList());
        }

        public JsonResult PersonFindAll()
        {
            return Json(GetPersonList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult PersonSearch(string Id, string FirstName, string pageNumber)
        {

            List<Person> PersonList = GetPersonList();

            var persons = from person in PersonList

                          select person;

            if (String.IsNullOrEmpty(Id) == false) {
                persons = persons.Where(p => p.Id.ToString().StartsWith(Id));
            }

            if (String.IsNullOrEmpty(FirstName) == false)
            {
                persons = persons.Where(p => p.FirstName.Contains(FirstName));
            }

            int tsize = persons.Count();
            int pNumber = String.IsNullOrEmpty(pageNumber) ? 1 : int.Parse(pageNumber);

            persons = persons.ToPagedList(pNumber, 10);

            return Json(new { pageSize = 10, totalSize = tsize, pageNumber = pNumber, pageData = persons }, JsonRequestBehavior.AllowGet);
        }



        private List<Person> GetPersonList()
        {
            List<Person> personList = new List<Person>();
            personList.Add(new Person { Id = 1, FirstName = "William", LastName = "Adama", Episodes = 73 });
            personList.Add(new Person { Id = 2, FirstName = "Laura", LastName = "Roslin", Episodes = 73 });
            personList.Add(new Person { Id = 3, FirstName = "Gaius", LastName = "Baltar", Episodes = 73 });
            personList.Add(new Person { Id = 4, FirstName = "Number", LastName = "Six", Episodes = 73 });
            personList.Add(new Person { Id = 5, FirstName = "Saul", LastName = "Tigh", Episodes = 69 });
            personList.Add(new Person { Id = 6, FirstName = "Kara", LastName = "Thrace", Episodes = 71 });
            personList.Add(new Person { Id = 7, FirstName = "Lee", LastName = "Adama", Episodes = 73 });

            personList.Add(new Person { Id = 11, FirstName = "Dad Of William", LastName = "Adama", Episodes = 73 });
            personList.Add(new Person { Id = 12, FirstName = "Dad Of Laura", LastName = "Roslin", Episodes = 73 });
            personList.Add(new Person { Id = 13, FirstName = "Dad Of Gaius", LastName = "Baltar", Episodes = 73 });
            personList.Add(new Person { Id = 14, FirstName = "Dad Of Number", LastName = "Six", Episodes = 73 });
            personList.Add(new Person { Id = 15, FirstName = "Dad Of Saul", LastName = "Tigh", Episodes = 69 });
            personList.Add(new Person { Id = 16, FirstName = "Dad Of Kara", LastName = "Thrace", Episodes = 71 });
            personList.Add(new Person { Id = 17, FirstName = "Dad Of Lee", LastName = "Adama", Episodes = 73 });

            personList.Add(new Person { Id = 21, FirstName = "Mom of William", LastName = "Adama", Episodes = 73 });
            personList.Add(new Person { Id = 22, FirstName = "Mom of Laura", LastName = "Roslin", Episodes = 73 });
            personList.Add(new Person { Id = 23, FirstName = "Mom of Gaius", LastName = "Baltar", Episodes = 73 });
            personList.Add(new Person { Id = 24, FirstName = "Mom of Number", LastName = "Six", Episodes = 73 });
            personList.Add(new Person { Id = 25, FirstName = "Mom of Saul", LastName = "Tigh", Episodes = 69 });
            personList.Add(new Person { Id = 26, FirstName = "Mom of Kara", LastName = "Thrace", Episodes = 71 });
            personList.Add(new Person { Id = 27, FirstName = "Mom of Lee", LastName = "Adama", Episodes = 73 });

            personList.Add(new Person { Id = 31, FirstName = "Brother of William", LastName = "Adama", Episodes = 73 });
            personList.Add(new Person { Id = 32, FirstName = "Brother of Laura", LastName = "Roslin", Episodes = 73 });
            personList.Add(new Person { Id = 33, FirstName = "Brother of Gaius", LastName = "Baltar", Episodes = 73 });
            personList.Add(new Person { Id = 34, FirstName = "Brother of Number", LastName = "Six", Episodes = 73 });
            personList.Add(new Person { Id = 35, FirstName = "Brother of Saul", LastName = "Tigh", Episodes = 69 });
            personList.Add(new Person { Id = 36, FirstName = "Brother of Kara", LastName = "Thrace", Episodes = 71 });
            personList.Add(new Person { Id = 37, FirstName = "Brother of Lee", LastName = "Adama", Episodes = 73 });

            return personList;
        }
    }
}