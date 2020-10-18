using LibraryMamagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryMamagementSystem.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        LibraryDB db = new LibraryDB();
        public ActionResult Index()
        {
            var data = db.Categories.ToList();
            return View(data);
        }
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult InsertCategory(Category category)
        {

            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }


        public ActionResult Delete(int? id)
        {
            var data = db.Categories.Find(id);
            if (data != null)
            {
                db.Categories.Remove(data);
                db.SaveChanges();
            }
            return RedirectToAction("/");
        }
        public ActionResult Details(int? id)
        {
            var data = db.Categories.Find(id);
            if (data == null)
            {
                Response.Redirect("/Index");
            }
            return View(data);

        }

    }
}