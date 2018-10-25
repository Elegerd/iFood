using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lifestyle.Controllers
{
    public class ArticlesController : Controller
    {
        // GET: Articles
        public ActionResult Index()
        {
            return View();
        }


        // Articles/ArcticleNumOne
        public ActionResult ArticlesNumOne()
        {
            return View();
        }

       // Articles/ArcticleNumTwo
        public ActionResult ArcticleNumTwo()
        {
            return View();
        }
    }
}
