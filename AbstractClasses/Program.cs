using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractClasses
{

    class Program
    {
        static void Main(string[] args)
        {
            //var shape = new Shape(); // error: abstract class cannot be instantiated.
            var circle = new Circle();
            circle.Draw();
            var rectangle = new Rectangle();
            rectangle.Draw();
        }
    }
}
