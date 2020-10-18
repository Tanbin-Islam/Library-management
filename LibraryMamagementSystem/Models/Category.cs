using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryMamagementSystem.Models
{
    public class Category
    {
        public Category()
        {
            this.Books = new HashSet<Book>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }


        public virtual ICollection<Book> Books { get; set; }
    }
}