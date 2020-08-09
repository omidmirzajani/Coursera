using System;
using System.Collections.Generic;

namespace OptimalKMers
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> reads = new List<string>();
            string d = "a";
            while (!string.IsNullOrEmpty(d))
            {
                d = Console.ReadLine();
                if (!string.IsNullOrEmpty(d))
                {
                    reads.Add(d);
                }
            }
            for (int i = reads[2].Length; i >1; i--)
            {
                if (isOprimized(i, reads))
                {
                    Console.WriteLine(i);
                    break;
                }
            }
        }

        private static bool isOprimized(int k, List<string> reads)
        {
            HashSet<string> kmers = new HashSet<string>();
            foreach (var read in reads)
            {
                for (int i = 0; i < read.Length-k+1; i++)
                {
                    kmers.Add(read.Substring(i, k));
                }
            }
            HashSet<string> prefix = new HashSet<string>();
            HashSet<string> suffix = new HashSet<string>();
            foreach (var kmer in kmers)
            {
                prefix.Add(kmer.Substring(0, kmer.Length - 1));
                suffix.Add(kmer.Substring(1));
            }
            foreach (var item in prefix)
            {
                if (!suffix.Contains(item))
                    return false;
            }
            return true;
        }
    }
}
