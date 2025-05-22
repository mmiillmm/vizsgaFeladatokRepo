using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate
{
    internal class Ad
    {
        public int Area { get; set; }
        public Category? Category { get; set; }
        public DateTime CreateAt { get; set; }
        public string? Description { get; set; }
        public int Floors { get; set; }
        public bool FreeOfCharge { get; set; }
        public int Id { get; set; }
        public string? ImageUrl { get; set; }
        public string? LatLong { get; set; }
        public int Rooms { get; set; }
        public Seller? Seller { get; set; }

        public static List<Ad> LoadFromCsv(string fileName)
        {
            var ads = new List<Ad>();
            using var reader = new StreamReader(fileName);
            string? headerLine = reader.ReadLine();

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line)) continue;

                var values = line.Split(';');

                var ad = new Ad
                {
                    Id = int.Parse(values[0]),
                    Rooms = int.Parse(values[1]),
                    LatLong = values[2],
                    Floors = int.Parse(values[3]),
                    Area = int.Parse(values[4]),
                    Description = values[5],
                    FreeOfCharge = values[6] == "1" ? true : false,
                    ImageUrl = values[7],
                    CreateAt = DateTime.Parse(values[8], CultureInfo.InvariantCulture),
                    Seller = new Seller
                    {
                        Id = int.Parse(values[9]),
                        Name = values[10],
                        Phone = values[11]
                    },
                    Category = new Category
                    {
                        Id = int.Parse(values[12]),
                        Name = values[13]
                    },

                };

                ads.Add(ad);
            }

            return ads;
        }

        public double DistanceTo(double lat, double lon)
        {
            var parts = LatLong.Split(',');

            double adLat = double.Parse(parts[0], CultureInfo.InvariantCulture);
            double adLon = double.Parse(parts[1], CultureInfo.InvariantCulture);

            double dLat = adLat - lat;
            double dLon = adLon - lon;

            return Math.Sqrt(dLat * dLat + dLon * dLon);
        }
    }
}
