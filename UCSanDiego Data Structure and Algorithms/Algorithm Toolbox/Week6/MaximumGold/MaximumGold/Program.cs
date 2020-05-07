using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaximumGold
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] s = Console.ReadLine().Split();
            long w = Convert.ToInt64(s[0]);
            s = Console.ReadLine().Split();
            long[] arr = s.Select(d => Convert.ToInt64(d)).ToArray();
            Console.WriteLine(Solve(w,arr));
        }
        public static long Solve(long W, long[] goldBars)
        {
            long[,] DP = new long[W + 1, goldBars.Length + 1];
            for (int i = 0; i <= W; i++)
                DP[i, 0] = 0;
            for (int i = 0; i <= goldBars.Length; i++)
                DP[0, i] = 0;
            for (int i = 1; i <= W; i++)
            {
                for (int j = 1; j <= goldBars.Length; j++)
                {
                    long m1 = DP[i, j - 1];
                    long m2 = -1;
                    if (i >= goldBars[j - 1])
                        m2 = DP[i - goldBars[j - 1], j - 1] + goldBars[j - 1];
                    DP[i, j] = Math.Max(m1, m2);
                }
            }
            return DP[W, goldBars.Length];
        }
    }
}
