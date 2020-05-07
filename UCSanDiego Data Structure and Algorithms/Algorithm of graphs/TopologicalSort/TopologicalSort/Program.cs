using System;
using System.Collections.Generic;
using System.Linq;

namespace TopologicalSort
{
    public class Graph
    {
        public long V;
        public bool[] visited;
        public List<long>[] adj;
        public List<long> postorder;
        public Stack<long> st;

        public Graph(long v)
        {
            V = v;
            adj = new List<long>[v];
            for (int i = 0; i < v; ++i)
                adj[i] = new List<long>();
            visited = new bool[v];
            postorder = new List<long>();
        }

        public void addEdge(long start, long end)
        {
            adj[start].Add(end);
        }
        public List<long> DFS(long node)
        {
            visited[node] = true;
            List<long> res = new List<long>();
            res.Add(node);
            foreach (long n in adj[node])
            {
                if (!visited[n])
                    res.AddRange(DFS(n));

            }
            return res;
        }

        public void DFS2(long node)
        {
            visited[node] = true;
            foreach (long n in adj[node])
            {
                if (!visited[n])
                    DFS2(n);

            }
            postorder.Add(node + 1);
        }

        public void DFS1(long node)
        {
            visited[node] = true;
            foreach (long n in adj[node])
                if (!visited[n])
                    DFS1(n);
        }

        public long SCC(long[][] edges)
        {
            long res = 0;
            st = new Stack<long>();
            for (long i = 0; i < adj.Length; i++)
                if (visited[i] == false)
                    dfs_stack(i);

            Graph g1 = new Graph(adj.Length);
            for (int i = 0; i < edges.Length; i++)
                g1.addEdge(edges[i][1] - 1, edges[i][0] - 1);

            while (st.Count != 0)
            {
                var v = st.Pop();
                if (!g1.visited[v])
                {
                    g1.dfs_visiting(v);
                    res++;
                }
            }
            return res;
        }

        private void dfs_visiting(long v)
        {
            visited[v] = true;
            for (int i = 0; i < adj[v].Count; i++)
                if (!visited[adj[v][i]])
                    dfs_visiting(adj[v][i]);
        }

        private void dfs_stack(long i)
        {
            visited[i] = true;
            for (int j = 0; j < adj[i].Count; j++)
                if (!visited[adj[i][j]])
                    dfs_stack(adj[i][j]);
            st.Push(i);
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
            long[] tt = Solve(v, edges);
            for(int i = 0; i < tt.Length; i++)
            {
                Console.Write(tt[i]);
                if (i != tt.Length - 1)
                    Console.Write(' ');
            }
        }
        public static long[] Solve(long nodeCount, long[][] edges)
        {
            Graph g = new Graph(nodeCount);
            for (int i = 0; i < edges.Length; i++)
                g.addEdge(edges[i][0] - 1, edges[i][1] - 1);

            for (long node = 0; node < nodeCount; node++)
            {
                if (!g.visited[node])
                    g.DFS2(node);
            }
            List<long> v = g.postorder;
            v.Reverse();
            return v.ToArray();
        }
    }
}
