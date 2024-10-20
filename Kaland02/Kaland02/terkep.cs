using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaland02
{

    using System;
    using System.Collections.Generic;
    using System.IO;

    public class Terkep
    {
        public Dictionary<int, szoba> Szobak { get; set; }
        public Dictionary<int, targy> Targyak { get; set; }

        public Terkep()
        {
            Szobak = new Dictionary<int, szoba>();
            Targyak = new Dictionary<int, targy>();
        }

        public void BetoltTerkepFajlbol(string szobaFajl, string targyFajl)
        {
            BetoltSzobak(szobaFajl);
            BetoltTargyak(targyFajl);
        }

        private void BetoltSzobak(string fajl)
        {
            using (StreamReader olvaso = new StreamReader(fajl))
            {
                string sor;
                while ((sor = olvaso.ReadLine()) != null)
                {
                    string[] adatok = sor.Split('|');
                    int id = int.Parse(adatok[0]);
                    string nev = adatok[^1]; // Szoba neve az utolsó mező

                    var ajtok = new Dictionary<string, Tuple<string, int>>();
                    ajtok["É"] = AjtoParse(adatok[1]);
                    ajtok["NY"] = AjtoParse(adatok[2]);
                    ajtok["D"] = AjtoParse(adatok[3]);
                    ajtok["K"] = AjtoParse(adatok[4]);

                    List<int> targyak = new List<int>();
                    if (adatok[5].Split(':')[1] != "0")
                    {
                        targyak.Add(int.Parse(adatok[5].Split(':')[1])); // Tárgy ID
                    }

                    string[] ellensegek = adatok[6].Split(':')[1].Split('+'); // Ellenség típusa
                    string ellenseg_monolog = adatok[7]; // Ellenség beszéde

                    szoba szoba = new szoba(id, nev, ajtok, targyak, ellensegek, ellenseg_monolog);
                    HozzaadSzoba(szoba);
                }
            }
        }

        private Tuple<string, int> AjtoParse(string ajtoInfo)
        {
            string[] ajtoAdatok = ajtoInfo.Split(':');
            string ajtoTipus = ajtoAdatok[1]; // Ajtó típusa (A, Z, L)
            int szobaId = int.Parse(ajtoAdatok[2]); // Szoba ID
            return Tuple.Create(ajtoTipus, szobaId);
        }

        private void BetoltTargyak(string fajl)
        {
            using (StreamReader olvaso = new StreamReader(fajl))
            {
                string sor;
                while ((sor = olvaso.ReadLine()) != null)
                {
                    string[] adatok = sor.Split('|');
                    int id = int.Parse(adatok[0]);
                    bool kulcs = adatok[1] == "Z"; // Z = kulcs
                    string nev = adatok[2];

                    targy targy = new targy(id, nev, kulcs);
                    HozzaadTargy(targy);
                }
            }
        }

        public void HozzaadSzoba(szoba szoba)
        {
            Szobak[szoba.Id] = szoba;
        }

        public void HozzaadTargy(targy targy)
        {
            Targyak[targy.Id] = targy;
        }

        public szoba GetSzoba(int szobaId)
        {
            return Szobak.ContainsKey(szobaId) ? Szobak[szobaId] : null;
        }

        public targy GetTargy(int targyId)
        {
            return Targyak.ContainsKey(targyId) ? Targyak[targyId] : null;
        }
    }



}
