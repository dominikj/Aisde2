using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aisde2
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();
           for (int aa = 1000; aa <= 1000; aa += 50)
            {
                int VERTEX = aa;
                //  Plik p = new Plik();
                graph g = new graph((uint)VERTEX + 1);

                for (int i = 0; i < 1000000; ++i)
                {
                    g.insert(rnd.Next(VERTEX) + 1, rnd.Next(VERTEX) + 1, rnd.NextDouble());
                }
                Dijkstra d = new Dijkstra(g);
              //  Floyd f = new Floyd(g);
                Double Dcz;
                System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
                watch.Start();
                 //for (int i = 1; i <= VERTEX+1; ++i)
                d.getDistance(1, 0);
              //  f.getDistance(0);
                watch.Stop();
                Dcz = watch.ElapsedMilliseconds;
                Console.Write(" " + Dcz);
            }
          /*  graph g = new graph(4);
            g.insert(1, 2, 2);
            g.insert(2, 3, 5);
            g.insert(3, 4, 2);
            g.insert(4, 1, 1);
            //g.insert(4, 2, 1);
            Floyd f = new Floyd(g);
            f.getDistance(0);

            List<int> path1 = f.getPath(1,4);
           for (int i = 0; i < path1.Count; ++i)
            Console.Write(" " + path1[i]);
            Console.WriteLine();
          Dijkstra d = new Dijkstra(g);
            d.getDistance(2,0);
            List<int> path2 = d.getPath(4);
            for (int i = 0; i < path2.Count; ++i)
                Console.Write(" " + path2[i]);*/
            Console.ReadKey();

        }
    }
}
