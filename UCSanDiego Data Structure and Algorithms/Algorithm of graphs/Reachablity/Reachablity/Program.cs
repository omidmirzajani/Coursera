using System;
using System.Linq;

namespace Reachablity
{
    class Program
    {
        static void Main(string[] args)
        {
            long[] t = Console.ReadLine().Split().Select(d => Convert.ToInt64(d)).ToArray();
            long v = t[0];
            long e = t[1];
            long[][] edges = new long[e][];
            for(int i = 0; i < e; i++)
            {
                edges[i] = Console.ReadLine().Split().Select(d => Convert.ToInt64(d)).ToArray();
            }
            t = Console.ReadLine().Split().Select(d => Convert.ToInt64(d)).ToArray();
            Console.WriteLine(Solve(v,edges,t[0],t[1]));
        }
        public static long Solve(long nodeCount, long[][] edges, long StartNode, long EndNode)
        {
            long[] res = ToDisjoint(nodeCount, edges);
            if (res[StartNode] == res[EndNode])
                return 1;
            return 0;
        }

        private static long[] ToDisjoint(long nodeCount, long[][] edges)
        {
            long[] res = new long[nodeCount + 1];
            for (int i = 0; i < nodeCount + 1; i++)
            {
                res[i] = i;
            }
            for (int i = 0; i < edges.Length; i++)
            {
                long first = edges[i][0];
                long last = edges[i][1];
                long min = Math.Min(res[first], res[last]);
                long max = res[first] + res[last] - min;
                for (int j = 0; j <= nodeCount; j++)
                    if (res[j] == max)
                        res[j] = min;
            }
            return res;
        }
    }
}
