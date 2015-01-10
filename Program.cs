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
            graph g = new graph(6);
            g.insert(1, 2, 2);
            g.insert(1, 6, 1);
            g.insert(1, 5, 7);
            g.insert(1, 4, 3);
            g.insert(2,6, 4);
            g.insert(2, 3, 2);
            g.insert(3, 4, 8);
            g.insert(4, 5, 6);
            g.insert(5, 6, 2);
   
            Floyd f = new Floyd(g, 6);
            f.getDistance(2);
          //  Dijkstra d = new Dijkstra(g, 6);
           // d.getPath(2,0);
            g.delete(2, 3);
            Console.ReadKey();

        }
    }
}
