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
        public Floyd(graph graph)
        {
            numberNodes_ = graph.numberNodes();
            graph_ = new double[numberNodes_, numberNodes_];
            preNodes_ = new int[numberNodes_, numberNodes_];
      
            for (int i = 0; i < numberNodes_; ++i)
            {
                for (int j = 0; j < numberNodes_; ++j)
                {
                    graph_[i, j] = INF;
                }
                for (int j = 0; j < graph.list[i].Count; ++j)
                {
                    graph_[i, graph.list[i][j].id - 1] = graph.list[i][j].distance;
                    graph_[graph.list[i][j].id - 1, i] = graph.list[i][j].distance;
                }
                graph_[i, i] = 0;
                for (int n = 0; n < numberNodes_; ++n)
                {
                    preNodes_[i,n] = i;
                }
            }
        }
        public double[,] getDistance(int u){
            double newDist;
            int i;
            if (u != 0)
            {
                i = u - 1;
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
                           preNodes_[i,j] = k;
                           preNodes_[i,k] = i;
                           graph_[i, j] = newDist;
                           graph_[j, i] = newDist;
                       }
                   }
                }
                if (u != 0)
                    break;
            }
            return graph_;
        }
        public List<int> getPath(int u,int v)
        {
            List<int> path = new List<int>();
            path.Add(v);
            int i = v - 1; 
            do
            {
                path.Add(preNodes_[u-1,i]+1);
                i = preNodes_[u-1,i];
            } while (i != u -1);
            return path;
        }
        int[,] preNodes_;
        double[,] graph_;
        uint numberNodes_;
    }
}
