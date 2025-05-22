using text2srt;

List<IdozitettFelirat> idfel = new List<IdozitettFelirat>();

using (StreamReader sr = new(@"..\..\..\src\feliratok.txt", System.Text.Encoding.UTF8))
{
    while (!sr.EndOfStream)
    {
        string? timing = sr.ReadLine();
        if (string.IsNullOrWhiteSpace(timing)) break;

        string? subtitle = sr.ReadLine();
        if (string.IsNullOrWhiteSpace(subtitle)) break;

        idfel.Add(new IdozitettFelirat(timing, subtitle));
    }
}

Console.WriteLine($"5. feladat - Feliratok száma: {idfel.Count}");

if (idfel.Count > 0)
{
    var maxFelirat = idfel.MaxBy(f => f.SzavakSzama);
    Console.WriteLine("7. feladat - A legtöbb szóból álló felirat:");
    Console.WriteLine(maxFelirat.Felirat);
}

using (StreamWriter sw = new(@"..\..\..\src\felirat.srt", false, System.Text.Encoding.UTF8))
{
    for (int i = 0; i < idfel.Count; i++)
    {
        sw.WriteLine(i + 1);
        sw.WriteLine(idfel[i].SrtIdozites);
        sw.WriteLine(idfel[i].Felirat);
        sw.WriteLine();
    }
}

Console.ReadKey();