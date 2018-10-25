using Lifestyle.Models;
using Lifestyle.Models.Daybook;
using Lifestyle.Models.DaybookContext;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Lifestyle.Controllers
{
    public class DaybookController : Controller
    {
        private UserContext db = new UserContext();
        private DaybookContext dbDaybook = new DaybookContext();
        private ProductContext dbProduct = new ProductContext();

        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {

            User user = db.Users.FirstOrDefault(x => x.Email == User.Identity.Name);

            //IEnumerable<DefaultProduct> dayproducts = dbDaybook.Daybooks;
            //ViewBag.DaybookProduct = dayproducts;

            SelectList product = new SelectList(dbProduct.DefaultProducts, "Id", "Name");
            ViewBag.Product = product;

            ViewData["Calories"] = (user.Weight * 10 + (user.Height * 6.25) -
            ((DateTime.UtcNow.Month < user.BirthDate.Month || (DateTime.UtcNow.Month == user.BirthDate.Month && DateTime.UtcNow.Day < user.BirthDate.Day)) ?
            (DateTime.UtcNow.Year - user.BirthDate.Year) : (DateTime.UtcNow.Year - user.BirthDate.Year) - 1) +
            (user.Sex == true ? 5 : -161)) * 1.2;

            if (user.Sex == null || user.Height == null || user.Weight == null)
            {
                return HttpNotFound();
            }

            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Index(DefaultProduct model)
        {
            User user = db.Users.FirstOrDefault(x => x.Email == User.Identity.Name);
            SelectList product = new SelectList(dbProduct.DefaultProducts, "Id", "Name");
            ViewBag.Product1 = product;
            model = dbProduct.DefaultProducts.FirstOrDefault(x => x.Id == model.Id);

            dbDaybook.Daybooks.Add(new DaybookProduct { UId = user.UserId, AProduct = model });
            dbDaybook.SaveChanges();
            return View();
        }

        [Authorize]
        public ActionResult Help()
        {
            return View();
        }
    }
}