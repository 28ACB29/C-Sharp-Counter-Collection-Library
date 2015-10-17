using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Counter
{
	public class Counter<T> : IDictionary<T, uint>
	{
		private Dictionary<T, uint> counts;

		#region Constructors
		/// <summary>
		/// 
		/// </summary>
		public Counter()
		{
			this.counts = new Dictionary<T, uint>();
		}

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

		#region Interface Methods
		/// <summary>
		/// 
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		public void Add(T key, uint value)
		{
			this.counts.Add(key, value);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public bool ContainsKey(T key)
		{
			return this.counts.ContainsKey(key);
		}

		/// <summary>
		/// 
		/// </summary>
		ICollection<T> Keys
		{
			get
			{
				return this.counts.Keys;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public bool Remove(T key)
		{
			return this.counts.Remove(key);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public bool TryGetValue(T key, out uint value)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// 
		/// </summary>
		public ICollection<uint> Values
		{
			get
			{
				return this.counts.Values;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public uint this[T key]
		{
			get
			{
				return this.counts[key];
			}
			set
			{
				this.counts[key] = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="item"></param>
		public void Add(KeyValuePair<T, uint> item)
		{
			this.counts.Add(item.Key, item.Value);
		}

		/// <summary>
		/// 
		/// </summary>
		public void Clear()
		{
			this.counts.Clear();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="item"></param>
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
		public int Count
		{
			get
			{
				return this.counts.Count;
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
		/// <param name="item"></param>
		/// <returns></returns>
		public bool Remove(KeyValuePair<T, uint> item)
		{
			return this.counts.Remove(item.Key);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public IEnumerator<KeyValuePair<T, uint>> System.Collections.Generic.IEnumerable<KeyValuePair<T, uint>>.GetEnumerator()
		{
			return this.counts.GetEnumerator();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
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
			List<KeyValuePair<T, uint>> SortedItems;
			List<KeyValuePair<T, uint>> items;
			items = this.counts.ToList();
			if (ascending)
			{
				SortedItems = (from item in items orderby item.Value ascending select item).ToList();
			}
			else
			{
				SortedItems = (from item in items orderby item.Value descending select item).ToList();
			}
			return SortedItems;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public List<T> ToList()
		{
			List<T> elements;
			uint size;
			size = 0;
			foreach (var value in this.counts.Values)
			{
				size += value;
			}
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
