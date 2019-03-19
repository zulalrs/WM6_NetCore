using Kuzey.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kuzey.BLL.Repository.Abstracts
{
    public interface IRepository<T,TId> where T:BaseEntity<TId> // Generic olarak bir tip gelecek ve o tipinde id tipi olacak. T tipi baseentity tipinde olacak 
    {
        List<T> GetAll(); // Tüm listeyi getiren metod
        List<T> GetAll(Func<T,bool> predicate); // Şart yazarak bir liste getiren metod
        List<T> GetAll(params string[] includes);
        List<T> GetAll(Func<T,bool> predicate, params string[] includes);
        T GetById(T Id); // id getiren metod
        int Insert(T entity); // ekleme metodu
        void Delete(T entity); // silme metodu
        void Update(T entity);  // guncelleme metodu
        void Save(); // Kaydetme metodu
        IQueryable<T> Queryable();
        IQueryable<T> Queryable(params string[] includes);
    }
}
