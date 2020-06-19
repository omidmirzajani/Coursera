using System;
using System.Collections.Generic;
using System.Linq;

namespace Circuit
{
    public class Graph
    {
        public long V;
        public List<long>[] adj;
        public bool[] visited;
        public long Time;
        public List<long> postorder;
        public Stack<long> st;
        public Graph(long v)
        {
            V = v;
            adj = new List<long>[V];
            for (int i = 0; i < V; i++)
                adj[i] = new List<long>();
            Time = 0;
            visited = new bool[v];
            postorder = new List<long>();
        }
        public void AddEdge(long s, long e)
        {
            if (s < 0)
                s = (int)(V / 2) - s;
            if (e < 0)
                e = (int)(V / 2) - e;
            s--;
            e--;
            if (!adj[s].Contains(e))
            {
                adj[s].Add(e);
            }
        }
        void DFSUtil(int v, bool[] visited, List<long> res)
        {
            visited[v] = true;
            res.Add(v);
            foreach (var i in adj[v])
                if (visited[i] == false)
                    DFSUtil((int)i, visited, res);
        }
        public Graph getTranspose()
        {
            Graph g = new Graph(V);
            for (int v = 0; v < V; v++)
                foreach (var j in adj[v])
                    g.AddEdge(j + 1, v + 1);
            return g;
        }
        public void fillOrder(int v, bool[] visited, Stack<long> stack)
        {
            visited[v] = true;

            foreach (var i in adj[v])
                if (visited[i] == false)
                    fillOrder((int)i, visited, stack);

            stack.Push(v);
        }
        public List<List<long>> printSCCs()
        {
            var result = new List<List<long>>();
            Stack<long> stack = new Stack<long>();

            bool[] visited = new bool[V];
            for (int i = 0; i < V; i++)
                visited[i] = false;

            for (int i = 0; i < V; i++)
                if (visited[i] == false)
                    fillOrder(i, visited, stack);

            Graph gr = getTranspose();
            for (int i = 0; i < V; i++)
                visited[i] = false;

            while (stack.Count != 0)
            {
                int v = (int)stack.Pop();

                if (visited[v] == false)
                {
                    var xx = new List<long>();
                    gr.DFSUtil(v, visited, xx);
                    result.Add(xx);
                }
            }
            return result;
        }
        public long[] Topological(long nodeCount, long[][] edges)
        {
            for (long node = 0; node < nodeCount; node++)
                if (!visited[node])
                    DFS2(node);
            List<long> v = postorder;
            v.Reverse();
            return v.ToArray();
        }
        public void DFS2(long node)
        {
            visited[node] = true;
            foreach (long n in adj[node])
                if (!visited[n])
                    DFS2(n);
            postorder.Add(node + 1);
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

        public void DFS1(long node)
        {
            visited[node] = true;
            foreach (long n in adj[node])
                if (!visited[n])
                    DFS1(n);
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
            int[] a = Console.ReadLine().Split().Select(d => Convert.ToInt32(d)).ToArray();
            int v = a[0];
            int c = a[1];
            long[][] cnf = new long[c][];
            for (int i = 0; i < c; i++)
                cnf[i] = Console.ReadLine().Split().Select(d => Convert.ToInt64(d)).ToArray();
            var xx = Solve(v, c, cnf);
            if (xx.Item1)
            {
                Console.WriteLine("SATISFIABLE");
                foreach (var item in xx.Item2)
                {
                    Console.Write(item + " ");
                }
            }
            else
            {
                Console.WriteLine("UNSATISFIABLE");
            }
        }
        public static Tuple<bool, long[]> Solve(long v, long c, long[][] cnf)
        {
            Graph ig = new Graph(2 * v);
            foreach (var item in cnf)
            {
                ig.AddEdge(-item[1], item[0]);
                ig.AddEdge(-item[0], item[1]);
            }
            var SCC = ig.printSCCs();
            foreach (var item in SCC)
            {
                foreach (var i in item)
                {
                    if (item.Contains(i + v))
                        return new Tuple<bool, long[]>(false, new long[0]);
                }
            }
            long[] assigned = new long[2 * v];

            foreach (var item in SCC)
            {
                if (AllNotAsigned(item, assigned))
                {
                    foreach (var i in item)
                    {
                        assigned[i] = 1;
                        if (i >= v)
                            assigned[i - v] = -1;
                        else
                            assigned[i + v] = -1;
                    }
                }
            }
            long[] result = new long[v];
            int idx = 0;
            for (int xxx = 0; xxx < v; xxx++)
            {
                var item = assigned[xxx];
                if (item == -1)
                    result[idx++] = (idx);
                else
                    result[idx++] = -(idx);
            }
            return new Tuple<bool, long[]>(true, result);
        }

        private static bool AllNotAsigned(List<long> item, long[] assigned)
        {
            foreach (var i in item)
                if (assigned[i] != 0)
                    return false;
            return true;
        }
    }
}
