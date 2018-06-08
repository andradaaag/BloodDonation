using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;

namespace BloodDonation.Logic.Services
{
    public class LocationService
    {
        public List<Double> getCoordinates(String location)
        {
            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri("https://nominatim.openstreetmap.org")
            };
            String formattedLocation = location.Trim();
            formattedLocation = formattedLocation.Replace(" ", "%20");

            String url = "https://nominatim.openstreetmap.org/search/"+ formattedLocation + "?format=json";
            WebClient webClient = new WebClient();
            webClient.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
            webClient.Headers.Add("Referer", "http://www.microsoft.com");
            var jsonData = webClient.DownloadData(url);
            String val = Encoding.UTF8.GetString(jsonData);
            val = val.Replace("\\", "");
            val = val.Replace("\"", "");
            String[] values = val.Split(',');
            Double lat = values.AsEnumerable()
                  .Where(el =>
                  {
                      return el.Split(':')[0].Equals("lat");
                  })
                  .Select(s => Convert.ToDouble(s.Split(':')[1]))
                  .ToList()[0];
            Double lon = values.AsEnumerable()
                  .Where(el =>
                  {
                      return el.Split(':')[0].Equals("lon");
                  })
                  .Select(s => Convert.ToDouble(s.Split(':')[1]))
                  .ToList()[0];
            List<Double> coordinates = new List<double>();

            coordinates.Add(lat);
            coordinates.Add(lon);
            return coordinates;



        }

    }
}
