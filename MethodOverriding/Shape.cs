using System;

namespace MethodOverriding
{
    public class Circle : Shape
    {
        public override void Draw()
        {
            // base.Draw();
            Console.WriteLine("Draw a circle.");
        }
    }
    public class Rectangle : Shape
    {
        public override void Draw()
        {
            Console.WriteLine("Draw a rectangle.");
        }
    }
    public class Shape
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public Position Position { get; set; }

        //public ShapeType Type { get; set; }
        // not need ShapeType.cs.  inherited classes created instead

        public virtual void Draw()
        {

        }
    }
}
