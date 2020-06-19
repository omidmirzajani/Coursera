using System;
using System.Collections.Generic;
using System.Linq;

namespace Cycle
{
    public class Graph
    {
        public long V;
        public bool[] visited;
        public List<Tuple<long, long>>[] adj;
        public List<long>[] adjWithoutWeights;
        public List<Tuple<long, long>>[] adjReversed;
        public long[][] edges;
        long[,] weights;
        public Graph(long v, long[][] ed)
        {

            V = v;
            edges = ed;
            adj = new List<Tuple<long, long>>[v];
            adjReversed = new List<Tuple<long, long>>[v];
            adjWithoutWeights = new List<long>[v];
            for (int i = 0; i < v; ++i)
            {
                adj[i] = new List<Tuple<long, long>>();
                adjReversed[i] = new List<Tuple<long, long>>();
                adjWithoutWeights[i] = new List<long>();
            }
            visited = new bool[v];

            weights = new long[V, V];
            for (int i = 0; i < V; i++)
                for (int j = 0; j < V; j++)
                    weights[i, j] = long.MaxValue;
        }
        public void addEdge(long start, long end, long weight)
        {
            adj[start].Add(new Tuple<long, long>(end, weight));
            adjReversed[end].Add(new Tuple<long, long>(start, weight));
            weights[start, end] = weight;
            adjWithoutWeights[start].Add(end);
        }

        public long Cycle()
        {
            long[] weight = new long[V];
            for (int i = 0; i < V; i++)
                weight[i] = long.MaxValue;
            for (int k = 0; k < V; k++)
            {
                if (weight[k] == long.MaxValue)
                {
                    weight[k] = 0;
                    for (int i = 0; i < V; i++)
                        foreach (long[] edge in edges)
                        {
                            long start = edge[0] - 1;
                            long end = edge[1] - 1;
                            long w = edge[2];

                            if (weight[start] != long.MaxValue && weight[start] + w < weight[end])
                            {
                                weight[end] = weight[start] + w;
                                if (i == V - 1)
                                    return 1;
                            }
                        }
                }
            }
            return 0;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            long[] t = Console.ReadLine().Split().Select(d => Convert.ToInt64(d)).ToArray();
            long v = t[0];
            long e = t[1];
            long[][] edges = new long[e][];
            for (int i = 0; i < e; i++)
            {
                edges[i] = Console.ReadLine().Split().Select(d => Convert.ToInt64(d)).ToArray();
            }
            Console.WriteLine(Solve(v,edges));
        }
        public static long Solve(long nodeCount, long[][] edges)
        {
            Graph g = new Graph(nodeCount, edges);
            for (long i = 0; i < edges.Length; i++)
                g.addEdge(edges[i][0] - 1, edges[i][1] - 1, edges[i][2]);

            return g.Cycle();
        }
    }
}
