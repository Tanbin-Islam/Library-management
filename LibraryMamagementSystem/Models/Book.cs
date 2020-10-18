using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryMamagementSystem.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public int Price { get; set; }
        public bool IsDeleted { get; set; }
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}