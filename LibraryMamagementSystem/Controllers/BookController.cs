using LibraryMamagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using Formatting = Newtonsoft.Json.Formatting;
using Newtonsoft.Json;

namespace LibraryMamagementSystem.Content
{
    public class BookController : Controller
    {
        // GET: Book

        LibraryDB db = new LibraryDB();
        //Searching, Sorting, Paging Index
       // [Authorize(Roles = "Admin,User")]
        public ActionResult SearchSortPagingIndex(string SearchString, string CurrentFilter, string SortOrder, int? Page)
        {
            ViewBag.SortNameParam = string.IsNullOrEmpty(SortOrder) ? "name_desc" : " ";
            //Searching
            if (SearchString != null)
            {
                Page = 1;
            }
            else
            {
                SearchString = CurrentFilter;
            }
            ViewBag.CurrentFilter = SearchString;

            var BookList = db.Books.Where(s => s.IsDeleted == false)
                .Select(s => new BookViewModel
                {
                    BookId = s.BookId,
                    BookName = s.BookName,
                    AuthorName = s.AuthorName,
                    Price = s.Price,
                    CategoryName = s.Category.CategoryName
                }).ToList();

            if (!string.IsNullOrEmpty(SearchString))
            {
                BookList = BookList.Where(s => s.BookName.ToUpper().Contains(SearchString.ToUpper())).ToList();
            }
            //Sorting
            switch (SortOrder)
            {
                case "name_desc":
                    BookList = BookList.OrderByDescending(s => s.BookName).ToList();
                    break;
                default:
                    BookList = BookList.OrderBy(s => s.BookName).ToList();
                    break;
            }
            ViewBag.CurrentSort = SortOrder;

            //Paging
            int PageSize = 2;
            int PageNumber = (Page ?? 1);
            return View(BookList.ToPagedList(PageNumber, PageSize));
        }
        //Ajax Option
        public PartialViewResult AllBooks()
        {
            System.Threading.Thread.Sleep(2000);
            List<BookViewModel> BookList = db.Books.Where(s => s.IsDeleted == false)
                 .Select(s => new BookViewModel
                 {
                     BookId = s.BookId,
                     BookName = s.BookName,
                     AuthorName = s.AuthorName,
                     Price = s.Price,
                     CategoryName = s.Category.CategoryName
                 }).ToList();
            return PartialView("_BookList", BookList);
        }

        public PartialViewResult AllBookAsc()
        {
            System.Threading.Thread.Sleep(2000);

            List<BookViewModel> BookList = db.Books.Where(s => s.IsDeleted == false)
                 .Select(s => new BookViewModel
                 {
                     BookId = s.BookId,
                     BookName = s.BookName,
                     AuthorName = s.AuthorName,
                     Price = s.Price,
                     CategoryName = s.Category.CategoryName
                 }).ToList();
            BookList = BookList.OrderBy(s => s.BookName).ToList();
            return PartialView("_BookList", BookList);
        }
        [Authorize(Roles = "Admin")]
        public PartialViewResult AllBookDsc()
        {
            System.Threading.Thread.Sleep(2000);
            List<BookViewModel> BookList = db.Books.Where(s => s.IsDeleted == false)
                 .Select(s => new BookViewModel
                 {
                     BookId = s.BookId,
                     BookName = s.BookName,
                     AuthorName = s.AuthorName,
                     Price = s.Price,
                     CategoryName = s.Category.CategoryName
                 }).ToList();
            BookList = BookList.OrderByDescending(s => s.BookName).ToList();
            return PartialView("_BookList", BookList);
        }

        public ActionResult Index()
        {
            List<Category> Clist = db.Categories.ToList();
            ViewBag.ListOfCategorys = new SelectList(Clist, "CategoryId", "CategoryName");
            return View();
        }
        public JsonResult GetBookList()
        {
            System.Threading.Thread.Sleep(2000);
            List<BookViewModel> Blist = db.Books.Where(s => s.IsDeleted == false).Select(s => new BookViewModel
            {
                BookId = s.BookId,
                BookName = s.BookName,
                AuthorName = s.AuthorName,
                Price = s.Price,
                CategoryName = s.Category.CategoryName

            }).ToList();
            return Json(Blist, JsonRequestBehavior.AllowGet);

        }
        public PartialViewResult GetDetailBookRecord(int BookId)
        {
            Book obj = db.Books.SingleOrDefault(s => s.IsDeleted == false && s.BookId == BookId);
            BookViewModel viewObj = new BookViewModel();
            viewObj.BookId = obj.BookId;
            viewObj.BookName = obj.BookName;
            viewObj.AuthorName = obj.AuthorName;
            viewObj.Price = obj.Price;
            viewObj.CategoryName = obj.Category.CategoryName;
            return PartialView("_BookDetails", viewObj);
        }
        public JsonResult GetBookById(int BookId)
        {
            Book model = db.Books.Where(s => s.BookId == BookId).SingleOrDefault();
            string value = string.Empty;
            value = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveDataInDatabase(BookViewModel model)
        {
            var result = false;
            try
            {
                if (model.BookId > 0)
                {
                    Book book = db.Books.SingleOrDefault(x => x.IsDeleted == false && x.BookId == model.BookId);
                    book.BookName = model.BookName;
                    book.AuthorName = model.AuthorName;
                    book.Price = model.Price;
                    book.CategoryId = model.CategoryId;
                    db.SaveChanges();
                    result = true;
                }
                else
                {
                    Book book = new Book();
                    book.BookName = model.BookName;
                    book.AuthorName = model.AuthorName;
                    book.Price = model.Price;
                    book.CategoryId = model.CategoryId;
                    book.IsDeleted = false;
                    db.Books.Add(book);
                    db.SaveChanges();
                    result = true;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Json(result, JsonRequestBehavior.AllowGet);

        }
        //public JsonResult DeleteBookRecord(int BookId)
        //{
        //    bool result = false;
        //    Book bk = db.Books.SingleOrDefault(x => x.IsDeleted == false && x.BookId == BookId);
        //    if (bk != null)
        //    {
        //        bk.IsDeleted = true;
        //        db.SaveChanges();
        //        result = true;

        //    }
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult DeleteBookRecord(int BookId)
        {
            bool result = false;
            Book bk = db.Books.SingleOrDefault(x => x.IsDeleted == false && x.BookId == BookId);
            if (bk != null)
            {
                bk.IsDeleted = true;
                db.SaveChanges();
                result = true;

            }
            return RedirectToAction("Index");
        }
    }
}