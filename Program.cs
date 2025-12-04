using System;
using кр_подготовка_2;

class Program
{
	static void Main()
	{
		INumberSeries fib = new FibonacciSeries();
		INumberSeries euler = new EulerSeries();

		int N = 10;

		Console.WriteLine("Первые 10 чисел ряда Фибоначчи:");
		foreach (int x in new SeriesIterator(fib, N))
			Console.Write(x + " ");

		Console.WriteLine("\n\nПервые 10 чисел ряда Эйлера:");
		foreach (int x in new SeriesIterator(euler, N))
			Console.Write(x + " ");

		Console.WriteLine("\n\nРазность рядов Фибоначчи и Эйлера:");
		DifferenceSeries diff = new DifferenceSeries(fib, euler);

		foreach (int x in diff.Iterate(N))
			Console.Write(x + " ");

		Console.WriteLine();
	}
}
