using System;

namespace SecondDiet
{
    class Equation
    {
        public int A;
        public int B;
        public Equation(int a,int b)
        {
            A = a;
            B = b;
        }
    }
    class Position
    {
        public int Column;
        public int Row;
        public Position(int a, int b)
        {
            Column = a;
            Row = b;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
        
    }
}
