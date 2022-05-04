using System;
using System.Collections;
using System.Collections.Generic;

namespace SAOD_ListWithLambda
{
	class Program
	{
		static void Main(string[] args)
		{
			void print_lst(SLList<char> list)
			{
				for (int i = 0; i < list.Count; i++)
				{
					Console.Write(list[i] + " -> ");
				}
				Console.WriteLine("Null");
			}

			var lst = new SLList<char>(); // ваш список
			Console.WriteLine(lst.Count + " " + lst.Empty());

            for (int i = 0; i < 5; i++)
                lst.PushBack((char)(i + 97));
            print_lst(lst);

            for (int i = 0; i < 5; i++)
                lst.Insert(0, (char)(122 - i));
            print_lst(lst);

			Console.WriteLine(lst.Contains('a'));
			Console.WriteLine(lst.IndexOf('c'));
        }
	}

	public interface IEnumerator
	{
		bool MoveNext();

		object Current { get; }
		void Reset();
	}

	public interface IEnumerable
	{
		IEnumerator GetEnumerator();
	}

	public class Node<T>
	{
		public T data;
		public Node<T> next;
		public Node()
		{
			data = default(T);
		}
		public Node(T v)
		{
			data = v;
		}
	}

	public class SLList<T> : IEnumerable<T>
	{
		public Node<T> head;
		public Node<T> link;
		public int count;

		public SLList()
		{
			link = new Node<T>(default);
			head = link;
		}

		public System.Collections.IEnumerator GetEnumerator()
		{
			return list.GetEnumerator();
		}

		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			var current = head;
			while (current != null)
			{
				yield return current.data;
				current = current.next;
			}
		}

		public T[] list;

		public T this[int index]
		{
			get
			{
				if (index > count - 1)
				{
					throw new IndexOutOfRangeException();
				}
				if (Empty())
				{
					throw new NullReferenceException();
				}
				var current = head;
				for (int i = 0; i < index; i++)
				{
					current = current.next;
				}
				return current.data;
			}

			set
			{
				if (index > count - 1)
				{
					throw new IndexOutOfRangeException();
				}
				if (Empty())
				{
					throw new NullReferenceException();
				}
				var current = head;
				for (int i = 0; i < index; i++)
				{
					current = current.next;
				}
				current.data = value;
			}
		}

		//Лямбда - выражения 
		public bool Contains(T item)
		{
			var current = head;
			for (int i = 0; i < count; i++)
			{
				if (current.data.Equals(item))
				{
					return true;
				}
				current = current.next;
			}
			return false;
		}

		public int IndexOf(T item)
		{
			var current = head;
			for (int i = 0; i < count; i++)
			{
				if (current.data.Equals(item))
				{
					return i;
				}
				current = current.next;
			}
			return -1;
		}

		public void ForEach(Action<T> action)
		{
			var current = head;
			for (int i = 0; i < count; i++)
			{
				action(current.data);
				current = current.next;
			}
		}

		public T Find(Predicate<T> match)
		{
			var current = head;
			for (int i = 0; i < count; i++)
			{
				if (match(current.data))
				{
					return current.data;
				}
				current = current.next;
			}
			return default;
		}

		public int FindIndex(Predicate<T> match)
		{
			var current = head;
			for (int i = 0; i < count; i++)
			{
				if (match(current.data))
				{
					return i;
				}
				current = current.next;
			}
			return -1;
		}

		public int Count { get => count; }

		public bool Empty() => count == 0;

		public void Clear()
		{
			head = link;
			count = 0;
		}

		public T First()
		{
			if (count > 0)
			{
				return head.data;
			}
			else
			{
				throw new Exception();
			}
		}

		public T Last()
		{
			var current = head;
			if (count > 0)
			{
				while (current.next != null)
				{
					current = current.next;
				}
				return current.data;
			}
			else
			{
				throw new Exception();
			}
		}

		public void PushBack(T v)
		{
			if (count == 0)
			{
				head = new Node<T>(v);
				head.next = link;
			}
			else
			{
				var current = head;
				for (int i = 0; i < count - 2; i++)
				{
					current = current.next;
				}
				current.next = new Node<T>(v);
				current.next.next = link;
			}
			count++;
		}

		public void PopBack()
		{
			if (count != 0)
			{
				var current = head;
				for (int i = 0; i < count - 2; i++)
				{
					current = current.next;
				}
				current.next = link;
				--count;
			}
		}

		public void Insert(int index, T v)
		{
			var current = head;
			var n = new Node<T>(v);

			if (index == 0)
			{
				n.next = current;
				head = n;
				count++;
				return;
			}
			else if (count == 0) throw new IndexOutOfRangeException();

			for (int i = 0; i < index - 1; i++)
			{
				current = current.next;
			}
			n.next = current.next;
			current.next = n;
			count++;
		}

		public void RemoveAt(int index)
		{
			if (index == 0) PopFront();
			else if (index == count - 1) PopBack();
			else
			{
				var current = head;
				for (int i = 0; i < index - 1; i++)
				{
					current = current.next;
				}
				current.next = current.next.next;
				--count;
			}
		}

		public void PushFront(T v)
		{
			++count;
			if (head == null)
			{
				head = new Node<T>(v);
			}
			else
			{
				var n = new Node<T>(v);
				n.next = head;
				head = n;
			}
		}

		public void PopFront()
		{
			if (head != null)
			{
				var current = head;
				head = current.next;
				--count;
			}
		}
	}
}
