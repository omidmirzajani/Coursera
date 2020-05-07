using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizing_a_Lottery
{
    class Program
    {
        static void Main(string[] args)
        {
            long[] a = Console.ReadLine().Split().Select(d => Convert.ToInt64(d)).ToArray();
            long n = a[0];
            long[] start = new long[n];
            long[] end = new long[n];
            for(int i = 0; i < n; i++)
            {
                long[] b = Console.ReadLine().Split().Select(d => Convert.ToInt64(d)).ToArray();
                start[i] = b[0];
                end[i] = b[1];
            }
            long[] points = Console.ReadLine().Split().Select(d => Convert.ToInt64(d)).ToArray();
            long[] res = Solve(points, start, end);
            for(int i=0;i<res.Length-1;i++)
                Console.Write($"{res[i]} ");
            Console.WriteLine(res[res.Length-1]) ;
        }
        public static long[] Solve(long[] points, long[] startSegments, long[] endSegment)
        {
            long[] res = new long[points.Length];
            for (int i = 0; i < points.Length; i++)
            {
                long t = 0;
                for (int j = 0; j < startSegments.Length; j++)
                {
                    if (points[i] <= endSegment[j] && points[i] >= startSegments[j])
                        t++;
                }
                res[i] = t;
            }
            return res;
        }
    }
}
