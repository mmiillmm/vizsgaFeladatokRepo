using System.Runtime.CompilerServices;
using tehenek;

List<Tehen> happyCows = new List<Tehen>();

using (StreamReader sr = new(@"..\..\..\src\hozam.txt", System.Text.Encoding.UTF8))
{
    while (!sr.EndOfStream)
    {
        string? line = sr.ReadLine();
        string[] parts = line.Split(';');

        string id = parts[0];
        string nap = parts[1];
        string menny = parts[2];

        Tehen? aktTehen = happyCows.FirstOrDefault(t => t.Id == id);
        if (aktTehen == null)
        {
            aktTehen = new Tehen(id);
            happyCows.Add(aktTehen);
        }

        aktTehen.EredmenytRogzit(nap, menny);
    }

    Console.WriteLine($"3. Feladat:\n Az állomány {happyCows.Count} tehén adatait tartalmazza.");
}

var eligibleCows = happyCows.Where(t => t.Mennyisegek.Count(m => m > 0) >= 3).ToList();

using (StreamWriter sw = new(@"..\..\..\joltejelok.txt", false, System.Text.Encoding.UTF8))
{
    foreach (var cow in eligibleCows)
    {
        sw.WriteLine($"{cow.Id} {cow.HetiAtlag}");
    }
}

Console.WriteLine($"6. Feladat:\n {eligibleCows.Count} darab sort írtam az állományba.");

Console.Write("7. Feladat: Kérem egy tehén azonosítóját:\n ");
string? keresettId = Console.ReadLine();

int leszarmazottak = happyCows.Count(t => t.Id.StartsWith(keresettId) && t.Id.Length > keresettId.Length);

Console.WriteLine($"A leszármazottak száma: {leszarmazottak}");

Console.ReadKey();