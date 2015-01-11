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
        public Dijkstra(graph graph)
        {
            graph_ = graph;
            numberNodes_ = graph.numberNodes();
            nodes_ = new double[numberNodes_];
            preNodes_ = new int[numberNodes_];
            queue_ = new List<node>();
        }
       public double[] getDistance(int u, int v)
        {
            u_ = u;
      
            for (int i = 0; i < nodes_.Length; ++i)
            {
                if (i != u - 1)
                {
                    nodes_[i] = INF;
                }
                else
                    nodes_[i] = 0;
            }
            queue_.Add(new node(0,u));
            var sortVer = from element in queue_
                          orderby element.distance
                          select element.id;
            do
            {
                int ind = sortVer.ElementAt(0);
                if (ind == v - 1)
                    break;
                queue_.RemoveAt(0);
                for (int i = 0; i < graph_.list[ind - 1].Count; ++i)
                {

                    if (nodes_[ind - 1] + graph_.list[ind - 1][i].distance < nodes_[graph_.list[ind - 1][i].id - 1])
                    {
                        nodes_[graph_.list[ind - 1][i].id - 1] = nodes_[ind - 1] + graph_.list[ind - 1][i].distance;
                        preNodes_[graph_.list[ind - 1][i].id - 1] = ind -1;
                        queue_.Add(new node(nodes_[graph_.list[ind - 1][i].id - 1], graph_.list[ind - 1][i].id));
                        sortVer = from element in queue_
                                      orderby element.distance
                                      select element.id;
                    }
                }
            } while (queue_.Count != 0);
           return nodes_;

        }
       public List<int> getPath(int v)
       {
           List<int> path = new List<int>();
           path.Add(v);
           int i = v - 1;
           do
           {
               path.Add(preNodes_[i] + 1);
               i = preNodes_[i];
           } while (i != u_ - 1);
           return path;
       }
       int[] preNodes_;
         int u_;
        double[] nodes_;
        graph graph_;
        uint numberNodes_;
        List<node> queue_;
    }
}

