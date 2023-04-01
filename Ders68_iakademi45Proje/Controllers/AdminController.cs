using Ders68_iakademi45Proje.Models;
using Ders68_iakademi45Proje.Models.MVVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ders68_iakademi45Proje.Controllers
{
    public class AdminController : Controller
    {
        cls_User u = new cls_User();
        cls_Product p = new cls_Product();
        cls_Category c = new cls_Category();
        cls_Supplier s = new cls_Supplier();
        cls_Status st = new cls_Status();
        cls_Setting cs = new cls_Setting();
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Email,Password,NameSurname")] User user)
        {

            if (ModelState.IsValid) //başına ünlem koyarsak bu metod çalışmaz, ünlemsiz hali ile zorunlu alanların tamamlanmasını ister. ! olursa tamamlanmasını istemez
            {
                User? usr = await u.loginControl(user);
                if (usr != null)
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ViewBag.error = "login bilgileri hatalı";
            }
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> ProductIndex()
        {
            List<Product> products = await p.ProductSelect();
            ViewBag.productCount = context.Products.Count();
            return View(products);
        }
        public async Task<IActionResult> CategoryIndex()
        {
            List<Category> categories = await c.CategorySelect();
            return View(categories);
        }
        [HttpGet]
        public IActionResult CategoryCreate()
        {
            CategoryFill();
            return View();
        }

        void CategoryFill()
        {
            List<Category> categories = c.CategorySelectMain();
            ViewData["categoryList"] = categories.Select(c => new SelectListItem { Text = c.CategoryName, Value = c.CategoryID.ToString() });
        }
        async void CategoryFillAll()
        {
            List<Category> categories = await c.CategorySelect();
            ViewData["categoryList"] = categories.Select(c => new SelectListItem { Text = c.CategoryName, Value = c.CategoryID.ToString() });
        }

        async void SupplierFill()
        {
            List<Supplier> suppliers = await s.SupplierSelect();
            ViewData["supplierList"] = suppliers.Select(s => new SelectListItem { Text = s.BrandName, Value = s.SupplierID.ToString() });
        }
        async void StatusFill()
        {
            List<Status> statuses = await st.StatusSelect();
            ViewData["statusList"] = statuses.Select(st => new SelectListItem { Text = st.StatusName, Value = st.StatusID.ToString() });
        }

        iakademi45Context context = new iakademi45Context();
        [HttpPost]
        public IActionResult CategoryCreate(Category category)
        {
            bool answer = cls_Category.CategoryInsert(category);
            if (answer == true)
            {
                TempData["Message"] = "Eklendi";
            }
            else
            {
                TempData["Message"] = "HATA";
            }
            return RedirectToAction(nameof(CategoryCreate));
        }
        public async Task<IActionResult> CategoryEdit(int? id)
        {
            CategoryFill();
            if (id == null || context.Categories == null)
            {
                return NotFound();
            }
            var category = await c.CategoryDetails(id);
            return View(category);
        }
        [HttpPost]
        public IActionResult CategoryEdit(Category category)
        {
            bool answer = cls_Category.CategoryUpdate(category);
            if (answer == true)
            {
                TempData["Message"] = "Güncellendi";
                return RedirectToAction(nameof(CategoryIndex));
            }
            else
            {
                TempData["Message"] = "HATA";
                return RedirectToAction(nameof(CategoryEdit));
            }
        }
        [HttpGet]
        public async Task<IActionResult> CategoryDelete(int? id)
        {
            if (id == null || context.Categories == null)
            {
                return NotFound();
            }
            var category = await context.Categories.FirstOrDefaultAsync(c => c.CategoryID == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        //bu metod yazımına rooting denir. güvenlik için kullanılır.
        [HttpPost, ActionName("CategoryDelete")]
        public async Task<IActionResult> CategoryDeleteConfirmed(int id)
        {
            //if (context.Categories == null)
            //{
            //    return Problem("Kategori'context.Categories' is null");
            //}
            //var category = await context.Categories.FindAsync(id);
            //if (category != null)
            //{
            //    context.Categories.Remove(category);
            //}
            //await context.SaveChangesAsync();
            //return RedirectToAction(nameof(CategoryIndex));

            bool answer = cls_Category.CategoryDelete(id);
            if (answer == true)
            {
                TempData["Message"] = "Silindi";
                return RedirectToAction(nameof(CategoryIndex));
            }
            else
            {
                TempData["Message"] = "HATA";
                return RedirectToAction(nameof(CategoryDelete));
            }

        }
        public async Task<IActionResult> CategoryDetails(int? id)
        {
            var category = await context.Categories.FirstOrDefaultAsync(c => c.CategoryID == id);
            ViewBag.categoryname = category?.CategoryName.ToUpper();
            return View(category);
        }
        public async Task<IActionResult> SupplierIndex()
        {
            List<Supplier> suppliers = await s.SupplierSelect();
            return View(suppliers);
        }
        public IActionResult SupplierCreate()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SupplierCreate(Supplier supplier)
        {
            bool answer = cls_Supplier.SupplierInsert(supplier);
            if (answer == true)
            {
                TempData["Message"] = "Eklendi";
            }
            else
            {
                TempData["Message"] = "HATA";
            }
            return RedirectToAction(nameof(SupplierCreate));

        }
        public async Task<IActionResult> SupplierEdit(int? id)
        {
            if (id == null || context.Suppliers == null)
            {
                return NotFound();
            }
            var supplier = await s.SupplierDetails(id);
            return View(supplier);
        }
        [HttpPost]
        public IActionResult SupplierEdit(Supplier supplier)
        {
            if (supplier.PhotoPath == null)
            {
                string? photopath = context.Suppliers.FirstOrDefault(s => s.SupplierID == supplier.SupplierID).PhotoPath;
                supplier.PhotoPath = photopath;
            }
            bool answer = cls_Supplier.SupplierUpdate(supplier);
            if (answer == true)
            {
                TempData["Message"] = "Güncellendi";
                return RedirectToAction(nameof(SupplierIndex));
            }
            else
            {
                TempData["Message"] = "HATA";
                return RedirectToAction(nameof(SupplierEdit));
            }
        }
        public async Task<IActionResult> SupplierDetails(int? id)
        {
            var supplier = await context.Suppliers.FirstOrDefaultAsync(c => c.SupplierID == id);
            ViewBag.brandname = supplier?.BrandName.ToUpper();
            return View(supplier);
        }
        [HttpGet]
        public async Task<IActionResult> SupplierDelete(int? id)
        {
            if (id == null || context.Suppliers == null)
            {
                return NotFound();
            }
            var supplier = await context.Suppliers.FirstOrDefaultAsync(c => c.SupplierID == id);
            if (supplier == null)
            {
                return NotFound();
            }
            return View(supplier);
        }

        [HttpPost, ActionName("SupplierDelete")]
        public async Task<IActionResult> SupplierDeleteConfirmed(int id)
        {
            bool answer = cls_Supplier.SupplierDelete(id);
            if (answer == true)
            {
                TempData["Message"] = "Silindi";
                return RedirectToAction(nameof(SupplierIndex));
            }
            else
            {
                TempData["Message"] = "HATA";
                return RedirectToAction(nameof(SupplierDelete));
            }
        }
        public async Task<IActionResult> StatusIndex()
        {
            List<Status> statuses = await st.StatusSelect();
            return View(statuses);
        }
        public IActionResult StatusCreate()
        {
            return View();
        }
        [HttpPost]
        public IActionResult StatusCreate(Status status)
        {
            bool answer = cls_Status.StatusInsert(status);
            if (answer == true)
            {
                TempData["Message"] = "Eklendi";
            }
            else
            {
                TempData["Message"] = "HATA";
            }
            return RedirectToAction(nameof(StatusCreate));

        }
        public async Task<IActionResult> StatusEdit(int? id)
        {
            if (id == null || context.Statuses == null)
            {
                return NotFound();
            }
            var statuses = await st.StatusDetails(id);
            return View(statuses);
        }
        [HttpPost]
        public IActionResult StatusEdit(Status statuses)
        {
            bool answer = cls_Status.StatusUpdate(statuses);
            if (answer == true)
            {
                TempData["Message"] = "Güncellendi";
                return RedirectToAction(nameof(StatusIndex));
            }
            else
            {
                TempData["Message"] = "HATA";
                return RedirectToAction(nameof(StatusEdit));
            }
        }
        [HttpGet]
        public async Task<IActionResult> StatusDelete(int? id)
        {
            if (id == null || context.Statuses == null)
            {
                return NotFound();
            }
            var statuses = await context.Statuses.FirstOrDefaultAsync(c => c.StatusID == id);
            if (statuses == null)
            {
                return NotFound();
            }
            return View(statuses);
        }

        [HttpPost, ActionName("StatusDelete")]
        public async Task<IActionResult> StatusDeleteConfirmed(int id)
        {
            bool answer = cls_Status.StatusDelete(id);
            if (answer == true)
            {
                TempData["Message"] = "Silindi";
                return RedirectToAction(nameof(StatusIndex));
            }
            else
            {
                TempData["Message"] = "HATA";
                return RedirectToAction(nameof(StatusDelete));
            }
        }
        public async Task<IActionResult> StatusDetails(int id)
        {
            var statuses = await context.Statuses.FirstOrDefaultAsync(c => c.StatusID == id);
            ViewBag.statusname = statuses?.StatusName.ToUpper();
            return View(statuses);
        }
        [HttpGet]
        public async Task<IActionResult> ProductCreate()
        {

            List<Category> categories = await c.CategorySelect();
            ViewData["categoryList"] = categories.Select(c => new SelectListItem { Text = c.CategoryName, Value = c.CategoryID.ToString() });



            List<Supplier> suppliers = await s.SupplierSelect();
            ViewData["supplierList"] = suppliers.Select(s => new SelectListItem { Text = s.BrandName, Value = s.SupplierID.ToString() });


            List<Status> statuses = await st.StatusSelect();
            ViewData["statusList"] = statuses.Select(st => new SelectListItem { Text = st.StatusName, Value = st.StatusID.ToString() });

            return View();
        }
        [HttpPost]
        public IActionResult ProductCreate(Product product)
        {
            bool answer = cls_Product.ProductInsert(product);
            if (answer == true)
            {
                TempData["Message"] = product.ProductName + " Eklendi";
            }
            else
            {
                TempData["Message"] = "HATA";
            }
            return RedirectToAction(nameof(ProductCreate));
        }
        public async Task<IActionResult> ProductEdit(int? id)
        {
            List<Category> categories = await c.CategorySelect();
            ViewData["categoryList"] = categories.Select(c => new SelectListItem { Text = c.CategoryName, Value = c.CategoryID.ToString() });



            List<Supplier> suppliers = await s.SupplierSelect();
            ViewData["supplierList"] = suppliers.Select(s => new SelectListItem { Text = s.BrandName, Value = s.SupplierID.ToString() });


            List<Status> statuses = await st.StatusSelect();
            ViewData["statusList"] = statuses.Select(st => new SelectListItem { Text = st.StatusName, Value = st.StatusID.ToString() });

            if (id == null || context.Products == null)
            {
                return NotFound();
            }
            var product = await p.ProductDetails(id);
            return View(product);
        }
        [HttpPost]
        public IActionResult ProductEdit(Product product)
        {
            //veritabanından kaydı getirdim
            Product prd = context.Products.FirstOrDefault(s => s.ProductID == product.ProductID);
            //formdan gelmeyecek olan bazı kolonları null yerine eski bilgilerini yazdım
            product.AddDate = prd.AddDate;
            product.HighLighted = prd.HighLighted;
            product.TopSeller = prd.TopSeller;
            if (product.PhotoPath == null)
            {
                string? photopath = context.Products.FirstOrDefault(s => s.ProductID == product.ProductID).PhotoPath;
                product.PhotoPath = photopath;
            }

            bool answer = cls_Product.ProductUpdate(product);
            if (answer == true)
            {
                TempData["Message"] = "Güncellendi";
                return RedirectToAction(nameof(ProductIndex));
            }
            else
            {
                TempData["Message"] = "HATA";
                return RedirectToAction(nameof(ProductEdit));
            }
        }
        public async Task<IActionResult> ProductDetails(int? id)
        {
            var product = await context.Products.FirstOrDefaultAsync(c => c.ProductID == id);
            ViewBag.productname = product?.ProductName.ToUpper();
            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> ProductDelete(int? id)
        {
            if (id == null || context.Products == null)
            {
                return NotFound();
            }
            var product = await context.Products.FirstOrDefaultAsync(c => c.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        //bu metod yazımına rooting denir. güvenlik için kullanılır.
        [HttpPost, ActionName("ProductDelete")]
        public async Task<IActionResult> ProductDelete(int id)
        {


            bool answer = cls_Product.ProductDelete(id);
            if (answer == true)
            {
                TempData["Message"] = "Silindi";
                return RedirectToAction(nameof(ProductIndex));
            }
            else
            {
                TempData["Message"] = "HATA";
                return RedirectToAction(nameof(ProductDelete));
            }

        }
        public  IActionResult SettingEdit()
        {
            var settings = cs.SettingDetails();
            return View(settings);
        }
        [HttpPost]
        public IActionResult SettingEdit(Setting settings)
        {
            bool answer = cls_Setting.SettingUpdate(settings);
            if (answer == true)
            {
                TempData["Message"] = "Güncellendi";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["Message"] = "HATA";
                return RedirectToAction(nameof(SettingEdit));
            }
        }

        //public async Task<IActionResult> CategoryRevCon(int id)
        //{
        //    bool answer = cls_Category.CategoryRevCon(id);
        //    if (answer == true)
        //    {
        //        TempData["Message"] = "Kategori Aktif Edildi";
        //        return RedirectToAction(nameof(CategoryIndex));

        //    }
        //    else
        //    {
        //        TempData["Message"] = "Kategori Aktif Edilemedi. Ana Kategori Aktifliğini kontrol ediniz";
        //        return RedirectToAction(nameof(CategoryEdit));

        //    }
        //}
        //public async Task<IActionResult> CatRevcon(int id)
        //{
        //    // Kullanıcının seçtiği alt kategorinin Parent Id değerini alarak doğrulama yapma
        //    var altKategoriAdi = "alt kategori adi";
        //    var parentKategoriId = 1; // Parent Id değeri
        //    var kategori = context.Categories.FirstOrDefault(k => k.CategoryName == altKategoriAdi && k.ParentID == parentKategoriId);
        //    if (kategori != null)
        //    {
        //        if (!kategori.Active)
        //        {
        //            // Ana kategori pasif durumdaysa kullanıcıya uyarı mesajı gösterme
        //            ViewBag.UyariMesaji = $"{kategori.CategoryName} kategorisi pasif durumda olduğu için {altKategoriAdi} alt kategorisi aktif edilemez.";
        //            return RedirectToAction(nameof(CategoryEdit));

        //        }
        //        else
        //        {
        //            // Alt kategori aktif durumdaysa kullanıcıya mesaj gösterme
        //            ViewBag.Mesaj = $"{altKategoriAdi} alt kategorisi aktif edildi.";
        //            return RedirectToAction(nameof(CategoryIndex));

        //        }
        //    }
        //    else
        //    {
        //        // Alt kategori veritabanında bulunamazsa kullanıcıya hata mesajı gösterme
        //        ViewBag.UyariMesaji = $"{altKategoriAdi} adında bir alt kategori bulunamadı.";
        //        return RedirectToAction(nameof(CategoryIndex));

        //    }
        //}
    }
}