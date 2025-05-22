using System;
using System.Collections.Generic;

namespace appsCLI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var apps = App.LoadFromCsv("apps.csv");

            var appsByCategory = new Dictionary<string, List<App>>();

            foreach (var app in apps)
            {
                string categoryName = app.Category.CategoryName;
                if (!appsByCategory.ContainsKey(categoryName))
                {
                    appsByCategory[categoryName] = new List<App>();
                }
                appsByCategory[categoryName].Add(app);
            }

            foreach (var kvp in appsByCategory)
            {
                Console.WriteLine($"Kategória: {kvp.Key}");
                foreach (var app in kvp.Value)
                {
                    Console.WriteLine(app);
                }
            }

            Console.ReadKey();
        }
    }
}