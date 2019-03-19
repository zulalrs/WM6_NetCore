﻿using Kuzey.Models.Entities;
using Kuzey.Models.IdentityEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Kuzey.DAL
{
    public class MyContext : IdentityDbContext<ApplicationUser,ApplicationRole,string>  // Bu sınıftan kalıtım alıyor.
    {
        public MyContext(DbContextOptions<MyContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
    }
}
