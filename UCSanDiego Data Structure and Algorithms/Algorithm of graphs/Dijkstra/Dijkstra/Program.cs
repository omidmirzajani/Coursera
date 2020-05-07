using System;
using System.Collections.Generic;
using System.Linq;
using Priority_Queue;

namespace Dijkstra
{
    public class Graph
    {
        public long V;
        public bool[] visited;
        public List<Tuple<long, long>>[] adj;
        public List<long>[] adjWithoutWeights;
        public List<Tuple<long, long>>[] adjReversed;
        public long[][] edges;
        long[,] weights;
        public Graph(long v, long[][] ed)
        {

            V = v;
            edges = ed;
            adj = new List<Tuple<long, long>>[v];
            adjReversed = new List<Tuple<long, long>>[v];
            adjWithoutWeights = new List<long>[v];
            for (int i = 0; i < v; ++i)
            {
                adj[i] = new List<Tuple<long, long>>();
                adjReversed[i] = new List<Tuple<long, long>>();
                adjWithoutWeights[i] = new List<long>();
            }
            visited = new bool[v];

            weights = new long[V, V];
            for (int i = 0; i < V; i++)
                for (int j = 0; j < V; j++)
                    weights[i, j] = long.MaxValue;
        }
        public void addEdge(long start, long end, long weight)
        {
            adj[start].Add(new Tuple<long, long>(end, weight));
            adjReversed[end].Add(new Tuple<long, long>(start, weight));
            weights[start, end] = weight;
            adjWithoutWeights[start].Add(end);
        }

        public long Dijkstra(long startNode, long endNode)
        {
            SimplePriorityQueue<long, long> H = new SimplePriorityQueue<long, long>();
            long[] dist = new long[V];
            for (int i = 0; i < V; i++)
                dist[i] = long.MaxValue;
            dist[startNode] = 0;
            for (int i = 0; i < V; i++)
                H.Enqueue(i, dist[i]);
            while (H.Count > 0)
            {
                var u = H.Dequeue();
                foreach (var edge in adj[u])
                {
                    if (dist[u] != long.MaxValue)
                        if (dist[edge.Item1] > dist[u] + edge.Item2)
                        {
                            dist[edge.Item1] = dist[u] + edge.Item2;
                            H.UpdatePriority(edge.Item1, dist[edge.Item1]);
                        }
                }
            }
            if (dist[endNode] != long.MaxValue)
                return dist[endNode];
            return -1;
        }

        public long BiDirectional(long startNode, long endNode)
        {
            if (startNode == endNode)
                return 0;

            long[] weightsStart = new long[V];
            long[] weightsEnd = new long[V];

            for (int i = 0; i < V; i++)
            {
                weightsStart[i] = long.MaxValue;
                weightsEnd[i] = long.MaxValue;
            }

            weightsStart[startNode] = 0;
            weightsEnd[endNode] = 0;

            List<long> proccessedStart = new List<long>();
            List<long> proccessedEnd = new List<long>();
            proccessedStart.Add(startNode);
            proccessedEnd.Add(endNode);



            for (int iterate = 0; iterate < V; iterate++)
            {
                List<Tuple<long, long>> toProccessStart = new List<Tuple<long, long>>();
                List<Tuple<long, long>> toProccessEnd = new List<Tuple<long, long>>();

                List<long> ToWeightStart = new List<long>();
                List<long> ToWeightEnd = new List<long>();

                long minStart = long.MaxValue;
                long indStart = long.MaxValue;

                long minEnd = long.MaxValue;
                long indEnd = long.MaxValue;

                //Proccessing from start
                foreach (long e in proccessedStart)
                {
                    foreach (Tuple<long, long> node in adj[e])
                    {
                        if (!proccessedStart.Contains(node.Item1))
                        {
                            if (weightsStart[e] + node.Item2 < minStart)
                            {
                                minStart = weightsStart[e] + node.Item2;
                                indStart = ToWeightStart.Count;
                            }
                            ToWeightStart.Add(weightsStart[e] + node.Item2);
                            toProccessStart.Add(node);
                        }
                    }
                }
                //Proccessing from end
                foreach (long e in proccessedEnd)
                {
                    foreach (Tuple<long, long> node in adjReversed[e])
                    {
                        if (!proccessedEnd.Contains(node.Item1))
                        {
                            if (weightsEnd[e] + node.Item2 < minEnd)
                            {
                                minEnd = weightsEnd[e] + node.Item2;
                                indEnd = ToWeightEnd.Count;
                            }
                            ToWeightEnd.Add(weightsEnd[e] + node.Item2);
                            toProccessEnd.Add(node);
                        }
                    }
                }

                long nodeS = -1;
                long nodeE = -1;

                if (toProccessStart.Count > 0)
                {
                    long min = minStart;
                    long index = indStart;
                    Tuple<long, long> my = toProccessStart[(int)index];
                    proccessedStart.Add(my.Item1);
                    nodeS = my.Item1;
                    weightsStart[my.Item1] = Math.Min(min, weightsStart[my.Item1]);
                }

                if (toProccessEnd.Count > 0)
                {
                    long min = minEnd;
                    long index = indEnd;
                    Tuple<long, long> my = toProccessEnd[(int)index];
                    proccessedEnd.Add(my.Item1);
                    nodeE = my.Item1;
                    weightsEnd[my.Item1] = Math.Min(min, weightsEnd[my.Item1]);
                }

                if (proccessedStart.Contains(nodeE) || proccessedEnd.Contains(nodeS))
                {
                    long best = long.MaxValue;

                    //foreach(long[] ss in edges)
                    //{
                    //    if(proccessedStart.Contains(ss[0]-1) && proccessedEnd.Contains(ss[1]-1))
                    //        best = Math.Min(best, weightsStart[ss[0]-1] + weightsEnd[ss[1]-1] + ss[2]);
                    //}
                    foreach (long pS in proccessedStart)
                        foreach (long pE in proccessedEnd)
                            if (weights[pS, pE] != long.MaxValue)
                                best = Math.Min(best, weightsStart[pS] + weightsEnd[pE] + weights[pS, pE]);
                    return best;

                }
            }
            return -1;

        }

