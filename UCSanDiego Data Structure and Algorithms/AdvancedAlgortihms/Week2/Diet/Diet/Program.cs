using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace Diet
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] nm = Console.ReadLine().Split();
            int n = int.Parse(nm[0]);
            int m = int.Parse(nm[1]);
            double[,] matrix1 = new double[n + 1, m + 1];
            for (int i = 0; i < n; ++i)
            {
                string[] tmp = Console.ReadLine().Split();
                for (int j = 0; j < tmp.Length; ++j)
                {
                    matrix1[i, j] = double.Parse(tmp[j]);
                }
            }
            string[] temp = Console.ReadLine().Split();
            for (int i = 0; i < temp.Length; ++i)
            {
                matrix1[i, m] = double.Parse(temp[i]);
            }
            temp = Console.ReadLine().Split();
            for (int i = 0; i < temp.Length; ++i)
            {
                matrix1[n, i] = double.Parse(temp[i]);
            }
            Program p = new Program();
            string s = Solve(n, m, matrix1);
            Console.WriteLine(s);
        }

        public static string Solve(int N, int M, double[,] matrix1)
        {
            double[] values = new double[N];
            for (int i = 0; i < N; i++)
            {
                values[i] = matrix1[i, M];
            }
            var allBinaries = Combine(M + N, M).ToArray();
            double[,] input = new double[M, M + 1];
            int x = 0;
            List<double[]> allResults = new List<double[]>();
            foreach (string binary in allBinaries)
            {
                x = 0;
                for (int i = 0; i < binary.Length; i++)
                    if (binary[i] == '1')
                    {
                        if (i >= N)
                            for (int k = 0; k <= M; k++)
                                if (k == i - N)
                                    input[x, k] = 1;
                                else
                                    input[x, k] = 0;
                        else
                            for (int k = 0; k <= M; k++)
                                input[x, k] = matrix1[i, k];
                        x++;
                    }
                double[] result = Q1Solve(M, input);
                if (result.Length != M + 1)
                    if (Check(result, matrix1, values, M, N))
                        allResults.Add(result);
            }
            if (allResults.Count == 0)
                return "No Solution";
            double max = double.MinValue;
            int idx = 0;
            for (int j = 0; j < allResults.Count; j++)
            {
                double s = 0;
                for (int i = 0; i < M; i++)
                    s += matrix1[N, i] * allResults[j][i];
                if (s > max)
                {
                    max = s;
                    idx = j;
                }
            }
            double[] mine = allResults[idx];
            Relax(mine);
            StringBuilder sb = new StringBuilder();
            sb.Append("Bounded Solution\n");
            sb.Append(string.Join(' ', mine));
            return sb.ToString();
        }

        private static bool Check(double[] result, double[,] matrix1, double[] values, int m, int n)
        {
            foreach (double num in result)
                if (num < 0)
                    return false;
            for (int i = 0; i < n; i++)
            {
                double res = 0;
                for (int j = 0; j < m; j++)
                    res += matrix1[i, j] * result[j];
                if (res - values[i] >= 0.0001)
                    return false;
            }
            return true;
        }


        public static double[] Q1Solve(long MATRIX_SIZE, double[,] matrix)
        {
            for (int i = 0; i < MATRIX_SIZE; i++)
            {
                if (matrix[i, i] == 0)
                {
                    int idx = 0;
                    for (int k = i; k < MATRIX_SIZE; k++)
                        if (matrix[k, i] != 0)
                        {
                            idx = k;
                            break;
                        }
                    Swap(matrix, idx, i, MATRIX_SIZE);
                }
                for (int j = i + 1; j < MATRIX_SIZE; j++)
                {
                    if (matrix[i, i] == 0)
                        return new double[MATRIX_SIZE + 1];
                    double zarib = matrix[j, i] / matrix[i, i];
                    for (int k = 0; k <= MATRIX_SIZE; k++)
                        matrix[j, k] -= zarib * matrix[i, k];
                }
            }

            double[] result = new double[MATRIX_SIZE];
            for (long i = MATRIX_SIZE - 1; i >= 0; i--)
            {
                double mul = Multiply(matrix, i, result);
                if (matrix[i, i] == 0)
                    return new double[MATRIX_SIZE + 1];
                result[i] = (matrix[i, MATRIX_SIZE] - mul) / matrix[i, i];
            }
            return result;
        }

        private static void Swap(double[,] matrix, int j, int v, long MATRIX_SIZE)
        {
            for (int i = 0; i < MATRIX_SIZE + 1; i++)
                (matrix[j, i], matrix[v, i]) = (matrix[v, i], matrix[j, i]);
        }

        private static double Multiply(double[,] matrix, long i, double[] result)
        {
            double d = 0;
            for (int j = 0; j < result.Length; j++)
                d += matrix[i, j] * result[j];
            return d;
        }
        public static void Relax(double[] vs)
        {
            for (int i = 0; i < vs.Length; i++)
            {
                double rest = Math.Abs(vs[i] - (int)vs[i]);
                if (rest < 0.25)
                    vs[i] = (int)(vs[i]);
                else if (rest > 0.75)
                    if (vs[i] >= 0)
                        vs[i] = (int)(vs[i]) + 1;
                    else
                        vs[i] = (int)(vs[i]) - 1;
                else
                    if (vs[i] >= 0)
                    vs[i] = (int)(vs[i]) + 0.5;
                else
                    vs[i] = (int)(vs[i]) - 0.5;
            }
        }
    }
}
