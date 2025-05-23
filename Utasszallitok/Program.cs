using System.IO;
using System.Xml.Linq;
using Utasszallitok;

List<Sebessegkategoria> sebesseg = new List<Sebessegkategoria>();

using (StreamReader sr = new(@"..\..\..\src\utasszallitok.txt", System.Text.Encoding.UTF8))
{
    string header = sr.ReadLine()!;
    var lines = new List<string> { header };

    int boeingCount = 0;
    int maxUtas = int.MinValue;
    string maxUtasTipus = "";
    string maxUtasEv = "";

    using (StreamWriter sw = new(@"..\..\..\src\utasszallitok_new.txt", false, System.Text.Encoding.UTF8))
    {
        sw.WriteLine(header);

        while (!sr.EndOfStream)
        {
            string? line = sr.ReadLine();
            if (string.IsNullOrWhiteSpace(line)) continue;

            string[] parts = line.Split(';');

            string utas = parts[2];
            if (utas.Contains('-'))
                utas = utas.Split('-')[1];

            string szemelyzet = parts[3];
            if (szemelyzet.Contains('-'))
                szemelyzet = szemelyzet.Split('-')[1];

            int felszalloTomegTonna = (int)Math.Round(double.Parse(parts[5].Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture) / 1000);

            int fesztavLab = (int)Math.Round(double.Parse(parts[6].Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture) * 3.2808);

            string newLine = $"{parts[0]};{parts[1]};{utas};{szemelyzet};{parts[4]};{felszalloTomegTonna};{fesztavLab}";
            sw.WriteLine(newLine);

            var sebessegKategoria = new Sebessegkategoria(
                (int)double.Parse(parts[4].Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture)
            );

            if (parts[0].StartsWith("Boeing"))
            {
                boeingCount++;
            }

            string utasStr = parts[2];
            int utasMax;
            if (utasStr.Contains('-'))
            {
                string[] range = utasStr.Split('-');
                utasMax = int.Parse(range[1]);
            }
            else
            {
                utasMax = int.Parse(utasStr);
            }
            if (utasMax > maxUtas)
            {
                maxUtas = utasMax;
                maxUtasTipus = parts[0];
                maxUtasEv = parts[1];
            }
            sebesseg.Add(sebessegKategoria);
        }
    }

    Console.WriteLine($"4. Feladat: Adatsorok száma: {sebesseg.Count()}");
    Console.WriteLine($"5. Feladat: A Boeing által gyártott repülőgéptípusok száma: {boeingCount}");
    Console.WriteLine($"6. Feladat: Legtöbb utast szállító típus: {maxUtasTipus} ({maxUtasEv}) - {maxUtas} fő");

    var kategoriak = new[] { "Alacsony sebességű", "Szubszonikus", "Transzszonikus", "Szuperszonikus" };
    var nincsRepulo = kategoriak.Where(kat => !sebesseg.Any(s => s.Kategorianev == kat)).ToList();

    if (nincsRepulo.Count == 0)
    {
        Console.WriteLine("7. Feladat: Minden sebességkategóriából van repülőgéptípus.");
    }
    else
    {
        Console.WriteLine("7. Feladat: Ebből a kategóriából nincs repülő: " + string.Join(" ", nincsRepulo));
    }

    Console.ReadKey();
}