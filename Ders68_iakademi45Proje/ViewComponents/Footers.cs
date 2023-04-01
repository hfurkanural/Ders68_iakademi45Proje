using Ders68_iakademi45Proje.Models;
using Ders68_iakademi45Proje.Models.MVVM;
using Microsoft.AspNetCore.Mvc;

namespace Ders68_iakademi45Proje.ViewComponents
{
    public class Footers : ViewComponent
    {
        iakademi45Context context = new iakademi45Context();
        public IViewComponentResult Invoke()
        {
            List<Supplier> suppliers = context.Suppliers.ToList();
            return View(suppliers);
        }
    }
}