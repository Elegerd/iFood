using Lifestyle.Models;
using Lifestyle.Models.Daybook;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Lifestyle.Controllers
{
    public class DaybookController : Controller
    {
        private UserContext db = new UserContext();
        private DaybookContext dbDaybook = new DaybookContext();
        private DefaultProductContext dbDefaultProduct = new DefaultProductContext();
        private CustomProductContext dbCustomProduct = new CustomProductContext();

        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            User user = db.Users.FirstOrDefault(x => x.Email == User.Identity.Name);

            int c = 0;
            int f = 0;
            int p = 0;
            int car = 0;
            var daybook = dbDaybook.Daybooks.Where(x => x.UserId == user.UserId);
            List<DefaultProduct> d_products = new List<DefaultProduct> { };
            List<CustomProduct> c_products = new List<CustomProduct> { };
            foreach (var item in daybook)
            {
                if (item.Custom == false)
                {
                    DefaultProduct defaultProduct = dbDefaultProduct.DefaultProducts.Find(item.ProductId);
                    c += defaultProduct.Calories * (item.Gram / 100);
                    f += defaultProduct.Fats;
                    p += defaultProduct.Protein;
                    car += defaultProduct.Carbs;
                    d_products.Add(defaultProduct);
                }
                else
                {
                    CustomProduct customProduct = dbCustomProduct.CustomProducts.Find(item.ProductId);
                    c += customProduct.Calories * (item.Gram / 100);
                    f += customProduct.Fats;
                    p += customProduct.Protein;
                    car += customProduct.Carbs;
                    c_products.Add(customProduct);
                }
            }
            ViewBag.c = c;
            ViewBag.f = f;
            ViewBag.p = p;
            ViewBag.car = car;
            ViewBag.dp = d_products;
            ViewBag.Daybook = daybook;
            ViewBag.cp = c_products;
            ViewBag.Calories = (int)((user.Weight * 10 + (user.Height * 6.25) -
            ((DateTime.UtcNow.Month < user.BirthDate.Month || (DateTime.UtcNow.Month == user.BirthDate.Month && DateTime.UtcNow.Day < user.BirthDate.Day)) ?
            (DateTime.UtcNow.Year - user.BirthDate.Year) : (DateTime.UtcNow.Year - user.BirthDate.Year) - 1) +
            (user.Sex == true ? 5 : -161)) * 1.2 - c);

            ViewBag.Product = dbDefaultProduct.DefaultProducts.ToList();
            // ViewBag.Product = new SelectList(dbDefaultProduct.DefaultProducts, "Id", "Name");
            //ViewBag.Product_2 = new SelectList(dbCustomProduct.CustomProducts, "Id", "Name");
            ViewBag.Product_2 = dbCustomProduct.CustomProducts.Where(x => x.UserId == user.UserId);

            if (user.Sex == null || user.Height == null || user.Weight == null)
            {
                return HttpNotFound();
            }
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Index(ProductModel productModel, int gram, bool custom)
        {
            /*
            var model = new ProductModel
            {
                DefaultProducts = dbDefaultProduct.DefaultProducts.ToList(),
                CustomProducts = dbCustomProduct.CustomProducts.ToList()
            }; */

            User user = db.Users.FirstOrDefault(x => x.Email == User.Identity.Name);
            ViewBag.Product = new SelectList(dbDefaultProduct.DefaultProducts, "Id", "Name");
            ViewBag.Product_2 = new SelectList(dbCustomProduct.CustomProducts, "Id", "Name");
            if (custom == false)
            {
                dbDaybook.Daybooks.Add(new DaybookProduct { UserId = user.UserId, ProductId = productModel.DefaultProducts.Id, Custom = custom, Gram = gram });

            }
            else
            {
                dbDaybook.Daybooks.Add(new DaybookProduct { UserId = user.UserId, ProductId = productModel.CustomProducts.Id, Custom = custom, Gram = gram });
            }
            dbDaybook.SaveChanges();

            int c = 0;
            int f = 0;
            int p = 0;
            int car = 0;
            var daybook = dbDaybook.Daybooks.Where(x => x.UserId == user.UserId);
            List<DefaultProduct> d_products = new List<DefaultProduct> { };
            List<CustomProduct> c_products = new List<CustomProduct> { };
            foreach (var item in daybook)
            {
                if (item.Custom == false)
                {
                    DefaultProduct defaultProduct = dbDefaultProduct.DefaultProducts.Find(item.ProductId);
                    c += (int)(defaultProduct.Calories * (double)(item.Gram / 100));
                    f += defaultProduct.Fats;
                    p += defaultProduct.Protein;
                    car += defaultProduct.Carbs;
                    d_products.Add(defaultProduct);
                }
                else
                {
                    CustomProduct customProduct = dbCustomProduct.CustomProducts.Find(item.ProductId);
                    c += (int)(customProduct.Calories * (double)(item.Gram / 100));
                    f += customProduct.Fats;
                    p += customProduct.Protein;
                    car += customProduct.Carbs;
                    c_products.Add(customProduct);
                }

            }
            ViewBag.c = c;
            ViewBag.f = f;
            ViewBag.p = p;
            ViewBag.car = car;
            ViewBag.dp = d_products;
            ViewBag.cp = c_products;
            ViewBag.Daybook = daybook;
            ViewBag.Calories = (int)((user.Weight * 10 + (user.Height * 6.25) -
            ((DateTime.UtcNow.Month < user.BirthDate.Month || (DateTime.UtcNow.Month == user.BirthDate.Month && DateTime.UtcNow.Day < user.BirthDate.Day)) ?
            (DateTime.UtcNow.Year - user.BirthDate.Year) : (DateTime.UtcNow.Year - user.BirthDate.Year) - 1) +
            (user.Sex == true ? 5 : -161)) * 1.2 - c);

            return View(productModel);
        }

        [Authorize]
        public ActionResult Help()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult DelProduct(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DaybookProduct b = dbDaybook.Daybooks.Find(id);
            if (b == null)
            {
                return HttpNotFound();
            }
            dbDaybook.Daybooks.Remove(b);
            dbDaybook.SaveChanges();
            return Redirect("~/Daybook/Index");
        }
    }
}