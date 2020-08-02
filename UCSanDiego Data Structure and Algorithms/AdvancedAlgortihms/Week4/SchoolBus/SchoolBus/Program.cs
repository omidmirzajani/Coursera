using System;
using System.Collections.Generic;
using System.Linq;

namespace SchoolBus
{
    class Program
    {
        static void Main(string[] args)
        {
            long[] a = Console.ReadLine().Split().Select(d => long.Parse(d)).ToArray();
            long nodeCount = a[0];
            long edgeCount = a[1];
            long[][] edges = new long[edgeCount][];
            for (int i = 0; i < edgeCount; i++)
            {
                a = Console.ReadLine().Split().Select(d => long.Parse(d)).ToArray();
                edges[i] = a;
            }
            var x = Solve(nodeCount, edges);
            Console.WriteLine(x.Item1);
            if(x.Item1!=(-1))
            {
                foreach (var item in x.Item2)
                {
                    Console.Write(item+" ");
                }
            }
        }
        public static Tuple<long, long[]> Solve(long nodeCount, long[][] edges)
        {
            long[,] C = new long[(long)Math.Pow(2, nodeCount), nodeCount];
            for (int i = 0; i < (long)Math.Pow(2, nodeCount); i++)
                for (int j = 0; j < nodeCount; j++)
                    C[i, j] = long.MaxValue;
            C[1, 0] = 0;

            //List<long> lastNum = new List<long>() { 1 };
            //List<long> tmpNum = new List<long>();

            HashSet<long> set = new HashSet<long>() { 1 };
            long start = 0;
            long end = 1;

            long[,] d = new long[nodeCount, nodeCount];
            foreach (var item in edges)
            {
                d[item[0] - 1, item[1] - 1] = item[2];
                d[item[1] - 1, item[0] - 1] = item[2];
            }
            for (int s = 2; s <= nodeCount; s++)
            {
                //for (int j = start; j < end; j++)
                //{
                //    var item = set[j];
                //    for (int i = 1; i <= nodeCount; i++)
                //    {
                //        if ((item | (long)Math.Pow(2, i - 1)) != item)
                //        {
                //            if (!set.Contains((long)Math.Pow(2, i - 1) + item))
                //            {
                //                set.Add((long)Math.Pow(2, i - 1) + item);
                //            }
                //        }
                //    }
                //}
                //tmpNum.Clear();
                //foreach (var item in lastNum)
                //{
                //    for (int i=1;i<=nodeCount; i++)
                //    {
                //        if ((item | (long)Math.Pow(2, i-1)) != item)  
                //        {
                //            if (!tmpNum.Contains((long)Math.Pow(2, i - 1) + item)) 
                //            {
                //                tmpNum.Add((long)Math.Pow(2, i-1) + item);
                //            }
                //        }
                //    }
                //}
                List<long> tmpNum = AllSubsets(s - 1, nodeCount);

                foreach (var S in tmpNum)
                {
                    long power = 2;
                    for (int i = 2; i <= nodeCount; i++)
                    {
                        if ((S | power) == S)
                        {

                            for (int j = 1; j <= nodeCount; j++)
                            {
                                if (i != j)
                                    if ((S | (long)Math.Pow(2, j - 1)) == S)
                                    {
                                        if (d[j - 1, i - 1] != 0 && C[S - power, j - 1] != long.MaxValue)
                                            C[S, i - 1] = Math.Min(C[S, i - 1], C[S - power, j - 1] + d[j - 1, i - 1]);
                                    }
                            }
                        }
                        power *= 2;
                    }
                }
                //Copy(ref lastNum, tmpNum);
            }
            long min = long.MaxValue;
            long n = (long)Math.Pow(2, nodeCount) - 1;
            int index = 0;
            for (int i = 1; i < nodeCount; i++)
                if (d[i, 0] != 0 && C[n, i] != long.MaxValue)
                    if (C[n, i] + d[i, 0] < min)
                    {
                        min = C[n, i] + d[i, 0];
                        index = i;
                    }
            if (min == long.MaxValue)
                return new Tuple<long, long[]>(-1, new long[0]);
            else
            {
                long[] result = new long[nodeCount];
                result[0] = 1;
                result[1] = index + 1;

                long last = 0;
                n -= (long)Math.Pow(2, index);
                last = index;
                for (int j = 2; j < nodeCount; j++)
                {
                    long mm = long.MaxValue;
                    long lastI = 0;
                    for (int i = 0; i < nodeCount; i++)
                    {
                        if (d[i, last] != 0 && C[n, i] != long.MaxValue)
                            if (C[n, i] + d[i, last] < mm /*&& !res.Contains(i + 1)*/)
                            {
                                mm = C[n, i] + d[i, last];
                                lastI = i;
                            }
                    }
                    n -= (long)Math.Pow(2, lastI);
                    result[j] = lastI + 1;
                    last = lastI;
                }
                return new Tuple<long, long[]>(min, result);
            }
        }
        public static List<long> AllSubsets(int length, long nodeCount, long number = 1, long lastNum = 2)
        {
            List<long> container = new List<long>();
            if (length == 0)
            {
                if (!container.Contains(number))
                    container.Add(number);
                return container;
            }
            if (lastNum > nodeCount)
                return container;
            lastNum++;
            container.AddRange(AllSubsets(length, nodeCount, number, lastNum));
            container.AddRange(AllSubsets(length - 1, nodeCount, number + (long)Math.Pow(2, lastNum - 2), lastNum));
            return container;
        }
        public static List<long> Binary(long omid)
        {
            string s = Convert.ToString(omid, 2);
            List<long> res = new List<long>();
            for (int i = 0; i < s.Length; i++)
                if (s[i] == '1')
                    res.Add(s.Length - i);
            return res;
        }

        public static void Copy<T>(ref List<T> lastNum, List<T> tmpNum)
        {
            lastNum.Clear();
            foreach (var item in tmpNum)
                lastNum.Add(item);
        }

        public static void Copy(ref HashSet<long> t, HashSet<long> hashSet)
        {
            foreach (var item in hashSet)
                t.Add(item);
        }

        public static int ConvertToInt(HashSet<long> allChar)
        {
            int sum = 0;
            foreach (var item in allChar)
            {
                sum += Convert.ToInt32(Math.Pow(2, item - 1));
            }
            return sum;
        }

        public static List<long> AllCharWithout(string s, int v)
        {
            List<long> res = new List<long>();
            int i = 0;
            foreach (var item in s)
            {
                if (item == '1' && i != v)
                    res.Add(i);
                i++;
            }
            return res;
        }
    }
}
