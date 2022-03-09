using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab07A
{
    class MyLinkedList
    {
        // dane
        public class Node
        {
            public int Value { get; set; }
            public Node Next { get; set; }


            public Node(int value, Node next=null)
            {
                Value = value;
                Next = next;
            }
        }

        private Node head;
        private int count;

        public Node Head 
        { 
            get
            {
                return this.head;
            }
        }
        public int Count
        {
            get
            {
                return this.count;
            }
        }
        public bool IsEmpty
        {
            get
            {
                return count == 0;
            }
        }

        public MyLinkedList()
        {
            head = null;
            count = 0;
        }

        public void PushFront(int val)
        {
            this.head = new Node(val, this.head); ;
            count++;
        }

        public MyLinkedList(int[] tab)
        {
            int[] tmp = new int[tab.Length];
            for(int i = 0; i < tab.Length; i++)
            {
                tmp[i] = tab[i];
            }
            Array.Reverse(tmp);

            foreach(int item in tmp)
            {
                PushFront(item);
            }
        }

        public static explicit operator int[](MyLinkedList list)
        {
            int size = list.count;
            int[] tab = new int[size];
            Node tmp = list.head;

            for(int i = 0; i < size; i++)
            {
                tab[i] = tmp.Value;
                tmp = tmp.Next;
            }

            return tab;
        }

        public static implicit operator MyLinkedList(int[] tab)
        {
            return new MyLinkedList(tab);
        }

        public MyLinkedList Clone()
        {
            return ((int[])this);
        }

        public override string ToString()
        {
            int[] tab = (int[])this;
            string result = "[";
            result += string.Join(';', tab);
            result += "]";

            return result;
        }

        public static MyLinkedList operator +(MyLinkedList listL, MyLinkedList listR)
        {
            MyLinkedList resList = listR.Clone();
            int[] tabL = (int[])listL;
            Array.Reverse(tabL);

            foreach(int item in tabL)
            {
                resList.PushFront(item);
            }

            return resList;
        }

        public static MyLinkedList operator -(MyLinkedList listL, MyLinkedList listR)
        {
            // A - B na zbiorach
            Node tmp = listL.Head;
            MyLinkedList result = new MyLinkedList();
            for (int i = 0; i < listL.count; i++)
            {
                if(!listR.DoesContain(tmp.Value))
                {
                    result.PushFront(tmp.Value);
                }
                tmp = tmp.Next;
            }
            return result;
        }

        private bool DoesContain(int val)
        {
            Node tmp = this.head;
            for(int i = 0; i < this.count; i++)
            {
                if (tmp.Value == val) return true;
                tmp = tmp.Next;
            }
            return false;
        }

        public static bool operator ==(MyLinkedList listL, MyLinkedList listR)
        {
            if (listL.Count != listR.Count) return false;

            Node tmpL = listL.Head;
            Node tmpR = listR.Head;

            for(int i = 0; i < listL.Count; i++)
            {
                if (tmpL.Value != tmpR.Value) return false;
                tmpL = tmpL.Next;
                tmpR = tmpR.Next;
            }

            return true;
        }

        public static bool operator !=(MyLinkedList listL, MyLinkedList listR)
        {
            return !(listL == listR);
        }

        public override bool Equals(Object obj)
        { 
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                MyLinkedList tmp = (MyLinkedList) obj;
                return (this == tmp);
            }
        }

        public override int GetHashCode()
        {
            int hashCode = count << 2;
            Node tmp = this.head;
            for(int i = 0; i < count; i++)
            {
                hashCode ^= tmp.Value;
            }
            return hashCode;
        }

        public void Deconstruct(out MyLinkedList listEven, out MyLinkedList listOdd)
        {
            int[] tab = (int[])this;
            int evenCount = CountEven();
            int oddCount = count - evenCount;

            int[] evenTab = new int[evenCount];
            int[] oddTab = new int[oddCount];

            int evenIdx = 0;
            int oddIdx = 0;
 
            foreach(int item in tab)
            {
                if(item % 2 == 0)
                {
                    evenTab[evenIdx++] = item;
                }
                else
                {
                    oddTab[oddIdx++] = item;
                }
            }

            Array.Reverse(evenTab);
            Array.Reverse(oddTab);

            listEven = new MyLinkedList(evenTab);
            listOdd = new MyLinkedList(oddTab);

        }

        private int CountEven()
        {
            int count = 0;
            int[] tab = (int[])this;
            foreach(int item in tab)
            {
                if (item % 2 == 0)
                    count++;
            }
            return count;
        }

        public int this[int i]
        {
            get
            {
                int[] tab = (int[])this;
                if (i >= 0 && i < tab.Length)
                    return tab[i];
                else
                    throw new IndexOutOfRangeException();
            }
            set
            {
                if (i < 0 || i >= count)
                    throw new IndexOutOfRangeException();
                else
                {
                    Node tmp = head;
                    for(int j = 0; j < i; j++)
                    {
                        tmp = tmp.Next;
                    }
                    tmp.Value = value;
                   
                }
                    
            }
        }
    }
}
