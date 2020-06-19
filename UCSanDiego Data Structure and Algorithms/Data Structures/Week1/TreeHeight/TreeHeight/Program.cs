using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeHeight
{
    class Program
    {
        static void Main(string[] args)
        {
            long n = Convert.ToInt64(Console.ReadLine());
            long[] tree = Console.ReadLine().Split().Select(d => Convert.ToInt64(d)).ToArray();
            Console.WriteLine(Solve(n,tree));
        }
        public static long Solve(long nodeCount, long[] tree)
        {
            long rishe = tree.ToList().IndexOf(-1);
            Dictionary<long, List<long>> graph = new Dictionary<long, List<long>>();

            for (int i = 0; i < nodeCount; i++)
                graph[i] = new List<long>();

            for (int i = 0; i < nodeCount; i++)
                if (tree[i] != -1)
                    graph[tree[i]].Add(i);

            return Height(graph, rishe);
        }

        private static long Height(Dictionary<long, List<long>> graph, long rishe)
        {
            if (graph[rishe].Count == 0)
                return 1;
            long max = 0;
            for (int i = 0; i < graph[rishe].Count; i++)
            {
                max = Math.Max(max, Height(graph, graph[rishe][i]));
            }
            return max + 1;
        }
    }
}
