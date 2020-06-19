using System;
using System.Linq;
using System.Text;

namespace Gsm
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] a = Console.ReadLine().Split().Select(d => Convert.ToInt32(d)).ToArray();
            int V = a[0];
            int E = a[1];
            long[,] matrix = new long[E, 2];
            for(int i = 0; i < E; i++)
            {
                a = Console.ReadLine().Split().Select(d => Convert.ToInt32(d)).ToArray();
                matrix[i, 0] = a[0];
                matrix[i, 1] = a[1];
            }
            foreach (var item in Solve(V,E,matrix))
            {
                Console.WriteLine(item);
            }
        }
        public static string[] Solve(int V, int E, long[,] matrix)
        {
            string[] res = new string[4 * V + 3 * E + 1];
            res[0] = (3 * V).ToString() + " " + (4 * V + 3 * E).ToString();
            int index = 1;
            StringBuilder s = new StringBuilder();
            for (int i = 0; i < V; i++)
            {
                var a = i * 3 + 1;
                var b = i * 3 + 2;
                var c = i * 3 + 3;
                res[index++] = a.ToString() + " " + b.ToString() + " " + c.ToString() + " 0";
                res[index++] = "-" + a.ToString() + " -" + b.ToString() + " 0";
                res[index++] = "-" + a.ToString() + " -" + c.ToString() + " 0";
                res[index++] = "-" + b.ToString() + " -" + c.ToString() + " 0";
            }
            for (int i = 0; i < E; i++)
            {
                res[index++] = "-" + (matrix[i, 0] * 3 - 2).ToString() + " -" + (matrix[i, 1] * 3 - 2).ToString() + " 0";
                res[index++] = "-" + (matrix[i, 0] * 3 - 1).ToString() + " -" + (matrix[i, 1] * 3 - 1).ToString() + " 0";
                res[index++] = "-" + (matrix[i, 0] * 3).ToString() + " -" + (matrix[i, 1] * 3).ToString() + " 0";
            }
            return res;
        }
    }
}
