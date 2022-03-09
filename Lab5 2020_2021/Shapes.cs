using System;

namespace Lab05_pl
{
    abstract class Shape2D
    {
        protected static int counter = 0;
        protected int ObjectNumber;

        public Shape2D()
        {
            ObjectNumber = ++counter;
            Console.WriteLine($"Shape2D({ObjectNumber}) created");
        }
        ~Shape2D()
        {
            Console.WriteLine($"Shape2D({ObjectNumber}) destroyed");
        }

        abstract public double CalculateArea();
        abstract public string PrintShape2D();
    }

    class Circle : Shape2D
    {
        public double radius;
        public Circle(double radius)
        {
            this.radius = radius;
            Console.WriteLine($"Circle({ObjectNumber}) with radius={radius} created");
        }
        ~Circle()
        {
            Console.WriteLine($"Circle({ObjectNumber}) destroyed");
        }
        public override double CalculateArea()
        {
            return Math.PI * radius * radius;
        }

        public override string PrintShape2D()
        {
            return $"Circle(r={radius})";
        }

    }


    abstract class Shape3D
    {
        protected Shape2D baseShape;
        protected double height;
        public static int NumberOfCreatedObjects = 0;
        

        public Shape3D(Shape2D baseShape, double height)
        {
            this.baseShape = baseShape;
            this.height = height;
            NumberOfCreatedObjects++;
        }

        abstract public double CalculateCapacity();
        public virtual string PrintShape3D()
        {
            return $"Shape3D with base {baseShape.PrintShape2D()} and height: {height}";
        }
        

    }

    class Cone : Shape3D
    {
        public static new int NumberOfCreatedObjects = 0;
       
        public Cone(Shape2D circle, double height) : base(circle, height)
        {
            NumberOfCreatedObjects++;
        }

        public override double CalculateCapacity()
        {
            return 1.0 / 3.0 * baseShape.CalculateArea() * height;
        }

        public override string PrintShape3D()
        {
            return $"Cone(h={height}) with base: {baseShape.PrintShape2D()}";        
        }
    }

    class Cylinder : Shape3D
    {
        public static new int NumberOfCreatedObjects = 0;
        
        public Cylinder(Shape2D circle, double height) : base(circle, height)
        {
            NumberOfCreatedObjects++;
        }

        public override double CalculateCapacity()
        {
            return baseShape.CalculateArea() * height;
        }

        public new string PrintShape3D()
        {
            return $"Cylinder(h={height}) with base: {baseShape.PrintShape2D()}";
            
        }
    }
}
