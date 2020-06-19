using System;
using System.Collections.Generic;
using System.Linq;

namespace IndependentSet
{
    public class FunParty
    {
        public static long INF = 2000000;
        public long V;
        public List<long>[] adj;
        public List<long>[] children;
        public long[] D;
        public long[] weight;
        public bool[] visited;
        public FunParty(long v)
        {
            V = v;
            adj = new List<long>[V];
            for (int i = 0; i < V; i++)
                adj[i] = new List<long>();
            children = new List<long>[V];
            for (int i = 0; i < V; i++)
                children[i] = new List<long>();
            D = new long[V];
            for (int i = 0; i < V; i++)
                D[i] = INF;
            weight = new long[V];
            visited = new bool[V];
        }
        public void AddEdge(long s, long e)
        {
            adj[s].Add(e);
            adj[e].Add(s);
        }
        public long Party(long v)
        {
            if (D[v] == INF)
            {
                if (children[v].Count == 0)
                {
                    D[v] = weight[v];
                }
                else
                {
                    long m1 = weight[v];
                    foreach (var u in children[v])
                    {
                        foreach (var w in children[u])
                        {
                            m1 += Party(w);
                        }
                    }
                    long m0 = 0;
                    foreach (var u in children[v])
                    {
                        m0 += Party(u);
                    }
                    D[v] = Math.Max(m0, m1);
                }
            }
            return D[v];
        }

        public void UpdateChildren(long v)
        {
            visited[v] = true;
            foreach (var item in adj[v])
            {
                if (!visited[item])
                {
                    children[v].Add(item);
                    UpdateChildren(item);
                }
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            long n = Convert.ToInt64(Console.ReadLine());
            long[] fun = Console.ReadLine().Split().Select(d => Convert.ToInt64(d)).ToArray();
            long[][] hier = new long[n - 1][];
            for(int i = 0; i < hier.Length; i++)
            {
                hier[i] = Console.ReadLine().Split().Select(d => Convert.ToInt64(d)).ToArray();
            }
            Console.WriteLine(Solve(n, fun, hier));
        }
        public static long Solve(long n, long[] funFactors, long[][] hierarchy)
        {
            FunParty fp = new FunParty(n);
            fp.weight = funFactors;
            foreach (var item in hierarchy)
                fp.AddEdge(item[0] - 1, item[1] - 1);
            fp.UpdateChildren(0);
            long m = fp.Party(0);
            return m;
        }
    }
}
