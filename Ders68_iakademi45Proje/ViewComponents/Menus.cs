using Ders68_iakademi45Proje.Models;
using Ders68_iakademi45Proje.Models.MVVM;
using Microsoft.AspNetCore.Mvc;

namespace Ders68_iakademi45Proje.ViewComponents
{
    public class Menus : ViewComponent
	{
		iakademi45Context context = new iakademi45Context();
		public IViewComponentResult Invoke()
		{
			List<Category> categories = context.Categories.ToList();
			return View(categories);
		}
	}
}
