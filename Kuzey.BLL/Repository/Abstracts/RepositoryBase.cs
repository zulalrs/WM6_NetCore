using Kuzey.DAL;
using Kuzey.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kuzey.BLL.Repository.Abstracts
{
    public abstract class RepositoryBase<T,TId>:IRepository<T,TId> where T:BaseEntity<TId> // Bu class IRepositoryden kalıtım alacak ve onun içindeki metodlar implement edilmek zorunda.
    {
        private readonly MyContext DbContext;
        private readonly DbSet<T> DbObject;

        internal RepositoryBase(MyContext dbContext)
        {
            DbContext = dbContext;
            DbObject = dbContext.Set<T>();
        }

        // Temel CRUD işlemlerinin metodlarını implement ettikten sonra içlerini burada doldurduk.
        public List<T> GetAll()
        {
            return DbObject.ToList();
        }

        public List<T> GetAll(Func<T, bool> predicate)
        {
            return DbObject.Where(predicate).ToList();
        }

        public List<T> GetAll(params string[] includes)
        {
            foreach (var inc in includes)
            {
                DbObject.Include(inc);
            }
            return DbObject.ToList();
        }

        public List<T> GetAll(Func<T, bool> predicate, params string[] includes)
        {
            foreach (var inc in includes)
            {
                DbObject.Include(inc);
            }
            return DbObject.Where(predicate).ToList();
        }

        public T GetById(T Id)
        {
            return DbObject.Find(Id);
        }

        public int Insert(T entity)
        {
            DbObject.Add(entity);
            return DbContext.SaveChanges();
        }
        public void Delete(T entity)
        {
            DbObject.Remove(entity);
            DbContext.SaveChanges();
        }
        public void Update(T entity)
        {
            DbObject.Attach(entity);
            DbContext.Entry(entity).State = EntityState.Modified;
            this.Save();
        }
        public void Save()
        {
            DbContext.SaveChanges();
        }

        public IQueryable<T> Queryable()
        {
            return DbObject.AsQueryable();
        }

        public IQueryable<T> Queryable(params string[] includes)
        {
            foreach (var inc in includes)
            {
                DbObject.Include(inc);
            }
            return DbObject.AsQueryable();
        }
    }
}
