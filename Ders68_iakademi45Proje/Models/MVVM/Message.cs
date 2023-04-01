using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ders68_iakademi45Proje.Models.MVVM
{
    public class Message
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MessageID { get; set; }
        public int UserID { get; set; }
        public int ProductID { get; set; }
        [Required]
        public string? Content { get; set; }
    }
}
