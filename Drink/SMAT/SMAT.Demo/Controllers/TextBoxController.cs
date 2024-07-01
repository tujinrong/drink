using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASMAT.Demo.Controllers
{
    public class TextBoxController : Controller
    {

        public ActionResult Basic_usage(string did, string aid)
        {
            ViewBag.did = did;

            ViewBag.aid = aid;

            return View();
        }

        public ActionResult Events(string did, string aid)
        {
            ViewBag.did = did;

            ViewBag.aid = aid;

            return View();
        }
    }
}