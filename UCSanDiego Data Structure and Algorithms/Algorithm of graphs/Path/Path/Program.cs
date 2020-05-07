using System;
using System.Collections.Generic;
using System.Linq;

namespace Path
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
        public string[] Path(long startNode)
        {
            string[] result = new string[V];
            long[] weight = new long[V];
            for (int i = 0; i < V; i++)
                weight[i] = long.MaxValue;
            weight[startNode] = 0;
            for (int i = 0; i <= V * 2; i++)
                foreach (long[] edge in edges)
                {
                    long start = edge[0] - 1;
                    long end = edge[1] - 1;
                    long w = edge[2];

                    if (weight[start] != long.MaxValue && weight[start] + w < weight[end])
                    {
                        weight[end] = weight[start] + w;
                        if (i >= V - 1)
                            result[end] = "-";
                    }
                }
            for (int i = 0; i < V; i++)
            {
                if (result[i] != "-")
                {
                    if (weight[i] == long.MaxValue)
                        result[i] = "*";
                    else
                        result[i] = weight[i].ToString();
                }

            }
            return result;
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
            t = Console.ReadLine().Split().Select(d => Convert.ToInt64(d)).ToArray();
            string[] res= (Solve(v, edges, t[0]));
            foreach (var omid in res)
                Console.WriteLine(omid) ;
        }
        public static string[] Solve(long nodeCount, long[][] edges, long startNode)
        {
            Graph g = new Graph(nodeCount, edges);
            for (long i = 0; i < edges.Length; i++)
            {
                g.addEdge(edges[i][0] - 1, edges[i][1] - 1, edges[i][2]);
            }
            return g.Path(startNode - 1);
        }
    }
}
