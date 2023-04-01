using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ders68_iakademi45Proje.Models.MVVM
{
    public class Category
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryID { get; set; }
        public int ParentID { get; set; }

        [StringLength(100, ErrorMessage = "Kategori Adı Zorunlu")]
        [Required(ErrorMessage = "Kategori Adı Zorunlu")]
        public string? CategoryName { get; set; }

        [DisplayName("Aktif/Pasif")]
        public bool Active { get; set; }
    }
}
