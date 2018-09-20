using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lifestyle.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
               

        public ActionResult Help()
        {
            ViewBag.Message = "Hello! THis small help";

            return View();
        }
    }
}