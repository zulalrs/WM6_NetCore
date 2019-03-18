using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kuzey.Models.Entities
{
    [Table("Categories")]
    public class Category : BaseEntity<int> // BaseEntity sınıfından kalıtım aldık. Buradaki <tip> id mizin tipini temsil ediyor.
    {
        [Required, StringLength(50)]
        public string CategoryName { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>(); // Navigation propertynin çokluk kısmını buraya yazdık.
    }
}
