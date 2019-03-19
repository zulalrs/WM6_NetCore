using Kuzey.BLL.Repository.Abstracts;
using Kuzey.DAL;
using Kuzey.Models.Entities;

namespace Kuzey.BLL.Repository
{
    public class CategoryRepo:RepositoryBase<Category,int>
    {
        private readonly MyContext _dbContext;
        public CategoryRepo(MyContext dbContext): base(dbContext)   // Bizim Repositoryimizde sadece parametreli constructor oldugu için bunu yazmamız gerekiyor yoksa hata veriyor.
        {
           _dbContext = dbContext;

        }
    }
}
