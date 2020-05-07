using System;
using System.Collections.Generic;

namespace Maximum_Value_of_the_Loot
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] s = Console.ReadLine().Split();
            long n = Convert.ToInt64(s[0]);
            long w = Convert.ToInt64(s[1]);
            List<long> weights = new List<long>();
            List<long> values = new List<long>();
            for(int i=0;i<n;i++)
            {
                string[] s2 = Console.ReadLine().Split();
                long n2 = Convert.ToInt64(s2[0]);
                long w2 = Convert.ToInt64(s2[1]);
                weights.Add(n2);
                values.Add(w2);
            }
            Console.WriteLine(Solve(w,values.ToArray(),weights.ToArray()));
        }
        public static long Solve(long capacity, long[] weights, long[] values)
        {
            float[] better = new float[weights.Length];
            for (int i = 0; i < weights.Length; i++)
            {
                better[i] = (float)values[i] / weights[i];
            }
            for (int i = 0; i < weights.Length; i++)
            {
                for (int j = i + 1; j < weights.Length; j++)
                {
                    if (better[j] > better[i])
                    {
                        (better[i], better[j]) = (better[j], better[i]);
                        (weights[i], weights[j]) = (weights[j], weights[i]);
                        (values[i], values[j]) = (values[j], values[i]);
                    }
                }
            }
            double value = 0;
            for (int i = 0; i < weights.Length; i++)
            {
                if (weights[i] > capacity)
                {
                    value += (float)capacity * ((float)values[i] / weights[i]);
                    capacity = 0;
                }
                else
                {
                    value += values[i];
                    capacity -= weights[i];
                }
            }
            return (long)value;
        }

    }
}
