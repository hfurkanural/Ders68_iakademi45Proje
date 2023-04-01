using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ders68_iakademi45Proje.Models.MVVM
{
    public class Setting
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SettingID { get; set; }
        public string? telephone { get; set; }
        [EmailAddress]
        public string? email { get; set; }
        public string? address { get; set; }
        public int mainpageCount { get; set; }
        public int subpageCount { get; set; }
    }
}
