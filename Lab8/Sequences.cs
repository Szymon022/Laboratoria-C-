using System;
using System.Collections;

namespace Lab8
{
    public class Naturals : IEnumerable
    {
        private int n;
        public Naturals(int n = 0)
        {
            this.n = n;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            for(int i = n; ; i++)
            {
                yield return i;
            }
        }       

    }

    public class RandomNumbers : IEnumerable
    {
        private int max;
        private Random random;
        public RandomNumbers(int seed, int max)
        {
            this.max = max;
            random = new Random(seed);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            while(true)
            {
                yield return random.Next(max);
            }
        }
    }

    public class Tribonacci : IEnumerable
    {
        private int[] DefVals;

        public Tribonacci() 
        {
            DefVals = new int[3];
            DefVals[0] = 0;
            DefVals[1] = DefVals[0];
            DefVals[2] = 1;
        }
        private int t(int n)
        {
            if(n >= 0 && n < 3)
            {
                return DefVals[n];
            }
            int result = 0;
            int t_1 = DefVals[0];
            int t_2 = DefVals[1];
            int t_3 = DefVals[2];

            for(int i = 3; i <= n; i++)
            {
                result = t_3 + t_2 + t_1;
                t_1 = t_2;
                t_2 = t_3;
                t_3 = result;
            }
            return result;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            for(int i = 0; ; i++)
            {
                yield return t(i);

            }
        }
    }
    
    public class Catalan : IEnumerable
    {
        private int t_0;

        public Catalan()
        {
            this.t_0 = 1;
        }

        private int t(int n)
        {
            if(n == 0)
            {
                return t_0;
            }

            int result = 0;
            int t = t_0;

            // c0 = 1, cn+1 = cn * 2 * (2 * n + 1) / (n + 2);

            for(int i = 0; i < n; i++)
            {
                result = t * 2 * (2 * i + 1) / (i + 2);
                t = result;
            }
            return result;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            for(int i = 0; ; i++)
            {
                yield return t(i);
            }
        }
    }

    public class Polynomial : IEnumerable
    {
        int[] coef;
        int n;
        public Polynomial(int[] coef)
        {
            this.coef = new int[coef.Length];
            this.n = coef.Length - 1;
            coef.CopyTo(this.coef, 0);
        }

        private int W(int x)
        {
            int result = 0;
            int tmp = 0;
            for(int i = 0; i <= n; i++)
            {
                if (coef[i] != 0)
                {
                    tmp = coef[i] * pow(x, i);
                    result += tmp;
                }
            }
            return result;
        }

        private int pow(int x, int n)
        {
            int result = 1;
            for(int i = 0; i < n; i++)
            {
                result *= x;
            }
            return result;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            Naturals naturals = new Naturals(0);
            foreach(int x in naturals)
            {
                yield return W(x);
            }
        }
    }

    public class MultiplicationTable : IEnumerable
    {
        private int n;
        public MultiplicationTable(int n = 0)
        {
            this.n = n;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            Naturals naturals = new Naturals(1);
            IModifier firstN = new FirstN(n);
            IModifier linearTransform = new LinearTransform(0, 0);
            Add add = new Add();
            IEnumerable row = firstN.Modify(naturals);
            IEnumerable iRow = linearTransform.Modify(row);


            for(int i = 0; i < n; i++)
            {
                linearTransform = new LinearTransform(i + 1, 0);
                yield return linearTransform.Modify(row);
            }
            
        }
    }
}
