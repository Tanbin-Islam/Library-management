namespace LibraryMamagementSystem.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LibraryMamagementSystem.Models.LibraryDB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(LibraryMamagementSystem.Models.LibraryDB context)
        {
            context.Books.AddOrUpdate(x => x.BookId,
          new Models.Book() { BookName = "Deyal", AuthorName = "Humayan Ahmed", Price = 150, CategoryId = 1 },
             new Models.Book() { BookName = "MisirAli", AuthorName = "Humayan Ahmed", Price = 650, CategoryId = 2 }
          );


            context.Categories.AddOrUpdate(x => x.CategoryId,
                new Models.Category()
                {
                    CategoryId = 1,
                    CategoryName = "Story"
                },

                 new Models.Category()
                 {
                     CategoryId = 2,
                     CategoryName = "Nobel",

                 },
                  new Models.Category()
                  {
                      CategoryId = 3,
                      CategoryName = "Fiction",

                  },
              new Models.Category()
              {
                  CategoryId = 4,
                  CategoryName = "Tragedy",

              });


            context.Users.AddOrUpdate(x => x.UserId,
               new Models.User() { UserName = "Tanbin", Password = "123", Role = "Admin" },
                  new Models.User() { UserName = "Tania", Password = "1234", Role = "User" }
               );

           
        }
    }
}
