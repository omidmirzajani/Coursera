using System;
using System.Collections.Generic;
using System.Linq;

namespace BubbleDetection
{
    class Program
    {
        static int k;
        static int t;
        static long result = 0;
        static List<string> reads=new List<string>();
        static Dictionary<string, int> nameToIndex=new Dictionary<string, int>();
        static List<HashSet<int>> adj = new List<HashSet<int>>();
        static HashSet<int> inn=new HashSet<int>();
        static HashSet<int> outt=new HashSet<int>();
        static HashSet<int> visit=new HashSet<int>();
        static Dictionary<int, Dictionary<int, List<HashSet<int>>>> paths=new Dictionary<int, Dictionary<int, List<HashSet<int>>>>();

        public static void Build()
        {
            int counter = 0;
            foreach (var str in reads)
            {
                for (int i = 0; i < str.Length-k+1; ++i) 
                {
                    if (!nameToIndex.ContainsKey(str.Substring(i, k - 1)))
                    {
                        nameToIndex.Add(str.Substring(i, k - 1), counter++);
                        HashSet<int> empty = new HashSet<int>();
                        adj.Add(empty);
                    }
                    if (!nameToIndex.ContainsKey(str.Substring(i+1, k - 1)))
                    {
                        nameToIndex.Add(str.Substring(i+1, k - 1), counter++);
                        HashSet<int> empty = new HashSet<int>();
                        adj.Add(empty);
                    }
                    adj[nameToIndex[str.Substring(i, k - 1)]].Add(nameToIndex[str.Substring(i + 1, k - 1)]);
                }
            }
        }
        public static void FindVertex()
        {
            int[] inEdge = new int[adj.Count];
            int[] outEdge = new int[adj.Count];
            for (int i = 0; i < adj.Count; ++i)
            {
                foreach (var v in adj[i])
                {
                    inEdge[v]++;
                    outEdge[i]++;
                }
            }
            for (int i = 0; i < adj.Count; i++)
            {
                if (inEdge[i] > 1)
                    inn.Add(i);
                if (outEdge[i] > 1)
                    outt.Add(i);
            }
        }

        public static void DFS(int root,int last,HashSet<int> visited)
        {
            if((last!=root) && (inn.Contains(last)))
            {
                HashSet<int> path = new HashSet<int>();
                foreach (var item in visited)
                {
                    path.Add(item);
                }
                path.Remove(last);
                path.Remove(root);
                if (!paths.ContainsKey(root))
                    paths[root] = new Dictionary<int, List<HashSet<int>>>();
                if (!paths[root].ContainsKey(last))
                    paths[root][last] = new List<HashSet<int>>();
                paths[root][last].Add(path);
            }

            if (visited.Count > t)
                return;
            foreach (int v in adj[last])
            {
                if (!visited.Contains(v))
                {
                    HashSet<int> tempMark = new HashSet<int>();
                    foreach (var item in visited)
                    {
                        tempMark.Add(item);
                    }
                    tempMark.Add(v);
                    DFS(root, v, tempMark);
                }
            }
        }
        public static void Main(string[] args)
        {
            int[] a = Console.ReadLine().Split().Select(d => int.Parse(d)).ToArray();
            k = a[0];
            t = a[1];
            string s="s";
            while (!string.IsNullOrEmpty(s))
            {
                s = Console.ReadLine();
                if (!string.IsNullOrEmpty(s))
                {
                    reads.Add(s);
                }
            }
            Build();
            FindVertex();
            foreach (var o in outt)
            {
                visit.Clear();
                visit.Add(o);
                DFS(o, o, visit);
            }
            foreach (var path in paths)
            {
                foreach (var item in path.Value)
                {
                    List<HashSet<int>> mypaths = item.Value;
                    for (int i = 0; i < mypaths.Count; ++i)
                    {
                        for (int j = i+1; j < mypaths.Count; ++j)
                        {
                            bool flag = true;
                            foreach (int x in mypaths[i].ToArray())
                            {
                                if (mypaths[j].Contains(x))
                                {
                                    flag = false;
                                    break;
                                }
                            }
                            if (flag)
                                result++;
                        }
                    }
                }
            }
            Console.WriteLine(result);
        }
    }
}
