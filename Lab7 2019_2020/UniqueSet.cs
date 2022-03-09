using System;

namespace Lab7b
{
    public class UniqueSet
    {
        private int[] tab;
        private int _size;
        public int Size 
        { 
            get
            {
                return tab.Length;
            }
        }

        public UniqueSet()
        {
            _size = 0;
            tab = new int[_size];
        }

        public UniqueSet(int[] tab)
        {
            // liczymy unique
            int uniquesCounter = CountUniques(tab);
            // tworzymy nowa tablice z unique elementami
            if (tab.Length == uniquesCounter)
            {
                int size = tab.Length;
                this.tab = new int[size];
                for (int i = 0; i < size; i++)
                {
                    this.tab[i] = tab[i];
                }
            }
            else
            {
                this.tab = CreateUniqueSetFrom(tab);
            }
        }

        public UniqueSet Clone()
        {
            // kopia głęboka
            return new UniqueSet(this.tab);
        }

        private int CountUniques(int[] tab)
        {
            int uniquesCounter = 0;
            
            for(int i = 0; i < tab.Length; i++)
            {
                bool unique = true;
                for(int j = 0; j < i; j++)
                {
                    if(tab[i] == tab[j])
                    {
                        unique = false;
                        break;
                    }
                }
                if(unique)
                {
                    uniquesCounter++;
                }
            }

            return uniquesCounter;
        }

        private int[] CreateUniqueSetFrom(int[] tab)
        {
            int uniques = CountUniques(tab);
            if (uniques == 0) return new int[0];
            int[] unique_tab = new int[uniques];
            int idx = 0;

            for (int i = 0; i < tab.Length; i++)
            {
                bool itemAlreadyAdded = false;
                for (int j = 0; j < i; j++)
                {
                    if (tab[i] == tab[j])
                    {
                        itemAlreadyAdded = true;
                        break;
                    }
                        
                }
                if(!itemAlreadyAdded)
                {
                    unique_tab[idx] = tab[i];
                    idx++;
                }
            }

            return unique_tab;
        }

        public override string ToString()
        {
            string result = "[";
            result += string.Join(';', tab);
            result += "]";

            return result;
        }

        public void Deconstruct(out UniqueSet evenSet, out UniqueSet oddSet)
        {
            int evenCount = CountEven(this.tab);
            int oddCount = this.tab.Length - evenCount;

            int[] evenNums = new int[evenCount];
            int[] oddNums = new int[oddCount];

            int evenIdx = 0;
            int oddIdx = 0;

            foreach(int item in this.tab)
            {
                if(item % 2 == 0)
                {
                    evenNums[evenIdx] = item;
                    evenIdx++;
                }
                else
                {
                    oddNums[oddIdx] = item;
                    oddIdx++;
                }
            }

            evenSet = new UniqueSet(evenNums);
            oddSet = new UniqueSet(oddNums);
        }

        private int CountEven(int[] tab)
        {
            int n = 0;
            foreach(int item in tab)
            {
                if (item % 2 == 0) n++;
            }
            return n;
        }

        public int this[int i]
        {
            get
            {
                if (i < 0 || i >= this.tab.Length)
                    throw new IndexOutOfRangeException();
                else
                    return this.tab[i];
            }
            set
            {
                if (i < 0 || i >= this.tab.Length)
                    throw new IndexOutOfRangeException();
                else
                    this.tab[i] = value;
            }
        }
    }
}
