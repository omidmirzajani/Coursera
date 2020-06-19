using System;
using System.Collections.Generic;

namespace ExtendTrieMatching
{
    class Node
    {
        public string sub;                     // a substring of the input string
        public List<int> ch = new List<int>(); // vector of child nodes

        public Node()
        {
            sub = "";
        }

        public Node(string sub, params int[] children)
        {
            this.sub = sub;
            ch.AddRange(children);
        }
    }
    class SuffixTree
    {
        readonly List<Node> nodes = new List<Node>();

        public SuffixTree(string str)
        {
            nodes.Add(new Node());
            for (int i = 0; i < str.Length; i++)
            {
                AddSuffix(str.Substring(i));
            }
        }

        public List<string> Q3()
        {
            List<string> res = new List<string>();

            void f(int n, string last)
            {
                var children = nodes[n].ch;
                if (children.Count == 0)
                {
                    res.Add(nodes[n].sub);
                    return;
                }
                res.Add(nodes[n].sub);

                var it = children.GetEnumerator();
                if (it.MoveNext())
                {
                    do
                    {
                        var cit = it;
                        if (!cit.MoveNext())
                            break;

                        res.Add(last);
                        f(it.Current, last);
                    } while (it.MoveNext());
                }

                res.Add(last);
                f(children[children.Count - 1], last);
            }

            f(0, "");
            return res;
        }

        private void AddSuffix(string suf)
        {
            int n = 0;
            int i = 0;
            while (i < suf.Length)
            {
                char b = suf[i];
                int x2 = 0;
                int n2;
                while (true)
                {
                    var children = nodes[n].ch;
                    if (x2 == children.Count)
                    {
                        n2 = nodes.Count;
                        nodes.Add(new Node(suf.Substring(i)));
                        nodes[n].ch.Add(n2);
                        return;
                    }
                    n2 = children[x2];
                    if (nodes[n2].sub[0] == b)
                        break;
                    x2++;
                }
                var sub2 = nodes[n2].sub;
                int j = 0;
                while (j < sub2.Length)
                {
                    if (suf[i + j] != sub2[j])
                    {
                        var n3 = n2;
                        n2 = nodes.Count;
                        nodes.Add(new Node(sub2.Substring(0, j), n3));
                        nodes[n3].sub = sub2.Substring(j);
                        nodes[n].ch[x2] = n2;
                        break;
                    }
                    j++;
                }
                i += j;
                n = n2;
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string t = Console.ReadLine();
            foreach(var s in Solve(t))
            {
                Console.WriteLine(s);
            }
        }
        public static string[] Solve(string text)
        {
            List<string> s = new SuffixTree(text).Q3();
            return s.ToArray();
        }
    }

}
