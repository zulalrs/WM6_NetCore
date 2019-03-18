using Kuzey.Models.IdentityEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kuzey.DAL
{
    public class MyContext: IdentityDbContext
    {
        public MyContext(DbContextOptions<MyContext> options)
            : base(options)
        {
        }
    }
}
