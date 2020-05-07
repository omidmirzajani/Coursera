using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeHeap
{
    class Program
    {
        static void Main(string[] args)
        {
            long n = Convert.ToInt64(Console.ReadLine());
            long[] b = Console.ReadLine().Split().Select(d => Convert.ToInt64(d)).ToArray();
            var s = Solve(b);
            Console.WriteLine(s.Length);
            foreach(var t in s)
            {
                Console.WriteLine($"{t.Item1} {t.Item2}");
            }
        }
        public static Tuple<long, long>[] Solve(long[] b)
        {
            List<Tuple<long, long>> omid = new List<Tuple<long, long>>();

            long n = b.Length;
            for (int i = Convert.ToInt32(n / 2); i >= 0; i--)
            {
                SiftDown(b, i, omid);
            }
            return omid.ToArray();
        }

        public static void SiftDown(long[] b, long i, List<Tuple<long, long>> omid)
        {
            long n = b.Length;
            long maxIndex = i;
            long l = 2 * i + 1;
            if (l < n && b[l] < b[maxIndex])
                maxIndex = l;
            long r = 2 * i + 2;
            if (r < n && b[r] < b[maxIndex])
                maxIndex = r;
            if (i != maxIndex)
            {
                long swap = b[i];
                b[i] = b[maxIndex];
                b[maxIndex] = swap;
                omid.Add(new Tuple<long, long>(i, maxIndex));
                SiftDown(b, maxIndex, omid);
            }
        }
    }
}
