using Ders68_iakademi45Proje.Models.MVVM;

namespace Ders68_iakademi45Proje.Models
{
    public class cls_Orders
    {
        iakademi45Context context = new iakademi45Context();
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public string? MyCart { get; set; }
        public decimal UnitPrice { get; set; }
        public string? ProductName { get; set; }
        public int KDV { get; set; }
        public string PhotoPath { get; set; }

        //sepete ekle metodu
        //10 numaralı ürün = 1 adet
        //20 numaralı ürün = 1 adet
        //sepete ekle
        //herhangi bir sayfada ürünü sepete ekle butonu tıklanınca
        public bool AddToMyCart(string id)
        {
            bool exists = false;
            if (MyCart == "")
            {
                MyCart = id + "=1";
            }
            else
            {
                string[] MyCartArray = MyCart.Split('&');
                for (int i = 0; i < MyCartArray.Length; i++)
                {
                    //for ilk defa dönerken içinde 10=1 kaydı var
                    //for ikinci defa dönerken içinde 20=1 kaydı var
                    //for üçüncü defa dönerken içinde 18=1 kaydı var
                    string[] MyCartArrayLoop = MyCartArray[i].Split('=');
                    if (MyCartArrayLoop[0] == id)
                    {
                        //bu ürün sepette var demektir
                        exists = true;
                    }
                }
                if (exists == false)
                {
                    MyCart = MyCart + "&" + id.ToString() + "=1";
                }
            }
            return exists;
        }
        //sepet sayfasında ürünün bütün b ilgilerini getiren metod
        public List<cls_Orders> SelectMyCart()
        {
            //10 numaralı ürün = 1 adet&
            //20 numaralı ürün = 1 adet&

            List<cls_Orders> list = new List<cls_Orders>();
            string[] MyCartArray = MyCart.Split('&');
            if (MyCartArray[0] != "")
            {
                for (int i = 0; i < MyCartArray.Length; i++)
                {
                    string[] MyCartArrayLoop = MyCartArray[i].Split('=');
                    int MyCartID = Convert.ToInt32(MyCartArrayLoop[0]);
                    Product? prd = context.Products.FirstOrDefault(p => p.ProductID == MyCartID);

                    //veri tabanındaki verileri propertylere koydum
                    cls_Orders ord = new cls_Orders();
                    ord.ProductID = prd.ProductID;
                    ord.Quantity = Convert.ToInt32(MyCartArrayLoop[1]);
                    ord.UnitPrice = prd.UnitPrice;
                    ord.ProductName = prd.ProductName;
                    ord.PhotoPath = prd.PhotoPath;
                    ord.KDV = prd.KDV;
                    list.Add(ord);
                }
            }
            return list;
        }
        //sepetten sil
        public void DeleteFromMyCart(string id)
        {
            string[] MyCartArray = MyCart.Split('&');
            string newMyCartArray = "";
            int count = 1;
            for (int i = 0; i < MyCartArray.Length; i++)
            {
                //productID ile adet ayrıldı
                string[] MyCartArrayLoop = MyCartArray[i].Split('=');
                //for her döndüğünde dizinn sıfırıncı alanındaki değeri (10,20,30,40) mycartID değişkenine atadım
                string MyCartID = MyCartArrayLoop[0];
                if (MyCartID != id)
                {
                    //sepetten silinmeyecek olanlar buraya girecek
                    if (count == 1)
                    {
                        newMyCartArray = MyCartArrayLoop[0] + "=" + MyCartArrayLoop[1];
                        count++;
                    }
                    else
                    {
                        newMyCartArray += "&" + MyCartArrayLoop[0] + "=" + MyCartArrayLoop[1];
                    }
                }
                //else
                //{
                //    //buraya girerse bu silinecek olan üründür. bunu newMyCartArray içine eklemeyeceğim
                //    //sepetten silinecek olan buraya girecek

                //}
            }
            MyCart = newMyCartArray;
        }
    }
}
