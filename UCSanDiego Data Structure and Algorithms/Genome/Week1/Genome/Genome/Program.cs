using System;
using System.Collections.Generic;
using System.Linq;

namespace Genome
{
    public class MyDict : IComparable<MyDict>
    {
        public string str;
        public int value;
        public MyDict(string s, int v)
        {
            str = s;
            value = v;
        }
        public int CompareTo(MyDict other)
        {
            return other.value.CompareTo(value);
        }
    }
    class Program
    {
        public static MyDict x;
        static void Main(string[] args)
        {
            List<string> input = new List<string>();
            string s="a";
            while (!string.IsNullOrEmpty(s))
            {
                s = Console.ReadLine();
                if(!string.IsNullOrEmpty(s))
                    input.Add(s);
            }

            bool[] visited = new bool[input.Count];
            int current = 0;
            string final = input[0];
            for (int i = 0; i < input.Count; i++)
            {
                visited[current] = true;
                int index = -1;
                int best = 0;
                for (int j = 0; j < input.Count; ++j)
                {
                    if (!visited[j])
                    {
                        int co = calculateOverlap(input[current], input[j]);
                        if (co >= best)
                        {
                            best = co;
                            index = j;
                        }
                    }
                }
                if (index == -1)
                {
                    break;
                }
                final += input[index].Substring(best);
                current = index;
            }
            Console.WriteLine(final.Substring(calculateOverlap(input[current], input[0])));
            //x = new MyDict("",0);
            //List<string> s = new List<string>();
            //string d = "a";
            //while ((d=Console.ReadLine())!=null)
            //{
            //    if (!string.IsNullOrEmpty(d))
            //        s.Add(d);
            //}
            //string final = s[0];
            //bool[] marked = new bool[s.Count];
            //marked[0] = true;

            //int remind = s.Count - 1;
            //while (remind != 0)
            //{
            //    int Max = 0;
            //    string mine = "";
            //    int flag = 0;
            //    int counter = 0;

            //    x.value = 0;
            //    x.str = "";
            //    foreach (var item in s)
            //    {
            //        if (!marked[counter])
            //        {
            //            Moshtarak1(final, item);
            //            if (x.value >= Max)
            //            {
            //                Max = x.value;
            //                mine = x.str;
            //                flag = counter;
            //            }
            //        }
            //        counter++;
            //    }
            //    final = mine;
            //    marked[flag] = true;
            //    remind--;
            //}
            //Moshtarak1(final, s[0]);
            //final = final.Substring(x.value);

            //Console.WriteLine(final);
        }

        private static int calculateOverlap(string v1, string v2)
        {
            for (int i = 0; i < v1.Length; ++i)
            {
                if (v1.Substring(i) == v2.Substring(0, v1.Length - i))
                {
                    return v1.Length - i;
                }
            }
            return 0;
        }

        public static void Moshtarak1(string a, string b)
        {
            for (int length = b.Length; length >= 1; length--)
            {
                if (a.Substring(a.Length - length) == b.Substring(0, length))
                {
                    x.str = a + b.Substring(length);
                    x.value = length;
                    break;
                }
                x.str = a + b;
                x.value = 0;
            }

        }
        public static void Moshtarak2(string a, string b)
        {
            for (int length = b.Length; length >= 0; length--)
            {
                if (a.Substring(0, length) == b.Substring(b.Length - length, length))
                {
                    x.str = b + a.Substring(length);
                    x.value = length;
                    break;
                }
            }
        }
    }

}
