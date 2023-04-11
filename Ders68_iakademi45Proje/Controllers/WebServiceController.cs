using Microsoft.AspNetCore.Mvc;

namespace Ders68_iakademi45Proje.Controllers
{
    public class WebServiceController : Controller
    {
        public static string tckimlik_vergi_no = "";
        public IActionResult Index()
        {
            return View();
        }
    }
}
