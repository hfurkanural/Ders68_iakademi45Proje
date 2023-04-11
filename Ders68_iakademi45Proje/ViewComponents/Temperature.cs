using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace Ders68_iakademi45Proje.ViewComponents
{
    public class Temperature : ViewComponent
    {
        public string Invoke()
        {
            string apikey = "52b72dad903d5a0244a91d029fce3686";
            string url = "https://api.openweathermap.org/data/2.5/weather?q=izmir&mode=xml&lang=tr&units=metric&appid=" + apikey;

            XDocument weather = XDocument.Load(url);

            var temperature = weather.Descendants("temperature").ElementAt(0).Attribute("value").Value;

            return $"{temperature} Derece";
        }

    }
}
