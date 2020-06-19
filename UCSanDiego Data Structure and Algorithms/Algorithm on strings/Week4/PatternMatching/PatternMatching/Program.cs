using System;

namespace PatternMatching
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
        protected static long[] Solve(string text, long n, string[] patterns)
        {
            //List<long> result = new List<long>();
            //foreach (string patt in patterns)
            //{
            //    long[] t = Q1FindAllOccur.Solve(text, patt);
            //    foreach (long v in t)
            //        if (!result.Contains(v) && v != -1)
            //            result.Add(v);
            //}
            //if (result.Count == 0)
            //    return new long[1] { -1 };
            //return result.ToArray();
            long[] suff = Q2CunstructSuffixArray.Solve(text + "$");
            List<long> result = new List<long>();
            text += "$";
            foreach (string pat in patterns)
            {
                foreach (var t in Search(pat, text, suff, 0, text.Length - 1))
                {
                    if (!result.Contains(t))
                        result.Add(t);
                }
            }
            if (result.Count == 0)
                return new long[1] { -1 };
            return result.ToArray();
        }

        private static long[] Search(string pat, string text, long[] suff, long up, long bottom)
        {
            if (up > bottom)
                return new long[0];
            List<long> res = new List<long>();
            long mid = (up + bottom) / 2;
            if (strcmp(pat, text, suff[mid]) == 0)
            {
                if (!res.Contains(suff[mid]))
                    res.Add(suff[mid]);
                foreach (long tt in Search(pat, text, suff, up, mid - 1))
                {
                    if (!res.Contains(tt))
                        res.Add(tt);
                }
                foreach (long tt in Search(pat, text, suff, mid + 1, bottom))
                {
                    if (!res.Contains(tt))
                        res.Add(tt);
                }
            }
            else if (strcmp(pat, text, suff[mid]) < 0)
            {
                foreach (long tt in Search(pat, text, suff, up, mid - 1))
                {
                    if (!res.Contains(tt))
                        res.Add(tt);
                }
            }
            else
            {
                foreach (long tt in Search(pat, text, suff, mid + 1, bottom))
                {
                    if (!res.Contains(tt))
                        res.Add(tt);
                }
            }
            return res.ToArray();
        }

        private int strcmp(string pat, string text, long v)
        {
            if (v == text.Length)
                return 1;
            for (long i = v; (i < text.Length) && ((i - v) < pat.Length); i++)
            {
                if (pat[(int)(i - v)] < text[(int)i])
                {
                    return -1;
                }
                else if (pat[(int)(i - v)] > text[(int)i])
                {
                    return 1;
                }

            }
            return 0;
        }
    }
}
