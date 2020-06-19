using System;
using System.Linq;

namespace EnergyValue
{
    public class Program
    {
        static void Main(string[] args)
        {
            long n = Convert.ToInt64(Console.ReadLine());
            double[,] matrix = new double[n, n + 1];
            for (int i = 0; i < n; i++)
            {
                long[] nums = Console.ReadLine().Split().Select(d => Convert.ToInt64(d)).ToArray();
                for (int j = 0; j < n + 1; j++)
                {
                    matrix[i, j] = nums[j];
                }
            }
            Solve(n, matrix).ToList().ForEach(d => Console.Write(d + " "));
        }
        public static double[] Solve(long MATRIX_SIZE, double[,] matrix)
        {
            for (int i = 0; i < MATRIX_SIZE; i++)
            {
                if (matrix[i, i] == 0)
                {
                    int idx = 0;
                    for (int k = i; k < MATRIX_SIZE; k++)
                    {
                        if (matrix[k, i] != 0)
                        {
                            idx = k;
                            break;
                        }
                    }
                    Swap(matrix, idx, i, MATRIX_SIZE);
                }
                for (int j = i + 1; j < MATRIX_SIZE; j++)
                {
                    if (matrix[i, i] == 0)
                    {
                        return new double[MATRIX_SIZE + 1];
                    }
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
                {
                    return new double[MATRIX_SIZE + 1];
                }
                result[i] = (matrix[i, MATRIX_SIZE] - mul) / matrix[i, i];
            }
            Relax(result);
            return result;
        }

        private static void Swap(double[,] matrix, int j, int v, long MATRIX_SIZE)
        {
            for (int i = 0; i < MATRIX_SIZE + 1; i++)
            {
                var tmp = matrix[j, i];
                matrix[j, i] = matrix[i, j];
                matrix[i, j] = tmp;
            }
        }

        private static double Multiply(double[,] matrix, long i, double[] result)
        {
            double d = 0;
            for (int j = 0; j < result.Length; j++)
            {
                d += matrix[i, j] * result[j];
            }
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
