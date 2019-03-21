using Kuzey.BLL.Repository.Abstracts;
using Kuzey.DAL;
using Kuzey.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kuzey.BLL.Repository
{
    public class ProductRepo: RepositoryBase<Product,string>
    {
        private readonly MyContext _dbContext;
        public ProductRepo(MyContext dbContext): base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
