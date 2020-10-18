using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryMamagementSystem.Models
{
    public class BookViewModel
    {
        [Display(Name = "Book Id")]
        public int BookId { get; set; }
        [Display(Name = "Book Name")]
        public string BookName { get; set; }
        [Display(Name = "Author Name")]
        public string AuthorName { get; set; }
        [Display(Name = "Price")]
        public int Price { get; set; }
        public bool IsDeleted { get; set; }
        public int CategoryId { get; set; }
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }
    }
}