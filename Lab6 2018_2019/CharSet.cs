using System;
using System.Collections.Generic;
using System.Text;

namespace p3z
{
    class CharSet
    {
        private Dictionary<char, int> _items;
        public Dictionary<char, int> Items
        {
            get { return _items; }

            private set
            {
                // deep copy of dictionary
                foreach (KeyValuePair<char, int> item in value)
                {
                    char key = item.Key;
                    int val = item.Value;
                    this._items.Add(key, val);
                }
            }
        }
        public CharSet(params (char, int)[] paramItems)
        {
            _items = new Dictionary<char, int>();
            foreach ((char, int) item in paramItems)
            {
                char key = item.Item1;
                if (!this._items.ContainsKey(key))
                {
                    int val = item.Item2;
                    _items.Add(key, val);
                }
            }
        }

        public override string ToString()
        {
            string resString = "{";
            foreach (KeyValuePair<char, int> item in _items)
            {
                char key = item.Key;
                int val = item.Value;
                resString += $"({key}:{val}),";
            }
            resString += "}";
            return resString;
        }

        public static CharSet operator +(CharSet charset, char c)
        {
            CharSet resCharset = new CharSet();
            resCharset.Items = charset.Items;

            if (!resCharset.Items.ContainsKey(c))
                resCharset.Items.Add(c, 1);
            else
                resCharset.Items[c]++;
            return resCharset;
        }
        public static CharSet operator +(CharSet charset1, CharSet charset2)
        {
            CharSet resCharSet = new CharSet();
            resCharSet.Items = charset1.Items;

            foreach (KeyValuePair<char, int> item in charset2.Items)
            {
                char key = item.Key;
                int val = item.Value;
                if (resCharSet.Items.ContainsKey(key))
                    resCharSet.Items[key] += val;
                else
                    resCharSet.Items.Add(key, val);
            }

            return resCharSet;
        }

        public static CharSet operator *(CharSet charset1, CharSet charset2)
        {
            CharSet resCharSet = new CharSet();
            CharSet tmpCharSet = new CharSet();
            tmpCharSet = charset1 + charset2;

            foreach (KeyValuePair<char, int> item in tmpCharSet.Items)
            {
                char key = item.Key;
                if (charset1.Items.ContainsKey(key) && charset2.Items.ContainsKey(key))
                {

                    if (charset1.Items[key] <= charset2.Items[key])
                    {
                        int val = charset1.Items[key];
                        resCharSet.Items.Add(key, val);
                    }
                    else
                    {
                        int val = charset2.Items[key];
                        resCharSet.Items.Add(key, val);
                    }
                }
            }

            return resCharSet;
        }

        public static CharSet operator ++(CharSet charset)
        {
            CharSet resCharSet = new CharSet();

            foreach (KeyValuePair<char, int> item in charset.Items)
            {
                char key = item.Key;
                int val = item.Value + 1;
                resCharSet.Items.Add(key, val);
            }

            return resCharSet;
        }

        public static bool operator ==(CharSet charset1, CharSet charset2)
        {
            if (charset1.Items.Count != charset2.Items.Count) return false;

            foreach (KeyValuePair<char, int> item in charset1.Items)
            {
                char key = item.Key;
                int val = item.Value;
                if (!charset2.Items.ContainsKey(key)) return false;
                if (charset2.Items[key] != val) return false;
            }

            return true;
        }

        public static bool operator !=(CharSet charset1, CharSet charset2)
        {
            return !(charset1 == charset2);
        }

        public static bool operator <(CharSet charset1, CharSet charset2)
        {
            if (charset1.Items.Count > charset2.Items.Count) return false;
            // zawieranie się w dwie strony, ale nie wiadomo czy o to chodzi
            // if (charset1 == charset2) return true;
            foreach (KeyValuePair<char, int> item in charset1.Items)
            {
                char key = item.Key;
                int val = item.Value;
                if (!charset2.Items.ContainsKey(key)) return false;
                else if (charset2.Items[key] <= val) return false;
            }
            return true;
        }

        public static bool operator >(CharSet charset1, CharSet charset2)
        {
            //if (charset1.Items.Count < charset2.Items.Count) return false;
            //// zawieranie się w dwie strony, ale nie wiadomo czy o to chodzi
            //// if (charset1 == charset2) return true;
            //foreach (KeyValuePair<char, int> item in charset2.Items)
            //{
            //    char key = item.Key;
            //    int val = item.Value;
            //    if (!charset1.Items.ContainsKey(key)) return false;
            //    else if (charset1.Items[key] <= val) return false;
            //}
            //return true;
            return charset2 < charset1;
        }

        public int this[char key]
        {
            get
            {
                if (_items.ContainsKey(key)) return _items[key];
                return 0;
            }
            set
            {
                if(_items.ContainsKey(key)) 
                    _items[key] = value;
                else 
                    _items.Add(key, value);
            }
        }

        public static implicit operator CharSet(string str)
        {
            CharSet resCharSet = new CharSet();

            foreach(char c in str)
            {
                resCharSet += c;
            }

            return resCharSet;
        }

        public static explicit operator CharSet(char c)
        {
            CharSet resCharSet = new CharSet();
            return resCharSet += c;
        }

        //public static implicit operator CharSet(string str)
        //{
        //    CharSet resCharSet = new CharSet();
        //    resCharSet = (CharSet)str;
        //    return resCharSet;
        //}
    }
}
