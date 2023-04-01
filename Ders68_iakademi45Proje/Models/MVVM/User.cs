using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ders68_iakademi45Proje.Models.MVVM
{
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }

        [StringLength(100)]
        [Required]
        public string? NameSurname { get; set; }

        [EmailAddress]
        [Required]
        [StringLength(100)]
        public string? Email { get; set; }

        [StringLength(100)]
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [StringLength(100)]
        public string? Telephone { get; set; }

        [StringLength(100)]
        public string? InvoiceAddress { get; set; }
        public bool IsAdmin { get; set; }
        public bool Active { get; set; }
    }
}