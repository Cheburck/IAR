using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Курсовая
{
    static public class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        static public int vert;
        static public string name;
        static public int size;
        static double InfSum(double a, double b)
        {
            var infinity = double.MaxValue;
            if (a == infinity || b == infinity)
                return infinity;

            return a + b;
        }

        static double[,] WeightGraph(int n)
        {
            var graphMatrix = new double[n, n];
            var infinity = double.MaxValue;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    graphMatrix[i, j] = infinity;
                }
            }

            return graphMatrix;
        }

        static double[,] AlgFY(double[,] graphMatrix)
        {
            var length = graphMatrix.GetLength(0);
            var d = new double[length, length];
            Array.Copy(graphMatrix, d, graphMatrix.Length);

            for (int k = 0; k < length; k++)
            {
                for (int j = 0; j < length; j++)
                {
                    for (int i = 0; i < length; i++)
                    {
                        d[i, j] = Math.Min(d[i, j], InfSum(d[i, k], d[k, j]));
                    }
                }
            }

            return d;
        }

        static double[] GetEccentricity(double[,] d)
        {
            var length = d.GetLength(0);
            var e = new double[length];

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    e[i] = Math.Max(e[i], d[i, j]);
                }
            }
            return e;
        }

        static double GetRadius(double[] e)
        {
            var length = e.Length;
            var radius = double.MaxValue;

            for (int i = 0; i < length; i++)
                radius = Math.Min(radius, e[i]);

            return radius;
        }

        static List<int> GetCenter(double[,] graphMatrix, double min = 0)
        {
            var length = graphMatrix.GetLength(0);
            var d = AlgFY(graphMatrix);
            var e = GetEccentricity(d);
            var radius = GetRadius(e);
            var center = new List<int>();

            for (int i = 0; i < length; i++)
            {
                if (e[i] == radius)
                    center.Add(i);
            }

            return center;
        }

        static bool[,] IsEdge(double[,] graphMatrix)
        {
            var length = graphMatrix.GetLength(0);
            var isEdge = new bool[length, length];
            var infinity = double.MaxValue;

            for(int i = 0; i < length; i++)
            {
                for(int j = 0; j < length; j++)
                {
                    if (graphMatrix[i,j] != infinity)
                        isEdge[i,j] = true;
                }
            }

            return isEdge;
        }
        //[STAThread]
        static public void Main()
        {
            string[] args = File.ReadAllLines("dpp.txt");
            var length = args.Length;
            var n = Convert.ToInt32(args[0]);
            var type = args[1];
            size = n;
            var dict = new Dictionary<int, string>();
            var graphMatrix = WeightGraph(n);
            for (int i = 0; i<n; i++)
            {
                dict[i] = args[i + 2];
            }
            for(int k = n + 2; k < length; k++)
            {
                var ijW = args[k].Split(' ').Select(arg => Convert.ToInt32(arg)).ToArray();
                int i = ijW[0],
                    j = ijW[1],
                    w = ijW[2];
                graphMatrix[i,j] = w;
                graphMatrix[j,i] = w;
            }
            
            var isEdge = IsEdge(graphMatrix);
            var list = GetCenter(graphMatrix);
            vert = list[0];
            name = dict[vert];
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(true);
            Application.Run(new Form1());
        }
    }
}
