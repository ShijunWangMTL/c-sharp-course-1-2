using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppDayThree
{
    class Program
    {
        static void Main(string[] args)
        {
            var person = new Person(new DateTime(1980, 1, 1));
            //person.BirthDate = new DateTime(1980, 1, 1);
            Console.WriteLine(person.Age);
        }
    }
}
