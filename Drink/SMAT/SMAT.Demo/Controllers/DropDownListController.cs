using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASMAT.Demo.Controllers
{
    public class DropDownListController : Controller
    {

        public ActionResult Basic_usage(string did, string aid)
        {
            ViewBag.did = did;

            ViewBag.aid = aid;

            return View();
        }
    }
}