using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Majority_Element
{
    class Program
    {
        static void Main(string[] args)
        {
            long n =Convert.ToInt64( Console.ReadLine());
            long[] a = Console.ReadLine().Split().Select(d => Convert.ToInt64(d)).ToArray();
            Console.WriteLine(Solve(n,a));
        }
        public static long Solve(long n, long[] a)
        {
            a = a.OrderBy(x => x).ToArray();
            for (int i = 0; i < n / 2; i++)
            {
                if (a[i] == a[i + n / 2])
                    return 1;
            }
            return 0;
        }
    }
}
