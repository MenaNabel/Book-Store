using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.repo
{
    public class BookRepo : IBookStoreRepo<Book>
    {
        List<Book> books;
        public BookRepo()
        {
            books = new List<Book>()
            {
                new Book{ID = 1 , Title = "C#" , Description = "Nothing" , Author = new Author() , ImageURL = "InnovEgy.jpeg"} ,
                new Book{ID = 2 , Title = "C++" , Description = "Nothing" , Author = new Author() , ImageURL = "14.png"} ,
                new Book{ID = 3 , Title = "Java" , Description = "Nothing" , Author = new Author() } , 
            };
        }
        public void Add(Book entity)
        {
            books.Add(entity);
        }

        public void Delete(int id)
        {
            var book = Find(id);
            books.Remove(book);
        }


        public Book Find(int id)
        {
            var book = books.SingleOrDefault(b => b.ID == id);
            return book;
        }

        public IList<Book> List()
        {
            return books;
        }

        public List<Book> search(string term)
        {
            throw new NotImplementedException();
        }

        public void Update(int id , Book entity)
        {
            var book = Find(id);
            book.Title = entity.Title;
            book.Author = entity.Author;
            book.Description = entity.Description;
            book.ImageURL = entity.ImageURL;
        }
    }
}