        public string[] Path(long startNode)
        {
            string[] result = new string[V];
            long[] weight = new long[V];
            for (int i = 0; i < V; i++)
                weight[i] = long.MaxValue;
            weight[startNode] = 0;
            for (int i = 0; i <= V * 2; i++)
                foreach (long[] edge in edges)
                {
                    long start = edge[0] - 1;
                    long end = edge[1] - 1;
                    long w = edge[2];

                    if (weight[start] != long.MaxValue && weight[start] + w < weight[end])
                    {
                        weight[end] = weight[start] + w;
                        if (i >= V - 1)
                            result[end] = "-";
                    }
                }
            for (int i = 0; i < V; i++)
            {
                if (result[i] != "-")
                {
                    if (weight[i] == long.MaxValue)
                        result[i] = "*";
                    else
                        result[i] = weight[i].ToString();
                }

            }
            return result;
        }

        public long Cycle()
        {
            long[] weight = new long[V];
            for (int i = 0; i < V; i++)
                weight[i] = long.MaxValue;
            for (int k = 0; k < V; k++)
            {
                if (weight[k] == long.MaxValue)
                {
                    weight[k] = 0;
                    for (int i = 0; i < V; i++)
                        foreach (long[] edge in edges)
                        {
                            long start = edge[0] - 1;
                            long end = edge[1] - 1;
                            long w = edge[2];

                            if (weight[start] != long.MaxValue && weight[start] + w < weight[end])
                            {
                                weight[end] = weight[start] + w;
                                if (i == V - 1)
                                    return 1;
                            }
                        }
                }
            }
            return 0;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            long[] t = Console.ReadLine().Split().Select(d => Convert.ToInt64(d)).ToArray();
            long v = t[0];
            long e = t[1];
            long[][] edges = new long[e][];
            for (int i = 0; i < e; i++)
            {
                edges[i] = Console.ReadLine().Split().Select(d => Convert.ToInt64(d)).ToArray();
            }
            t = Console.ReadLine().Split().Select(d => Convert.ToInt64(d)).ToArray();
            Console.WriteLine(Solve(v,edges,t[0],t[1]));
        }
        public static long Solve(long nodeCount, long[][] edges, long startNode, long endNode)
        {
            Graph g = new Graph(nodeCount, edges);
            for (long i = 0; i < edges.Length; i++)
            {
                g.addEdge(edges[i][0] - 1, edges[i][1] - 1, edges[i][2]);
            }
            return g.Dijkstra(startNode - 1, endNode - 1);
        }
    }
}
