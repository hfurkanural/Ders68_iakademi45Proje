using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ders68_iakademi45Proje.Models.MVVM
{
    public class Product
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductID { get; set; }
        [StringLength(100)]
        [Required]
        [DisplayName("Ürün Adı")]
        public string? ProductName { get; set; }
        private decimal _UnitPrice { get; set; }
        [DisplayName("Fiyatı")]
        public decimal UnitPrice { get { return _UnitPrice; } set { _UnitPrice = Math.Abs(value); } }
        [DisplayName("Kategorisi")]
        public int CategoryID { get; set; }
        [DisplayName("Markası")]
        public int SupplierID { get; set; }
        private int _Stock { get; set; }
        public int Stock { get { return _Stock; } set { _Stock = Math.Abs(value); } }
        private int _Discount { get; set; }
        public int Discount { get { return _Discount; } set { _Discount = Math.Abs(value); } }
        [DisplayName("Statüsü")]
        public int StatusID { get; set; }
        [DisplayName("Ürün Ekleme Tarihi")]
        public DateTime AddDate { get; set; }
        public string? Keywords { get; set; }
        //encapsulation
        //math.abs negatif değeri pozitif yapar. eksi değer girilemez
        private int _KDV { get; set; }
        public int KDV
        {
            get { return _KDV; }
            set
            {
                _KDV = Math.Abs(value);
            }
        }
        [DisplayName("Öne Çıkanar")]
        public int HighLighted { get; set; }//öne çıkanlar
        [DisplayName("Çok Satanlar")]
        public int TopSeller { get; set; }//cok satanlar
        [DisplayName("Buna Bakanlar Buna Da Baktı")]
        public int Related { get; set; }//buna bakanlar
        [DisplayName("Notlar")]
        public string? Notes { get; set; }//notlar
        public string? PhotoPath { get; set; }
        public bool Active { get; set; }
    }
}
