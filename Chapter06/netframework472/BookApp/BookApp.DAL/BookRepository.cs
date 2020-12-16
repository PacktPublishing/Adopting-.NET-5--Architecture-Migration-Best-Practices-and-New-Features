using BookApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data;

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
        private BooksDBContext context;
        public BookRepository(BooksDBContext context)
        {
            this.context = context;
        }

        public IEnumerable<Book> GetBooks()
        {
            return context.Books
                .Include(b=>b.Reviews)
                .ToList();
        }

        public Book UpsertBook(Book book)
        {
            if (book.Id <=0)
                context.Books.Add(book);
            context.SaveChanges();
            return book;

        }

        public void DeleteBook(int Id)
        {
            var book = context.Books.FirstOrDefault(b => b.Id == Id);
            if (book == null)
                throw new DataException($"No book found with id {Id}");
            context.Books.Remove(book);
            context.SaveChanges();
        }

        public Book GetBookById(int? Id)
        {
            if (!Id.HasValue)
                return null;
            return context.Books.Include(b=>b.Reviews).FirstOrDefault(b => b.Id == Id.Value); 
        }

        public BookReview AddReviewToBook(int id, BookReview bookReview)
        {
            if (bookReview.Id > 0 || bookReview.Book !=null)
                throw new DataException("The bookreview id must be 0 for new reviews");

            var book = GetBookById(id);
            if (book == null)
                throw new DataException($"No book found with id {id}");

            book.Reviews.Add(bookReview);
            context.SaveChangesAsync();
            return bookReview;
        }

        public BookReview GetBookReviewById(int? Id)
        {
            if (!Id.HasValue)
                return null;
            return context.BookReviews.Include(b => b.Book).FirstOrDefault(b => b.Id == Id.Value);
        }

        public BookReview UpdateBookReview(BookReview bookReview)
        {
            var bookReviewDb = GetBookReviewById(bookReview.Id);
            bookReviewDb.Title = bookReview.Title;
            bookReviewDb.Review = bookReview.Review;
            bookReviewDb.Rating = bookReview.Rating;
            context.SaveChanges();
            return bookReviewDb;
        }

        public void DeleteBookReview(int id)
        {
            BookReview bookReview = GetBookReviewById(id);
            if (bookReview == null)
                throw new DataException($"No BookReview found with id {id}");
            context.BookReviews.Remove(bookReview);
            context.SaveChanges();

        }
    }
}
