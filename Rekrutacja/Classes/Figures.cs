using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rekrutacja.Classes
{
    public class Figures
    {
        abstract public class Shape
        {
            public double[] sides;
            abstract public string InputFormat { get; }

            public abstract double Area();
        }

        public class Rectangle : Shape
        {
            public override string InputFormat
            {
                get { return "Format: Bok 1\\Bok2 (Przykład: 5,2\\2)"; }
            }
            public override double Area()
            {
                return (sides[0] * sides[1]);
            }
        }

        public class Triangle : Shape
        {
            public override string InputFormat
            {
                get { return "Format: Podstawa\\Wysokość (Przykład: 5,2\\2)"; }
            }
            public override double Area()
            {
                return ((sides[0] * sides[1]) / 2);
            }
        }

        public class Trapeze : Shape
        {
            public override string InputFormat {
                get { return "Format: Podstawa 1\\Podstawa 2\\Wysokość (Przykład: 5,0\\2,0\\3)"; } 
            }
            public override double Area()
            {
                return (((sides[0] + sides[1]) /2)* sides[2]);
            }
        }

        public class Parallelogram : Shape
        {
            public override string InputFormat
            {
                get
                {
                    return "Format: Podstawa\\Wysokość (Przykład: 5,2\\2)";
                }
            }
            public override double Area()
            {
                return (sides[0] * sides[1]);
            }
        }

        public class Circle: Shape
        {
            public override string InputFormat
            {
                get
                {
                    return "Format: Promień (Przykład: 5)";
                }
            }
            public override double Area()
            {
                return (Math.PI*(Math.Pow(sides[0], 2)));
            }
        }
    }
}