using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Курсовая
{
    public abstract class MakeProducts //Product
    {
        public abstract int D(int m); //создание характеристик для вывода
        public abstract bool canMakeEdge(int j, int k, List<Vertex> vertices, List<Edge> edges);
    }

    //Product A
    public class DagProduct : MakeProducts
    {
        public override int D(int m)
        {
            //так как у нас ориентированный граф, то и провести дуги можно ко всем предыдущим вершинам, соответственно:
            return m;
        }
        public override bool canMakeEdge(int j, int k, List<Vertex> vertices, List<Edge> edges)
        {
            int n = edges.Count;
            for (int i = 0; i < n; i++)
            {
                if (edges[i].v1.Num == j && edges[i].v2.Num == k) return false; //если существует дуга из j в k
            }
            return true;
        }
    }

    //Product B
    public class UnDagProduct : MakeProducts
    {
        public override int D(int m)
        {
            //так как у нас неориентированный граф, то с каждой новой вершины мо можем провести 1 ребро:
            return 1;
        }
        public override bool canMakeEdge(int j, int k, List<Vertex> vertices, List<Edge> edges)
        {
            int n = edges.Count;
            for (int i = 0; i < n; i++)
            {
                if ((edges[i].v1.Num == j && edges[i].v2.Num == k) || (edges[i].v1.Num == k && edges[i].v2.Num == j))
                    return false; //если существует ребро из j в k и наоборот
            }
            return true;
        }
    }
}
