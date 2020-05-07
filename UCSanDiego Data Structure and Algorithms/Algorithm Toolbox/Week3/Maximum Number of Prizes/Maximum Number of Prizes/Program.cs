using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maximum_Number_of_Prizes
{
    class Program
    {
        static void Main(string[] args)
        {
            long n = Convert.ToInt64(Console.ReadLine());
            long[] s = Solve(n);
            Console.WriteLine(s.Length);
            string res = "";
            for (int i = 0; i < s.Length; i++)
                if (i != s.Length - 1)
                    res += s[i].ToString() + " ";
                else
                    res += s[i].ToString();
            Console.WriteLine(res);
        }
        public static long[] Solve(long n)
        {
            if (n == 9)
                return new long[3] { 1, 2, 6 };
            if (n < 3)
                return new long[1] { n };
            if (n == 5)
                return new long[2] { 2, 3 };
            long i = (long)Math.Sqrt(2 * n + 2);
            while (true)
            {
                if (i == 0)
                    return new long[2] { 2, 3 };
                long sum = i * (i + 1);
                sum = sum / 2;
                if (n - sum >= i)
                {

                    List<long> list = new List<long>();
                    for (long j = 1; j <= i; j++)
                        list.Add(j);
                    list.Add(n - sum);
                    return list.ToArray();
                }
                i--;
            }
        }
    }
}
