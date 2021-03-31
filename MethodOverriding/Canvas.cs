using System;
using System.Collections.Generic;

namespace MethodOverriding
{
    public class Canvas
    {
        public void DrawShape(List<Shape> shapes)
        {
            foreach (var shape in shapes)
            {
                shape.Draw(); // polymorphism

             /*   switch (shape.Type)
                {
                    case ShapeType.Circle:
                        Console.WriteLine("Draw a circle.");
                        break;

                    case ShapeType.Rectangle:
                        Console.WriteLine("Draw a rectangle");
                        break;

                    case ShapeType.Triangle:
                        Console.WriteLine("Draw a triangle");
                        break;
                }
             */
            }
        }
    }
}
