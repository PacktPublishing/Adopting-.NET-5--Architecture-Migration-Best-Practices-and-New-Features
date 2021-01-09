using BookApp.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace BookApp.DAL
{
    public class BooksDBContext:DbContext
    {
        public BooksDBContext():base("BooksDB")
        {
            Database.SetInitializer(new BooksDBInitialiser());
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookReview> BookReviews { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }
}
