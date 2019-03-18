using CoreIdentity.Models.IdentityModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CoreIdentity.Data
{
    // Normal DbContextten kalıtım almıyoruz çünkü identityin kendi entity frameworkunu kullanıyoruz.
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser,ApplicationRole,string>   // Generic olarak, oluşturduğumuz identity sınıflarını verdik. Eger string yerine başka bir tip vermek istersek ApplicationUser ve ApplicationRole sınıflarının kalıtım aldığı IdentityUser ve IdentityRole e generic olarak o tipi vermeliyiz.
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
