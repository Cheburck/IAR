using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Курсовая
{
    public class Vertex //вершина
    {
        private float x, y;
        private int number;
        public int Num { get { return number; } }
        public float X { get { return x; } }
        public float Y { get { return y; } }
        public Brush color;

        public Vertex(float x, float y, int number)
        {
            this.x = x;
            this.y = y;
            color = Brushes.White;
            this.number = number;
        }

        public Vertex(float x, float y, Brush color, int number)
        {
            this.x = x;
            this.y = y;
            this.color = color;
            this.number = number;
        }

    }

    public class Edge //ребро
    {
        public Vertex v1, v2;
        public int number;
        public bool isDirect;

        public Edge(Vertex v1, Vertex v2, bool isDirect)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.isDirect = isDirect;
        }
    }

    public class Graph  //является совокупность вышеописанных классов, являясь графом
    {
        public List<Vertex> vertices;
        public List<Edge> edges;

        public Graph(List<Vertex> vertices, List<Edge> edges)
        {
            this.vertices = vertices;
            this.edges = edges;
        }
    }

    public class DrawGraph          //используется для отрисовки графа: точек, рёбер, дуг и полного графа
    {                               
        Bitmap bitmap;         
        Graphics graphics;
        Pen blackPen;
        Pen darkSlateBluePen;
        PointF point;
        Font font;
        Brush brushForText;
        private int R = 10;
        public int r { get { return R; } }

        public DrawGraph(int width, int height)
        {
            bitmap = new Bitmap(width, height);
            graphics = Graphics.FromImage(bitmap);
            clearSheet();
            blackPen = new Pen(Color.Black); blackPen.Width = 2;
            darkSlateBluePen = new Pen(Color.DarkSlateBlue); darkSlateBluePen.Width = 2;
            font = new Font("Arial", R);
            brushForText = Brushes.Black;
        }
        public void clearSheet()
        {
            graphics.Clear(Color.White);
        }

        public Bitmap GetBitmap()
        {
            return bitmap;
        }
        public void drawVertex(Vertex vertex)
        {
            graphics.FillEllipse(vertex.color, vertex.X - R, vertex.Y - R, 2 * R, 2 * R);
            graphics.DrawEllipse(blackPen, vertex.X - R, vertex.Y - R, 2 * R, 2 * R);
            point = new PointF(vertex.X - R / 2 - 2, vertex.Y - R / 2 - 2);
            graphics.DrawString(((vertex.Num)).ToString(), font, brushForText, point);
        }

        public void drawVertex(Vertex vertex, Brush brush)
        {
            graphics.FillEllipse(brush, vertex.X - R, vertex.Y - R, 2 * R, 2 * R);
            graphics.DrawEllipse(blackPen, vertex.X - R, vertex.Y - R, 2 * R, 2 * R);
            point = new PointF(vertex.X - R / 2 - 2, vertex.Y - R / 2 - 2);
            graphics.DrawString((vertex.Num).ToString(), font, brushForText, point);
        }

        public void drawEdge(Edge E)
        {
            if (E.isDirect == true)
            {
                darkSlateBluePen.CustomEndCap = new AdjustableArrowCap(5, 10);
                graphics.DrawLine(darkSlateBluePen, E.v1.X, E.v1.Y, E.v2.X, E.v2.Y);
                drawVertex(E.v1); drawVertex(E.v2);
            }
            else
            {
                darkSlateBluePen.EndCap = LineCap.Flat;
                graphics.DrawLine(darkSlateBluePen, E.v1.X, E.v1.Y, E.v2.X, E.v2.Y);
                drawVertex(E.v1); drawVertex(E.v2);
            }
        }

        public void drawGraph(List<Vertex> vertices, List<Edge> edges)
        {
            //int n = vertices.Count;
            int n = 22;
            int m = edges.Count;
            clearSheet();
            for (int i = 0; i < n; i++)
                drawVertex(vertices[i]);
            for (int j = 0; j < m; j++)
                drawEdge(edges[j]);
        }
    }


}
