using Kuzey.DAL;
using Kuzey.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kuzey.BLL.Repository.Abstracts
{
    public abstract class RepositoryBase<T,TId>:IRepository<T,TId> where T:BaseEntity<TId>
    {
        private readonly MyContext DbContext;

        protected RepositoryBase(MyContext dbContext)
        {
            DbContext = dbContext;
        }

        public List<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<T> GetAll(Func<T, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public List<T> GetAll(params string[] includes)
        {
            throw new NotImplementedException();
        }

        public List<T> GetAll(Func<T, bool> predicate, params string[] includes)
        {
            throw new NotImplementedException();
        }

        public T GetById(T Id)
        {
            throw new NotImplementedException();
        }

        public void Insert(T entity)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
