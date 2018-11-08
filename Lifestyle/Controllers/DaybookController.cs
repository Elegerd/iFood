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

            int c = 0;
            int f = 0;
            int p = 0;
            int car = 0;
            var daybook = dbDaybook.Daybooks.Where(x => x.UId == user.UserId);
            List<DefaultProduct> d_products = new List<DefaultProduct> { };
            foreach (var item in daybook)
            {
                DefaultProduct defaultProduct = dbProduct.DefaultProducts.Find(item.DPId);
                c += defaultProduct.Calories;
                f += defaultProduct.Fats;
                p += defaultProduct.Protein;
                car += defaultProduct.Carbs;
                d_products.Add(defaultProduct);
            }
            ViewBag.c = c;
            ViewBag.f = f;
            ViewBag.p = p;
            ViewBag.car = car;
            ViewBag.dp = d_products;
            ViewBag.Daybook = daybook;
            ViewBag.Calories = (int)((user.Weight * 10 + (user.Height * 6.25) -
            ((DateTime.UtcNow.Month < user.BirthDate.Month || (DateTime.UtcNow.Month == user.BirthDate.Month && DateTime.UtcNow.Day < user.BirthDate.Day)) ?
            (DateTime.UtcNow.Year - user.BirthDate.Year) : (DateTime.UtcNow.Year - user.BirthDate.Year) - 1) +
            (user.Sex == true ? 5 : -161)) * 1.2 - c);

            ViewBag.Product = new SelectList(dbProduct.DefaultProducts, "Id", "Name");

            if (user.Sex == null || user.Height == null || user.Weight == null)
            {
                return HttpNotFound();
            }
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Index(int id)
        {
            User user = db.Users.FirstOrDefault(x => x.Email == User.Identity.Name);
            ViewBag.Product = new SelectList(dbProduct.DefaultProducts, "Id", "Name");
            dbDaybook.Daybooks.Add(new DaybookProduct { UId = user.UserId, DPId = id});
            dbDaybook.SaveChanges();

            int c = 0;
            int f = 0;
            int p = 0;
            int car = 0;
            var daybook = dbDaybook.Daybooks.Where(x => x.UId == user.UserId);
            List<DefaultProduct> d_products = new List<DefaultProduct> { };
            foreach (var item in daybook)
            {
                DefaultProduct defaultProduct = dbProduct.DefaultProducts.Find(item.DPId);
                c += defaultProduct.Calories;
                f += defaultProduct.Fats;
                p += defaultProduct.Protein;
                car += defaultProduct.Carbs;
                d_products.Add(defaultProduct);
            }
            ViewBag.c = c;
            ViewBag.f = f;
            ViewBag.p = p;
            ViewBag.car = car;
            ViewBag.dp = d_products;
            ViewBag.Daybook = daybook;
            ViewBag.Calories = (int)((user.Weight * 10 + (user.Height * 6.25) -
            ((DateTime.UtcNow.Month < user.BirthDate.Month || (DateTime.UtcNow.Month == user.BirthDate.Month && DateTime.UtcNow.Day < user.BirthDate.Day)) ?
            (DateTime.UtcNow.Year - user.BirthDate.Year) : (DateTime.UtcNow.Year - user.BirthDate.Year) - 1) +
            (user.Sex == true ? 5 : -161)) * 1.2 - c);
      

            return View();
        }

        [Authorize]
        public ActionResult Help()
        {
            return View();
        }

        [Authorize]
        public ActionResult DelProduct(int? Id)
        {
            DaybookProduct b = dbDaybook.Daybooks.Find(Id);
            if (b != null)
            {
                dbDaybook.Daybooks.Remove(b);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Daybook");
        }
    }
}