using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartitioningSuveniers
{
    class Program
    {
        static void Main(string[] args)
        {
            long n = Convert.ToInt64(Console.ReadLine());
            long[] sove = Console.ReadLine().Select(d => Convert.ToInt64(d)).ToArray();
            Console.WriteLine(Solve(n,sove));
        }
        public static long Solve(long souvenirsCount, long[] souvenirs)
        {
            long Sum = sum(souvenirs);
            if (Sum % 3 != 0)
                return 0;
            bool[,] arr = new bool[souvenirsCount + 1, 1 + 2 * Sum / 3];
            for (int i = 0; i <= souvenirsCount; i++)
                arr[i, 0] = true;
            for (int i = 1; i <= 2 * Sum / 3; i++)
                arr[0, i] = false;
            for (int i = 1; i <= souvenirsCount; i++)
            {
                for (int j = 1; j <= 2 * Sum / 3; j++)
                {
                    bool a1 = arr[i - 1, j];
                    bool a2 = false;
                    if (j >= souvenirs[i - 1])
                        a2 = arr[i - 1, j - souvenirs[i - 1]];
                    if (a1 == true || a2 == true)
                        arr[i, j] = true;
                }
            }
            if (souvenirsCount == 0)
                return 0;
            if (arr[souvenirsCount, 2 * Sum / 3] && arr[souvenirsCount, Sum / 3])
                return 1;
            return 0;
        }
        public static long sum(long[] souvenirs)
        {
            long s = 0;
            for (int I = 0; I < souvenirs.Length; I++)
                s += souvenirs[I];
            return s;
        }
    }
}
