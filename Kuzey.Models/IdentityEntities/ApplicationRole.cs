using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Kuzey.Models.IdentityEntities
{
    public class ApplicationRole : IdentityRole
    {
        // Role tablosuna ekleme yapacak. IdentityRole yapısını burada geliştirmiş olduk.
        [StringLength(128)]
        public string Description { get; set; }
    }
}
