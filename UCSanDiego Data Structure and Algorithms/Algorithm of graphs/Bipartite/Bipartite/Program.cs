using System;
using System.Collections.Generic;
using System.Linq;

namespace Bipartite
{
    public class Graph
    {
        public long V;
        public bool[] visited;
        public List<long>[] adj;
        public List<long>[] adjReversed;
        public long[][] edges;
        public Graph(long v, long[][] ed)
        {
            V = v;
            edges = ed;
            adj = new List<long>[v];
            adjReversed = new List<long>[v];

            for (int i = 0; i < v; ++i)
            {
                adj[i] = new List<long>();
                adjReversed[i] = new List<long>();
            }

            visited = new bool[v];
        }
        public void addEdge(long start, long end)
        {
            adj[start].Add(end);
        }

        public long TwoPart()
        {
            long[] group = new long[V];
            group[0] = 1;
            Queue<long> q = new Queue<long>();
            q.Enqueue(0);
            long last = 1;
            for (int i = 0; i < V; i++)
            {
                Queue<long> qPrime = new Queue<long>();
                while (q.Count != 0)
                {
                    long node = q.Dequeue();
                    foreach (long t in adj[node])
                    {
                        if (group[t] == 0)
                        {
                            qPrime.Enqueue(t);
                            group[t] = 3 - last;
                        }
                        else if (group[t] == last)
                            return 0;
                    }
                }
                last = 3 - last;
                q = qPrime;
            }
            return 1;
        }

        public long ShortestPath(long start, long end)
        {
            long[] vis = new long[V];
            vis[start] = 1;
            Queue<long> q = new Queue<long>();
            q.Enqueue(start);
            long last = 1;
            for (int i = 0; i < V; i++)
            {
                Queue<long> qPrime = new Queue<long>();
                while (q.Count != 0)
                {
                    long node = q.Dequeue();
                    foreach (long t in adj[node])
                    {
                        if (vis[t] == 0)
                        {
                            qPrime.Enqueue(t);
                            vis[t] = last + 1;
                        }
                    }
                }
                last++;
                q = qPrime;
            }
            return vis[end] - 1;
        }


        public long Dijkstra(long startNode, long endNode)
        {
            #region init
            long[] values = new long[V];
            for (int i = 0; i < V; i++)
                values[i] = long.MaxValue;
            values[startNode] = 0;
            List<long> left = new List<long>();
            List<long> right = new List<long>();
            left.Add(startNode);
            for (int i = 0; i < V; i++)
                if (i != startNode)
                    right.Add(i);
            #endregion

            Queue<long> q = new Queue<long>();
            q.Enqueue(startNode);
            long node;
            while (q.Count != 0)
            {
                node = q.Dequeue();
                long min = long.MaxValue;
                long minIndex = node;
                foreach (long n in adj[node])
                {
                    values[n] = Math.Min(values[n], values[node] + 1);
                    if (values[n] < min)
                    {
                        min = values[n];
                        minIndex = n;
                    }
                }
                foreach (long n in right)
                    if (values[n] == min)
                    {
                        right.Remove(n);
                        left.Add(n);
                    }

                node = minIndex;
            }
            if (values[endNode] == long.MaxValue)
                return -1;
            return values[endNode];
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
        public static long Solve(long NodeCount, long[][] edges)
        {
            Graph g = new Graph(NodeCount, edges);
            for (long i = 0; i < edges.Length; i++)
            {
                g.addEdge(edges[i][0] - 1, edges[i][1] - 1);
                g.addEdge(edges[i][1] - 1, edges[i][0] - 1);

            }
            return g.TwoPart();
            throw new NotImplementedException();
        }
    }
}
