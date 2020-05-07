using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabinKarp
{
    class Program
    {
        static void Main(string[] args)
        {
        }
        public long[] Solve(string pattern, string text)
        {
            long p = 1000000007;
            long x = 263;
            List<long> res = new List<long>();
            Random t = new Random();
            long pHash = Q2HashingWithChain.PolyHash(pattern, 0, pattern.Length);
            long[] tHash = PreComputeHashes(text, pattern.Length, p, x);

            for (int i = 0; i < tHash.Length; i++)
                if (tHash[i] == pHash)
                    if (AreEqual(text, i, pattern))
                        res.Add(i);
            return res.ToArray();
        }

        private bool AreEqual(string t, int i, string p)
        {
            for (int xxx = 0; xxx < p.Length; xxx++)
                if (t[i + xxx] != p[xxx])
                    return false;
            return true;
        }

        public static long[] PreComputeHashes(
            string T,
            int P,
            long p,
            long x)
        {
            string last = T.Substring(T.Length - P);
            long[] res = new long[T.Length - P + 1];
            res[T.Length - P] = Q2HashingWithChain.PolyHash(last, 0, last.Length);

            long pow = 1;
            for (int i = 0; i < P; i++)
                pow = (pow * x) % p;
            for (int i = T.Length - P - 1; i >= 0; i--)
            {
                long firstChr = res[i + 1] * x;
                long lastChr = pow * (long)T[i + P];
                res[i] = (((T[i] + firstChr - lastChr) % p) + p) % p;
            }
            return res;
        }
    }
}
