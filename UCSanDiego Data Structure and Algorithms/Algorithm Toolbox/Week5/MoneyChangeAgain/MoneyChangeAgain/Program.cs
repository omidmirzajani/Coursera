using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyChangeAgain
{
    class Program
    {
        static void Main(string[] args)
        {
            long n = Convert.ToInt64(Console.ReadLine());
            Console.WriteLine(Solve(n));
        }
        public static long Solve(long n)
        {
            if (n == 0)
                return 0;
            if (n == 1)
                return 1;
            if (n == 2)
                return 2;
            if (n == 3)
                return 1;
            if (n == 4)
                return 1;

            long[] arr = new long[n + 1];
            arr[0] = 0;
            arr[1] = 1;
            arr[2] = 2;
            arr[3] = 1;
            arr[4] = 1;
            for (int i = 5; i <= n; i++)
            {
                arr[i] = Math.Min(Math.Min(arr[i - 1], arr[i - 3]), arr[i - 4]) + 1;
            }
            int c = 0;
            return arr[n];
        }
    }
}
