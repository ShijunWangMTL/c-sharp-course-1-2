using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethodOverriding
{
    class Program
    {
        static void Main(string[] args)
        {
            var shapes = new List<Shape>();
            //var circle = new Circle();
            //var rectangle = new Rectangle();
            shapes.Add(new Circle());
            //shapes.Add(circle);
            shapes.Add(new Rectangle());
            //shapes.Add(rectangle);

            var canvas = new Canvas();
            canvas.DrawShape(shapes);
            // result: Draw a circle. Draw a rectangle.
        }
    }
}
