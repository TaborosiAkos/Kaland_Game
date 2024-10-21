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
        private bool aloevera = false;
        private bool beszelt_Feri = false;
        private bool beszelt_Viktor = false;
        private bool beszelt_fa = false;
        private bool tej = false;
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

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Játék indul!");
            while (true)
            {
                szoba aktualisSzoba = terkep.GetSzoba(aktualisSzobaId);
                Console.ForegroundColor= ConsoleColor.White;
                Console.WriteLine($"Jelenlegi helyszín: {aktualisSzoba.Nev}");

                MegjelenitSzobaInfo(aktualisSzoba);

                Console.WriteLine("Merre szeretnél menni?/Mit szeretnél csinálni? (É/NY/D/K/B/F vagy 'kilépés' a kilépéshez)");
                string irany = Console.ReadLine().ToUpper();

                if (irany == "KILÉPÉS") {
                    Console.ForegroundColor = ConsoleColor.Black; 
                    break;
                } 
                if (irany == "F" && aktualisSzoba.Id !=4) {
                    Console.Clear();
                    Felveszem();

                }
                else if (irany == "F" && aktualisSzoba.Id == 4 && !kerdes)
                {
                    Console.Clear();
                    Beszelgetes(aktualisSzoba);
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
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write(ajto.Key);
                    Console.ForegroundColor= ConsoleColor.White;
                    Console.WriteLine($" irányban {ajto.Value.Item1} vezet a(z){aktualisSzoba.Nev}-ba/be");
                }
            }
            if (szoba.Targyak.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("A szobában található tárgyak:");
                Console.ForegroundColor = ConsoleColor.White;
                foreach (var targyId in szoba.Targyak)
                {
                    targy targy = terkep.GetTargy(targyId);
                    if (targy != null && !inventory.Contains(targyId))
                    {
                        Console.WriteLine($"- {targy.Nev}");
                    }
                }
            }

            if (szoba.Ellenseg[0] != "0")
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"Ember(ek):");
                Console.ForegroundColor = ConsoleColor.White;
                for (int i = 0; i < szoba.Ellenseg.Count(); i++)
                {
                    Console.Write("{0} ",szoba.Ellenseg[i]);
                }
                Console.WriteLine(" ");
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
                        Console.ForegroundColor = ConsoleColor.Green;
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
                                    Console.Clear();
                                    aktualisSzobaId = ajto.Item2;
                                    inventory.Add(6);
                                    
                                }
                                else
                                {
                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                    Console.WriteLine("Áh, elrontottam!");
                                    
                                }
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("A bejáratot egy nagy fa torlaszolta el!");
                            }
                        }else if (ajto.Item2 == 6)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("Az ajtó előtt egy gigászi mamut áll??");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("Meg kell várnom míg elmegy!");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("Az ajtó zárva van.");
                        }
                        
                    }
                }
                else if (ajto.Item2 != 0)
                {
                    aktualisSzobaId = ajto.Item2;
                    if (ajto.Item2 != 5 && !beszelt_Feri && inventory.Contains(6))
                    {
                        inventory.Remove(6);
                    }
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Mozgás sikerült.");
                    if (aktualisSzobaId == 10)
                    {
                        inventory.Add(7);
                    }
                }
                else
                {
                    Console.ForegroundColor= ConsoleColor.DarkRed;
                    Console.WriteLine("Erre nem vezet út.");
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Nem érvényes irány.");
            }
        }
        private void Felveszem()
        {
            szoba aktualisSzoba = terkep.GetSzoba(aktualisSzobaId);
            List<int> targyak = aktualisSzoba.Targyak;
            if (targyak.Count() <= 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Itt nincs mit felvennem!");
            }
            else
            {
                

                if (!inventory.Contains(aktualisSzoba.Targyak[0]))
                {
                    if (aktualisSzoba.Targyak[0] == 4)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Ezt inkább nem veszem fel!");
                    }
                    else
                    {
                        int targy_id = targyak[0];
                        inventory.Add(targy_id);
                        targy targy = terkep.GetTargy(targy_id);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Felvetted: {0}",targy.Nev);
                    }
                    
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Már felvettem mindent!");
                }
            }
 
        }

        private void Beszelgetes(szoba szoba)
        {
            if (szoba.Ellenseg[0] != "0" && szoba.Ellenseg_monolog != "-")
            {
                string[] cucc = szoba.Ellenseg_monolog.Split("%");

                if (szoba.Id == 4)
                {
                    Favago_kerdes(cucc);
                }else if (szoba.Id == 5)
                {
                    targy targy = terkep.GetTargy(inventory[^1]);
                    string item = targy.Nev;
                    Feri_beszel(cucc, item);
                }
                else if (szoba.Id == 10)
                {
                    targy targy = terkep.GetTargy(inventory[^1]);
                    string item = targy.Nev;
                    Vitya_beszel(cucc, item);
                }
                else if(szoba.Id == 9)
                {
                    Feri_beszel2(cucc);
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
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Itt nincs is senki!");
            }
        }

        private void Feri_beszel(string[] cucc, string nev)
        {
            for (int i = 0; i < (cucc.Length - 3); i++)
            {
                Console.WriteLine(cucc[i]);
            }
            while (!beszelt_Feri)
            {  
                Console.WriteLine("{0} {1}", cucc[cucc.Length - 3], nev);
                Console.WriteLine("Megiszod? (i/n)");
                string valasz = Console.ReadLine();
                if (valasz.ToUpper() == "I")
                {
                    Console.Clear();
                    aloevera = true;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(cucc[cucc.Length - 2]);
                    beszelt_Feri = true;
                }
                else if (valasz.ToUpper() == "N")
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(cucc[^1]);
                    beszelt_Feri = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Ezt nem választhatod!");
                }

            }
        }

        private void Feri_beszel2(string[] cucc)
        {
            int reag = 3000;
            for (int i = 0; i < (cucc.Length); i++)
            {
                Console.WriteLine(cucc[i]);
            }
            if (aloevera)
            {
                
                for (int i = 0; i < 10; i++)
                {
                    Console.SetCursorPosition(0, 6);
                    if ( i % 2 == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                    }

                    Console.WriteLine("Áh, megittam amit adott, ezért még gyorsabban kell reagálnom!");
                    Thread.Sleep(500);
                }

                reag = 2000;
            }
            Thread.Sleep(9500);
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Feri elkezd megidézni egy portált, amiről még ő sem tudja, melyik dimenzióba vezet.");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Gyorsan kell cselekedned!");
            if (tej)
            {
                int vegTej = 0;
                while (vegTej == 0)
                {
                    Console.WriteLine("Rádobom a tejet (t)/ Magammal rántom a portálba (p)");
                    Thread inputThread = new Thread(() =>
                    {
                        var keyInfo = Console.ReadKey(intercept: true);


                        if (keyInfo.Key == ConsoleKey.T)
                        {
                            vegTej = 1;
                        }
                        else if (keyInfo.Key == ConsoleKey.P)
                        {
                            vegTej = 2;
                        }
                    });
                    inputThread.Start();
                    inputThread.Join(reag);

                    if (vegTej == 1)
                    {
                        Console.Clear();
                        Console.WriteLine("NEEEE! A 4,5%-os tej az egyetlen gyengém!");
                        Console.WriteLine();
                        Console.WriteLine("Megmentettél mindenkit! Feri a tej hatására egy ártalmatlan békává változott és Petivel él tovább.");
                        Console.WriteLine("Te és Vivien összeházasodtatok, és lett egy gyereketek, Viktor. Nevét arról a féfiról kapta, aki a tejet adta neked,");
                        Console.WriteLine("és akit senki nem látott se a történtek előtt, se a történtek után");
                        Console.WriteLine("Vágó úr otthagyta a favágást, és teljes állásban kvíz műsorokat vezet.");
                        Console.WriteLine("A falut nem érte több árvíz, így mindenki boldogan él tovább.");
                        Thread.Sleep(14000);
                        Credits();
                    }
                    else if (vegTej == 2) {
                        Console.Clear();
                        Console.WriteLine("MIT CSINÁLSZ???");
                        Console.WriteLine();
                        Console.WriteLine("Megmentettél mindenkit! De milyen áron....");
                        Console.WriteLine("Te és Feri is átkerültetek egy másik világba, ahol folyamatos éjszaka van.");
                        Console.WriteLine("Próbáljátok kerülni egymást, így inkább magányosan töltötik mindennapjaitok.");
                        Console.WriteLine("A faluban ez a nap azóta a te napod. Mindenki megemlékezik rőlad, és reménykednek hogy egyszer még visszatérsz.");
                        Console.WriteLine("A falut nem érte több árvíz, így ők boldogan élnek tovább. Nem volt hiábavaló az áldozatod.");
                        Thread.Sleep(14000);
                        Credits();
                    }

                }
            }
            else if (!tej) {
                int vegPortal = 0;
                while (vegPortal == 0)
                {
                    Console.WriteLine("Magammal rántom a portálba (p)");
                    Thread inputThread = new Thread(() =>
                    {
                        var keyInfo = Console.ReadKey(intercept: true);


                        if (keyInfo.Key == ConsoleKey.P)
                        {
                            vegPortal = 1;
                        }
                        else
                        {
                            vegPortal = 2;
                        }
                    });
                    inputThread.Start();
                    inputThread.Join(reag);

                    if (vegPortal == 1)
                    {
                        Console.Clear();
                       Console.WriteLine("MIT CSINÁLSZ???");
                        Console.WriteLine();
                        Console.WriteLine("Megmentettél mindenkit! De milyen áron....");
                        Console.WriteLine("Te és Feri is átkerültetek egy másik világba, ahol folyamatos éjszaka van.");
                        Console.WriteLine("Próbáljátok kerülni egymást, így inkább magányosan töltötik mindennapjaitok.");
                        Console.WriteLine("A faluban ez a nap azóta a te napod. Mindenki megemlékezik rólad, és reménykednek hogy egyszer még visszatérsz.");
                        Console.WriteLine("A falut nem érte több árvíz, így ők boldogan élnek tovább. Nem volt hiábavaló az áldozatod.");
                        Thread.Sleep(14000);
                        Credits();
                    }
                    else {
                        Console.Clear();
                        Console.WriteLine("HA HA HA HA");
                        Console.WriteLine();
                        Console.WriteLine("Nem voltál elég gyors....");
                        Console.WriteLine("Átkerültél egy másik világba, ahol folyamatos éjszaka van.");
                        Console.WriteLine("Próbálsz visszajutni, de eddig még nem sikerült, így egyedül töltöd mindennapjaid.");
                        Console.WriteLine("A faluban ez a nap azóta a te napod. Ferit elüldözték, és mindenki megemlékezik rólad, reménykedve hogy egyszer még visszatérsz.");
                        Console.WriteLine("A falut nem érte több árvíz, így ők boldogan él tovább. Te pedig reménykedsz, hogy egy nap visszatérhetsz.");
                        Thread.Sleep(14000);
                        Credits();
                    }
                }
            }

        }

        private void Vitya_beszel(string[] cucc, string nev)
        {
            for (int i = 0; i < (cucc.Length - 3); i++)
            {
                Console.WriteLine(cucc[i]);
            }
            while (!beszelt_Viktor)
            {
                Console.WriteLine("{0} {1}", cucc[cucc.Length - 3], nev);
                Console.WriteLine("Elveszed? (i/n)");
                string valasz = Console.ReadLine();
                if (valasz.ToUpper() == "I")
                {
                    Console.Clear();
                    Console.WriteLine(cucc[cucc.Length - 2]);
                    tej = true;
                    beszelt_Viktor = true;
                }
                else if (valasz.ToUpper() == "N")
                {
                    Console.Clear();
                    Console.WriteLine(cucc[^1]);
                    beszelt_Viktor = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Ezt nem választhatod!");
                }
            }

        }


        private void Favago_kerdes(string[] cucc)
        {
            inventory.Add(5);
            if (!beszelt_fa)
            {
                for (int i = 0; i < cucc.Length - 3; i++)
                {
                    Console.WriteLine(cucc[i]);
                }
            }
            else
            {
                Console.WriteLine("Ugye milyen jó kérdést találtam ki!");
            }

            while (!kerdes)
            {
                string valasz = Console.ReadLine();
                if (valasz != null && valasz.ToUpper() == "NAPFÉNY")
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine(cucc[cucc.Length - 3]);
                    kerdes = true;
                    beszelt_fa = true;
                }
                else if (valasz != null && valasz.ToUpper() != "NAPFÉNY" && tippek == 3)
                {
                    Console.Clear();
                    Console.ForegroundColor= ConsoleColor.Red;
                    Console.WriteLine(cucc[cucc.Length - 2]);
                    kerdes = true;
                    beszelt_fa = true;
                }
                else
                {
                    
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine(cucc[^1]);
                    Console.ForegroundColor = ConsoleColor.White;
                    tippek++;
                }
            }
        }
        private void Favagas()
        {
            if (fa > 0)
            {
                fa_vagas = true;
                Console.WriteLine("Fhu, oké, mehet a favágás. (nyomd meg a v-betűt mielőtt letelne az idő)");
                Thread.Sleep(1000);
            }
            
                while (fa_vagas)
                {
                    Thread.Sleep(1000);
                    Console.WriteLine("Most:");
                    string? vag = null;
                    Thread inputThread = new Thread(() =>
                    {
                        var keyInfo = Console.ReadKey(intercept: true);

                        if (keyInfo.Key == ConsoleKey.V)
                        {
                            vag = "v";
                        }
                        
                        

                    });
                    inputThread.Start();
                    inputThread.Join(500);
                    if (vag == null || vag != "v")
                    {
                        fa_vagas = false;
                        fa = 5;
                }
                if (vag == "v")
                {
                    Console.WriteLine("Áh jó");
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
            Console.WriteLine("Nem vagy az erőszak híve, és megsimogattad a mamutot. Ez egy jó döntés volt, mivel így sikerült mindenkit megmentened vérontás nélkül.");
            Console.WriteLine("Ending 2/2");
            Console.ForegroundColor = ConsoleColor.Black;
            System.Environment.Exit(0);
        }

        private void Credits()
        {
            Console.Clear();
            Console.SetCursorPosition(30, 26);
            Console.WriteLine("Készítette: Nagy Zsombor, Táborosi Ákos");
            Console.SetCursorPosition(30, 27);
            Console.WriteLine("Történetet írta: Nagy Zsombor");
            Console.SetCursorPosition(30, 28);
            Console.WriteLine("Programozta: Táborosi Ákos");
            Console.SetCursorPosition(30, 29);
            Console.WriteLine("A történet valós események alapján készült....");
            Thread.Sleep(1000);
            Console.Clear();
            Console.SetCursorPosition(30, 13);
            Console.WriteLine("Készítette: Nagy Zsombor, Táborosi Ákos");
            Console.SetCursorPosition(30, 14);
            Console.WriteLine("Történetet írta: Nagy Zsombor");
            Console.SetCursorPosition(30, 15);
            Console.WriteLine("Programozta: Táborosi Ákos");
            Console.SetCursorPosition(30, 16);
            Console.WriteLine("A történet valós események alapján készült....");
            Thread.Sleep(1000);
            Console.Clear();
            Console.SetCursorPosition(30, 7);
            Console.WriteLine("Készítette: Nagy Zsombor, Táborosi Ákos");
            Console.SetCursorPosition(30, 8);
            Console.WriteLine("Történetet írta: Nagy Zsombor");
            Console.SetCursorPosition(30, 9);
            Console.WriteLine("Programozta: Táborosi Ákos");
            Console.SetCursorPosition(30, 10);
            Console.WriteLine("A történet valós események alapján készült....");
            Thread.Sleep(1000);
            Console.Clear();
            Console.SetCursorPosition(30, 0);
            Console.WriteLine("Készítette: Nagy Zsombor, Táborosi Ákos");
            Console.SetCursorPosition(30, 1);
            Console.WriteLine("Történetet írta: Nagy Zsombor");
            Console.SetCursorPosition(30, 2);
            Console.WriteLine("Programozta: Táborosi Ákos");
            Console.SetCursorPosition(30, 3);
            Console.WriteLine("A történet valós események alapján készült....");
            Thread.Sleep(1000);
            Console.Clear();
            Console.WriteLine("Vége....");
            Console.ForegroundColor = ConsoleColor.Black;
            System.Environment.Exit(0);

        }

    }

}
