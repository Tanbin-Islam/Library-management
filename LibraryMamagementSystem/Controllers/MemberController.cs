using LibraryMamagementSystem.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace LibraryMamagementSystem.Controllers
{
    public class MemberController : Controller
    {
        // GET: Member
        LibraryDB db = new LibraryDB();
        public ActionResult Index(string SearchString, string CurrentFilter, string SortOrder, int? page)
        {
            List<MemberViewModel> memberList = db.Members.Select(u => new MemberViewModel
            {
                MemberId = u.MemberId,
                MemberName = u.MemberName,
                Email = u.Email,
                DOB = u.DOB,
                ImageName = u.ImageName,
                ImageUrl = u.ImageUrl
            }).ToList();
            ViewBag.SortNameParam = string.IsNullOrEmpty(SortOrder) ? "name_desc" : "";
            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = CurrentFilter;
            }
            ViewBag.CurrentFilter = SearchString;

            var list = memberList;
            if (!string.IsNullOrEmpty(SearchString))
            {
                list = list.Where(u => u.MemberName.ToUpper().
                Contains(SearchString.ToUpper())).ToList();
            }
            switch (SortOrder)
            {
                case "name_desc":
                    list = list.OrderByDescending(u => u.MemberName).ToList();
                    break;
                default:
                    list = list.OrderBy(u => u.MemberName).ToList();
                    break;
            }
            int PageSize = 3;
            int PageNumber = (page ?? 1);

            return View(list.ToPagedList(PageNumber, PageSize));
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(MemberViewModel vobj)
        {
            var result = false;
            try
            {
                Member obj;
                if (vobj.MemberId == 0)
                {
                    obj = new Member();
                    obj.MemberName = vobj.MemberName;
                    obj.Email = vobj.Email;
                    obj.DOB = vobj.DOB;
                    string fileName = Path.GetFileNameWithoutExtension(vobj.ImageFile.FileName);
                    string extension = Path.GetExtension(vobj.ImageFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    vobj.ImageUrl = "~/Images/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/Images/" + fileName));
                    vobj.ImageFile.SaveAs(fileName);
                    obj.ImageName = vobj.ImageName;
                    obj.ImageUrl = vobj.ImageUrl;
                    db.Members.Add(obj);
                    db.SaveChanges();
                    result = true;
                }
                else
                {
                    obj = db.Members.SingleOrDefault(u => u.MemberId == vobj.MemberId);
                    obj.MemberName = vobj.MemberName;
                    obj.Email = vobj.Email;
                    obj.DOB = vobj.DOB;
                    string fileName = Path.GetFileNameWithoutExtension(vobj.ImageFile.FileName);
                    string extension = Path.GetExtension(vobj.ImageFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    vobj.ImageUrl = "~/Images/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/Images/" + fileName));
                    vobj.ImageFile.SaveAs(fileName);
                    obj.ImageName = vobj.ImageName;
                    obj.ImageUrl = vobj.ImageUrl;
                    db.SaveChanges();
                    result = true;

                }
                if (result)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception )
            {
                throw;
            }

        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Member obj = db.Members.SingleOrDefault(u => u.MemberId ==id);
            MemberViewModel vobj = new MemberViewModel();
            vobj.MemberName = obj.MemberName;
            vobj.Email = obj.Email;
            vobj.DOB = obj.DOB;
            vobj.ImageName = obj.ImageName;
            vobj.ImageUrl = obj.ImageUrl;
            vobj.MemberId = obj.MemberId;
            return View(vobj);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            Member obj = db.Members.SingleOrDefault(u => u.MemberId == id);
            MemberViewModel vobj = new MemberViewModel();
            vobj.MemberName = obj.MemberName;
            vobj.Email = obj.Email;
            vobj.DOB = obj.DOB;
            vobj.ImageName = obj.ImageName;
            vobj.ImageUrl = obj.ImageUrl;
            vobj.MemberId = obj.MemberId;
            return View(vobj);
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            Member obj = db.Members.SingleOrDefault(u => u.MemberId == id);
            if (obj != null)
            {
                db.Members.Remove(obj);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(obj);
            }

        }
        public PartialViewResult Details(int id)
        {
            Member obj = db.Members.SingleOrDefault(u => u.MemberId == id);
            MemberViewModel vobj = new MemberViewModel();
            vobj.MemberName = obj.MemberName;
            vobj.Email = obj.Email;
            vobj.DOB = obj.DOB;
            vobj.ImageName = obj.ImageName;
            vobj.ImageUrl = obj.ImageUrl;
            vobj.MemberId = obj.MemberId;
            ViewBag.Details = "Show";
            return PartialView("_MemberDetails", vobj);
        }
    }
}