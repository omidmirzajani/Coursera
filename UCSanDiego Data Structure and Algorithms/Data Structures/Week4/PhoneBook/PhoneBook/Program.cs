using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook
{
    public class Contact
    {
        public string Name;
        public int Number;

        public Contact(string name, int number)
        {
            Name = name;
            Number = number;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
        }
        public string[] Solve(string[] commands)
        {
            //con.Clear();
            Dictionary<long, string> con = new Dictionary<long, string>();
            List<string> result = new List<string>();
            foreach (var cmd in commands)
            {
                var toks = cmd.Split();
                var cmdType = toks[0];
                var args = toks.Skip(1).ToArray();
                int number = int.Parse(args[0]);
                switch (cmdType)
                {
                    case "add":
                        Add(args[1], number);
                        break;
                    case "del":
                        Delete(number);
                        break;
                    case "find":
                        result.Add(Find(number));
                        break;
                }
            }
            return result.ToArray();
        }

        public void Add(string name, int number)
        {
            int n = number.GetHashCode();

            if (!con.Keys.Contains(n))
                con.Add(n, name);
            else
                con[n] = name;


        }

        public string Find(int number)
        {
            int n = number.GetHashCode();

            if (!con.ContainsKey(n))
                return "not found";
            else
                return con[n];
        }

        public void Delete(int number)
        {
            int n = number.GetHashCode();
            if (con.ContainsKey(n))
                con.Remove(n);
        }
    }
}
