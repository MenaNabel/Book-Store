using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.repo
{
    public class AuthoeRepo : IBookStoreRepo<Author>
    {
        IList<Author> authors;

        public AuthoeRepo()
        {
            authors = new List<Author>()
            {
                new Author{ID = 1 , FullName = "Mena" } ,
                new Author{ID = 2 , FullName = "Besho"} ,
                new Author{ID = 3 , FullName = "fady" }
            };
            
        }

        public void Add(Author entity)
        {
            authors.Add(entity);
        }

        public void Delete(int id)
        {
            var author = Find(id);
            authors.Remove(author);

        }

        public Author Find(int id)
        {
            var author = authors.SingleOrDefault(b => b.ID == id);
            return author;
        }

        public IList<Author> List()
        {
            return authors;
        }

        public List<Author> search(string term)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, Author entity)
        {
            var author = Find(id);
            author.FullName = entity.FullName;
        }
    }
}
