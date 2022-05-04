using System;
using System.Collections;
using System.Collections.Generic;

namespace DoubleLinkList
{
    class Program
    {
        static void Main(string[] args)
        {
            DoubleLinkList<char> linkedList = new DoubleLinkList<char>();

            linkedList.AddLast('a');
            linkedList.AddFirst('b');
            linkedList.AddLast('c');
            linkedList.AddFirst('d');

            linkedList.AddFirst('t');
            linkedList.AddFirst('e');
            linkedList.AddFirst('f');
            linkedList.AddFirst('g');

            print_lst(linkedList);
            linkedList.Insert(4, 'z');
            linkedList.Insert(4, 'y');
            linkedList.Insert(4, 'x');
            print_lst(linkedList);

            Console.WriteLine("First: " + linkedList.First() +
                " |Last: " + linkedList.Last() +
                " |Count: " + linkedList.Size);

            void print_lst(DoubleLinkList<char> list)
            {
                for (int i = 0; i < linkedList.Size; i++)
                {
                    Console.Write(linkedList[i] + " -> ");
                }
                Console.WriteLine("Null");
            }
        }
    }

    public class DoubleNode<T>
    {

        public T Data;
        public DoubleNode<T> Previous;
        public DoubleNode<T> Next;

        public DoubleNode(T data)
        {
            Data = data;
        }

    }

    public class DoubleLinkList<T> : IEnumerable<T>  // двусвязный список
    {
        DoubleNode<T> head; // головной/первый элемент
        DoubleNode<T> tail; // последний/хвостовой элемент
        int count;  // количество элементов в списке

        public int Size { get => count; }
        public bool IsEmpty() => count == 0; 

        public T[] list;

        public T this[int index] 
        { 
            get
            {
                if (index > count - 1)
                {
                    throw new IndexOutOfRangeException();
                }
                if(IsEmpty())
                {
                    throw new NullReferenceException();
                }
                var current = head;
                for (int i = 0; i < index; i++)
                {
                    current = current.Next;
                }
                return current.Data;
            }

            set
            {
                if (index > count - 1)
                {
                    throw new IndexOutOfRangeException();
                }
                if (IsEmpty())
                {
                    throw new NullReferenceException();
                }
                var current = head;
                for (int i = 0; i < index; i++)
                {
                    current = current.Next;
                }
                current.Data = value;
            }
        }


        public void Clear()
        {
            head = tail = null;
            count = 0;
        }

        public T First()
        {
            if (count > 0) return head.Data;
            else throw new Exception();
        }

        public T Last()
        {
            if (count > 0) return tail.Data;
            else throw new Exception();
        }

        public void AddFirst(T data)
        {
            DoubleNode<T> node = new DoubleNode<T>(data);
            DoubleNode<T> temp = head;
            node.Next = temp;
            head = node;
            if (count == 0)
                tail = head;
            else
                temp.Previous = node;
            count++;
        }

        public void RemoveFirst()
        {
            if(head != null)
            {
                DoubleNode<T> current = head;
                head = current.Next;
                --count;
            }
        }

        public void AddLast(T data)
        {
            DoubleNode<T> node = new DoubleNode<T>(data);

            if (head == null)
                head = node;
            else
            {
                tail.Next = node;
                node.Previous = tail;
            }
            tail = node;
            count++;
        }

        public void RemoveLast()
        {
            if(head != null)
            {
                DoubleNode<T> current = tail;
                tail = current.Previous;
                --count;
            }
        }
        
        public void Remove(T data)
        {
            DoubleNode<T> current = head;

            while (current != null)
            {
                if (current.Data.Equals(data)) break;
                current = current.Next;
            }
            if (current != null)
            {
                if (current.Next != null)
                    current.Next.Previous = current.Previous;

                else tail = current.Previous;

                if (current.Previous != null)
                    current.Previous.Next = current.Next;

                else head = current.Next;

                count--;
            }
        }

        public void Insert(int index, T data)
        {
            if (count == 0 || index > count) throw new IndexOutOfRangeException();
            else if (index == 0) AddFirst(data);
            else if (index == (count - 1)) AddLast(data);

            else
            {
                DoubleNode<T> node = new DoubleNode<T>(data);
                DoubleNode<T> current = head;
                for(int i = 0; i < index - 1; i++)
                {
                    current = current.Next;
                }
                node.Next = current.Next;
                current.Next = node;

                current.Next.Previous = node;
                node.Previous = current;
                
                count++;
            }
        }

        public int IdexOf(T data)
        {
            DoubleNode<T> current = head;
            for(int i = 0; i < count; i ++)
            {
                if (current.Data.Equals(data)) return i;
                current = current.Next;
            }
            return -1;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this).GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            DoubleNode<T> current = head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }
    }
}
