using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aisde2
{
    struct node
    {
       public node(double dist, int i)
       {
           distance = dist;
            id = i;
        }
        public double distance;
        public int id;
    }

    class graph
    {
        public graph(uint numberNodes)
        {
            numberNodes_ = numberNodes;
            list = new List<node>[numberNodes];
            for (int i = 0; i < list.Length; ++i)
            {
                list[i] = new List<node>();
            }
        }
        public void insert(int u, int v, double distance)
        {
            node tmp = new node(distance, v);
            list[u - 1].Add(tmp);
            tmp = new node(distance, u);
            list[v - 1].Add(tmp);
        }
        public void delete(int u, int v)
        {
            int i = search(u, v);
            if( i != -1)
            list[u - 1].RemoveAt(i);
             i = search(v, u);
            if( i != -1)
            list[v - 1].RemoveAt(i);
        }
        public int search(int u, int v)
        {
            for (int i = 0; i < list[u - 1].Count; ++i )
            {
                if (list[u - 1][i].id == v)
                {
                    return i;
                }
            }
            return -1;
        }

        private uint numberNodes_;
        public List<node>[] list; 
    }
}
