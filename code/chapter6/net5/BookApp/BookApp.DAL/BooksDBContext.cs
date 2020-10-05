using BookApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookApp.DAL
{
    public class BooksDBContext:DbContext
    {
        public BooksDBContext(DbContextOptions<BooksDBContext> options)
            : base(options)
        {
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookReview> BookReviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .ToTable("Book");

            modelBuilder.Entity<Book>()
                .HasKey(b => b.Id);

            modelBuilder.Entity<Book>()
                .Property(b => b.Id)
                .UseIdentityColumn().ValueGeneratedOnAdd(); 


            modelBuilder.Entity<BookReview>()
                .ToTable("BookReview")
                .HasKey(b => b.Id);

            modelBuilder.Entity<BookReview>()
            .Property(b => b.Id)
            .UseIdentityColumn().ValueGeneratedOnAdd();

            modelBuilder.Entity<BookReview>()
                .HasOne(b => b.Book)
                .WithMany(b => b.Reviews)
                .HasForeignKey("Book_Id");

            SeedData(modelBuilder);
        }

        private static void SeedData(ModelBuilder modelBuilder)
        {
            var books = new[]
            {
                new Book
                {
                    Id=1,
                    Author="Hammad Arif, Habib Qureshi",
                    DatePublished=new DateTime(2021,1,1),
                    Title="Introducing .NET 5",
                    SubTitle="Building & migrating apps using modern architecture on latest .NET platform"
                },
                new Book
                {
                    Id=2,
                    Author="Sebastian Raschka, Vahid Mirjalili",
                    DatePublished= new DateTime(2019,12,1),
                    Title="Python Machine Learning",
                    SubTitle="Machine learning and deep learning with Python",
                    CoverImage=Utilities.ExtractImage("pythonmachinelearning.png")
                },
                new Book
                {
                    Id=3,
                    Author="Matthew Weston",
                    DatePublished= new DateTime(2019,11,1),
                    Title="Learn Microsoft PowerApps",
                    SubTitle="Build customised business applications without writing any code",
                    CoverImage=Utilities.ExtractImage("learnpowerapps.png"),

                }
            };

            modelBuilder.Entity<Book>().HasData(books);
            modelBuilder.Entity<BookReview>().HasData(new
            {
                Book_Id = 1,
                Id = 1,
                Rating = 3,
                Title = "Aenean ut est dolor",
                Review = "Aenean ut est dolor. Curabitur in arcu vel quam mattis porta. Integer accumsan dignissim auctor. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Ut nec mauris sit amet velit facilisis elementum eget non dui. Proin sagittis tincidunt libero sit amet interdum. Duis posuere volutpat purus, vel sodales arcu sagittis non. Quisque tincidunt id mi ut posuere. Etiam semper velit non tristique efficitur. Sed consequat lobortis fermentum. Fusce ac dolor tellus"

            });
            modelBuilder.Entity<BookReview>().HasData(new
            {
                Book_Id = 1,
                Id = 2,
                Rating = 5,
                Title = "Lorem ipsum dolor sit amet",
                Review = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean congue nec ex at tincidunt. Phasellus vel porta massa. Suspendisse id orci at ex faucibus dictum. Mauris rhoncus nec leo in congue. Donec eu neque sagittis est iaculis interdum. Mauris laoreet elit non blandit tempor. Nunc elementum est sed lorem ultrices, sed tincidunt leo mattis."

            });

            modelBuilder.Entity<BookReview>().HasData(new
            {
                Book_Id = 2,
                Id = 3,
                Rating = 3,
                Title = "Lorem ipsum dolor sit amet",
                Review = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean congue nec ex at tincidunt. Phasellus vel porta massa. Suspendisse id orci at ex faucibus dictum. Mauris rhoncus nec leo in congue. Donec eu neque sagittis est iaculis interdum. Mauris laoreet elit non blandit tempor. Nunc elementum est sed lorem ultrices, sed tincidunt leo mattis."

            });
            modelBuilder.Entity<BookReview>().HasData(new
            {
                Book_Id = 2,
                Id = 4,
                Rating = 4,
                Title = "Lorem ipsum dolor sit amet",
                Review = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean congue nec ex at tincidunt. Phasellus vel porta massa. Suspendisse id orci at ex faucibus dictum. Mauris rhoncus nec leo in congue. Donec eu neque sagittis est iaculis interdum. Mauris laoreet elit non blandit tempor. Nunc elementum est sed lorem ultrices, sed tincidunt leo mattis."

            });
            modelBuilder.Entity<BookReview>().HasData(new
            {
                Book_Id = 3,
                Id = 5,
                Rating = 5,
                Title = "Lorem ipsum dolor sit amet",
                Review = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean congue nec ex at tincidunt. Phasellus vel porta massa. Suspendisse id orci at ex faucibus dictum. Mauris rhoncus nec leo in congue. Donec eu neque sagittis est iaculis interdum. Mauris laoreet elit non blandit tempor. Nunc elementum est sed lorem ultrices, sed tincidunt leo mattis."

            });

        }
    }
}
