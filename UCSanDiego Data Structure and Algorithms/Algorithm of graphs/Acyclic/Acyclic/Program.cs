using System;
using System.Collections.Generic;
using System.Linq;

namespace Acyclic
{
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
            List<long>[] adj = new List<long>[nodeCount];
            for (int i = 0; i < nodeCount; i++)
                adj[i] = new List<long>();

            for (int i = 0; i < edges.Length; i++)
                adj[edges[i][0] - 1].Add(edges[i][1] - 1);

            return isAcyclic(nodeCount, adj);

        }
        public static long isAcyclic(long nodeCount, List<long>[] adj)
        {
            bool[] visited = new bool[nodeCount];
            bool[] recuersive = new bool[nodeCount];
            for (int i = 0; i < nodeCount; i++)
                if (!visited[i])
                    if (dfs(i, visited, recuersive, adj))
                        return 1;
            return 0;

        }
        public static bool dfs(long v, bool[] visited, bool[] recuersive, List<long>[] adj)
        {
            visited[v] = true;
            recuersive[v] = true;
            foreach (long ad in adj[v])
            {
                if (!visited[ad])
                {
                    if (dfs(ad, visited, recuersive, adj))
                        return true;
                }
                else if (recuersive[ad])
                    return true;

            }
            recuersive[v] = false;
            return false;
        }
    }
}
