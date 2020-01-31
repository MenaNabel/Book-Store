﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.repo
{
    public interface IBookStoreRepo<TEntity>
    {
        IList<TEntity> List();
        TEntity Find(int id);
        void Add(TEntity entity);
        void Update(int id , TEntity entity);
        void Delete(int id);
        List<TEntity> search(string term);

    }
}
