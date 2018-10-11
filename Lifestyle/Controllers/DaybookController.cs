using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lifestyle.Controllers
{
    public class DaybookController : Controller
    {

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Help()
        {
            return View();
        }
    }
}