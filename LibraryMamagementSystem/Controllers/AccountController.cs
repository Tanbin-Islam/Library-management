using LibraryMamagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace LibraryMamagementSystem.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        LibraryDB db = new LibraryDB();
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User obj)
        {
            var count = db.Users.Where(s => s.UserName == obj.UserName && s.Password == obj.Password).Count();
            if (count <= 0)
            {
                ViewBag.Message = "invalid User";
                return View();
            }
            else
            {
                FormsAuthentication.SetAuthCookie(obj.UserName, false);
                return RedirectToAction("Index", "Home");
            }


        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

    }
}