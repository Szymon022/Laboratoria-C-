#define STAGE1
#define STAGE2
#define STAGE3
#define STAGE4

using System;
using System.Collections.Generic;
using System.Xml.XPath;


namespace LAB_2021_CS_10A
{
    class Program
    {
        static void Main(string[] args)
        {
            //Stage 1 - 1.5 point
#if STAGE1
            {
                Console.WriteLine("STAGE 1");
                var fib = new List<int>();
                //Uzupełnij
                int fib1 = 1, fib2 = 1, i = 1;
                fib.FillWith(10,() => 
                {
                    if (i == 1 || i == 2)
                    {
                        i++;
                        return fib1;
                    }
                    int result = fib1 + fib2;
                    fib2 = fib1;
                    fib1 = result;
                    return result;
                });
                Console.Write("List: ");
                PrintIEnumerable(fib);

                var ints = new List<int> { 10, -5, 0, 4, 6, -6, 2321, -2452, 342, -67, 2 };
                //Wykorzystaj funkcję lambda
                ints.Sort((num1, num2) =>
                {
                    if (Math.Abs(num1) < Math.Abs(num2))
                        return -1;
                    else
                        return 1;

                });
                Console.Write("Sorted list: ");
                PrintIEnumerable(ints);
                Console.WriteLine("\n\n");
            }
#endif
            //Stage 2 - 1 point
#if STAGE2
            {
                Console.WriteLine("STAGE 2");
                var randomInt = Generators.RandomInteger(5, 14);
                Console.WriteLine("Random integer");
                for (int i = 0; i < 10; i++)
                {
                    Console.Write(randomInt() + ", ");
                }
                Console.WriteLine("\n\n");


                List<double> doubles = new List<double> {0.2, 5.6f, 2, 4, -24, 16.2134124, 22, 82, 8.6, 12.4};
                var projekcja = Generators.ReturnElement(doubles);
                Console.WriteLine("Return element");
                for (int i = 0; i <= doubles.Count; i++)
                {
                    Console.WriteLine((i, projekcja(i)));
                }
                Console.WriteLine("\n\n");
            }
#endif
            //Stage 3 - 1 point
#if STAGE3
            {
                Console.WriteLine("Stage 3");
                //f = x +3
                var f = (Func<double, double>)(x => x + 3);
                //f = x*x
                var g = (Func<double, double>)(x => x * x);
                Console.WriteLine("Combine");
                var combine = FunctionsManipulator.Combine(f, g);
                for (int i = -5; i <= 5; i++)
                {
                    Console.WriteLine($"x: {i}, f(g(x)): {combine(i)}");
                }
                Console.WriteLine("\n\n");

                //f = (x+1)^2
                f = (Func<double, double>)(x => (x + 1) * (x + 1));
                var dfdx = FunctionsManipulator.Derivative(f);
                Console.WriteLine("Derivative");
                for (int i = -5; i <= 5; i++)
                {
                    Console.WriteLine($"x: {i}, f(x): {f(i)}, dfdx: {dfdx(i)}");
                }
                Console.WriteLine("\n\n");
            }
#endif
            //Stage 4 - 1.5 points
#if STAGE4
            {
                Console.WriteLine("STAGE 4");
                Random random = new Random(0);
                Console.WriteLine("Accumulate extension");
                var ints = new List<int> {10, -5, 0, 4, 6, 2321, -2452, 342, -67, 2};
                ;
                PrintIEnumerable(ints);

                //Uzupelnij
                int absSum = 0;
                var sumOfEven = ints.Accumulate(0, (item, sum) =>
                {
                    absSum += Math.Abs(item);
                    if (item % 2 == 0 && item > 0)
                        return sum + item;
                    return sum;
                });
                Console.WriteLine($"Sum of even: {sumOfEven}");
                Console.WriteLine($"Sum of odd: {absSum}");
                Console.WriteLine("\n\n");

                Console.WriteLine("Transform extension");
                var doubles = new List<double>();
                for (int i = 0; i < 10; i++)
                {
                    doubles.Add(random.NextDouble() * 20);
                }

                PrintIEnumerable(doubles);

                //Transform function to fill
                var transformedDoubles = doubles.Transform((num) => 
                {
                    if (num % 1 > 0.5) return num % 1;
                    return num / 2.0;
                });
                PrintIEnumerable(transformedDoubles);
            }
#endif
        }

        private static void PrintIEnumerable<T>(IEnumerable<T> ints)
        {
            foreach (var i in ints)
            {
                Console.Write(i + ", ");
            }

            Console.WriteLine();
        }
    }
}