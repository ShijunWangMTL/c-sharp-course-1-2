using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxing
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = new ArrayList();
            list.Add(1);
            list.Add("John");
            list.Add(DateTime.Today);

            var number = (int)list[1]; // exception. list[1] is a string
            
            var anotherList = new List<int>();
            anotherList.Add((int)list[0]);

            var name = new List<string>();
            name.Add((string)list[1]);


        }
    }
}
