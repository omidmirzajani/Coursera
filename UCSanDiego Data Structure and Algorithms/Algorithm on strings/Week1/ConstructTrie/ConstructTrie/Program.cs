using System;
using System.Collections.Generic;
using System.Linq;

namespace ConstructTrie
{
    public class Graph
    {
        public long V;
        public List<Tuple<long, char>>[] adj;
        public List<string> Q1ConstructTrie;
        public Graph(long v)
        {
            V = v;
            adj = new List<Tuple<long, char>>[v];
            Q1ConstructTrie = new List<string>();
            for (int i = 0; i < v; ++i)
                adj[i] = new List<Tuple<long, char>>();
        }
        public void addEdge(long start, long end, char weight)
        {
            adj[start].Add(new Tuple<long, char>(end, weight));
            Q1ConstructTrie.Add($"{start}->{end}:{weight}");
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            long n = Convert.ToInt64(Console.ReadLine());
            string[] pattern = new string[n];
            for(int i = 0; i < n; i++)
            {
                pattern[i] = Console.ReadLine();
            }
            foreach(var t in Solve(n, pattern))
            {
                Console.WriteLine(t);
            }
        }
        public static string[] Solve(long n, string[] patterns)
        {
            long tt = patterns.Select(d => d.Length).Max();
            Graph g = new Graph(n * tt);
            long last = 1;
            for (int i = 0; i < patterns[0].Length; i++)
            {
                g.addEdge(i, i + 1, patterns[0][i]);
                last++;
            }
            for (int i = 1; i < n; i++)
            {
                long check = 0;
                int j = 0;
                long t = myfunc(g.adj[check], patterns[i][j]);
                while (t != -1)
                {
                    check = t;
                    j++;
                    t = myfunc(g.adj[check], patterns[i][j]);
                }
                for (long k = j; k < patterns[i].Length; k++)
                {
                    g.addEdge(check, last++, patterns[i][(int)k]);
                    check = last - 1;
                }
            }
            return g.Q1ConstructTrie.ToArray();
        }
        private static long myfunc(List<Tuple<long, char>> list, char v)
        {
            foreach (var vv in list)
                if (vv.Item2 == v)
                    return vv.Item1;
            return -1;
        }
    }
}
