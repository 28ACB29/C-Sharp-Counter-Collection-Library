using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Counter
{
	class SortedCounter<T> : Counter<T> where T : IComparable<T>
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="ascending"></param>
		/// <returns></returns>
		public List<T> ToList(bool ascending = true)
		{
			List<T> elements;
			uint size;
			List<KeyValuePair<T, uint>> counts;
			List<KeyValuePair<T, uint>> SortedCounts;
			size = (uint)this.counts.Values.Sum(x => x);
			elements = new List<T>((int)size);
			counts = this.counts.ToList();
			if (ascending)
			{
				SortedCounts = (from count in counts orderby count.Key ascending select count).ToList();
			}
			else
			{
				SortedCounts = (from count in counts orderby count.Key descending select count).ToList();
			}
			foreach (KeyValuePair<T, uint> item in SortedCounts)
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
