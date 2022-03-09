using System;
using System.Collections.Generic;

namespace Lab10b
{
    public static class EnumerableExtender
    {
        /* Uzupełnić */
        public static void ForEach<T>(this IEnumerable<T> enumebrable, Action<T> func)
        {
            foreach(var item in enumebrable)
            {
                func(item);
            }
        }

        public static void Print<T>(this IEnumerable<T> enumerable)
        {
            enumerable.ForEach<T>( item => Console.Write($"{item};") );
            Console.Write("\n");
        }

        public static IEnumerable<T> GenerateN<T>(this IEnumerable<T> enumerable, Func<T> func, int count)
        {
            //int counter = 0;
            //foreach(var item in enumerable)
            //{
            //    counter++;
            //    if (counter == count) break;
            //    yield return func();
            //}
            for(int i = 0; i < count; i++)
            {
                yield return func();
            }
        }

        public static IEnumerable<T> RemoveIf<T>(this IEnumerable<T> enumerable, Predicate<T> predicate)
        {
            foreach(T item in enumerable)
            {
                if (predicate(item) == false)
                {
                    yield return item;
                }
            }
        }

        public static IEnumerable<T1> Transform<T, T1>(this IEnumerable<T> enumerable, Func<T, T1> func)
        {
            foreach(T item in enumerable)
            {
                yield return func(item);
            }
        }

        public static double Accumulate(this IEnumerable<double> enumerable, double initValue)
        {
            double sum = initValue;
            foreach (double num in enumerable)
            {
                sum += num;
            }
            return sum;
        }

        public static T1 Accumulate<T, T1>(this IEnumerable<T> enumerable, T1 initValue, Func<T1, T, T1> func)
        { 
            T1 sum = initValue;
            foreach(T item in enumerable)
            {
                sum = func(sum, item);
            }
            return sum;
        }

        public static T FindEndIfOrDefault<T>(this IEnumerable<T> enumerable, Predicate<T> pred)
        {
            T result = default(T);
            if(pred == null)
            {
                foreach(T item in enumerable)
                {
                    result = item;
                }
                return result;
            }
            List<T> list = new List<T>();
            foreach(T item in enumerable)
            {
                if (pred(item) == true)
                    list.Add(item);
            }
            return list[list.Count-1];
        }

        public static T[] ToArray<T>(this IEnumerable<T> enumerable)
        {
            List<T> list = new List<T>();
            foreach(T item in enumerable)
            {
                list.Add(item);
            }
            T[] result = new T[list.Count];
            for(int i = 0; i < result.Length; i++)
            {
                result[i] = list[i];
            }
            enumerable.GetEnumerator().Reset();
            return result;
        }
        public static IEnumerable<T> Unique<T>(this IEnumerable<T> enumerable, Func<T, T, bool> func) where T: IComparable<T>
        {
            T[] tab = enumerable.ToArray();
            T prev = tab[0];
            yield return prev;
            for(int i = 1; i < tab.Length; i++)
            {
                if (func(prev, tab[i]) == false)
                    yield return tab[i];
                prev = tab[i];
            }
            enumerable.GetEnumerator().Reset();
        }

    }
}
