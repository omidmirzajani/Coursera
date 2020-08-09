using System;
using System.Collections.Generic;
using System.Linq;

namespace Flow
{
    public class MyGraph
    {
        public long V;
        public List<long>[] adj;
        public long[,] res;

        public MyGraph(long v)
        {
            V = v;
            adj = new List<long>[V];
            for (int i = 0; i < V; i++)
            {
                adj[i] = new List<long>();
            }
            res = new long[V, V];
        }

        public void addEdge(long i, long j, long w)
        {
            adj[i].Add(j);
            adj[j].Add(i);
            res[i, j] += w;
        }
        public long[] ShortestPath(long start, long end)
        {
            long[] parent = new long[V];
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
                        if (vis[t] == 0 && res[node, t] != 0)
                        {
                            parent[t] = node;
                            qPrime.Enqueue(t);
                            vis[t] = last + 1;
                        }
                    }
                }
                last++;
                q = qPrime;
            }
            List<long> result = new List<long>();
            if (vis[end] != 0)
            {
                result.Add(end);
                while (end != start)
                {
                    end = parent[end];
                    result.Add(end);
                }
            }
            result.Reverse();
            return result.ToArray();
        }
        public long[] findPathDFS(long current, long goal, List<long> visited)
        {
            if (current == goal)
                return new long[] { current };


            //if current in graph:
            foreach (var node in adj[current])
            {
                if (!visited.Contains(node))
                {
                    visited.Add(node);
                    long[] path = findPathDFS(node, goal, visited);
                    if (path.Length > 0)
                    {
                        List<long> t = new List<long>() { current };
                        t.AddRange(path);
                        return t.ToArray();
                    }
                }
            }
            return new long[0];
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            int[] a = Console.ReadLine().Split().Select(d => int.Parse(d)).ToArray();
            int nodeCount = a[0];
            int edgeCount = a[1];
            MyGraph demo = new MyGraph(nodeCount + 2);
            long[][] edges = new long[edgeCount][];

            long[] inn = new long[nodeCount];
            long[] outt = new long[nodeCount];

            long[] lower = new long[edgeCount];
            long[] upper = new long[edgeCount];

            for (int i = 0; i < edgeCount; i++)
            {
                a = Console.ReadLine().Split().Select(d => int.Parse(d)).ToArray();
                inn[a[1] - 1] += a[2];
                outt[a[0] - 1] += a[2];
                edges[i] = new long[] { a[0], a[1], a[2], a[3] };
                lower[i] = a[2];
                upper[i] = a[3];

            }
            for (int i = 0; i < nodeCount; i++)
            {
                demo.addEdge(0, i + 1, inn[i]);
                demo.addEdge(i + 1, nodeCount + 1, outt[i]);
            }
            foreach (var t in edges)
            {
                if (t[0] != t[1])
                    demo.addEdge(t[0], t[1], t[3] - t[2]);
            }
            long x = Solve(demo, nodeCount + 2, edgeCount);
            long sum = lower.Sum(d => d);
            if (x != sum)
            {
                Console.WriteLine("NO");
            }
            else
            {
                Console.WriteLine("YES");
                //for (int i = 0; i < edgeCount; i++)
                //{
                //    Console.WriteLine(upper[i] - demo.res[edges[i][0], edges[i][1]]);
                //}
                for (int i = 0; i < edgeCount; ++i)
                {
                    if (demo.res[edges[i][1], edges[i][ 0]] > edges[i][3] - edges[i][2])
                    {
                        Console.WriteLine(edges[i][3]);
                        demo.res[edges[i][1], edges[i][0]] -= edges[i][3] - edges[i][2];
                    }
                    else
                    {
                        Console.WriteLine(demo.res[edges[i][1], edges[i][0]] + edges[i][2]);
                        demo.res[edges[i][1], edges[i][0]] = 0;
                    }
                }
            }
        }
        public static long Solve(MyGraph demo, long nodeCount, long edgeCount)
        {
            long sum = 0;
            while (true)
            {
                long[] path = demo.ShortestPath(0, nodeCount - 1);
                if (path.Length == 0)
                {
                    return sum;
                }
                else
                {
                    long min = int.MaxValue;
                    for (int i = 0; i < path.Length - 1; i++)
                    {
                        min = Math.Min(min, demo.res[path[i], path[i + 1]]);
                    }
                    sum += min;
                    for (int i = 0; i < path.Length - 1; i++)
                    {
                        demo.res[path[i], path[i + 1]] -= min;
                        demo.res[path[i + 1], path[i]] += min;
                    }
                }
            }
        }
    }
}
