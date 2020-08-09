using System;
using System.Collections.Generic;

namespace KUniversal
{
    class Program
    {
        static List<string> visited = new List<string>();
        static List<int> edges = new List<int>();
        static void DFS(string node, int k)
        {
            for (int i = 0; i < k; ++i)
            {
                string str = node + (i % 2);
                if (!visited.Contains(str))
                {
                    if (!visited.Contains(str))
                        visited.Add(str);
                    DFS(str.Substring(1), k);
                    edges.Add(i);
                }
            }
        }

        public static string deBruijn(int n)
        {
            string start = "";
            for (int i = 0; i < n - 1; i++)
                start += '0';
            DFS(start, 2);

            string S = "";

            int l = (int)Math.Pow(2, n);
            for (int i = 0; i < l; ++i)
                S += (edges[i] % 2);
            S += start;

            return S;
        }

        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            Console.WriteLine(deBruijn(n));
        }
    }
}
