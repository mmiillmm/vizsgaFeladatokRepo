using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace text2srt
{
    internal class IdozitettFelirat
    {
        public IdozitettFelirat(string idozites, string felirat)
        {
            Idozites = idozites;
            Felirat = felirat;
        }

        public string Idozites { get; set; }
        public string Felirat { get; set; }

        public int SzavakSzama => Felirat.Split(' ').Length;

        public string SrtIdozites
        {
            get
            {
                var parts = Idozites.Split(" - ");
                return $"{ToSrtTime(parts[0])} --> {ToSrtTime(parts[1])}";
            }
        }

        private static string ToSrtTime(string percMasodperc)
        {
            var pm = percMasodperc.Split(':');
            int perc = int.Parse(pm[0]);
            int mp = int.Parse(pm[1]);
            int ora = perc / 60;
            int percMaradek = perc % 60;
            return $"{ora:00}:{percMaradek:00}:{mp:00}";
        }
    }
}
