using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Node
    {
        public string value;
    }
    class Graph
    {
        public List<Tuple<string,string,long>> adj;
        public List<string> nodes;
        public Graph()
        {
            adj = new List<Tuple<string, string, long>>();
            nodes = new List<string>();
        }
        public  void AddNode(string val)
        {
            foreach (var item in nodes)
            {
                long value = ToAward(val, item);
                adj.Add(new Tuple<string, string, long>(val, item, value));

                value = ToAward(item, val);
                adj.Add(new Tuple<string, string, long>(item, val, value));
            }
            nodes.Add(val);
        }

        private long ToAward(string val, string item)
        {
            for(int i = 0; i < val.Length; i++)
            {
                if (val.Substring(i) == item.Substring(0, val.Length - i))
                {
                    return val.Length - i;
                }
            }
            return 0;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            List<string> s = new List<string>();
            string d = "a";
            Dictionary<string, List<string>> mydict = new Dictionary<string, List<string>>();
            Graph g = new Graph();
            while (!string.IsNullOrEmpty(d))
            {
                string tt = (Console.ReadLine());
                d = tt;
                if (!string.IsNullOrEmpty(d))
                {
                    g.AddNode(tt);
                }
            }
        }
    }
}
