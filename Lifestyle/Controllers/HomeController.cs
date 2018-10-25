using Lifestyle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Lifestyle.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            if (User.Identity.IsAuthenticated == false)
            {
                ViewBag.Verification = "Войти";
            }
            else
            {
                return Redirect("~/Account/ProfileUser");
            }
            return View();
        }
    }
}