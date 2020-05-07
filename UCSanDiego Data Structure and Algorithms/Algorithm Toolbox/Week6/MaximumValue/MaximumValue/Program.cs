using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaximumValue
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = Console.ReadLine();
            Console.WriteLine(Solve(s));
        }
        public static long Solve(string expression)
        {
            string[] Operations = CnvrtToOprtns(expression);
            long[] nums = CnvrtToNum(expression);
            long[,] M = new long[nums.Length, nums.Length];
            long[,] m = new long[nums.Length, nums.Length];
            int n = nums.Length;
            for (int i = 0; i < n; i++)
            {
                M[i, i] = nums[i];
                m[i, i] = nums[i];
            }
            for (int i = 0; i < n - 1; i++)
            {
                if (Operations[i] == "+")
                {
                    M[i, i + 1] = nums[i] + nums[i + 1];
                    m[i, i + 1] = nums[i] + nums[i + 1];
                }
                if (Operations[i] == "-")
                {
                    M[i, i + 1] = nums[i] - nums[i + 1];
                    m[i, i + 1] = nums[i] - nums[i + 1];
                }
                if (Operations[i] == "*")
                {
                    M[i, i + 1] = nums[i] * nums[i + 1];
                    m[i, i + 1] = nums[i] * nums[i + 1];
                }
            }
            for (int i = 0; i < n - 2; i++)
            {
                for (int j = i + 2; j < n; j++)
                {
                    M[j - i - 2, j] = Maximom(M, m, j - i - 2, j, nums, Operations);
                    m[j - i - 2, j] = Minimom(M, m, j - i - 2, j, nums, Operations);
                }
            }
            return M[0, n - 1];
        }

        private static long Minimom(long[,] M, long[,] m, int i, int j, long[] nums, string[] operations)
        {
            long a = 0, b = 0, c = 0, d = 0;
            long minim = long.MaxValue;
            for (int x = i; x < j; x++)
            {
                if (operations[x] == "+")
                {
                    a = m[i, x] + m[x + 1, j];
                    b = m[i, x] + M[x + 1, j];
                    c = M[i, x] + M[x + 1, j];
                    d = M[i, x] + m[x + 1, j];
                }
                if (operations[x] == "-")
                {
                    a = m[i, x] - m[x + 1, j];
                    b = m[i, x] - M[x + 1, j];
                    c = M[i, x] - M[x + 1, j];
                    d = M[i, x] - m[x + 1, j];
                }
                if (operations[x] == "*")
                {
                    a = m[i, x] * m[x + 1, j];
                    b = m[i, x] * M[x + 1, j];
                    c = M[i, x] * M[x + 1, j];
                    d = M[i, x] * m[x + 1, j];
                }
                minim = Math.Min(minim, Math.Min(Math.Min(Math.Min(a, b), c), d));
            }
            return minim;

        }

        private static long Maximom(long[,] M, long[,] m, int i, int j, long[] nums, string[] operations)
        {
            long a = 0, b = 0, c = 0, d = 0;
            long maximom = long.MinValue;
            for (int x = i; x < j; x++)
            {
                if (operations[x] == "+")
                {
                    a = m[i, x] + m[x + 1, j];
                    b = m[i, x] + M[x + 1, j];
                    c = M[i, x] + M[x + 1, j];
                    d = M[i, x] + m[x + 1, j];
                }
                if (operations[x] == "-")
                {
                    a = m[i, x] - m[x + 1, j];
                    b = m[i, x] - M[x + 1, j];
                    c = M[i, x] - M[x + 1, j];
                    d = M[i, x] - m[x + 1, j];
                }
                if (operations[x] == "*")
                {
                    a = m[i, x] * m[x + 1, j];
                    b = m[i, x] * M[x + 1, j];
                    c = M[i, x] * M[x + 1, j];
                    d = M[i, x] * m[x + 1, j];
                }
                maximom = Math.Max(maximom, Math.Max(Math.Max(Math.Max(a, b), c), d));
            }
            return maximom;
        }
        private static long[] CnvrtToNum(string expression)
        {
            List<long> res = new List<long>();
            for (int i = 0; i < expression.Length; i++)
            {
                if (expression[i] != '+' && expression[i] != '*' && expression[i] != '-')
                    res.Add((long)expression[i] - 48);
            }
            return res.ToArray();
        }

        private static string[] CnvrtToOprtns(string expression)
        {
            List<string> res = new List<string>();
            for (int i = 0; i < expression.Length; i++)
            {
                if (expression[i] == '+' || expression[i] == '*' || expression[i] == '-')
                    res.Add(expression[i].ToString());
            }
            return res.ToArray();
        }
    }
}
