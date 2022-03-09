using System;
using System.Collections.Generic;
using System.Linq;

namespace LAB_2021_CS_10A
{
    //Stage 1 - 1.5 points
    public static class ListExtender
    {
        public static void FillWith<T>(this List<T> list, int incSize, Func<T> func)
        {
            for(int i = 0; i < incSize; i++)
            {
                list.Add(func());
            }
        }

    }

    //Stage 2 - 1 point
    public static class Generators
    {


        public static Func<int> RandomInteger(int seed, int mod)
        {
            Random rand = new Random(seed);
            return delegate { return rand.Next() % mod; };
        }

        public static Func<int, T> ReturnElement<T>(IEnumerable<T> enumerable)
        {
            return delegate(int i)
            {
                if (i <= 0) return enumerable.First();
                if (i >= enumerable.Count()) return enumerable.Last();
                T[] tab = enumerable.ToArray();
                return tab[i];
            };
        }


    }

    //Stage 3 - 1 point
    public static class FunctionsManipulator
    {
        public static Func<double, double> Combine(Func<double, double> f1, Func<double, double> f2)
        {
            return (x) => f1(f2(x));
        }

        public static Func<double, double> Derivative(Func<double, double> f, double h = 0.001)
        {
            return (x) =>
            {
                return (f(x - h) - f(x + h)) / (2 * h);
            };
        }
    }


    //Stage 4 - 1.5 points
    public static class ExtensionMethods
    {
        public static T Accumulate<T>(this IEnumerable<T> enumerable, T initSum, Func<T, T, T> f)
        {
            T sum = initSum;
            foreach(T item in enumerable)
            {
                sum = f(item, sum);
            }
            return sum;
        }

        public static IEnumerable<T> Transform<T>(this IEnumerable<T> enumerable, Func<T, T> f)
        {
            foreach(T item in enumerable)
            {
                yield return f(item);
            }
        }
    }
}