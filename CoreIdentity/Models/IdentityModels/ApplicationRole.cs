using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreIdentity.Models.IdentityModels
{
    public class ApplicationRole : IdentityRole
    {
        [Required, StringLength(128)]
        public string Description { get; set; }
    }
}
