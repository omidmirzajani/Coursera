using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Binary_Search
{
    class Program
    {
        static void Main(string[] args)
        {
            long[] a = Console.ReadLine().Split().Select(d => Convert.ToInt64(d)).ToArray();
            long[] b = Console.ReadLine().Split().Select(d => Convert.ToInt64(d)).ToArray();
            long[] a1 = new long[a.Length - 1];
            long[] b1 = new long[b.Length - 1];
            for(int i=0;i<a1.Length;i++)
                a1[i] = a[i + 1];
            for (int i = 0; i < b1.Length; i++)
                b1[i] = b[i + 1];
            long[] res = Solve(a1, b1);
            for(int i=0;i<res.Length-1;i++)
                Console.Write($"{res[i]} ");
            Console.WriteLine(res[res.Length-1]);

        }
        public static long[] Solve(long[] a, long[] b)
        {
            long[] res = new long[b.Length];
            for (int i = 0; i < b.Length; i++)
                res[i] = Binary(b[i], a, 0, a.Length - 1);
            return res;

        }

        public static  long Binary(long v, long[] a, long v1, long v2)
        {
            if (v1 > v2)
                return -1;
            if (v1 == v2)
                if (a[v1] == v)
                    return v1;
                else
                    return -1;
            long t1 = 0;
            long t2 = 0;
            t1 = (long)(v2 - v1 + 1) / 2;
            t2 = (long)(v2 - v1 + 1) - t1 - 1;
            long mid = a[t1 + v1];
            if (mid == v)
                return t1 + v1;
            if (v > mid)
            {

                long rec = Binary(v, a, v1 + t1 + 1, v2);
                return rec;
            }
            else
            {
                long rec = Binary(v, a, v1, v1 + t1 - 1);
                return rec;
            }
        }
    }
}
