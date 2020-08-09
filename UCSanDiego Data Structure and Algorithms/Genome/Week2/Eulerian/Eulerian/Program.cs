using System;
using System.Collections.Generic;
using System.Linq;

namespace Eulerian
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] a = Console.ReadLine().Split().Select(d => int.Parse(d)).ToArray();
            int v = a[0];
            int e = a[1];
            List<int>[] adj = new List<int>[v];
            for (int i = 0; i < v; i++)
            {
                adj[i] = new List<int>();
            }
            Dictionary<int, int> inn = new Dictionary<int, int>();
            Dictionary<int, int> outt = new Dictionary<int, int>();
            for (int i = 0; i < v; i++)
            {
                inn[i] = 0;
                outt[i] = 0;
            }
            for (int i = 0; i < e; i++)
            {
                a = Console.ReadLine().Split().Select(d => int.Parse(d)).ToArray();
                adj[a[0] - 1].Add(a[1] - 1);
                inn[a[1] - 1]++;
                outt[a[0] - 1]++;
            }
            bool flag = false;
            foreach (var item in inn.Keys)
            {
                if(inn[item]!=outt[item])
                    flag = true;
            }
            if (flag)
            {
                Console.WriteLine(0);
            }
            else
            {
                Console.WriteLine(1);
                EulerianPrint(adj);
            }
        }

        private static void EulerianPrint(List<int>[] adj)
        {
            Dictionary<int, int> nodeCount = new Dictionary<int, int>();
            for (int i = 0; i < adj.Length; i++)
            {
                nodeCount[i] = adj[i].Count;
            }
            if (adj.Length == 0)
                return;
            List<int> totalPath = new List<int>();
            List<int> circuit = new List<int>();
            totalPath.Add(0);
            int last = 0;
            while (totalPath.Count!=0)
            {
                if (nodeCount[last] != 0)
                {
                    totalPath.Add(last);
                    var next = adj[last].Last();
                    nodeCount[last]--;
                    adj[last].RemoveAt(adj[last].Count-1);
                    last = next;
                }
                else
                {
                    circuit.Add(last);
                    last = totalPath.Last();
                    totalPath.RemoveAt(totalPath.Count - 1);
                }
            }
            for (int i = circuit.Count-1; i > 0; i--)
            {
                Console.Write(circuit[i]+1);
                if (i != 0)
                    Console.Write(" ");
            }
        }
    }
}
