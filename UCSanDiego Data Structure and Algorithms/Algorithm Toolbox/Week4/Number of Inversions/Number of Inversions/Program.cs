using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Number_of_Inversions
{
    class Program
    {
        static void Main(string[] args)
        {
            long n = Convert.ToInt64(Console.ReadLine());
            long[] a = Console.ReadLine().Split().Select(d => Convert.ToInt64(d)).ToArray();
            Console.WriteLine(Solve(n,a));
        }
        public static long Solve(long n, long[] a)
        {
            if (n < 2)
                return 0;
            long t1 = 0;
            long t2 = 0;
            t1 = (long)a.Length / 2;
            t2 = a.Length - t1;

            long[] mid1 = new long[t1];
            long[] mid2 = new long[t2];
            for (int i = 0; i < t1; i++)
                mid1[i] = a[i];
            for (long i = t1; i < a.Length; i++)
                mid2[i - t1] = a[i];
            long b = Solve(t1, mid1) + Solve(t2, mid2);
            mid1 = mid1.OrderBy(x => x).ToArray();
            mid2 = mid2.OrderBy(x => x).ToArray();
            long xi = 0;
            long xj = 0;
            long numOfIversion = 0;
            while (xi < t1 && xj < t2)
            {
                if (mid1[xi] > mid2[xj])
                {
                    xj++;
                    numOfIversion += t1 - xi;
                }
                else if (mid1[xi] <= mid2[xj])
                    xi++;
            }
            return numOfIversion + b;
        }
    }
}
