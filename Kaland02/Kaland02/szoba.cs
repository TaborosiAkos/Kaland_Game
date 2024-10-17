using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaland02
{

    public class szoba
    {
        public int Id { get; set; }
        public string Nev { get; set; }
        public Dictionary<string, Tuple<string, int>> Ajtok { get; set; } // Irány (É, NY, D, K) -> (Ajtó típusa, Szoba ID)
        public List<int> Targyak { get; set; } // Tárgyak azonosítói
        public string Ellenseg { get; set; } // Ellenség típusa (ha van)
        public string Ellenseg_monolog { get; set; } // Ellenség beszéde (ha van)

        public szoba(int id, string nev, Dictionary<string, Tuple<string, int>> ajtok, List<int> targyak, string ellenseg, string ellenseg_monolog)
        {
            Id = id;
            Nev = nev;
            Ajtok = ajtok;
            Targyak = targyak;
            Ellenseg = ellenseg;
            Ellenseg_monolog = ellenseg_monolog;
        }
    }




}
