using System;
using System.Collections.Generic;
using System.Linq;

namespace Recoloring
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
    public class Q1CircuitDesign
    {
        public Q1CircuitDesign() { }
        public virtual Tuple<bool, long[]> Solve(long v, long c, long[][] cnf)
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

        private bool AllNotAsigned(List<long> item, long[] assigned)
        {
            foreach (var i in item)
                if (assigned[i] != 0)
                    return false;
            return true;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            long[] a = Console.ReadLine().Split().Select(d => long.Parse(d)).ToArray();
            long nodeCount = a[0];
            long edgeCount = a[1];
            char[] s = Console.ReadLine().ToCharArray();
            long[][] edges = new long[edgeCount][];
            for (int i = 0; i < edgeCount; i++)
            {
                a = Console.ReadLine().Split().Select(d => long.Parse(d)).ToArray();
                edges[i] = a;
            }
            foreach (var item in Solve(nodeCount, s, edges)) 
            {
                Console.Write(item);
            }
        }
        public static char[] Solve(long nodeCount, char[] colors, long[][] edges)
        {
            List<long[]> cnf = new List<long[]>();
            foreach (var edge in edges)
            {
                cnf.Add(new long[] { -edge[0], -edge[1] });
                cnf.Add(new long[] { -(edge[0] + nodeCount), -(edge[1] + nodeCount) });
                cnf.Add(new long[] { -(edge[0] + 2 * nodeCount), -(edge[1] + 2 * nodeCount) });
            }
            for (int i = 0; i < nodeCount; i++)
            {
                if (colors[i] == 'R')
                {
                    cnf.Add(new long[] { (nodeCount + i + 1), (2 * nodeCount + i + 1) });
                    cnf.Add(new long[] { -(i + 1), -(i + 1) });
                }
                if (colors[i] == 'G')
                {
                    cnf.Add(new long[] { (i + 1), (2 * nodeCount + i + 1) });
                    cnf.Add(new long[] { -(nodeCount + (i + 1)), -(nodeCount + (i + 1)) });
                }
                if (colors[i] == 'B')
                {
                    cnf.Add(new long[] { -(nodeCount * 2 + (i + 1)), -(nodeCount * 2 + (i + 1)) });
                    cnf.Add(new long[] { (nodeCount + i + 1), (i + 1) });
                }
            }
            Q1CircuitDesign q1 = new Q1CircuitDesign();
            var result = q1.Solve(3 * nodeCount, cnf.Count, cnf.ToArray());
            if (!result.Item1)
                return "Impossible".ToCharArray();
            else
            {
                char[] res = new char[(int)nodeCount];
                for (int i = 0; i < nodeCount; i++)
                {
                    if (result.Item2[i] > 0)
                        res[i] = 'R';
                }
                for (int i = (int)nodeCount; i < 2 * nodeCount; i++)
                {
                    if (result.Item2[i] > 0)
                        res[i - nodeCount] = 'G';
                }
                for (int i = 2 * (int)nodeCount; i < 3 * nodeCount; i++)
                {
                    if (result.Item2[i] > 0)
                        res[i - 2 * nodeCount] = 'B';
                }
                return res;
            }
        }
    }
}
