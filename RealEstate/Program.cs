using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace RealEstate
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Ad> ads = Ad.LoadFromCsv("realestates.csv");

            var groundFloorAds = ads.Where(x => x.Floors == 0).ToList();
            double avgArea = groundFloorAds.Any() ? groundFloorAds.Average(x => x.Area) : 0.0;

            Console.WriteLine($"1. Földszinti ingatlanok átlagos alapterülete: {avgArea:F2} m2");

            double mesevarLat = 47.4164220114023;
            double mesevarLon = 19.066342425796986;

            var closest = ads
                .Where(x => x.FreeOfCharge)
                .OrderBy(x => x.DistanceTo(mesevarLat, mesevarLon))
                .FirstOrDefault();

            if (closest != null)
            {
                Console.WriteLine("2. Mesevár óvodáoz légvonalban legközelebbi teherments ingatrlan adatai");
                Console.WriteLine($"\tEladó neve: {closest.Seller?.Name}");
                Console.WriteLine($"\tEladó telefonszáma: {closest.Seller?.Phone}");
                Console.WriteLine($"\tAlapterület: {closest.Area}");
                Console.WriteLine($"\tSzobák száma: {closest.Rooms}");
            }

            Console.ReadKey();
        }
    }
}
