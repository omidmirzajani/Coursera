using System;
using System.Linq;

namespace BWT
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Solve(Console.ReadLine()));
        }
        public static string Solve(string text)
        {
            string[] matris = new string[text.Length];
            for (int i = 0; i < text.Length; i++)
            {
                text = Rotate(text);
                matris[i] = text;
            }
            matris = matris.OrderBy(d => d).ToArray();
            string result = "";
            foreach (var t in matris)
            {
                result += t[t.Length - 1];
            }
            return result;
        }
        public static string Rotate(string s)
        {
            return s[s.Length - 1] + s.Substring(0, s.Length - 1);
        }
    }
}
