using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aisde2
{

     class Dijkstra
    {
        int INF = 1000;
        public Dijkstra(graph graph, uint numberNodes)
        {
            graph_ = graph;
            numberNodes_ = numberNodes;
            nodes_ = new double[numberNodes_];
            preNodes_ = new int[numberNodes_];
            queue_ = new Listaup<int>(1000);
        }
       public void getPath(int u, int v)
        {
            for (int i = 0; i < nodes_.Length; ++i)
            {
                if (i != u - 1)
                {
                    nodes_[i] = INF;
                }
                else
                    nodes_[i] = 0;
            }
            queue_.dodaj(0,u);
            do
            {
                int ind = queue_.pierwszyElem();
                if (ind == v - 1)
                    break;
                queue_.usun();
                for (int i = 0; i < graph_.list[ind - 1].Count; ++i)
                {

                    if (nodes_[ind - 1] + graph_.list[ind - 1][i].distance < nodes_[graph_.list[ind - 1][i].id - 1])
                    {
                        nodes_[graph_.list[ind - 1][i].id - 1] = nodes_[ind - 1] + graph_.list[ind - 1][i].distance;
                        preNodes_[graph_.list[ind - 1][i].id - 1] = ind;
                        queue_.dodaj(nodes_[graph_.list[ind - 1][i].id - 1], graph_.list[ind - 1][i].id);
                    }
                }
            } while (queue_.rozmiar() != 0);


        }
       int[] preNodes_;
        double[] nodes_;
        graph graph_;
        uint numberNodes_;
        Listaup<int> queue_;
    }
}

