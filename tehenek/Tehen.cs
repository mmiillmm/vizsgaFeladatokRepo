using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tehenek
{
    class Tehen : IEquatable<Tehen>
    {

        public string Id { get; private set; }
        public int[] Mennyisegek { get; private set; }

        public bool Equals(Tehen masik)
        {
            return masik != null && masik.Id == this.Id;
        }

        public void EredmenytRogzit(string nap, string menyiseg)
        {
            Mennyisegek[int.Parse(nap)] = int.Parse(menyiseg);
        }

        public int HetiTej
        {
            get
            {
                return Mennyisegek.Sum();
            }
        }

        public int HetiAtlag
        {
            get
            {
                var validDays = Mennyisegek.Where(m => m > 0).ToArray();
                if (validDays.Length < 3)
                    return -1;
                return (int)Math.Round(validDays.Average());
            }
        }

        public Tehen(string id)
        {
            Id = id;
            Mennyisegek = new int[7];
        }
    }
}
