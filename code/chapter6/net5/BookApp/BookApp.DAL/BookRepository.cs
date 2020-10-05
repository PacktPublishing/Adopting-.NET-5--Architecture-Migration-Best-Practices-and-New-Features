using BookApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace BookApp.DAL
{
    public interface IBookRepository
    {
        Book UpsertBook(Book book);
        IEnumerable<Book> GetBooks();
        void DeleteBook(int Id);
        Book GetBookById(int? Id);
        BookReview AddReviewToBook(int id, BookReview bookReview);
        BookReview GetBookReviewById(int? id);
        BookReview UpdateBookReview(BookReview bookReview);
        void DeleteBookReview(int id);
    }

    public class BookRepository : IBookRepository
    {
        private DbContextOptions<BooksDBContext> dbOptions;
        public BookRepository(DbContextOptions<BooksDBContext> dbOptions)
        {
            this.dbOptions = dbOptions;
        }

        public IEnumerable<Book> GetBooks()
        {
            using (var context = new BooksDBContext(dbOptions))
            {
                return context.Books
                .Include(b => b.Reviews)
                .ToList();
            }
        }

        public Book UpsertBook(Book book)
        {
            using (var context = new BooksDBContext(dbOptions))
            {
                if (book.Id <= 0)
                    context.Books.Add(book);
                else
                    context.Entry(book).State = EntityState.Modified;
                context.SaveChanges();
                return book;
            }

        }

        public void DeleteBook(int Id)
        {
            using (var context = new BooksDBContext(dbOptions))
            {
                var book = context.Books.FirstOrDefault(b => b.Id == Id);
                if (book == null)
                    throw new DataException($"No book found with id {Id}");
                context.Books.Remove(book);
                context.SaveChanges();
            }
        }

        public Book GetBookById(int? Id)
        {
            using (var context = new BooksDBContext(dbOptions))
            {
                if (!Id.HasValue)
                    return null;
                return context.Books.Include(b => b.Reviews).FirstOrDefault(b => b.Id == Id.Value);
            }
        }

        public BookReview AddReviewToBook(int id, BookReview bookReview)
        {
            using (var context = new BooksDBContext(dbOptions))
            {
                if (bookReview.Id > 0 || bookReview.Book != null)
                    throw new DataException("The bookreview id must be 0 for new reviews");

                var book = GetBookById(id);
                if (book == null)
                    throw new DataException($"No book found with id {id}");

                book.Reviews.Add(bookReview);
                context.SaveChangesAsync();
                return bookReview;
            }
        }

        public BookReview GetBookReviewById(int? Id)
        {
            using (var context = new BooksDBContext(dbOptions))
            {
                if (!Id.HasValue)
                    return null;
                return context.BookReviews.Include(b => b.Book).FirstOrDefault(b => b.Id == Id.Value);
            }
        }

        public BookReview UpdateBookReview(BookReview bookReview)
        {
            using (var context = new BooksDBContext(dbOptions))
            {
                var bookReviewDb = GetBookReviewById(bookReview.Id);
                bookReviewDb.Title = bookReview.Title;
                bookReviewDb.Review = bookReview.Review;
                bookReviewDb.Rating = bookReview.Rating;
                context.SaveChanges();
                return bookReviewDb;
            }
        }

        public void DeleteBookReview(int id)
        {
            using (var context = new BooksDBContext(dbOptions))
            {
                BookReview bookReview = GetBookReviewById(id);
                if (bookReview == null)
                    throw new DataException($"No BookReview found with id {id}");
                context.BookReviews.Remove(bookReview);
                context.SaveChanges();
            }

        }
    }
}
