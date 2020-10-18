using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LibraryMamagementSystem.Models
{
    public class LibraryDB:DbContext
    {
        public LibraryDB() : base("LibraryDB") { }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Member> Members { get; set; }
    }
}