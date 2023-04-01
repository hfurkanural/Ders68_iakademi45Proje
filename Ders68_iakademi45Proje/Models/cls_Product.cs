using Ders68_iakademi45Proje.Models.MVVM;
using Microsoft.EntityFrameworkCore;

namespace Ders68_iakademi45Proje.Models
{
    public class cls_Product
    {
        iakademi45Context context = new iakademi45Context();
        int subpageCount = 0;

        public async Task<List<Product>> ProductSelect()
        {
            List<Product> products = await context.Products.ToListAsync();
            return products;
        }
        public static bool ProductInsert(Product product)
        {
            try
            {
                //metod static olduğu için metodu burada tanımalamak zorundayız
                using (iakademi45Context context = new iakademi45Context())
                {
                    product.AddDate = DateTime.Now;
                    context.Add(product);
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
        public async Task<Product> ProductDetails(int? id)
        {
            Product? products = await context.Products.FindAsync(id);
            return products;
        }
        public static bool ProductUpdate(Product product)
        {
            try
            {
                //metod static olduğu için metodu burada tanımalamak zorundayız
                using (iakademi45Context context = new iakademi45Context())
                {
                    product.AddDate = DateTime.Now;
                    context.Update(product);
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
        public static bool ProductDelete(int id)
        {
            try
            {
                //metod static olduğu için metodu burada tanımalamak zorundayız
                using (iakademi45Context context = new iakademi45Context())
                {
                    Product? product = context.Products.FirstOrDefault(c => c.ProductID == id);
                    product.Active = false;

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
        public List<Product> ProductSelect(string mainPageName, int mainpageCount, string subPageName, int pagenumber)
        {
            subpageCount = context.Settings.FirstOrDefault(s => s.SettingID == 1).subpageCount;

            List<Product> products;
            if (mainPageName == "New")
            {
                if (subPageName == "")
                {
                    //Home/Index
                    products = context.Products.OrderByDescending(p => p.AddDate).Take(mainpageCount).ToList();
                }
                else
                {
                    if (pagenumber == 0)
                    {
                        //en yeni ürünler
                        products = context.Products.OrderByDescending(p => p.AddDate).Take(subpageCount).ToList();
                    }
                    else
                    {
                        //ajax
                        products = context.Products.OrderByDescending(p => p.AddDate).Skip(pagenumber * subpageCount).Take(subpageCount).ToList();
                    }
                }
            }
            else if (mainPageName == "Special")
            {
                if (subPageName == "")
                {
                    //Home/Index
                    products = context.Products.Where(p => p.StatusID == 2).Take(mainpageCount).OrderBy(p => p.ProductName).ToList();
                }
                else
                {
                    if (pagenumber == 0)
                    {
                        //en yeni ürünler
                        products = context.Products.Where(p => p.StatusID == 2).Take(subpageCount).OrderBy(p => p.ProductName).ToList();
                    }
                    else
                    {
                        //ajax
                        products = context.Products.Where(p => p.StatusID == 2).Skip(pagenumber * subpageCount).Take(subpageCount).ToList();
                    }
                }
            }
            else if (mainPageName == "Discounted")
            {
                if (subPageName == "")
                {
                    //Home/Index
                    products = context.Products.OrderByDescending(p => p.Discount).Take(mainpageCount).ToList();
                }
                else
                {
                    if (pagenumber == 0)
                    {
                        //en yeni ürünler
                        products = context.Products.OrderByDescending(p => p.Discount).Take(subpageCount).ToList();
                    }
                    else
                    {
                        //ajax
                        products = context.Products.OrderByDescending(p => p.Discount).Skip(pagenumber * subpageCount).Take(subpageCount).ToList();
                    }
                }
            }
            else if (mainPageName == "Highlighted")
            {
                if (subPageName == "")
                {
                    //Home/Index
                    products = context.Products.OrderByDescending(p => p.HighLighted).Take(mainpageCount).ToList();
                }
                else
                {
                    if (pagenumber == 0)
                    {
                        //en yeni ürünler
                        products = context.Products.OrderByDescending(p => p.HighLighted).Take(subpageCount).ToList();
                    }
                    else
                    {
                        //ajax
                        products = context.Products.OrderByDescending(p => p.HighLighted).Skip(pagenumber * subpageCount).Take(subpageCount).ToList();
                    }
                }
            }
            else if (mainPageName == "TopSeller")
            {
                products = context.Products.OrderByDescending(p => p.TopSeller).Take(mainpageCount).ToList();
            }
            else if (mainPageName == "Slider")
            {
                products = context.Products.Where(p => p.StatusID == 1).Take(mainpageCount).ToList();
            }
            else if (mainPageName == "Star")
            {
                products = context.Products.Where(p => p.StatusID == 3).Take(mainpageCount).ToList();
            }
            else if (mainPageName == "Featured")
            {
                products = context.Products.Where(p => p.StatusID == 4).Take(mainpageCount).ToList();
            }
            else if (mainPageName == "Notable")
            {
                products = context.Products.Where(p => p.StatusID == 5).Take(mainpageCount).ToList();
            }
            else
            {
                products = context.Products.Where(p => p.ProductID == 999).Take(mainpageCount).ToList();
            }
            return products;
        }
        public Product ProductDetails(string mainPageName)
        {
            Product? product = context.Products.FirstOrDefault(p => p.StatusID == 6);
            return product;
        }
        public List<Product> ProductSelectWithCategoryID(int id)
        {
            List<Product> products = context.Products.Where(p => p.CategoryID == id).OrderBy(p => p.ProductName).ToList();
            return products;
        }
        public List<Product> ProductSelectWithSupplierID(int id)
        {
            List<Product> products = context.Products.Where(p => p.SupplierID == id).OrderBy(p => p.ProductName).ToList();
            return products;
        }
        public static void HighlightedIncrease(int id)
        {
            using (iakademi45Context context = new iakademi45Context())
            {
                Product product = context.Products.FirstOrDefault(p => p.ProductID == id);
                product.HighLighted += 1;
                context.Update(product);
                context.SaveChanges();
            }
        }
    }
}