using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace кр_подготовка_2
{
	using System;
	using System.Collections;
	using System.Collections.Generic;

	public interface INumberSeries
	{
		int GetFirst();
		int GetSecond();
		int GetNext(int current, int previous);
	}

	// ---------------- РЯД ФИБОНАЧЧИ ----------------

	public class FibonacciSeries : INumberSeries
	{
		public int GetFirst() => 0;
		public int GetSecond() => 1;

		public int GetNext(int current, int previous)
		{
			return current + previous;
		}
	}

	// ---------------- РЯД ЭЙЛЕРА ----------------

	public class EulerSeries : INumberSeries
	{
		public int GetFirst() => 1;
		public int GetSecond() => 1;

		public int GetNext(int current, int previous)
		{
			return current + previous + 1;
		}
	}

	// ---------------- ВНЕШНИЙ ИТЕРАТОР ----------------

	public class SeriesIterator : IEnumerable<int>, IEnumerator<int>
	{
		private readonly INumberSeries series;
		private readonly int count;

		private int index = 0;
		private int prev;
		private int curr;

		public SeriesIterator(INumberSeries series, int count)
		{
			this.series = series;
			this.count = count;
			Reset();
		}

		public int Current { get; private set; }
		object IEnumerator.Current => Current;

		public bool MoveNext()
		{
			index++;

			if (index == 1)
			{
				Current = prev;
				return true;
			}
			else if (index == 2)
			{
				Current = curr;
				return true;
			}
			else if (index <= count)
			{
				int next = series.GetNext(curr, prev);
				Current = next;

				prev = curr;
				curr = next;

				return true;
			}

			return false;
		}

		public void Reset()
		{
			index = 0;
			prev = series.GetFirst();
			curr = series.GetSecond();
		}

		public void Dispose() { }

		public IEnumerator<int> GetEnumerator() => this;

		IEnumerator IEnumerable.GetEnumerator() => this;
	}

	// ---------------- КОМПОНОВЩИК (Разность двух рядов) ----------------

	public class DifferenceSeries
	{
		private readonly INumberSeries seriesA;
		private readonly INumberSeries seriesB;

		public DifferenceSeries(INumberSeries a, INumberSeries b)
		{
			seriesA = a;
			seriesB = b;
		}

		public IEnumerable<int> Iterate(int count)
		{
			var iterA = new SeriesIterator(seriesA, count).GetEnumerator();
			var iterB = new SeriesIterator(seriesB, count).GetEnumerator();

			while (iterA.MoveNext() && iterB.MoveNext())
			{
				yield return iterA.Current - iterB.Current;
			}
		}
	}

}
