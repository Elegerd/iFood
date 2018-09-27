using Lifestyle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Lifestyle.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
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
                    return RedirectToAction("ProfileUser", "Account");
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
                        db.Users.Add(new User { Email = model.Login, Password = model.Password, BirthDate = model.BirthDate, Name=model.Name, });
                        db.SaveChanges();

                        user = db.Users.Where(u => u.Email == model.Login && u.Password == model.Password).FirstOrDefault();
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
        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }


        [Authorize]
        public ActionResult ProfileUser()
        {          
           /* ViewBag.Message = User.Identity.Name;
          
                tableData = db.Zakazs.Find(id);
                if (tableData != null)
                {
                    if (tableData.AuthorId == User.Identity.Name)
                    {
                        PrivateTable.Add(new Zakaz() { Id = tableData.Id, AuthorId = tableData.AuthorId, Text = tableData.Text, Title = tableData.Title });
                    }
                }
            }
            ViewBag.Message2 = PrivateTable;*/
            return View();
        }

    }
}