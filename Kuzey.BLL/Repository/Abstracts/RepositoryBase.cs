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
    }
}
