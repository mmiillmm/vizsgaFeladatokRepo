using System.Globalization;
using System.IO;
using LaptopsCLI;

List<Laptop> laptop = new List<Laptop>();

using (StreamReader sr = new(@"..\..\..\src\laptops.txt", System.Text.Encoding.UTF8))
{
    sr.ReadLine();

    while (!sr.EndOfStream)
    {
        string? line = sr.ReadLine();
        if (string.IsNullOrWhiteSpace(line)) continue;
        string[] parts = line.Split(';');

        var category = new Category { CategoryCode = int.Parse(parts[0]), CategoryName = parts[1] };
        string cpu = parts[2];
        string manufacturer = parts[4];
        string model = parts[5];
        string os = parts[6];
        double priceInr = double.Parse(parts[7], new CultureInfo("hu-HU"));
        string ram = parts[8];
        string screen = parts[9];
        string storage = parts[10];

        laptop.Add(new Laptop(category, cpu, manufacturer, model, os, (int)priceInr, ram, screen, storage));
    }

    Console.WriteLine("5. Feladat");
    for (int i = 0; i < laptop.Count; i++)
    {
        var l = laptop[i];
        int priceHuf = (int)Math.Round(l.Price * 4.12);
        Console.WriteLine($"{i + 1}. {l.Category.CategoryName} | {l.Manufacturer} {l.Model} | {l.OS} |{priceHuf} HUF");
    }

    Console.WriteLine("6. Feladat");
    var f6 = laptop
        .Where(x => x.CPU.Contains("Intel Core i7", StringComparison.OrdinalIgnoreCase)
                 && x.Storage.Contains("SSD", StringComparison.OrdinalIgnoreCase))
        .ToList();

    for (int i = 0; i < f6.Count; i++)
    {
        var l = f6[i];
        Console.WriteLine($"{i + 1}. {l.Category.CategoryName} | {l.Manufacturer} {l.Model} | {l.OS}");
    }

    if (f6.Count > 0)
    {
        double avgInr = f6.Average(x => x.Price);
        Console.WriteLine($"Átlagár: {avgInr:F2} INR");
    }

    Console.WriteLine("7. Feladat");
    var cheapestGaming = laptop
        .Where(x => x.Category.CategoryName.Equals("Gaming", StringComparison.OrdinalIgnoreCase))
        .OrderBy(x => x.Price)
        .Take(20)
        .ToList();

    using (StreamWriter sw = new StreamWriter("cheap.txt", false, System.Text.Encoding.UTF8))
    {
        foreach (var l in cheapestGaming)
        {
            sw.WriteLine($"{l.Manufacturer} {l.Model}");
            sw.WriteLine(l.CPU);
            sw.WriteLine(l.Storage);
            sw.WriteLine(l.Screen);
            sw.WriteLine();
        }
    }
}