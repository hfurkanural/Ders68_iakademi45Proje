using Ders68_iakademi45Proje.Models;
using Ders68_iakademi45Proje.Models.MVVM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using PagedList.Core;

namespace Ders68_iakademi45Proje.Controllers
{
    public class HomeController : Controller
    {
        MainPageModel mpm = new MainPageModel();
        iakademi45Context context = new iakademi45Context();
        cls_Product cp = new cls_Product();
        cls_Orders o = new cls_Orders();
        int mainpageCount = 0;
        public HomeController()
        {
            mainpageCount = context.Settings.FirstOrDefault(s => s.SettingID == 1).mainpageCount;
        }
        public IActionResult Index()
        {
            mpm.Slider = cp.ProductSelect("Slider", mainpageCount, "", 0);
            mpm.Sponsored = cp.ProductDetails("Sponsored");
            mpm.New = cp.ProductSelect("New", mainpageCount, "", 0);
            mpm.Special = cp.ProductSelect("Special", mainpageCount, "", 0);
            mpm.Discounted = cp.ProductSelect("Discounted", mainpageCount, "", 0);
            mpm.Highlighted = cp.ProductSelect("Highlighted", mainpageCount, "", 0);
            mpm.TopSeller = cp.ProductSelect("TopSeller", mainpageCount, "", 0);
            mpm.Star = cp.ProductSelect("Star", mainpageCount, "", 0);
            mpm.Featured = cp.ProductSelect("Featured", mainpageCount, "", 0);
            mpm.Notable = cp.ProductSelect("Notable", mainpageCount, "", 0);

            return View(mpm);
        }
        public IActionResult Details(int id)
        {
            cls_Product.HighlightedIncrease(id);
            return View();
        }
        //nuget microsoft.aspnetcore.http
        //sepete ekle tıklanınca
        public IActionResult CartProcess(int id)
        {
            cls_Product.HighlightedIncrease(id);
            o.ProductID = id;
            o.Quantity = 1;
            var cookieOptions = new CookieOptions();
            //read
            //10 = 1 && 20 = 2 && 30 = 1 && 40=1
            var cookie = Request.Cookies["sepetim"];//tarayıcıdan aldık
            if (cookie == null)
            {
                cookieOptions = new CookieOptions();
                cookieOptions.Expires = DateTime.Now.AddDays(1); // 1 günlük çerez olur
                cookieOptions.Path = "/";
                o.MyCart = "";
                o.AddToMyCart(id.ToString());
                Response.Cookies.Append("sepetim", o.MyCart, cookieOptions);//tarayıcıya gönderdik
                HttpContext.Session.SetString("Message", "Ürün Sepetinize Eklendi");
                TempData["Message"] = "Ürün Sepetinize Eklendi";
            }
            else
            {
                o.MyCart = cookie; //tarayıcıdaki sepet bilgisini property'e gönder
                if (o.AddToMyCart(id.ToString()) == false)
                {
                    //ürün sepette daha önce eklenmemiş.ekleyelim
                    HttpContext.Response.Cookies.Append("sepetim", o.MyCart, cookieOptions);
                    cookieOptions.Expires = DateTime.Now.AddDays(1); // 1 günlük çerez olur
                    HttpContext.Session.SetString("Message", "Ürün Sepetinize Eklendi");
                    TempData["Message"] = "Ürün Sepetinize Eklendi";
                    return RedirectToAction("Index");//hangi sayfada ekledyisem o sayfada kalsın ödev
                }
                else
                {
                    //ürün zaten sepette var
                    HttpContext.Session.SetString("Message", "Bu Ürün Zaten Sepetinizde Var");
                    HttpContext.Session.GetString("Message");
                    TempData["Message"] = "Bu Ürün Zaten Sepetinizde Var";
                }
            }
            //bu kodla hangi sayfada ürün eklediysek o sayfada kalır
            string url = Request.Headers["Referer"].ToString();
            //return RedirectToAction(url);//hangi sayfada ekledyisem o sayfada kalsın ödev
            return Redirect(url);

        }
        public IActionResult CategoryPage(int id)
        {
            List<Product> products = cp.ProductSelectWithCategoryID(id);
            return View(products);
        }
        public IActionResult SupplierPage(int id)
        {
            List<Product> products = cp.ProductSelectWithSupplierID(id);
            return View(products);
        }
        public IActionResult NewProducts()
        {
            mpm.New = cp.ProductSelect("New", mainpageCount, "New", 0);

            return View(mpm);
        }
        public PartialViewResult _PartialNewProducts(string pageno)
        {
            int pagenumber = Convert.ToInt32(pageno);
            mpm.New = cp.ProductSelect("New", mainpageCount, "New", pagenumber);
            return PartialView(mpm);
        }
        public IActionResult SpecialProducts()
        {
            mpm.Special = cp.ProductSelect("Special", mainpageCount, "Special", 0);
            return View(mpm);
        }
        public PartialViewResult _PartialSpecialProducts(string pageno)
        {
            int pagenumber = Convert.ToInt32(pageno);
            mpm.New = cp.ProductSelect("Special", mainpageCount, "Special", pagenumber);
            return PartialView(mpm);
        }
        public IActionResult DiscountedProducts()
        {
            mpm.Discounted = cp.ProductSelect("Discounted", mainpageCount, "Discounted", 0);
            return View(mpm);

        }
        public PartialViewResult _PartialDiscountedProducts(string pageno)
        {
            int pagenumber = Convert.ToInt32(pageno);
            mpm.Discounted = cp.ProductSelect("Discounted", mainpageCount, "Discounted", pagenumber);
            return PartialView(mpm);
        }
        public IActionResult HighlightedProducts()
        {
            mpm.Highlighted = cp.ProductSelect("Highlighted", mainpageCount, "Highlighted", 0);
            return View(mpm);

        }
        public PartialViewResult _PartialHighlightedProducts(string pageno)
        {
            int pagenumber = Convert.ToInt32(pageno);
            mpm.Highlighted = cp.ProductSelect("Highlighted", mainpageCount, "Highlighted", pagenumber);
            return PartialView(mpm);
        }
        public IActionResult TopSellerProducts(int page = 1, int pagesize = 4)
        {
            PagedList<Product> model = new PagedList<Product>(context.Products.OrderByDescending(p => p.TopSeller), page, pagesize);
            return View("TopSellerProducts", model);
        }
        public PartialViewResult _PartialTopSellerProducts(string pageno)
        {
            int pagenumber = Convert.ToInt32(pageno);
            mpm.TopSeller = cp.ProductSelect("TopSeller", mainpageCount, "TopSeller", pagenumber);
            return PartialView(mpm);
        }
        public IActionResult Cart()
        {
            List<cls_Orders> sepet;

            if (HttpContext.Request.Query["scid"].ToString() != "")
            {
                cls_Orders o = new cls_Orders();
                //sil butonuyla geldim
                string? scid = HttpContext.Request.Query["scid"];
                o.MyCart = Request.Cookies["sepetim"]; //tarayıcıdan al property'e yaz
                o.DeleteFromMyCart(scid); //property'den sildim
                var cookieOptions = new CookieOptions();
                Response.Cookies.Append("sepetim", o.MyCart, cookieOptions); //tarayıcı güncellendi
                cookieOptions.Expires = DateTime.Now.AddDays(1); // 1 günlük cookie
                TempData["Message"] = "Ürün Sepetten Silindi";
                //sepet içindeki son halini cart.cshtml sayfasına göndereceğim
                sepet = o.SelectMyCart();
                ViewBag.sepetim = sepet;
                ViewBag.sepet_tablo_detay = sepet;

            }
            else
            {
                //sağ üst köşeden sepete tıklanınca gelecek
                var cookie = Request.Cookies["sepetim"];
                if (cookie == null)
                {
                    o.MyCart = "";
                    sepet = o.SelectMyCart();
                    ViewBag.sepetim = sepet;
                    ViewBag.sepet_tablo_detay = sepet;
                }
                else
                {
                    var cookieOptions = new CookieOptions();
                    o.MyCart = Request.Cookies["sepetim"];
                    sepet = o.SelectMyCart();
                    ViewBag.sepetim = sepet;
                    ViewBag.sepet_tablo_detay = sepet;
                }
            }
            if (sepet.Count == 0)
            {
                ViewBag.sepetim = null;
            }
            return View();
        }
        public IActionResult Order()
        {
            //HttpContext.Session.SetString("session", "deneme");
            //HttpContext.Session.GetString("Email");
            if (HttpContext.Session.GetString("Email") != null)
            {
                //kullancı login.cshtml'den giriş yapıp, session alıp gelmiştir
                User? usr = cls_User.SelectMemberInfo(HttpContext.Session.GetString("Email"));
                return View(usr);
            }
            else
            {
                //kullanıcı login.cshtml'ye gitmemiş, Session almamıştır
                return RedirectToAction("Login");
            }
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(User user)
        {
            string answer = cls_User.MemberControl(user);
            if (answer == "error")
            {
                HttpContext.Session.SetString("Message", "Email veya Şifre Yanlış");
                TempData["Message"] = "Email veya Şifre Yanlış";
                return View();

            }
            else if (answer == "admin")
            {
                HttpContext.Session.SetString("Email", answer);
                HttpContext.Session.SetString("Admin", answer);
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                HttpContext.Session.SetString("Email", answer);
                return RedirectToAction("Index");
            }
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(User usr)
        {
            if (cls_User.loginEmailControl(usr) == false)
            {
                bool answer = cls_User.AddUser(usr);
                if (answer)
                {
                    TempData["Message"] = "Üye Kaydı Yapıldı";
                    return RedirectToAction(nameof(Login));
                }
                TempData["Message"] = "Hata. Bilgilerinizi kontrol ediniz";
            }
            else
            {
                TempData["Message"] = "Bu Email Adresi Kayıtlıdır";

            }
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Email");
            HttpContext.Session.Remove("Admin");

            return RedirectToAction(nameof(Index));
        }
    }
}