using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reconstruct
{
    public struct myTuple
    {
        public char s;
        public long i;

    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Solve(Console.ReadLine()));
        }
        public static string Solve(string bwt)
        {
            List<myTuple> first = new List<myTuple>();

            myTuple myTuple;

            for (int i = 0; i < bwt.Length; i++)
            {
                myTuple.i = i;
                myTuple.s = bwt[i];
                first.Add(myTuple);
            }

            first = first.OrderBy(d => d.s).ToList();
            StringBuilder ss = new StringBuilder();
            long ind = 0;
            long tt = 0;
            long n = bwt.Length;
            while (tt != n)
            {
                ind = first[(int)ind].i;
                ss.Append(first[(int)ind].s);
                tt++;
            }
            char[] chararray = ss.ToString().ToCharArray();
            return new string(chararray);
        }
    }
}
