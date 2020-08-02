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
                    matrix[i, j] = nums[j];
            }
            Solve(n, matrix).ToList().ForEach(d => Console.Write(d + " "));
        }
        public static double[] Solve(long MATRIX_SIZE, double[,] matrix)
        {
            bool[] mark = new bool[MATRIX_SIZE];
            long[] order = new long[MATRIX_SIZE];
            for (int i = 0; i < MATRIX_SIZE; ++i)
            {
                for (int j = 0; j < MATRIX_SIZE; ++j)
                {
                    if (!mark[j] && matrix[j, i] != 0)
                    {
                        mark[j] = true;
                        order[i] = j;
                        double temp = matrix[j, i];
                        for (int k = i; k < MATRIX_SIZE + 1; ++k)
                        {
                            matrix[j, k] /= temp;
                        }
                        for (int k = 0; k < MATRIX_SIZE; ++k)
                        {
                            if (k == j) continue;
                            var zarib = matrix[k, i] / matrix[j, i];
                            for (int l = i; l < MATRIX_SIZE + 1; ++l)
                            {
                                matrix[k, l] -= matrix[j, l] * zarib;
                            }
                        }
                        break;
                    }
                }
            }

            return Relax(matrix, order);
        }

        private static double[] Relax(double[,] matrix, long[] order)
        {
            double[] res = new double[matrix.GetLength(0)];
            for (int i = 0; i < matrix.GetLength(0); ++i)
            {
                double t = matrix[order[i], matrix.GetLength(1) - 1];
                double abst = Math.Abs(t);
                if (Math.Abs(t) == 0)
                    res[i] = 0;
                else
                    res[i] = (t / Math.Abs(t)) * abst;
                if (res[i] == -0)
                    res[i] = 0;
            }
            return res;
        }
        //static void Main(string[] args)
        //{
        //    long n = Convert.ToInt64(Console.ReadLine());
        //    double[,] matrix = new double[n, n + 1];
        //    for (int i = 0; i < n; i++)
        //    {
        //        double[] nums = Console.ReadLine().Split().Select(d => Convert.ToDouble(d)).ToArray();
        //        for (int j = 0; j < n + 1; j++)
        //        {
        //            matrix[i, j] = nums[j];
        //        }
        //    }
        //    Solve(n, matrix).ToList().ForEach(d => Console.Write(d + " "));
        //}
        //public static string[] Solve(long MATRIX_SIZE, double[,] matrix)
        //{
        //    for (int i = 0; i < MATRIX_SIZE; i++)
        //    {
        //        if (matrix[i, i] == 0)
        //        {
        //            int idx = 0;
        //            for (int k = i; k < MATRIX_SIZE; k++)
        //            {
        //                if (matrix[k, i] != 0)
        //                {
        //                    idx = k;
        //                    break;
        //                }
        //            }
        //            Swap(matrix, idx, i, MATRIX_SIZE);
        //        }
        //        for (int j = i + 1; j < MATRIX_SIZE; j++)
        //        {
        //            if (matrix[i, i] == 0)
        //            {
        //                return new string[MATRIX_SIZE + 1];
        //            }
        //            double zarib = matrix[j, i] / matrix[i, i];
        //            for (int k = 0; k <= MATRIX_SIZE; k++)
        //                matrix[j, k] -= zarib * matrix[i, k];
        //        }
        //    }

        //    double[] result = new double[MATRIX_SIZE];
        //    for (long i = MATRIX_SIZE - 1; i >= 0; i--)
        //    {
        //        double mul = Multiply(matrix, i, result);
        //        if (matrix[i, i] == 0)
        //        {
        //            return new string[MATRIX_SIZE + 1];
        //        }
        //        result[i] = (matrix[i, MATRIX_SIZE] - mul) / matrix[i, i];
        //    }
        //    string[] res = result.Select(d => (d.ToString().Contains('.') ? Convert.ToDouble(d) + "000" : Convert.ToDouble(d) + ".000")).ToArray();
        //    Relax(res);
        //    return res;
        //}

        //private static void Swap(double[,] matrix, int j, int v, long MATRIX_SIZE)
        //{
        //    for (int i = 0; i < MATRIX_SIZE + 1; i++)
        //    {
        //        var tmp = matrix[j, i];
        //        matrix[j, i] = matrix[i, j];
        //        matrix[i, j] = tmp;
        //    }
        //}

        //private static double Multiply(double[,] matrix, long i, double[] result)
        //{
        //    double d = 0;
        //    for (int j = 0; j < result.Length; j++)
        //    {
        //        d += matrix[i, j] * result[j];
        //    }
        //    return d;
        //}
        //public static void Relax(string[] vs)
        //{
        //    for (int i = 0; i < vs.Length; i++)
        //    {
        //        vs[i] = vs[i].Substring(0, vs[i].IndexOf('.') + 4);
        //    }
        //}
        //public static double[] Solve(long MATRIX_SIZE, double[,] matrix)
        //{
        //    double[] result = new double[MATRIX_SIZE];
        //    for (int i = 0; i < MATRIX_SIZE; ++i)
        //    {
        //        int choosed = i;
        //        for (choosed = i; choosed < MATRIX_SIZE && matrix[choosed, i] == 0; ++choosed) ;
        //        for (int j = 0; j < matrix.GetLength(1); ++j)
        //        {
        //            var tmp = matrix[choosed, j];
        //            matrix[choosed, j] = matrix[i, j];
        //            matrix[i, j] = tmp;
        //        }
        //        double column = matrix[i, i];
        //        for (int j = 0; j < matrix.GetLength(1); ++j)
        //            matrix[i, j] /= column;
        //        for (int row = 0; row < MATRIX_SIZE; ++row)
        //        {
        //            if (i == row)
        //                continue;
        //            double multuplier = (matrix[row, i] / matrix[i, i]);
        //            for (int j = 0; j < matrix.GetLength(1); ++j)
        //                matrix[row, j] -= multuplier * matrix[i, j];
        //        }
        //    }
        //    //for (int i = 0; i < result.Length; ++i)
        //    //{
        //    //    double diet = matrix[i, matrix.GetLength(1) - 1];
        //    //    if (Math.Abs(diet - (int)diet) >= 0.75)
        //    //        diet = (int)diet + Math.Abs(diet) / diet;
        //    //    else if (Math.Abs(diet - (int)diet) <= 0.25)
        //    //        diet = (int)diet;
        //    //    else
        //    //        diet = (int)diet + 0.5 * Math.Abs(diet) / diet;
        //    //    result[i] = Math.Round(diet, 3);
        //    //}
        //    return result;
        //}
    }

}

