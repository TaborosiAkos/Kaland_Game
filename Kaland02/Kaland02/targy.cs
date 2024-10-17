using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaland02
{

    public class targy
    {
        public int Id { get; set; }
        public string Nev { get; set; }
        public bool Kulcs { get; set; } // true, ha kulcs

        public targy(int id, string nev, bool kulcs)
        {
            Id = id;
            Nev = nev;
            Kulcs = kulcs;
        }
    }



}
