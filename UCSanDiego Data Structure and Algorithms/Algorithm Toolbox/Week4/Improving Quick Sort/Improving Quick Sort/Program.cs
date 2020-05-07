using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Improving_Quick_Sort
{
    class Program
    {
        static void Main(string[] args)
        {
            long n = Convert.ToInt64(Console.ReadLine());
            long[] a = Console.ReadLine().Split().Select(d => Convert.ToInt64(d)).ToArray();
            long[] res = Solve(n, a);
            for (int i = 0; i < res.Length - 1; i++)
                Console.Write($"{res[i]} ");
            Console.WriteLine(res[res.Length - 1]);
        }
        public static long[] Solve(long n, long[] a)
        {
            if (n < 2)
                return a;
            long pivot = a[0];
            List<long> mid1 = new List<long>();
            List<long> mid2 = new List<long>();
            List<long> mid = new List<long>();

            for (int i = 0; i < n; i++)
            {
                if (pivot > a[i])
                    mid1.Add(a[i]);
                else if (pivot < a[i])
                    mid2.Add(a[i]);
                else
                    mid.Add(a[i]);
            }
            long[] arr1 = Solve(mid1.Count, mid1.ToArray());
            long[] arr2 = Solve(mid2.Count, mid2.ToArray());

            List<long> res = new List<long>();
            res.AddRange(arr1);
            res.AddRange(mid);
            res.AddRange(arr2);
            return res.ToArray();
        }
    }
}
