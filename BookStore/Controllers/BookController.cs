using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;
using BookStore.Models.repo;
using BookStore.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookStoreRepo<Book> bookRepo;
        private readonly IBookStoreRepo<Author> authorRepo;
        private readonly IHostingEnvironment hosting;

        public BookController(IBookStoreRepo<Book> bookRepo , IBookStoreRepo<Author> authorRepo , IHostingEnvironment hosting)
        {
            this.bookRepo = bookRepo;
            this.authorRepo = authorRepo;
            this.hosting = hosting;
        }
        // GET: Book
        public ActionResult Index()
        {
            var books = bookRepo.List();
            return View(books);
        }

        // GET: Book/Details/5
        public ActionResult Details(int id)
        {
            var book = bookRepo.Find(id);
            return View(book);
        }

        // GET: Book/Create
        public ActionResult Create()
        {
            var model = new BookAuthorViewModel()
            {
                Authors = GetAllAuthors().Authors
            };
            
            return View(model);
        }

        // POST: Book/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookAuthorViewModel BAVM)
        {
            if (ModelState.IsValid)
            {


                try
                {
                    // TODO: Add insert logic here
                    string fileName = UploadFile(BAVM.File) ?? string.Empty;
                    if (BAVM.AuthorID == -1)
                    {
                        ViewBag.Message = "please select an author";
                        var model = new BookAuthorViewModel()
                        {
                            Authors = GetAllAuthors().Authors
                        };
                        return View(model);
                    }
                    var author = authorRepo.Find(BAVM.AuthorID);
                    Book book = new Book()
                    {
                        Title = BAVM.Title,
                        Description = BAVM.Description,
                        Author = author,
                        ImageURL = fileName
                    };

                    bookRepo.Add(book);

                    return RedirectToAction(nameof(Index));
                }

                catch
                {
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("", "You have to fill all the requierd field");
                return View(GetAllAuthors());
            }
        }

        // GET: Book/Edit/5
        public ActionResult Edit(int id)
        {
            var book = bookRepo.Find(id);
            var authorID = book.Author == null ? 0 : book.Author.ID;
            BookAuthorViewModel BAVM = new BookAuthorViewModel() {
                ID = book.ID,
                Title = book.Title,
                AuthorID = authorID,
                Description = book.Description,
                Authors = GetAllAuthors().Authors,
                ImgURL = book.ImageURL
            };
            return View(BAVM);
        }

        // POST: Book/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BookAuthorViewModel BAVM)
        {
            
            try
            {
                // TODO: Add update logic here

                string fileName = UploadFile(BAVM.File, BAVM.ImgURL);

                if (BAVM.AuthorID == -1)
                {
                    BAVM.Authors = GetAllAuthors().Authors;
                    ViewBag.Message = "please select an author";
                    return View(BAVM);
                }
                var author = authorRepo.Find(BAVM.AuthorID);

                Book book = new Book()
                {
                    ID = BAVM.ID,
                    Title = BAVM.Title,
                    Description = BAVM.Description,
                    Author = author,
                    ImageURL = fileName
                };
                bookRepo.Update(BAVM.ID, book);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception EX)
            {
                return View();
            }
        }

        // GET: Book/Delete/5
        public ActionResult Delete(int id)
        {
            var book = bookRepo.Find(id);
            return View(book);
        }

        // POST: Book/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(int id)
        {
            try
            {
                // TODO: Add delete logic here
                bookRepo.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Search(string term)
        {
            var result = bookRepo.search(term);
            return View("Index" , result);
        }


        //List<Author> FillSelectList()
        //{
        //    var authors = authorRepo.List().ToList();
        //    authors.Insert(0, new Author { ID = -1, FullName = "--- please select an author ---" });
        //    return authors;
        //}
        BookAuthorViewModel GetAllAuthors()
        {
            var authors = authorRepo.List().ToList();
            authors.Insert(0, new Author { ID = -1, FullName = "--- please select an author ---" });
            var model = new BookAuthorViewModel()
            {
                Authors = authors
            };
            return model;
        }

        string UploadFile(IFormFile file)
        {
            if (file != null)
            {
                string uploads = Path.Combine(hosting.WebRootPath, "images");
                string fullPath = Path.Combine(uploads, file.FileName);
                file.CopyTo(new FileStream(fullPath, FileMode.Create));
                return file.FileName;
            }
            else
                return null;

        }

        string UploadFile(IFormFile File , string ImageURL)
        {
            if (File != null)
            {
                string uploads = Path.Combine(hosting.WebRootPath, "images");
                string newPath = Path.Combine(uploads, File.FileName);
                //Delete the old file
                string oldPath = Path.Combine(uploads, ImageURL);
                if (oldPath != newPath)
                {
                    System.IO.File.Delete(oldPath);
                    // Save a new file 
                    File.CopyTo(new FileStream(newPath, FileMode.Create));
                }
                return File.FileName;
            }
            else
                return ImageURL;
            }
    }
}