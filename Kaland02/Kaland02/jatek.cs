using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Kaland02
{


    public class Jatek
    {
        private Terkep terkep;
        private int aktualisSzobaId;
        private List<int> inventory = new List<int>();
        private bool kerdes = false;
        private bool fa_vagas = true;
        private bool fa_kidölt = false;
        private int tippek = 0;
        private int fa = 5;



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
                if (irany == "F" && aktualisSzoba.Id !=4) {
                    Console.Clear();
                    Felveszem();

                }
                else if (irany == "F" && aktualisSzoba.Id == 4 && !kerdes)
                {
                    Console.Clear();
                    Beszelgetes(aktualisSzoba);
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
            if (aktualisSzoba.Id == 4 && !kerdes && irany == "É")
            {
                inventory.Remove(5);
            }
            if (aktualisSzoba.Ajtok.ContainsKey(irany))
            {
                var ajto = aktualisSzoba.Ajtok[irany];
                if (ajto.Item1.Contains("Zárt"))
                {
                    bool seged = false;
                    for (int i = 0; i < inventory.Count(); i++)
                    {
                        targy targy = terkep.GetTargy(inventory[i]);
                        if (targy.Id == ajto.Item2 && targy.Id != 5)
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
                            if (inventory.Contains(5))
                            {
                                Favagas();
                                if (fa == 0)
                                {
                                    aktualisSzobaId = ajto.Item2;
                                }
                                else
                                {
                                    Console.WriteLine("Áh elrontottam");
                                    Console.Clear();
                                }
                            }
                            else
                            {
                                Console.WriteLine("A bejáratot egy nagy fa torlaszolta el!");
                            }
                        }else if (ajto.Item2 == 6)
                        {
                            Console.WriteLine("Az ajtó előtt egy nagy mamut áll??");
                            Console.WriteLine("Meg kell várnom míg elmegy!");
                        }
                        else
                        {
                            Console.WriteLine("Az ajtó zárva van.");
                        }
                        
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
                string[] cucc = szoba.Ellenseg_monolog.Split("%");

                if (szoba.Id == 4)
                {
                    inventory.Add(5);
                    for (int i = 0; i < cucc.Length-3; i++)
                    {
                        Console.WriteLine(cucc[i]);
                    }
                    while (!kerdes) { 
                    string valasz = Console.ReadLine();
                    if (valasz != null && valasz.ToUpper() == "NAPFÉNY")
                    {
                        Console.WriteLine(cucc[cucc.Length-3]);
                        kerdes = true;
                    }else if (valasz != null && valasz.ToUpper() != "NAPFÉNY" && tippek == 3)
                        {
                            Console.WriteLine(cucc[cucc.Length - 2]);
                            kerdes = true;
                        }
                    else
                    {
                        Console.WriteLine(cucc[^1]);
                        tippek++;
                    }
                    }
                }
                else
                {
                    Console.WriteLine("{0}: ", szoba.Ellenseg);
                    for (int i = 0; i < cucc.Length; i++)
                    {
                        Console.WriteLine(cucc[i]);
                    }
                }
                

               

            }
            else
            {
                Console.WriteLine("Skizofrén vagyok itt nincs is senki!");
            }
        }
        private void Favagas()
        {
            if (fa > 0)
            {
                fa_vagas = true;
                Console.WriteLine("Fhu oke mehet a fa vágás. (nyomd meg a v-betűt mielőtt letelne az idő)");
                Thread.Sleep(3000);
            }
            
                while (fa_vagas)
                {
                    Console.WriteLine("Most:");
                    string? vag = null;
                    Thread inputThread = new Thread(() =>
                    {
                        vag = Console.ReadLine();

                    });
                    inputThread.Start();
                    inputThread.Join(1000);
                    if (vag == null || vag != "v")
                    {
                        fa_vagas = false;
                        fa = 5;
                        Console.Clear();
                }
                    if (vag == "v")
                    {
                        Console.WriteLine("most jo");
                        fa--;
                        if (fa == 0)
                        {
                            fa_vagas = false;
                        }

                    }


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
