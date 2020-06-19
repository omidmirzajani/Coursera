using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSTHard
{
    class Program
    {
        static void Main(string[] args)
        {
            long n = Convert.ToInt64(Console.ReadLine());
            List<long>[] st = new List<long>[n];
            for (int i = 0; i < n; i++)
            {
                st[i] = Console.ReadLine().Split().Select(d => Convert.ToInt64(d)).ToList();
            }
            if (n == 0)
            {
                Console.WriteLine("CORRECT");
            }
            else
                if (Solve(st))
                Console.WriteLine("CORRECT");
            else
                Console.WriteLine("INCORRECT");
        }
        public static bool Solve(List<long>[] nodes)
        {
            var t = InOrder(nodes);
            List<long> s = new List<long>();
            for (int i = 0; i < nodes.Length; i++)
                s.Add(nodes[i][0]);
            s.Sort();
            if (!Eq(s.ToArray(), t))
                return false;
            for (int r = 0; r < nodes.Length; r++)
            {
                if (nodes[r][1] != -1)
                {
                    long[] left = Left(nodes, r);
                    for (int i = 0; i < left.Length; i++)
                        if (Con(left, i + 1, left[i]))
                            return false;
                }
            }
            return true;
        }

        private static bool Con(long[] left, int v1, long v2)
        {
            for (int i = v1; i < left.Length; i++)
                if (left[i] == v2)
                    return true;
            return false;
        }

        private static bool Eq(long[] v, long[] t)
        {
            for (int i = 0; i < v.Length; i++)
                if (v[i] != t[i])
                    return false;
            return true;
        }
        public static long[] InOrder(List<long>[] nodes)
        {
            List<long> res = new List<long>();
            Stack<long> s = new Stack<long>();
            long root = 0;
            while (true)
            {
                while (root != -1)
                {
                    s.Push(root);
                    root = nodes[root][1];
                }
                if (s.Count == 0)
                    return res.ToArray();
                root = s.Pop();
                res.Add(nodes[root][0]);
                root = nodes[root][2];
            }
        }
        public static long[] Left(List<long>[] nodes, long r)
        {
            List<long> res = new List<long>();
            Stack<long> s = new Stack<long>();
            long root = r;
            while (true)
            {
                while (root != -1)
                {
                    s.Push(root);
                    root = nodes[root][1];
                }
                root = s.Pop();
                res.Add(nodes[root][0]);
                if (root == r)
                    return res.ToArray();
                root = nodes[root][2];
            }
        }
    }
}
