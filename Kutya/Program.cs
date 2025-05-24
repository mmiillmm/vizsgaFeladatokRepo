using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
//Tools/NuGet Package Manager Console-ba a következő parancsot kell beírni:
//Install-Package MySql.Data

public class Fajta
{
    public int Id { get; private set; }
    public string FajtaNev { get; private set; }

    public Fajta(int id, string fajtaNev)
    {
        Id = id;
        FajtaNev = fajtaNev;
    }
}

public class Gazda
{
    public int Id { get; private set; }
    public string Nev { get; private set; }
    public string Tel { get; private set; }

    public Gazda(int id, string nev, string tel)
    {
        Id = id;
        Nev = nev;
        Tel = tel;
    }
}

public class Kutya
{
    public DateTime ChipDatum { get; private set; }
    public Fajta Fajta { get; private set; }
    public Gazda Gazda { get; private set; }
    public int Id { get; private set; }
    public bool Kan { get; private set; }
    public string KepUrl { get; private set; }
    public int Kor { get; private set; }
    public string Nev { get; private set; }

    public Kutya(int id, string nev, bool kan, int kor, DateTime chipDatum, string kepUrl, Fajta fajta, Gazda gazda)
    {
        Id = id;
        Nev = nev;
        Kan = kan;
        Kor = kor;
        ChipDatum = chipDatum;
        KepUrl = kepUrl;
        Fajta = fajta;
        Gazda = gazda;
    }

    public override string ToString()
    {
        return $"ID: {Id}\nNév: {Nev}\nKan: {(Kan ? "Igen" : "Nem")}\nKor: {Kor}\nChip Dátum: {ChipDatum.Date}\nKép URL: {KepUrl}\nFajta: {Fajta.FajtaNev}\nGazda neve: {Gazda.Nev}\n";
    }
}

class Program
{
    static void Main()
    {
        string connectionString = "server=localhost;user=root;password=;database=kutya";


        List<Kutya> kutyaLista = new List<Kutya>();

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            string kutyaQuery = 
                "SELECT kutyak.id, kutyak.nev, kan, fajtak.id, fajtak.fajta, gazdak.id, gazdak.nev, gazdak.tel, kor, chipdatum, kepurl FROM kutyak inner join gazdak on kutyak.gazdaid = gazdak.id inner join fajtak on kutyak.fajtaid = fajtak.id";
            using (MySqlCommand command = new MySqlCommand(kutyaQuery, connection))
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string nev = reader.GetString(1);
                    bool kan = reader.GetBoolean(2);
                    Fajta fajta = new Fajta(reader.GetInt32(3), reader.GetString(4));
                    Gazda gazda = new Gazda(reader.GetInt32(5), reader.GetString(6), reader.GetString(7));
                    int kor = reader.GetInt32(8);
                    DateTime chipDatum = reader.GetDateTime(9);
                    string kepUrl = reader.GetString(10);

                    Kutya kutya = new Kutya(id, nev, kan, kor, chipDatum, kepUrl, fajta, gazda);
                    kutyaLista.Add(kutya);
                }
            }
            connection.Close();
        }

        var rendezettList = kutyaLista.OrderBy(k => k.Nev).ToList();

        Console.WriteLine("Kutyák adatai név szerint rendezve:\n");
        foreach (var kutya in rendezettList)
        {
            Console.WriteLine(kutya);
        }

        Console.WriteLine("Nyomj egy billentyűt a kilépéshez...");
        Console.ReadKey();
    }
}
