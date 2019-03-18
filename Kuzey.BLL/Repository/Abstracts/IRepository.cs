using Kuzey.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kuzey.BLL.Repository.Abstracts
{
    public interface IRepository<T,TId> where T:BaseEntity<TId>
    {
        List<T> GetAll();
        List<T> GetAll(Func<T,bool> predicate);
        List<T> GetAll(params string[] includes);
        List<T> GetAll(Func<T,bool> predicate, params string[] includes);
        T GetById(T Id);
        void Insert(T entity);
        void Update(T entity);
         
    }
}
