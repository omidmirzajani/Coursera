using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectingPoints
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
        public double Solve(long pointCount, long[][] points)
        {
            Disjointset s = new Disjointset(pointCount);
            for (int o = 0; o < pointCount; o++)
                s.MakeSet(o);
            long len = pointCount * (pointCount - 1);
            len = Convert.ToInt64(len / 2);
            List<double>[] edges = new List<double>[len];
            int t = 0;
            for (int j = 0; j < pointCount; j++)
            {
                for (int k = j + 1; k < pointCount; k++)
                {
                    edges[t] = new List<double>() { j, k, Dist(points, j, k) };
                    t++;
                }
            }
            edges = edges.OrderBy(d => d[2]).ToArray();
            double weight = 0;
            int i = 0;
            var p = (1, 2);
            p.Item1 = 3;
            while (i != len)
            {
                var min = edges[i];
                if (s.FindSet(Convert.ToInt32(min[0])) != s.FindSet(Convert.ToInt32(min[1])))
                {
                    weight += min[2];
                    s.Union(Convert.ToInt32(min[0]), Convert.ToInt32(min[1]));
                }
                i++;
            }
            return (double)Math.Round(weight * 1000000d) / 1000000d;
        }

        private double Dist(long[][] points, int j, int k)
        {
            long d1 = points[j][0] - points[k][0];
            long d2 = points[j][1] - points[k][1];
            return Math.Pow((d1 * d1 + d2 * d2), 0.5);
        }
    }
}
