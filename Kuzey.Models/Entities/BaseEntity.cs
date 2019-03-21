using System;
using System.ComponentModel.DataAnnotations;

namespace Kuzey.Models.Entities
{
    // Tüm Poco classlarımızda ortak olarak kullanılacakları bu class içerisinde yazdık.
    public abstract class BaseEntity<T>:AuditEnity
    {
        [Key]
        public T Id { get; set; }
    }
    // Giriş yapan kullanıcının id sini yakalamak için BaseEntity sınıfını böldük.
    public abstract class AuditEnity
    {
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [StringLength(450)]
        public string CreatedUserId { get; set; }
        public DateTime? UpdatedDate { get; set; } 
        [StringLength(450)]
        public string UpdatedUserId { get; set; }
    }
}
