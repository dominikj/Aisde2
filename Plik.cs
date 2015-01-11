using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.ComponentModel;
using System.Configuration.Install;
using System.IO;

namespace Aisde2
{
    class Plik
    {
        public int liczbaWezlow;
        public int liczbaLacz;
        public graph graf;
        public Plik()
        {
            Console.WriteLine("Podaj ścieżkę do pliku z ustawieniami.");


            bool czyLacza = false;
            string selectedPath = " ";
            string[] linie;
            Random rnd = new Random();
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
             catch (Exception)
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

                        case "WEZLY":
                            {
                                int tmp = Convert.ToInt32(slowa[2]);
                                liczbaWezlow = tmp;
                                Console.WriteLine("Liczba wezłów:" + liczbaWezlow);
                                graf = new graph((uint)liczbaWezlow);
                                break;
                            }
                        case "LACZA":
                            {
                                int tmp = Convert.ToInt32(slowa[2]);
                                liczbaLacz = tmp;
                                czyLacza = true;
                                Console.WriteLine("Liczba lacz:" + liczbaLacz);
                                break;
                            }
                        default:
                            {
                                if (czyLacza == true && liczbaLacz != 0)
                                {
                                    int u = Convert.ToInt32(slowa[1]);
                                    int v = Convert.ToInt32(slowa[2]);
                                    
                                    double dis = rnd.NextDouble();
                                    graf.insert(u, v, dis);
                                    --liczbaLacz;
                                }
                                break;
                            }
                    }

                }
           }
            catch (Exception)
            {
                MessageBox.Show("Wystąpił błąd. Sprawdź liczbę wierzchołków i łącz oraz poprawność podanych połączeń", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    
}
}
