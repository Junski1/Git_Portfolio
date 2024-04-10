using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FizzBuzz
{
    class FizzBuzz
    {
        static void Main(string[] args)
        {
            string fizz = "Fizz";
            string buzz = "Buzz";
            int i = 1;

            while (i <= 100)
            {
                if (i % 5 == 0 && i % 3 == 0)
                    Console.WriteLine($"{i} {fizz}{buzz}");

                else if (i % 3 == 0)
                    Console.WriteLine($"{i} {fizz}");

                else if (i % 5 == 0)
                    Console.WriteLine($"{i} {buzz}");

                else
                    Console.WriteLine(i);
                i++;
            }
            Console.ReadLine();
        }
    }
}
