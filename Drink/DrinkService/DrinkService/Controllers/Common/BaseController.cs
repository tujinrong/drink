using DrinkService.Data.Models;
using DrinkService.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DrinkService.Controllers
{
    [AuthenticationFilter(Order = 1)]
    public class BaseController : Controller
    {
        public UserSession loginUser {
            get 
            {
                return Session["user"] as UserSession;
            }
        }
    }
}