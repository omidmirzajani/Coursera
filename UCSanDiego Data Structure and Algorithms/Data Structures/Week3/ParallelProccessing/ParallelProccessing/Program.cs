using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelProccessing
{
    class Program
    {
        static void Main(string[] args)
        {
            long[] a = Console.ReadLine().Split().Select(d => Convert.ToInt64(d)).ToArray();
            long n = a[0];
            long t = a[1];
            long[] table = Console.ReadLine().Split().Select(d => Convert.ToInt64(d)).ToArray();
            long[] target = new long[t];
            long[] source = new long[t];
            for(int i=0;i<t;i++)
            {
                long[] read = Console.ReadLine().Split().Select(d => Convert.ToInt64(d)).ToArray();
                target[i] = read[0];
                source[i] = read[1];
            }
            foreach (var mj in Solve(table, target, source))
            {
                Console.WriteLine(mj);
            }
        }
        public static long[] Solve(long[] tableSizes, long[] targetTables, long[] sourceTables)
        {
            long[] id = new long[tableSizes.Length + 1];
            long[] size = new long[tableSizes.Length + 1];
            List<long> res = new List<long>();

            for (int i = 1; i <= tableSizes.Length; i++)
            {
                id[i] = i;
                size[i] = tableSizes[i - 1];
            }
            for (int i = 0; i < targetTables.Length; i++)
            {
                long a = targetTables[i];
                long b = sourceTables[i];
                long ida = id[a];
                long idb = id[b];
                long sizeb = size[b];
                long sizea = size[a];
                if (ida != idb)
                {
                    for (int j = 0; j < id.Length; j++)
                    {
                        if (id[j] == ida)
                            size[j] += sizeb;
                        if (id[j] == idb)
                            size[j] += sizea;

                        if (id[j] == ida)
                            id[j] = idb;

                    }
                }
                res.Add(size.Max());
            }
            return res.ToArray();
        }
    }
}
