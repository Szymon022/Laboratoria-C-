using System;
using System.Collections;
using System.Collections.Generic;

namespace Lab8
{
    /// <summary>
    /// Interfejs klas modyfikujących sekwencje
    /// </summary>
    public interface IModifier
    {
        /// <summary>
        /// Nazwa modyfikatora
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Metoda modyfikuje kolekcje
        /// </summary>
        /// <param name="sequence">Sekwencja do modyfikacji</param>
        /// <returns>Zmodyfikowana sekwencja</returns>
        IEnumerable Modify(IEnumerable sequence);
    }

    public class FirstN : IModifier
    {
        private int n;
        private string name;
        public FirstN(int n)
        {
            this.n = n;
            name = "First " + n + " numbers";
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public IEnumerable Modify(IEnumerable sequence)
        {
            int idx = 0;
            foreach(int num in sequence)
            {   
                yield return num;
                if (++idx >= n) break;
            }
        }
    }

    public class LinearTransform : IModifier
    {
        private int a;
        private int b;
        private string name;
        public LinearTransform(int a, int b)
        {
            this.a = a;
            this.b = b;
            name = "Linear transform";
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public IEnumerable Modify(IEnumerable sequence)
        {
            foreach(int num in sequence)
            {
                yield return a * num + b;
            }
        }
    }

    public class Unique : IModifier
    {
        private string name;
        public Unique()
        {
            this.name = "Unique Values";
        }

        public string Name
        {
            get
            {
                return name;
            }
        }
        public IEnumerable Modify(IEnumerable sequence)
        {
            int? prev = null;
            foreach(int item in sequence)
            {
                if(prev == null)
                {
                    prev = item;
                    yield return item;
                }
                if(prev != item)
                {
                    prev = item;
                    yield return item;
                }
            }

        }
    }

    public class Prime : IModifier
    {
        private string name;
        public Prime()
        {
            this.name = "Prime numbers from sequence";
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public IEnumerable Modify(IEnumerable sequence)
        {
            foreach(int item in sequence)
            {
                if(IsPrime(item))
                {
                    yield return item;
                }
            }
        }

        private bool IsPrime(int x)
        {
            if (x <= 1) return false;
            if (x == 2) return true;
            for(int divider = 2; divider < x; divider++)
            {
                if(x % divider == 0)
                {
                    return false;
                }
            }
            return true;
        }

        



    }

    public class LocalMax : IModifier
    {
        private string name;

        public LocalMax()
        {
            this.name = "Locally max values";
        }
        
        public string Name
        {
            get
            {
                return name;
            }
        }

        public IEnumerable Modify(IEnumerable sequence)
        {
            int? prev_max = null;
            int? max = null;
            int? prev = null;
            bool ret = true;
            foreach(int item in sequence)
            { 
                if(prev == null)
                {
                    prev = item;
                    max = item;
                    prev_max = max;
                    continue;
                }

                if(item >= prev)
                {
                    prev = item;
                    max = item;
                    prev_max = item;
                    ret = true;
                    continue;
                }

                if(item < prev && ret == true)
                {
                    prev_max = max;
                    max = item;
                    prev = item;
                    ret = false;
                    yield return prev_max;
                }
                if(item < prev && ret == false)
                {
                    prev = item;
                    max = item;
                }
            }
            if(prev == prev_max)
            {
                yield return prev_max;
            }
           
            
        }
    }

    public class ComposedModifier : IModifier
    {
        private IModifier[] modifiers;
        private string name;
        public ComposedModifier(IModifier[] modifiers)
        {
            this.modifiers = (IModifier[])modifiers.Clone();
            this.name = "Composed modifier";
        }
        public string Name
        {
            get
            {
                return name;
            }
        }

        public IEnumerable Modify(IEnumerable sequence)
        {
            IEnumerable resultSequence = sequence;
            foreach(IModifier modifier in modifiers)
            {
                resultSequence = modifier.Modify(resultSequence);
            }
            foreach(int item in resultSequence)
            {
                yield return item;
            }
        }
    }
}
