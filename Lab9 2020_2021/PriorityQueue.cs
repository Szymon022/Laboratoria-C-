using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace lab09_a
{
    public interface IPriorityQueue<T>
    {
        void Put(T item);
        T Get();
        int Count { get; }
        T Peek();
    }

    // Tutaj należy umieścić cały kod z laboratorium



    class MinPriorityQueue<T> : IPriorityQueue<T>, IEnumerable<T>, PriorityQueueExtensions<T> where T : notnull, IComparable<T>
    {
        private int count;
        private Node head;
        private Node MinPriorityNode;
        public int Count
        {
            get => count;
        }

        //public T Peek
        //{
        //    get
        //    {
        //        return MinPriorityNode.Value;
        //    }
        //}
        
        private class Node
        {
            public T Value;
            public Node Next;
            public Node(T Value, Node Next)
            {
                this.Value = Value;
                this.Next = Next;               
            }
        }

        public MinPriorityQueue()
        {
            count = 0;
        }

        private void SetMin()
        {
            if (count == 0) return;
            Node it = head;
            MinPriorityNode = head;
            while(it != null)
            {
                if(it.Value.CompareTo(MinPriorityNode.Value) < 0)
                {
                    MinPriorityNode = it;
                }
                it = it.Next;
            }
        }

        public T Get()
        {
            if (count == 0) throw new InvalidOperationException();

            Node result = MinPriorityNode;
            if (MinPriorityNode.Value.CompareTo(head.Value) == 0)
            { 
                head = head.Next;
                count--;        
            }
            else if(MinPriorityNode.Next != null)
            {
                Node it = head;
                
                while (it.Next != null && it.Next.Value.CompareTo(MinPriorityNode.Value) != 0)
                {
                    it = it.Next;
                }
                if (it.Next != null)
                {
                    it.Next = it.Next.Next;
                }
                count--;
            }
            else
            {
                Node it = head;
                while (it.Next.Next != null && it.Next.Next.Value.CompareTo(MinPriorityNode.Value) != 0)
                {
                    it = it.Next;
                }
                count--;
                it.Next = null;

            }
            SetMin();
            return result.Value;
        }

        public T Peek()
        {
            if (count == 0) throw new InvalidOperationException();
            return MinPriorityNode.Value;
        }

        public void Put(T item)
        {
            if (count == 0)
            {
                head = new Node(item, null);
                MinPriorityNode = head;
                count++;
                return;
            }
            Node new_node = new Node(item, head);
            head = new_node;
            count++;
            if (new_node.Value.CompareTo(MinPriorityNode.Value) < 0)
            {
                MinPriorityNode = new_node;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node it = head;
            T val;
            while (it != null)
            {
                val = it.Value;
                it = it.Next;
                yield return val;
            }
        }

        public bool Exist(T value)
        {
            Node it = head;
            while(it != null)
            {
                if (it.Value.CompareTo(value) == 0) return true;
                it = it.Next;
            }
            return false;
        }

        public T MaxItem()
        {
            Node it = head;
            T max = head.Value;
            while(it != null)
            {
                if(it.Value.CompareTo(max) > 0)
                {
                    max = it.Value;
                }
                it = it.Next;
            }
            return max;
        }
    }

    public interface PriorityQueueExtensions<T>
    {
        public bool Exist(T value);
        public T MaxItem();
    }


}
