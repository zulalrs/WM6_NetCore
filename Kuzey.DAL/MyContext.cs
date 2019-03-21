using Kuzey.Models.Entities;
using Kuzey.Models.IdentityEntities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;

namespace Kuzey.DAL
{
    public class MyContext : IdentityDbContext<ApplicationUser,ApplicationRole,string>  // Bu sınıftan kalıtım alıyor.
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public MyContext(DbContextOptions<MyContext> options, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        // SaveChanges metodunu kullanarak giriş yapan kullanıcıyı bulma işlemleri
        public override int SaveChanges()
        {
            // ChangeTracker context  nesnesi içerisinde geliyor ve Crud işlemeleri içerisindeki Entryleri donuyor. 
            var selectedEntityList = ChangeTracker.Entries().Where(x=>x.Entity is AuditEnity && x.State==EntityState.Added);  // Dönen entrylerdeki state i Add olanları yani eklenmiş olanları  ve AuditEntity olanları çektik.
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            foreach (var entity in selectedEntityList)
            {
                // Yukarıdan gelen listedeki her bir elemanın entity sini AuditEntity ye çevirdik. Bize oradan 4 tane property geldi ve onlardan CreatedUserId ye giriş yapan kullanıcının Id sini atadık.
                ((AuditEnity)(entity.Entity)).CreatedUserId = userId;
            }

            // Burada da state i değişenleri çektik.
            selectedEntityList = ChangeTracker.Entries().Where(x => x.Entity is AuditEnity && x.State == EntityState.Modified);

            foreach (var entity in selectedEntityList)
            {
                // Gelen liste içerisinde dönerek her bir eleman için UpdatedUserId ve UpdatedDate propertylerine atama yaptık.
                ((AuditEnity)(entity.Entity)).UpdatedUserId = userId;
                ((AuditEnity)(entity.Entity)).UpdatedDate = DateTime.Now;
            }
            return base.SaveChanges();
        }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
    }
}
