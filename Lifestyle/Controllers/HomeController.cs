using Lifestyle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Lifestyle.Models.Product;

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

       
        public ActionResult Demo()
        {
            string par1 = this.Request.QueryString["id"];
             DefaultProductContext dbDefaultProduct = new DefaultProductContext();
            var pr = dbDefaultProduct.DefaultProducts.Find(Convert.ToInt32(par1));
            return Json(new { foo="bar", baz= pr.Calories}, JsonRequestBehavior.AllowGet);
            }

    }
}