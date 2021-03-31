using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBFirstConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            jacipddatabaseEntities context = new jacipddatabaseEntities();
            context.People.Add(new Person { Name = "Shijun" });
            context.SaveChanges();
            Console.ReadKey();
        }
    }
}
