using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Lab9A
{
    interface IMyDictionary<TKey, TValue>
    {
        int Count { get; }
        void Add(TKey key, TValue value);
        bool Contains(TKey key);
        bool TryGetValue(TKey key, out TValue value);
        bool Remove(TKey key);
    }

    

    class MyDictionary<TKey, TValue> : IMyDictionary<TKey, TValue>, IEnumerable<(TKey, TValue)> where TKey : notnull, IComparable<TKey> where TValue : IComparable<TValue>
    {
        private class Node
        {
            public TKey Key;
            public TValue Value;
            public Node(TKey k, TValue v)
            {
                Key = k;
                Value = v;
            }
            public override string ToString()
            {
                return $"[{Key}:{Value}]";
            }
        }

        private int count;
        private Node[] tab;
        public int Count
        {
            get => count;
        }

        public MyDictionary()
        {
            count = 0;
            tab = new Node[4];
        }

        public void Add(TKey key, TValue value)
        {
            if(Contains(key))
            {
                for (int i = 0; i < count; i++)
                {
                    if (tab[i].Key.CompareTo(key) == 0)
                    {
                        tab[i].Value = value;
                    }
                }
                return;
            }
            else
            {
                if(count == tab.Length)
                {
                    Node[] newArray = new Node[tab.Length * 2];
                    tab.CopyTo(newArray, 0);
                    tab = newArray;
                }
                tab[count] = new Node(key, value);
                count++;
            }

            
        }

        public bool Contains(TKey key)
        {
            for(int i = 0; i < count; i++)
            {
                if(tab[i].Key.CompareTo(key) == 0)
                {
                    if(i > 0)
                    {
                        Node tmp = tab[i - 1];
                        tab[i - 1] = tab[i];
                        tab[i] = tmp;
                    }
                    return true;
                }
            }
            return false;
        }

        public bool Remove(TKey key)
        {
            for(int i = 0; i < count; i++)
            {
                if(tab[i].Key.CompareTo(key) == 0)
                {
                    for(int j = i; j < tab.Length - 1; j++)
                    {
                        tab[j] = tab[j + 1];
                    }
                    count--;
                    return true;
                }
            }
            return false;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            value = default(TValue);
            if (Contains(key))
            {
                for(int i = 0; i < count; i++)
                {
                    if(tab[i].Key.CompareTo(key) == 0)
                    {
                        value =tab[i].Value;
                    }
                }
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            for(int i = 0; i < count; i++)
            {
                builder.Append(tab[i]);
            }
            return builder.ToString();
        }



        public IEnumerator<(TKey, TValue)> GetEnumerator()
        {
            for(int i = 0; i < count; i++)
            {
                yield return (tab[i].Key, tab[i].Value);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        
    }

    static class MyDictionaryExtensions
    {
        public static TKey[] GetKeys<TKey, TValue>(this MyDictionary<TKey, TValue> dict) where TKey : notnull, IComparable<TKey> where TValue : IComparable<TValue>
        {
            int idx = 0;
            TKey[] keys = new TKey[dict.Count];
            foreach ((TKey, TValue) item in dict)
            {
                keys[idx] = item.Item1;
                idx++;
            }
            return keys;
        }
        public static TValue MaxValue<TKey, TValue>(this MyDictionary<TKey, TValue> dict) where TKey : notnull, IComparable<TKey> where TValue : IComparable<TValue>
        {
            TValue maxVal = default(TValue);
            int counter = 0;
            foreach ((TKey, TValue) item in dict)
            {
                if (counter == 0)
                {
                    maxVal = item.Item2;
                    continue;
                }
                if (item.Item2.CompareTo(maxVal) > 0)
                {
                    maxVal = item.Item2;
                }
            }
            return maxVal;
        }
    }


}
