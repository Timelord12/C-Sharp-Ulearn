using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Проценты
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();

            Console.WriteLine(Calculate(input));
        }

        public static double Calculate(string userInput)
        {
            string[] num = userInput.Split(' ');
            double deposit = double.Parse(num[0]);
            double percent = double.Parse(num[1]);
            double months = double.Parse(num[2]);

            return deposit * Math.Pow(((percent / 12) + 100) / 100, months);
        }
    }
}
