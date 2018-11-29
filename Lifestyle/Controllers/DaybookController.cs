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
        private DaybookContext dbDaybook = new DaybookContext();
        private DefaultProductContext dbDefaultProduct = new DefaultProductContext();
        private CustomProductContext dbCustomProduct = new CustomProductContext();

        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            User user;
            int c = 0, f = 0, p = 0, car = 0;
            List<DefaultProduct> d_products = new List<DefaultProduct> { };
            List<CustomProduct> c_products = new List<CustomProduct> { };
            using (UserContext db = new UserContext())
            {
                user = db.Users.FirstOrDefault(x => x.Email == User.Identity.Name);
            }
            var daybook = dbDaybook.Daybooks.Where(x => x.UserId == user.UserId);

            foreach (var item in daybook)
            {
                if (item.Custom == false)
                {
                    DefaultProduct defaultProduct = dbDefaultProduct.DefaultProducts.Find(item.ProductId);
                    c += defaultProduct.Calories * (item.Gram / 100);
                    f += defaultProduct.Fats * (item.Gram / 100);
                    p += defaultProduct.Protein * (item.Gram / 100);
                    car += defaultProduct.Carbs * (item.Gram / 100);
                    d_products.Add(defaultProduct);
                }
                else
                {
                    CustomProduct customProduct = dbCustomProduct.CustomProducts.Find(item.ProductId);
                    c += customProduct.Calories * (item.Gram / 100);
                    f += customProduct.Fats * (item.Gram / 100);
                    p += customProduct.Protein * (item.Gram / 100);
                    car += customProduct.Carbs* (item.Gram / 100);
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

            ViewBag.DefaultProducts = dbDefaultProduct.DefaultProducts.ToList().ToList();
            ViewBag.CustomProducts = dbCustomProduct.CustomProducts.Where(x => x.UserId == user.UserId).ToList();

            if (user.Sex == null || user.Height == null || user.Weight == null)
            {
                return HttpNotFound();
            }
            return View();
        }

        [Authorize]
        public ActionResult Help()
        {
            return View();
        }

        [Authorize]
        public ActionResult AddProduct()
        {
            User user;
            int id;
            int allCalories = 0, fats = 0, protein = 0, carbs = 0;
            using (UserContext db = new UserContext())
            {
                user = db.Users.FirstOrDefault(x => x.Email == User.Identity.Name);
            }

            string selectId = Request.QueryString["id"];
            string product = Request.QueryString["product"];
            int gram = Convert.ToInt32(Request.QueryString["g"]);
            if (Convert.ToBoolean(product))
            {
                CustomProduct customProduct;
                using (CustomProductContext dbCustomProduct = new CustomProductContext())
                {
                    customProduct = dbCustomProduct.CustomProducts.Find(Convert.ToInt32(selectId));
                }
                using (DaybookContext dbDaybook = new DaybookContext())
                {
                    DaybookProduct daybookProduct = new DaybookProduct { UserId = user.UserId, ProductId = customProduct.Id, Custom = Convert.ToBoolean(product), Gram = gram };
                    dbDaybook.Daybooks.Add(daybookProduct);
                    dbDaybook.SaveChanges();
                    id = daybookProduct.Id;
                }

                var daybook = dbDaybook.Daybooks.Where(x => x.UserId == user.UserId);
                foreach (var item in daybook)
                {
                    if (item.Custom == false)
                    {
                        DefaultProduct defaultP = dbDefaultProduct.DefaultProducts.Find(item.ProductId);
                        allCalories += defaultP.Calories * (item.Gram / 100);
                        fats += defaultP.Fats * (item.Gram / 100);
                        protein += defaultP.Protein * (item.Gram / 100);
                        carbs += defaultP.Carbs * (item.Gram / 100);
                    }
                    else
                    {
                        CustomProduct customP = dbCustomProduct.CustomProducts.Find(item.ProductId);
                        allCalories += customP.Calories * (item.Gram / 100);
                        fats += customP.Fats * (item.Gram / 100);
                        protein += customP.Protein * (item.Gram / 100);
                        carbs += customP.Carbs * (item.Gram / 100);
                    }
                }

                int caloriesNeeded = (int)((user.Weight * 10 + (user.Height * 6.25) -
                ((DateTime.UtcNow.Month < user.BirthDate.Month || (DateTime.UtcNow.Month == user.BirthDate.Month && DateTime.UtcNow.Day < user.BirthDate.Day)) ?
                (DateTime.UtcNow.Year - user.BirthDate.Year) : (DateTime.UtcNow.Year - user.BirthDate.Year) - 1) +
                (user.Sex == true ? 5 : -161)) * 1.2) - allCalories;

                return Json(new
                {
                    addId = id,
                    addName = customProduct.Name,
                    addCalories = customProduct.Calories * gram / 100,
                    addFats = customProduct.Fats * gram / 100,
                    addProtein = customProduct.Protein * gram / 100,
                    addCarbs = customProduct.Carbs * gram / 100,
                    caloriesNeeded,
                    allCalories,
                    fats,
                    protein,
                    carbs
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                DefaultProduct defaultProduct;
                using (DefaultProductContext dbDefaultProduct = new DefaultProductContext())
                {
                    defaultProduct = dbDefaultProduct.DefaultProducts.Find(Convert.ToInt32(selectId));
                }
                using (DaybookContext dbDaybook = new DaybookContext())
                {
                    DaybookProduct daybookProduct = new DaybookProduct { UserId = user.UserId, ProductId = defaultProduct.Id, Custom = Convert.ToBoolean(product), Gram = gram };
                    dbDaybook.Daybooks.Add(daybookProduct);
                    dbDaybook.SaveChanges();
                    id = daybookProduct.Id;
                }

                var daybook = dbDaybook.Daybooks.Where(x => x.UserId == user.UserId);
                foreach (var item in daybook)
                {
                    if (item.Custom == false)
                    {
                        DefaultProduct defaultP = dbDefaultProduct.DefaultProducts.Find(item.ProductId);
                        allCalories += defaultP.Calories * (item.Gram / 100);
                        fats += defaultP.Fats * (item.Gram / 100);
                        protein += defaultP.Protein * (item.Gram / 100);
                        carbs += defaultP.Carbs * (item.Gram / 100);
                    }
                    else
                    {
                        CustomProduct customP = dbCustomProduct.CustomProducts.Find(item.ProductId);
                        allCalories += customP.Calories * (item.Gram / 100);
                        fats += customP.Fats * (item.Gram / 100);
                        protein += customP.Protein * (item.Gram / 100);
                        carbs += customP.Carbs * (item.Gram / 100);
                    }
                }

                int caloriesNeeded = (int)((user.Weight * 10 + (user.Height * 6.25) -
                ((DateTime.UtcNow.Month < user.BirthDate.Month || (DateTime.UtcNow.Month == user.BirthDate.Month && DateTime.UtcNow.Day < user.BirthDate.Day)) ?
                (DateTime.UtcNow.Year - user.BirthDate.Year) : (DateTime.UtcNow.Year - user.BirthDate.Year) - 1) +
                (user.Sex == true ? 5 : -161)) * 1.2) - allCalories;

                return Json(new
                {
                    addId = id,
                    addName = defaultProduct.Name,
                    addCalories = defaultProduct.Calories * gram / 100,
                    addFats = defaultProduct.Fats * gram / 100,
                    addProtein = defaultProduct.Protein * gram / 100,
                    addCarbs = defaultProduct.Carbs * gram / 100,
                    caloriesNeeded,
                    allCalories,
                    fats,
                    protein,
                    carbs
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteProduct()
        {
            User user;
            int allCalories = 0, fats = 0, protein = 0, carbs = 0;
            using (UserContext db = new UserContext())
            {
                user = db.Users.FirstOrDefault(x => x.Email == User.Identity.Name);
            }
            string id = Request.QueryString["id"];
            DaybookProduct daybookProduct = dbDaybook.Daybooks.Find(Convert.ToInt32(id));
            dbDaybook.Daybooks.Remove(daybookProduct);
            dbDaybook.SaveChanges();

            var daybook = dbDaybook.Daybooks.Where(x => x.UserId == user.UserId);
            foreach (var item in daybook)
            {
                if (item.Custom == false)
                {
                    DefaultProduct defaultP = dbDefaultProduct.DefaultProducts.Find(item.ProductId);
                    allCalories += defaultP.Calories * (item.Gram / 100);
                    fats += defaultP.Fats * (item.Gram / 100);
                    protein += defaultP.Protein * (item.Gram / 100);
                    carbs += defaultP.Carbs * (item.Gram / 100);
                }
                else
                {
                    CustomProduct customP = dbCustomProduct.CustomProducts.Find(item.ProductId);
                    allCalories += customP.Calories * (item.Gram / 100);
                    fats += customP.Fats * (item.Gram / 100);
                    protein += customP.Protein * (item.Gram / 100);
                    carbs += customP.Carbs * (item.Gram / 100);
                }
            }

            int caloriesNeeded = (int)((user.Weight * 10 + (user.Height * 6.25) -
            ((DateTime.UtcNow.Month < user.BirthDate.Month || (DateTime.UtcNow.Month == user.BirthDate.Month && DateTime.UtcNow.Day < user.BirthDate.Day)) ?
            (DateTime.UtcNow.Year - user.BirthDate.Year) : (DateTime.UtcNow.Year - user.BirthDate.Year) - 1) +
            (user.Sex == true ? 5 : -161)) * 1.2) - allCalories;

            return Json(new
            {
                caloriesNeeded,
                allCalories,
                fats,
                protein,
                carbs
            }, JsonRequestBehavior.AllowGet);
        }
    }
}