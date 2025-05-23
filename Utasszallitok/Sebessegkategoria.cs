using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utasszallitok
{
    class Sebessegkategoria
    {
        private int Utazosebesseg;
        public string Kategorianev
        {
            get
            {
                if (Utazosebesseg < 500) return "Alacsony sebességű";
                else if (Utazosebesseg < 1000) return "Szubszonikus";
                else if (Utazosebesseg < 1200) return "Transzszonikus";
                else return "Szuperszonikus";
            }
        }
        public Sebessegkategoria(int utazosebesseg)
        {
            Utazosebesseg = utazosebesseg;
        }
    }
}
