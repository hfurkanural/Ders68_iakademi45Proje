using Ders68_iakademi45Proje.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ders68_iakademi45Proje.ViewComponents
{
    public class Address : ViewComponent
    {
        iakademi45Context context = new iakademi45Context();
        public string Invoke()
        {
            string address = context.Settings.FirstOrDefault(s => s.SettingID == 1).address;
            return $"{address}";
        }
    }
}
