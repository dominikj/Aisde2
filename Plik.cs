using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.ComponentModel;
using System.Configuration.Install;
using System.IO;

namespace Aisde
{
    enum flaga { bufor, rozklady, strumienie };
 class wyjatek : Exception
 {
    public wyjatek(string wyj)
     {
         komunikat = wyj;
     }
      public string komunikat;
 }

    class Plik
    {
        public bufor [] bufory; //tablica buforów
        public strumien [] strumienie; //tablica strumieni
        rozklad [] rozklady; //tablica rozkładów
        public double szybkoscwyj = 1; //szybkość wyjścia
        public bool flagaEr; //zmienna pomocnicza
        public Plik()
        {
            Console.WriteLine("Podaj ścieżkę do pliku z ustawieniami.");
            flaga flag = flaga.bufor;
            string nazwaRozk = " ";
            int nr = 0;
            string selectedPath = " ";
            string[] linie;

            while(true)
            {
            var t = new Thread((ThreadStart)(() =>
            {
                OpenFileDialog opPlik = new OpenFileDialog();
                    if (opPlik.ShowDialog() == DialogResult.Cancel)
                        return;
                    selectedPath = opPlik.FileName;
            }));

            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();
            try{
                linie = System.IO.File.ReadAllLines(selectedPath);
            }
             catch (Exception ex)
            {
              MessageBox.Show("Nieprawidłowa ścieżka do pliku.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
              continue;
                }
                break;
            
        }
            try
            {
                for (int i = 0; i < linie.Length; ++i)
                {
                    linie[i].Trim();
                    string[] slowa = linie[i].Split(' ');
                    for (int j = 0; j < slowa.Length; ++j)
                        slowa[j].Trim();

                    if (slowa[0] == "#")
                        continue;

                    switch (slowa[0])
                    {
                        case "SYSTEM":
                            {
                                if (slowa[2] != "ROUTER")
                                    MessageBox.Show("Nieprawidłowy plik wejściowy. System inny niż ROUTER", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                            }
                        case "PRZEPLYWNOSC":
                            {
                                int tmp = Convert.ToInt32(slowa[2]);
                                szybkoscwyj = tmp / 8000;
                                if (szybkoscwyj == 0) throw new wyjatek("Prędkość łącza jest mniejsza niż 1B/ms");

                                Console.WriteLine("Przeplywnosc:" + szybkoscwyj + " [B/ms]");
                                break;
                            }
                        case "KOLEJKI":
                            {
                                nr = 0;
                                int lkolejek = Convert.ToInt32(slowa[2]);
                                bufory = new bufor[lkolejek];
                                flag = flaga.bufor;
                                Console.WriteLine("Liczba kolejek:" + lkolejek);
                                break;
                            }
                        case "NAZWA":
                            {
                                if (flag == flaga.bufor)
                                {
                                    if (Convert.ToInt32(slowa[5]) == 0) throw new wyjatek("Rozmiar bufora ma zerową wartość");
                                    bufory[nr] = new bufor(slowa[2], Convert.ToInt32(slowa[5]));
                                    Console.WriteLine("Nazwa bufora:" + slowa[2] + " pojemnosc: " + Convert.ToInt32(slowa[5]));
                                    ++nr;
                                }
                                if (flag == flaga.rozklady)
                                {
                                    nazwaRozk = slowa[2];
                                }
                                if (flag == flaga.strumienie)
                                {
                                    strumienie[ZnajdzBufor(slowa[5])] = new strumien(slowa[2], ZnajdzBufor(slowa[5]), ZnajdzRozklad(slowa[8]), ZnajdzRozklad(slowa[11]));
                                    Console.WriteLine("Nazwa strumienia:" + slowa[2] + " nazwa bufora: " + slowa[5] + " Rozklady:" + ZnajdzRozklad(slowa[8]) + " " + ZnajdzRozklad(slowa[11]));
                                    ++nr;
                                }
                                break;
                            }
                        case "ROZKLADY":
                            {
                                rozklady = new rozklad[Convert.ToInt32(slowa[2])];
                                nr = 0;
                                flag = flaga.rozklady;
                                Console.WriteLine("Liczba rozkladow:" + Convert.ToInt32(slowa[2]));
                                break;
                            }
                        case "STRUMIENIE":
                            {
                                strumienie = new strumien[Convert.ToInt32(slowa[2])];
                                nr = 0;
                                Console.WriteLine("Liczba strumieni:" + Convert.ToInt32(slowa[2]));
                                flag = flaga.strumienie;
                                break;
                            }
                        case "LAMBDA":
                            {
                                rozklady[nr] = new rozklad();
                                string tmp = slowa[2].Replace(".", ",");
                                rozklady[nr].lambda = Convert.ToDouble(tmp);
                                rozklady[nr].nazwa = nazwaRozk;
                                Console.WriteLine("Nazwa rozkladu:" + rozklady[nr].nazwa + " Rozklad:" + rozklady[nr].lambda);
                                ++nr;
                                break;
                            }
                    }

                }
            }
            catch (wyjatek ex)
            {
                MessageBox.Show(ex.komunikat, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                flagaEr = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sprawdź, czy podane przez Ciebie liczby strumieni, buforów i rozkładów zgadzają się z liczbą ich definicji", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                flagaEr = true;
            }
        }
        double ZnajdzRozklad(string nazwa) //odnajduje właściwy rozkład w tab. rozkładów według jego nazwy
    {
       for(int i = 0; i < rozklady.Length; ++i)
       {
           if(rozklady[i].nazwa == nazwa)
               return rozklady[i].lambda;
       }
        throw new wyjatek("Nie istnieje rozkład o podanej nazwie");
    }
        int ZnajdzBufor(string nazwa)  //odnajduje właściwy bufor w tab. buforów według jego nazwy
    {
       for(int i = 0; i < bufory.Length; ++i)
       {
           if(bufory[i].nazwa == nazwa)
               return i;
       }
       throw new wyjatek("Nie istnieje bufor o podanej nazwie");
    }
    
}
}
