using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kuzey.Models.Entities
{
    [Table("Products")]
    public class Product : BaseEntity<string> // Buraya string yazarsak Guid ye benzer şekilde bir string id üretiyormuş.  // BaseEntity sınıfından kalıtım aldık. Buradaki <tip> id mizin tipini temsil ediyor.
    {
        [Required, StringLength(50)]
        public string ProductName { get; set; }

        public decimal UnitPrice { get; set; }
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public virtual Category Category { get; set; } // Navigation propertyimizi yazdık.
    }
}
