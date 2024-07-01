using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASMAT.Demo.Controllers
{
    public class SMATController : Controller
    {

        public ActionResult Demo(string did, string aid,string index)
        {
            ViewBag.did = did;

            ViewBag.aid = aid;

            if (string.IsNullOrEmpty(index))
            {
                ViewBag.index = "0";
            }
            else 
            {
                ViewBag.index = index;
            }
            

            return View();
        }
    }
}