using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aisde2
{
    class Floyd
    {
        int INF = 1000;
        public Floyd(graph graph, uint NumberNodes)
        {
            numberNodes_ = NumberNodes;
            graph_ = new double[NumberNodes, NumberNodes];
            preNodes_ = new int[NumberNodes];
      
            for (int i = 0; i < NumberNodes; ++i)
            {
                for (int j = 0; j < NumberNodes; ++j)
                {
                    graph_[i, j] = INF;
                }
                for (int j = 0; j < graph.list[i].Count; ++j)
                {
                    graph_[i, graph.list[i][j].id - 1] = graph.list[i][j].distance;
                    graph_[graph.list[i][j].id - 1, i] = graph.list[i][j].distance;
                }
                graph_[i, i] = 0;
            }
        }
        public void getDistance(int u){
            double newDist;
            int i;
            if (u != 0)
            {
                i = u - 1;
                for (int n = 0; n < preNodes_.Length; ++n)
                {
                    preNodes_[n] = u -1;
                }
            }
            else
                i = 0;
            for (; i < numberNodes_ - 1; ++i)
            {
                int j;
                if (u != 0)
                    j = 0;
                else
                    j = i + 1;
                for (; j < numberNodes_; ++j)
                {
                   for (int k = 0; k < numberNodes_; ++k)
                   {
                       if (k == i || k == j)
                           continue;
                       newDist = graph_[i, k] + graph_[k, j];
                       if (newDist < graph_[i,j])
                       {
                           preNodes_[j] = k;
                           preNodes_[k] = i;
                           graph_[i, j] = newDist;
                           graph_[j, i] = newDist;
                       }
                   }
                }
                if (u != 0)
                    break;
            }
        }
        int[] preNodes_;
        double[,] graph_;
        uint numberNodes_;
    }
}
