﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.repo
{
    public class BookDBRepo : IBookStoreRepo<Book>
    {
        BookStoreDbContext db;
        public BookDBRepo(BookStoreDbContext _db)
        {
            db = _db;
        }
        public void Add(Book entity)
        {
            db.Books.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var book = Find(id);
            db.Books.Remove(book);
            db.SaveChanges();

        }


        public Book Find(int id)
        {
            var book = db.Books.Include(a=>a.Author).SingleOrDefault(b => b.ID == id);
            return book;
        }

        public IList<Book> List()
        {
            return db.Books.Include(a=>a.Author).ToList();
        }

        public void Update(int id, Book entity)
        {
            db.Books.Update(entity);
            db.SaveChanges();
        }

        public List<Book> search(string term)
        {
            var result = db.Books.Include(a => a.Author)
                .Where(
                b => b.Title.Contains(term) || b.Description.Contains(term) || b.Author.FullName.Contains(term)
                );
            return result.ToList();
        }

    }
}
