using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casting
{
    class Program
    {
        static void Main(string[] args)
        {
            // --------------upcasting---------------------
            /*
                        Text text = new Text();
                        Shape shape = text; // implicitly casting

                        text.Width = 200;
                        shape.Width = 100;

                        Console.WriteLine(text.Width);
            

            // StreamReader reader = new StreamReader(new MemoryStream());

            var list = new ArrayList();  // NOT type safe structure
            list.Add(1);
            list.Add("John");
            list.Add(new Text());


            var anotherList = new List<int>();
            var anotherList2 = new List<Shape>();

*/

            // --------------downcasting---------------------

            Shape shape = new Text();    // F9 -- F5 -- F10
          //  Text text = shape;   // error: cannot implicitly convert type Casting.Shape to Casting.Text
            Text text = (Text)shape;
            var fontname = text.FontName;


        }
    }
}
