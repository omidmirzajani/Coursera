using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Money_Change
{
    class Program
    {
        public static void Main(string[] args)
        {
            long n = Convert.ToInt64(Console.ReadLine());
            Console.WriteLine(Solve(n));
        }
        public static long Solve(long money)
        {
            long count = 0;
            count += money / 10;
            money = money % 10;
            count += money / 5;
            money = money % 5;
            count += money;
            return count;
        }
    }
}
