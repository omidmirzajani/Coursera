using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirLines
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
            long[] node = Console.ReadLine().Split().Select(d => Convert.ToInt64(d)).ToArray();
            long flight = node[0];
            long crew = node[1];
            long[][] info = new long[flight][];
            for(int i = 0; i < flight; i++)
                info[i]= Console.ReadLine().Split().Select(d => Convert.ToInt64(d)).ToArray();
            StringBuilder s = new StringBuilder();
            Solve(flight, crew, info).ToList().ForEach(d => s.Append(d+" "));
            Console.WriteLine(s.ToString());
        }
        public static long[] Solve(long flightCount, long crewCount, long[][] info)
        {
            long nodeCount = flightCount + crewCount + 2;
            MyGraph graph = new MyGraph(nodeCount);
            for (int i = 0; i < flightCount; i++)
            {
                graph.addEdge(0, i + 1, 1);
            }
            for (int i = (int)flightCount + 1; i < nodeCount - 1; i++)
            {
                graph.addEdge(i, nodeCount - 1, 1);
            }
            for (int i = 0; i < flightCount; i++)
            {
                for (int j = 0; j < crewCount; j++)
                {
                    if (info[i][j] == 1)
                        graph.addEdge(i + 1, j + flightCount + 1, 1);
                }
            }

            long[] result = new long[flightCount];
            result = result.Select(d => (long)-1).ToArray();
            while (true)
            {
                long[] path = graph.ShortestPath(0, nodeCount - 1);
                if (path.Length == 0)
                {
                    for (int i = 1; i < flightCount + 1; i++)
                    {
                        for (int j = (int)flightCount; j < nodeCount - 1; j++)
                        {
                            if (graph.adj[i].Contains(j) && graph.res[i, j] == 0)
                                result[i - 1] = j - flightCount;
                        }
                    }
                    return result;
                }
                else
                {
                    long start = path[path.Length - 3] - 1;
                    long end = path[path.Length - 2] - flightCount;
                    for (int i = 0; i < path.Length - 1; i++)
                    {
                        graph.res[path[i], path[i + 1]]--;
                        graph.res[path[i + 1], path[i]]++;
                    }
                }
            }
        }
    }
}
