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


        public ActionResult AddProduct()
        {
            string selectId = Request.QueryString["id"];
            string product = Request.QueryString["product"];
            if (Convert.ToBoolean(product))
            {
                CustomProductContext dbCustomProduct = new CustomProductContext();
                var customProduct = dbCustomProduct.CustomProducts.Find(Convert.ToInt32(selectId));
                return Json(new
                {
                    bazName = customProduct.Name,
                    bazCalories = customProduct.Calories,
                    bazFats = customProduct.Fats,
                    bazProtein = customProduct.Protein,
                    bazCarbs = customProduct.Carbs
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                DefaultProductContext dbDefaultProduct = new DefaultProductContext();
                var defaultProduct = dbDefaultProduct.DefaultProducts.Find(Convert.ToInt32(selectId));
                return Json(new
                {
                    bazName = defaultProduct.Name,
                    bazCalories = defaultProduct.Calories,
                    bazFats = defaultProduct.Fats,
                    bazProtein = defaultProduct.Protein,
                    bazCarbs = defaultProduct.Carbs
                }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}