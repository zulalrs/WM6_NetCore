using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Kuzey.Models.IdentityEntities
{
    public class ApplicationUser : IdentityUser //<> yazarak içine bir key tipi verebiliriz ve user tablomuzun id si o tipte olur. Defaultta ise  bu tip stringdir. EntityFrawork Core da Id kolonunu string olarak ayarlarsak onu veri tabanında Guid olarak ayarlıyor ve kendi Guid üretiyor.
    {
        // User tablosuna ekleme yapacak. IdentityUser yapısını burada geliştirmiş olduk.
        [Required, StringLength(50)]
        public string Name { get; set; }
        [Required, StringLength(50)]
        public string Surname { get; set; }
        public DateTime RegisterDate { get; set; } = DateTime.Now;
    }
}
