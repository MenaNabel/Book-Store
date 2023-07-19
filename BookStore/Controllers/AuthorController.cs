using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;
using BookStore.Models.repo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IBookStoreRepo<Author> authorRepo;
        public AuthorController(IBookStoreRepo<Author> authorRepo)
        {
            this.authorRepo = authorRepo;
        }
        // GET: Author
        public ActionResult Index()
        {
            var authors = authorRepo.List();
            return View(authors);
        }

        // GET: Author/Details/5
        public ActionResult Details(int id)
        {
            var author = authorRepo.Find(id);
            return View(author);
        }

        // GET: Author/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Author/Create
        [HttpPost]
        [ValidateAntiForgeryToken]  
        public ActionResult Create(Author newAuthor)
        {
            try
            {
                // TODO: Add insert logic here
                authorRepo.Add(newAuthor);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();   
            }
        }

        // GET: Author/Edit/5
        public ActionResult Edit(int id)
        {
            var author = authorRepo.Find(id);
            return View(author);
        }

        // POST: Author/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Author author)
        {
            try
            {
                // TODO: Add update logic here
                authorRepo.Update(id, author); 
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Author/Delete/5
        public ActionResult Delete(int id)
        {
            var author = authorRepo.Find(id);
            return View(author);
        }

        // POST: Author/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Author author)
        {
            try
            {
                // TODO: Add delete logic here
                authorRepo.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}