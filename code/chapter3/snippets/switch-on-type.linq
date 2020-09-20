<Query Kind="Program">
  <IncludeUncapsulator>false</IncludeUncapsulator>
</Query>

void Main()
{
  cs9samples.CS7TypeSwitch.Run();
}

namespace cs9samples
{
    public interface Shape{    }

    public class Square : Shape { }
    public class Circle : Shape { }
    public class Rectangle : Shape { }
    public class Triangle : Shape { }

    public static class CS7TypeSwitch
    {
        public static bool HasFourSides<T>(this T shape) where T :Shape
        {
            switch (shape)
            {
                case Square: return true;
                case Circle: return false;
                case Rectangle: return true;
                default: throw new ApplicationException("Unknown Shape Type");
            }
        }

        public static void Run()
        {
            Console.WriteLine(new Square().HasFourSides());
        }

    }
}
