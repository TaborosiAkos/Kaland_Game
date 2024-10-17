using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaland02
{


    public class Jatek
    {
        private Terkep terkep;
        private int aktualisSzobaId;
        private List<int> inventory = new List<int>();
        public szoba szoba60;

        public Jatek()
        {
            terkep = new Terkep();
            BetoltJatekAdatok();
            aktualisSzobaId = 1; // Kezdő szoba
        }

        private void BetoltJatekAdatok()
        {
            string szobaFajl = "terkep.txt"; // Szobák adatai fájl
            string targyFajl = "targyak.txt"; // Tárgyak adatai fájl

            terkep.BetoltTerkepFajlbol(szobaFajl, targyFajl);
        }

        public void Start()
        {
            Console.WriteLine("Játék indul!");
            while (true)
            {
                szoba aktualisSzoba = terkep.GetSzoba(aktualisSzobaId);
                Console.WriteLine($"Jelenlegi helyszín: {aktualisSzoba.Nev}");

                MegjelenitSzobaInfo(aktualisSzoba);

                Console.WriteLine("Merre szeretnél menni? (É/NY/D/K vagy 'kilépés' a kilépéshez)");
                string irany = Console.ReadLine().ToUpper();

                if (irany == "KILÉPÉS") break;
                if (irany == "F") {
                    Console.Clear();
                    Felveszem();
                    
                }
                else if(irany == "E")
                {
                    Console.Clear();
                    Inventory();
                    
                }
                else if(irany == "Z")
                {
                    
                }
                else if(irany == "B")
                {
                    Console.Clear();
                    Beszelgetes(aktualisSzoba);

                }
                else if (aktualisSzoba.Id == 3 && irany == "MEGSIMOGAT")
                {
                    Secret_Ending();
                }
                else
                {
                    Console.Clear();
                    MozogJatekos(irany);
                    
                }
                
            }
        }

        private void MegjelenitSzobaInfo(szoba szoba)
        {

            foreach (var ajto in szoba.Ajtok)
            {
                
                if (ajto.Value.Item2 != 0)
                {
                    szoba aktualisSzoba = terkep.GetSzoba(ajto.Value.Item2);
                    Console.WriteLine($"{ajto.Key} irányban {ajto.Value.Item1} vezet a(z){aktualisSzoba.Nev}-ba/be");
                }
            }
            if (szoba.Targyak.Count > 0)
            {
                Console.WriteLine("A szobában található tárgyak:");                
                foreach (var targyId in szoba.Targyak)
                {
                    targy targy = terkep.GetTargy(targyId);
                    if (targy != null && !inventory.Contains(targyId))
                    {
                        Console.WriteLine($"- {targy.Nev}");
                    }
                }
            }

            if (!string.IsNullOrEmpty(szoba.Ellenseg))
            {
                Console.WriteLine($"Ember(ek): {szoba.Ellenseg}");
            }
        }

        private void MozogJatekos(string irany)
        {
            szoba aktualisSzoba = terkep.GetSzoba(aktualisSzobaId);
            if (aktualisSzoba.Ajtok.ContainsKey(irany))
            {
                var ajto = aktualisSzoba.Ajtok[irany];
                if (ajto.Item1 == "Z")
                {
                    bool seged = false;
                    for (int i = 0; i < inventory.Count(); i++)
                    {
                        targy targy = terkep.GetTargy(inventory[i]);
                        if (targy.Id == ajto.Item2)
                        {
                            seged = true;
                        }
                        
                        
                    }
                    if (seged)
                    {
                        Console.WriteLine("Az ajtó kinyílt!");
                        aktualisSzobaId = ajto.Item2;
                    }
                    else
                    {
                        if (ajto.Item2 == 5)
                        {
                            Console.WriteLine("A bejáratot egy nagy fa torlaszolta el!");
                        }
                        Console.WriteLine("Az ajtó zárva van.");
                    }
                }
                else if (ajto.Item2 != 0)
                {
                    aktualisSzobaId = ajto.Item2;
                    Console.WriteLine("Mozgás sikerült.");
                }
                else
                {
                    Console.WriteLine("Erre nem vezet út.");
                }
            }
            else
            {
                Console.WriteLine("Nem érvényes irány.");
            }
        }
        private void Felveszem()
        {
            szoba aktualisSzoba = terkep.GetSzoba(aktualisSzobaId);
            List<int> targyak = aktualisSzoba.Targyak;
            if (targyak.Count() <= 0)
            {
                Console.WriteLine("Itt nincs mit felvennem!");
                Console.WriteLine(" ");
            }
            else
            {
                

                if (!inventory.Contains(aktualisSzoba.Targyak[0]))
                {
                    if (aktualisSzoba.Targyak[0] == 4)
                    {
                        Console.WriteLine("Ezt inkább nem veszem fel!");
                    }
                    else
                    {
                        int targy_id = targyak[0];
                        inventory.Add(targy_id);
                        targy targy = terkep.GetTargy(targy_id);
                        Console.WriteLine(targy.Nev);
                        Console.WriteLine("Most felveszem!");
                        Console.WriteLine(" ");
                    }
                    
                }
                else
                {
                    Console.WriteLine("Már felvettem mindent!");
                }
            }
 
        }

        private void Inventory()
        {
            Console.WriteLine("A tárgyaid listája: ");
            for (int i = 0; i < inventory.Count(); i++)
            {
                targy targy = terkep.GetTargy(inventory[i]);
                Console.WriteLine(targy.Id);
                Console.WriteLine(" ");
            }
        }

        private void Beszelgetes(szoba szoba)
        {
            if (szoba.Ellenseg != "0" && szoba.Ellenseg_monolog != "-")
            {
                Console.WriteLine(szoba.Ellenseg_monolog);
            }
            else
            {
                Console.WriteLine("Skizofrén vagyok itt nincs is senki!");
            }
        }

        private void Secret_Ending()
        {
            Console.WriteLine("Secret Ending unlocked!");
            Console.WriteLine("Nem vagy az erőszak híve és megsimogattad a mamutot. Ez egy jó döntés volt mivel így sikerült mindenkit megmentened vérontás nélkül.");
            Console.WriteLine("Ending 2/2");
            System.Environment.Exit(0);
        }

    }

}
