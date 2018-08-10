using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicoConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(GetActivationCode());
        }

        private static String GetActivationCode()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            int seed = DateTime.Now.Millisecond;
            var random = new Random(seed);
            return new string(Enumerable.Repeat(chars, 8)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
