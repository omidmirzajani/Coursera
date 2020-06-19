using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collecting_Signatures
{
    class Program
    {
        static void Main(string[] args)
        {
            long n = Convert.ToInt64(Console.ReadLine());
            long[] s1 = new long[n];
            long[] s2 = new long[n];
            for(int i=0;i<n;i++)
            {
                string[] t = Console.ReadLine().Split();
                s1[i]= Convert.ToInt64(t[0]);
                s2[i] = Convert.ToInt64(t[1]);

            }
            Console.WriteLine(Solve(n,s1,s2));
        }

        public static long Solve(long tenantCount, long[] startTimes, long[] endTimes)
        {
            long left = startTimes.Min();
            long right = endTimes.Max();
           
            for (int i = 0; i < tenantCount; i++)
            {
                for (int j = i + 1; j < tenantCount; j++)
                {
                    if (endTimes[i] > endTimes[j])
                    {
                        (startTimes[i], startTimes[j]) = (startTimes[j], startTimes[i]);
                        (endTimes[i], endTimes[j]) = (endTimes[j], endTimes[i]);
                    }
                }
            }
            long count = 0;
            long[] visited = new long[tenantCount];
            for (int i = 0; i < tenantCount; i++)
            {
                visited[i] = 0;
            }
            int c = 0;
            while (!AllVisited(visited))
            {
                while (visited[c] != 0)
                {
                    c++;
                }
                long end = endTimes[c];
                int k = 0;
                for (int j = 0; j < tenantCount; j++)
                {
                    if (visited[j] == 0 && end >= startTimes[j] && end <= endTimes[j])
                    {
                        visited[j] = 1;
                        k = 1;
                    }
                }
                count += k;
                c++;
            }
            return count;
        }

        public static bool AllVisited(long[] dots)
        {
            for (int i = 0; i < dots.Length; i++)
                if (dots[i] == 0)
                    return false;
            return true;
        }
    }
}
