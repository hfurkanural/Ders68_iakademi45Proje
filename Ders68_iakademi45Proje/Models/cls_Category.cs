using Ders68_iakademi45Proje.Models.MVVM;
using Microsoft.EntityFrameworkCore;

namespace Ders68_iakademi45Proje.Models
{
    public class cls_Category
    {
        iakademi45Context context = new iakademi45Context();
        Category c = new Category();
        public async Task<List<Category>> CategorySelect()
        {
            List<Category> categories = await context.Categories.ToListAsync();
            return categories;
        }
        public List<Category> CategorySelectMain()
        {
            List<Category> categories = context.Categories.Where(c => c.ParentID == 0).ToList();
            return categories;
        }
        public static bool CategoryInsert(Category category)
        {
            try
            {
                //metod static olduğu için metodu burada tanımalamak zorundayız
                using (iakademi45Context context = new iakademi45Context())
                {
                    context.Add(category);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        public async Task<Category> CategoryDetails(int? id)
        {
            Category? categories = await context.Categories.FindAsync(id);
            return categories;
        }
        public static bool CategoryUpdate(Category category)
        {
            try
            {
                //metod static olduğu için metodu burada tanımalamak zorundayız
                using (iakademi45Context context = new iakademi45Context())
                {
                    context.Update(category);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        public static bool CategoryDelete(int id)
        {
            try
            {
                //metod static olduğu için metodu burada tanımalamak zorundayız
                using (iakademi45Context context = new iakademi45Context())
                {
                    Category? category = context.Categories.FirstOrDefault(c => c.CategoryID == id);
                    category.Active = false;

                    List<Category> categoryList = context.Categories.Where(c => c.ParentID == id).ToList();
                    foreach (var item in categoryList)
                    {
                        item.Active = false;
                    }
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        //public static bool CategoryRevCon(int id)
        //{
        //    using (iakademi45Context context = new iakademi45Context)
        //    {
        //        Kullanıcının seçtiği alt kategorinin bağlı olduğu ana kategoriyi sorgulama
        //       var altKategoriAdi = "alt kategori adi";
        //        var kategori = context.Categories.FirstOrDefault(k => k.CategoryName == altKategoriAdi);
        //        if (kategori != null)
        //        {
        //            if (!kategori.Active)
        //            {
        //                Ana kategori pasif durumdaysa kullanıcıya uyarı mesajı gösterme
        //                ViewBag.UyariMesaji = $"{kategori.CategoryName} kategorisi pasif durumda olduğu için {altKategoriAdi} alt kategorisi aktif edilemez.";
        //            }
        //            else
        //            {
        //                Alt kategori aktif durumdaysa kullanıcıya mesaj gösterme
        //                ViewBag.Mesaj = $"{altKategoriAdi} alt kategorisi aktif edildi.";
        //            }
        //        }
        //        else
        //        {
        //            Alt kategori veritabanında bulunamazsa kullanıcıya hata mesajı gösterme
        //            ViewBag.UyariMesaji = $"{altKategoriAdi} adında bir alt kategori bulunamadı.";
        //        }
        //    }
            //using (iakademi45Context context = new iakademi45Context())
            //{
            //    Category categories = context.Categories.FirstOrDefault(c => c.ParentID == id);
            //    if (categories.CategoryID == id && categories.Active == false)
            //    {
            //        return false;
            //    }
            //    else
            //    {
            //        return true;
            //    }
            //}

        }
}

