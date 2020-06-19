using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maximum_Advertisement_Revenue
{
    class Program
    {
        public static void Main(string[] args)
        {
            long n = Convert.ToInt64(Console.ReadLine());
            string[] s1 = Console.ReadLine().Split(' ');
            string[] s2 = Console.ReadLine().Split(' ');
            long[] a1 = s1.Select(d => Convert.ToInt64(d)).ToArray();
            long[] a2 = s2.Select(d => Convert.ToInt64(d)).ToArray();
            Console.WriteLine(Solve(n,a1,a2));
        }

        public static long Solve(long slotCount, long[] adRevenue, long[] averageDailyClick)
        {
            for (int i = 0; i < slotCount; i++)
            {
                for (int j = i + 1; j < slotCount; j++)
                {
                    if (adRevenue[j] > adRevenue[i])
                    {
                        var tmp = adRevenue[i];
                        adRevenue[i] = adRevenue[j];
                        adRevenue[j] = tmp;
                    }
                    if (averageDailyClick[j] > averageDailyClick[i])
                    {
                        var tmp = averageDailyClick[i];
                        averageDailyClick[i] = averageDailyClick[j];
                        averageDailyClick[j] = tmp;
                    }
                }
            }

            long count = 0;
            for (int i = 0; i < slotCount; i++)
                count += adRevenue[i] * averageDailyClick[i];
            return count;


        }
    }
}
