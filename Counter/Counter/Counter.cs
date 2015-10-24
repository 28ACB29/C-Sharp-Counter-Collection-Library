using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Counter
{
	public class Counter<T> : Dictionary<T, uint>
	{
		protected Dictionary<T, uint> counts;

		#region Constructors
		/// <summary>
		/// 
		/// </summary>
		public Counter()
		{
			this.counts = new Dictionary<T, uint>();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="enumerable"></param>
		public Counter(IEnumerable<T> enumerable)
		{
			this.counts = new Dictionary<T, uint>();
			foreach (T item in enumerable)
			{
				if (this.counts.ContainsKey(item))
				{
					this.counts[item]++;
				}
				else
				{
					this.counts.Add(item, 0);
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="enumerable"></param>
		public Counter(IEnumerable<KeyValuePair<T, uint>> enumerable)
		{
			this.counts = new Dictionary<T, uint>();
			foreach (KeyValuePair<T, uint> item in enumerable)
			{
				this.counts.Add(item.Key, item.Value);
			}
		}
		#endregion

		#region Inherited Methods
		/// <summary>
		/// 
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public override bool Equals(object obj)
		{
			bool result;
			if (obj is Counter<T>)
			{
				result = this == (Counter<T>)obj;
			}
			else
			{
				result = false;
			}
			return result;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode()
		{
			return this.counts.GetHashCode();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return this.counts.ToString();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="count"></param>
		public void Add(KeyValuePair<T, uint> item)
		{
			this.counts.Add(item.Key, item.Value);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="count"></param>
		/// <returns></returns>
		public bool Contains(KeyValuePair<T, uint> item)
		{
			return this.counts.Contains(item);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="array"></param>
		/// <param name="arrayIndex"></param>
		public void CopyTo(KeyValuePair<T, uint>[] array, int arrayIndex)
		{
			int i;
			i = 0;
			foreach (KeyValuePair<T, uint> item in this.counts)
			{
				array[arrayIndex + i] = item;
				i++;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="count"></param>
		/// <returns></returns>
		public bool Remove(KeyValuePair<T, uint> item)
		{
			return this.counts.Remove(item.Key);
		}
		#endregion

		#region Operators
		/// <summary>
		/// 
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static Counter<T> operator &(Counter<T> a, Counter<T> b)
		{
			Counter<T> c = new Counter<T>();
			foreach (KeyValuePair<T, uint> item in a)
			{
				if (b.Contains(item))
				{
					c.Add(item);
				}
			}
			foreach (KeyValuePair<T, uint> item in b)
			{
				if (a.Contains(item))
				{
					c.Add(item);
				}
			}
			return c;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static Counter<T> operator -(Counter<T> a, Counter<T> b)
		{
			Counter<T> c = new Counter<T>();
			foreach (KeyValuePair<T, uint> item in a)
			{
				c.Add(item);
			}
			foreach (KeyValuePair<T, uint> item in b)
			{
				c.Remove(item);
			}
			return c;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static bool operator ==(Counter<T> a, Counter<T> b)
		{
			bool isEqual;
			isEqual = true;
			foreach (KeyValuePair<T, uint> item in a)
			{
				if (b.Contains(item))
				{
					isEqual &= (item.Value == b[item.Key]);
				}
				else
				{
					isEqual = false;
				}
			}
			return isEqual;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static bool operator !=(Counter<T> a, Counter<T> b)
		{
			return !(a == b);
		}

		public static Counter<T> operator +(Counter<T> a, Counter<T> b)
		{
			Counter<T> c = new Counter<T>();
			foreach (KeyValuePair<T, uint> item in a)
			{
				c.Add(item);
			}
			foreach (KeyValuePair<T, uint> item in b)
			{
				c.Add(item);
			}
			return c;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static Counter<T> operator ^(Counter<T> a, Counter<T> b)
		{
			Counter<T> c = new Counter<T>();
			foreach (KeyValuePair<T, uint> item in a)
			{
				if (!b.Contains(item))
				{
					c.Add(item);
				}
			}
			foreach (KeyValuePair<T, uint> item in b)
			{
				if (!a.Contains(item))
				{
					c.Add(item);
				}
			}
			return c;
		}
		#endregion

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ascending"></param>
		/// <returns></returns>
		public List<KeyValuePair<T, uint>> sortByCount(bool ascending = true)
		{
			List<KeyValuePair<T, uint>> SortedCounts;
			List<KeyValuePair<T, uint>> counts;
			counts = this.counts.ToList();
			if (ascending)
			{
				SortedCounts = (from count in counts orderby count.Value ascending select count).ToList();
			}
			else
			{
				SortedCounts = (from count in counts orderby count.Value descending select count).ToList();
			}
			return SortedCounts;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public List<T> ToList()
		{
			List<T> elements;
			uint size;
			size = (uint)this.counts.Values.Sum(x => x);
			elements = new List<T>((int)size);
			foreach (KeyValuePair<T, uint> item in this.counts)
			{
				for (int i = 0; i < item.Value; i++)
				{
					elements.Add(item.Key);
				}
			}
			return elements;
		}
	}
}
