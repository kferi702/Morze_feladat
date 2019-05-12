using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morze
{
    class Program
    {
        public static List<ABC> abc = new List<ABC>();
        public static char c;
        public static List<MorzeKod> morzeKod = new List<MorzeKod>();
        public static Dictionary<string, string> idezetek = new Dictionary<string, string>();
        public static List<Idezet> idezet = new List<Idezet>();

        public static void beolvAbc(string f_neve)
        {
            FileStream f = new FileStream(@f_neve, FileMode.Open);
            StreamReader r = new StreamReader(f, Encoding.UTF8);
            r.ReadLine();
            while (!r.EndOfStream)
                abc.Add(new ABC(r.ReadLine()));
            r.Close();
            f.Close();
        }
        public static void f3()
        {
            Console.WriteLine("3. feladat: A morze abc " + abc.Count + " db karakter kódját tartalmazza.");
        }
        public static void f4()
        {
            Console.Write("4. feladat: Kérek egy karaktert: ");
            c =  Char.ToUpper(char.Parse(Console.ReadLine()));
            foreach (ABC x in abc)
                if (x.betu == c)
                {
                    Console.WriteLine("\t\tA "+c+" karakter morse kódja: "+x.mKod);
                    return;
                }
            Console.WriteLine("\t\tNem található a kódtárban ilyen karakter!");
        }
        public static  void beolvMorze(string f_neve)
        {
            FileStream f = new FileStream(@f_neve, FileMode.Open);
            StreamReader r = new StreamReader(f, Encoding.UTF8);
            while (!r.EndOfStream)
                morzeKod.Add(new MorzeKod(r.ReadLine()));
            r.Close();
            f.Close();
        }
        public static string Morze2Szoveg(string be)
        {
            string ki = "";
            string[] szo = be.Split(' ');
            int szunet = 0;
            foreach(string s in szo)
            {
                if (s != "." || s != "–")
                    szunet++;
                if (szunet == 6)
                    ki += " ";
                foreach (ABC x in abc)
                {
                    if (x.mKod == s)
                    {
                        ki += x.betu;
                        szunet=0;
                    }
                }
            }
            return ki;
        }
        public static void f7()
        {
            Console.Write("7. feladat: Az első idézet szerzője: ");
            Console.WriteLine(Morze2Szoveg(morzeKod.ElementAt(0).szerzo));
        }
        public static void Forditasok()
        {
            foreach (MorzeKod m in morzeKod)
            {
                idezetek.Add(Morze2Szoveg(m.idezet), Morze2Szoveg(m.szerzo));
            }
        }
        public static void f8()
        {
            int max = int.MinValue;
            string maxIdezet = "";
            Forditasok();
            foreach (var v in idezetek)
                if (max < v.Key.Length)
                {
                    max = v.Key.Length;
                    maxIdezet = v.Value + ": " + v.Key;
                }
            Console.WriteLine("8. feladat: A leghosszabb idézet szerzője és az idézet: "+maxIdezet);
        }
        public static void f9()
        {
            Console.WriteLine("9. feladat: Arisztotelész idézetei: ");
            foreach (var v in idezetek)
            {
                if (v.Value == "ARISZTOTELÉSZ")
                    Console.WriteLine("\t\t- "+v.Key);
            }
        }
        public static void f10()
        {
            FileStream f = new FileStream("forditas.txt", FileMode.OpenOrCreate);
            StreamWriter w = new StreamWriter(f, Encoding.Default);
            foreach (var v in idezetek)
                w.WriteLine(v.Value+": "+v.Key);
            w.Close();
            f.Close();
        }
        //plusz feladatok
        public static void beolvIdezetek()
        {
            FileStream f = new FileStream(@"idezetek.txt",FileMode.Open);
            StreamReader r = new StreamReader(f, Encoding.UTF8);
            while (!r.EndOfStream)
                idezet.Add(new Idezet(r.ReadLine()));
            r.Close();
            f.Close();
        }
        public static void b2()
        {
            int max = int.MinValue;
            string maxI = "";
            foreach(Idezet i in idezet)
                if (i.idezet.Length > max)
                {
                    max = i.idezet.Length;
                    maxI = i.szerzo;
                }
            Console.WriteLine("BONUSZ 2. feladat: A leghosszabb idáézet szerzője: "+maxI);
        }
        public static void b3()
        {
            Dictionary<string, int> legtobb = new Dictionary<string, int>();
            foreach(Idezet i in idezet)
            {
                if (legtobb.Keys.Contains(i.szerzo))
                    legtobb[i.szerzo]++;
                else
                    legtobb.Add(i.szerzo, 1);
            }
            Console.WriteLine("BONUSZ 3. feladat: A legtöbbet idézet szerző :"+legtobb.OrderByDescending(x=>x.Value).First().Key);
        }
        public static string Szoveg2Morze(string be)
        {
            string ki="";
            foreach(char c in be)
            {
                if (c == ' ')
                    ki += "    ";
                if (c == ':')
                    ki += ":  ";
                if (c!= ' ' || c!=':')
                    foreach (ABC x in abc)
                        if (x.betu == char.ToUpper(c))
                            ki +=x.mKod+ "   ";
            }
            return ki;
        }
        public static void b5()
        {
            Console.WriteLine("BONUSZ 4. feladat: A harmadik szerző morze kóddal: "+ Szoveg2Morze(idezet.ElementAt(2).szerzo));
            Console.WriteLine("Visszakódolva: " +Morze2Szoveg(Szoveg2Morze(idezet.ElementAt(2).szerzo)));
        }
        public static void b6()
        {
            FileStream f = new FileStream("morzeidezet.txt", FileMode.Create);
            StreamWriter w = new StreamWriter(f,Encoding.UTF8);
            foreach (Idezet i in idezet)
                w.WriteLine(Szoveg2Morze(i.szerzo)+";"+Szoveg2Morze(i.idezet));
            w.Close();
            f.Close();
        }

        static void Main(string[] args)
        {
            beolvAbc("morzeabc.txt");
            f3();
            f4();
            beolvMorze("morze.txt");
            f7();
            f8();
            f9();
            f10();
            //plusz feladatok
            beolvIdezetek();
            b2();
            b3();
            b5();
            b6();
            beolvMorze("morzeidezet.txt");
            Console.WriteLine("Az összes idézet kiírása: ");
            morzeKod.ForEach(i=>Console.WriteLine("\t\t"+Morze2Szoveg(i.ToString())));
            

            Console.ReadKey();
        }
    }
}
