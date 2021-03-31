using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleToTest
{
    public class Person
    {
        public string name;
        public Person(string name)
        {
            this.name = name;
        }
    }

    public class Program
    {
        
        static void Main(string[] args)
        {
        }

        public List<Person> TestMethod()
        {
            return new List<Person>()
            {
                new Person("A"),
                new Person("B"),
                new Person("C"),
                new Person("D"),
            };
        }

    }
}
