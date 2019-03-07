using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreGiris.Models
{
    public class MyContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=UrunYonetimiDb;Trusted_Connection=True;"); //  WebConfig veya AppConfig classımız olmadıgı için connectionstringimizi buraya yazıyoruz.
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .Property(x => x.UnitPrice)
                .HasColumnType("decimal(7,2)"); // Fiyat sütununa girilecek verilerin uzunluğunu burada belirledik. Toplamda uzunluk 7 olmalı ama 2 basamagı virgulden sonraki rakamlardan oluşuyor.
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
    }
}
