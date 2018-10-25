using Lifestyle.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Lifestyle.Controllers
{
    public class AccountController : Controller
    {
        private UserContext db = new UserContext();

        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated == true)
            {
                return Redirect("~/Account/ProfileUser");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Authorization model)
        {
            if (ModelState.IsValid)
            {
                // поиск пользователя в бд
                User user = null;
                using (UserContext db = new UserContext())
                {
                    user = db.Users.FirstOrDefault(u => u.Email == model.Login && u.Password == model.Password);

                }
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Login, true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
                }
            }
            return View(model);
        }

        public ActionResult Register()
        {
            if (User.Identity.IsAuthenticated == true)
            {
                return Redirect("~/Account/ProfileUser");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Authentication model)
        {
            if (ModelState.IsValid)
            {
                User user = null;
                using (UserContext db = new UserContext())
                {
                    user = db.Users.FirstOrDefault(u => u.Email == model.Login);
                }
                if (user == null)
                {
                    // создаем нового пользователя
                    using (UserContext db = new UserContext())
                    {
                        db.Users.Add(new User { Email = model.Login, Password = model.Password, BirthDate = model.BirthDate });
                        db.SaveChanges();
                        user = db.Users.Where(u => u.Email == model.Login && u.Password == model.Password && u.BirthDate == model.BirthDate).FirstOrDefault();
                    }
                    // если пользователь удачно добавлен в бд
                    if (user != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.Login, true);
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с таким логином уже существует");
                }
            }
            return View(model);
        }

        [Authorize]
        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult ProfileUser(User user)
        {
            string email = User.Identity.Name;
            user = db.Users.FirstOrDefault(x => x.Email == email);

            if (user != null)
            {
                if (user.Sex == null)
                    ViewData["Sex"] = "Не выбран";
                else
                    ViewData["Sex"] = user.Sex == true ? "Мужской" : "Женский";

                ViewData["Height"] = user.Height == null ? "Не выбран" : user.Height.ToString();
                ViewData["Weight"] = user.Weight == null ? "Не выбран" : user.Weight.ToString();

                return View(user);
            }
            return HttpNotFound();
        }

        [HttpGet]
        public ActionResult EditUser()
        {
            User user = db.Users.FirstOrDefault(x => x.Email == User.Identity.Name);

            if (user != null)
            {
                return View(user);
            }
            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult EditUser(User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ProfileUser");
            }
            return View(user);
        }
    }
}