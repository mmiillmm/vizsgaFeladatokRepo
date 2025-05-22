using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace appsCLI
{
    internal class App
    {
        public string AppName { get; set; }
        public Category Category { get; set; }
        public ContentRating ContentRating { get; set; }
        public string CurrentVer { get; set; }
        public float Rating { get; set; }
        public int Reviews { get; set; }
        public int UpdateMonth { get; set; }
        public int UpdateYear { get; set; }

        public static List<App> LoadFromCsv(string fileName)
        {
            var apps = new List<App>();
            foreach (var line in File.ReadLines(fileName).Skip(1))
            {
                var fields = line.Split(';');
                if (fields.Length < 10) continue;

                string ratingStr = fields[4].Replace('.', ',');
                if (!float.TryParse(ratingStr, NumberStyles.Float, new CultureInfo("hu-HU"), out float rating))
                    continue; // Skip invalid rating

                if (!int.TryParse(fields[5], out int reviews) ||
                    !int.TryParse(fields[6], out int updateMonth) ||
                    !int.TryParse(fields[7], out int updateYear))
                    continue; // Skip invalid numbers

                var app = new App
                {
                    AppName = fields[0],
                    Category = new Category { CategoryName = fields[1] },
                    ContentRating = new ContentRating { ContentRatingName = fields[2] },
                    CurrentVer = fields[3],
                    Rating = rating,
                    Reviews = reviews,
                    UpdateMonth = updateMonth,
                    UpdateYear = updateYear
                };
                apps.Add(app);
            }
            return apps;
        }

        public override string ToString()
        {
            return $"{AppName} {GetStars()}";
        }

        private string GetStars()
        {
            if (Reviews == 0)
                return "-";
            int stars = (int)Math.Round(Rating);
            return new string('*', stars);
        }
    }
}
