using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTree
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
            foreach(var t in Solve(st))
            {
                foreach(var tt in t)
                {
                    Console.Write(tt+" ");
                }
                Console.WriteLine();
            }

        }
        public static long[][] Solve(List<long>[] nodes)
        {
            List<long[]> res = new List<long[]>();
            res.Add(InOrder(nodes));
            res.Add(PreOrder(nodes));
            res.Add(PostOrder(nodes));
            return res.ToArray();
        }

        public static long[] PostOrder(List<long>[] nodes)
        {
            Stack<long> s1 = new Stack<long>();
            Stack<long> s2 = new Stack<long>();
            List<long> res = new List<long>();
            long root = 0;
            s1.Push(root);
            while (s1.Count > 0)
            {
                long tmp = s1.Pop();
                s2.Push(tmp);
                if (nodes[tmp][1] != -1)
                    s1.Push(nodes[tmp][1]);
                if (nodes[tmp][2] != -1)
                    s1.Push(nodes[tmp][2]);
            }
            while (s2.Count > 0)
                res.Add(nodes[s2.Pop()][0]);
            return res.ToArray();

        }

        public static long[] PreOrder(List<long>[] nodes)
        {
            List<long> res = new List<long>();
            Stack<long> s = new Stack<long>();
            long root = 0;
            while (true)
            {
                while (root != -1)
                {
                    res.Add(nodes[root][0]);
                    s.Push(root);
                    root = nodes[root][1];

                }
                if (s.Count == 0)
                    return res.ToArray();
                root = s.Pop();
                root = nodes[root][2];
            }
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
    }
}
