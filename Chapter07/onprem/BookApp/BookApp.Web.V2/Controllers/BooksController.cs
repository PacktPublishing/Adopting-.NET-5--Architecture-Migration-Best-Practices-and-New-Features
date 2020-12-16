﻿using Microsoft.AspNetCore.Mvc;
using BookApp.DAL;
using BookApp.Models;

namespace BookApp.Web.V2.Controllers
{
    public class BooksController : Controller
    {
        private IBookRepository bookRepository;

        public BooksController(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        // GET: Books
        public ActionResult Index()
        {
            return View(bookRepository.GetBooks());
        }

        // GET: Books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new BadRequestResult();
            }
            Book book = bookRepository.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Id,Title,SubTitle,DatePublished,CoverImage,Author")] Book book)
        {
            if (ModelState.IsValid)
            {
                bookRepository.UpsertBook(book);
                return RedirectToAction("Index");
            }

            return View(book);
        }

        // GET: Books/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new BadRequestResult();
            }
            Book book = bookRepository.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("Id,Title,SubTitle,DatePublished,CoverImage,Author")] Book book)
        {
            if (ModelState.IsValid)
            {
                bookRepository.UpsertBook(book);

                return RedirectToAction("Index");
            }
            return View(book);
        }

        // GET: Books/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new BadRequestResult();
            }
            Book book = bookRepository.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            bookRepository.DeleteBook(id);
            return RedirectToAction("Index");
        }
    }
}
