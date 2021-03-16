using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Lab.Utility.Encryption;

namespace Lab.Utility.Multithreading
{
	public class Multithreading
	{
		public static void EncryptWithMultithread()
		{
			var watchForParallel = Stopwatch.StartNew();
			EncryptWithParallelForeach();
			watchForParallel.Stop();

			var watchForTaskWhenAll = Stopwatch.StartNew();
			EncryptWithWhenAll();
			watchForTaskWhenAll.Stop();


			Console.WriteLine($"Parallel.ForEach loop  | Time Taken : {watchForParallel.ElapsedMilliseconds} ms.");
			Console.WriteLine($"Task WhenAll ForEach loop  | Time Taken : {watchForTaskWhenAll.ElapsedMilliseconds} ms.");
			Console.WriteLine("Press any key to exit.");
			Console.ReadLine();
		}

		private static void EncryptWithParallelForeach()
		{
			Parallel.ForEach(Enumerable.Range(0, 2), number =>
			{
				Encryption.Encryption.EncryptInputParams();
			});
		}

		private static void EncryptWithWhenAll()
		{
			var tasks = new List<Task>();
			for (var i = 0; i < 3; i++)
			{
				tasks.Add(Task.Run(() => { Encryption.Encryption.EncryptInputParams(); }));
			}

			var t = Task.WhenAll(tasks);
		}

		public static void ExecutePararellOperations()
		{
			// 2 million
			var limit = 2_000_000;
			var numbers = Enumerable.Range(0, limit).Select(i => i.ToString()).ToList();

			var watch = Stopwatch.StartNew();
			var primeNumbersFromForeach = GetPrimeList(numbers);
			watch.Stop();

			var watchForParallel = Stopwatch.StartNew();
			var primeNumbersFromParallelForeach = GetPrimeListWithParallel(numbers);
			watchForParallel.Stop();

			var watchForTaskWhenAll = Stopwatch.StartNew();
			var primeNumbersFromTaskWhenAll = GetPrimeListWithTasks(numbers);
			watchForTaskWhenAll.Stop();

			Console.WriteLine($"Classical foreach loop | Total prime numbers : {primeNumbersFromForeach.Count} | Time Taken : {watch.ElapsedMilliseconds} ms.");
			Console.WriteLine($"Parallel.ForEach loop  | Total prime numbers : {primeNumbersFromParallelForeach.Count} | Time Taken : {watchForParallel.ElapsedMilliseconds} ms.");
			Console.WriteLine($"Task WhenAll ForEach loop  | Total prime numbers : {primeNumbersFromTaskWhenAll.Count} | Time Taken : {watchForTaskWhenAll.ElapsedMilliseconds} ms.");
			Console.WriteLine("Press any key to exit.");
			Console.ReadLine();
		}

		/// <summary>
		/// GetPrimeList returns Prime numbers by using sequential ForEach
		/// </summary>
		/// <param name="inputs"></param>
		/// <returns></returns>
		private static IList<string> GetPrimeList(IList<string> numbers) =>
			numbers.Select(num => string.Format("{0}", num)).ToList();

		/// <summary>
		/// GetPrimeListWithParallel returns Prime numbers by using Parallel.ForEach
		/// </summary>
		/// <param name="numbers"></param>
		/// <returns></returns>
		private static IList<string> GetPrimeListWithParallel(IList<string> numbers)
		{
			var numbersWithPrefix = new ConcurrentBag<string>();
			Parallel.ForEach(numbers, number =>
			{
				numbersWithPrefix.Add(string.Format("A{0}", number));
			});
			return numbersWithPrefix.ToList();
		}

		private static IList<string> GetPrimeListWithTasks(IList<string> numbers){
			var tasks = new List<Task>();
			var numbersWithPrefix = new ConcurrentBag<string>();
			foreach (var number in numbers){

				tasks.Add(Task.Run(() => { numbersWithPrefix.Add(string.Format("A{0}", number)); }));
			}

			var t = Task.WhenAll(tasks);
			return numbersWithPrefix.ToList();
		}

		public static void ExecuteOnMultithreading()
		{
			var tasks = new List<Action>();
			tasks.Add(TasksOnMultithreading.Test1);
			foreach (var i in Enumerable.Range(0, 100).ToArray())
			{
				var action = (i % 2 == 0)
					? new Action(TasksOnMultithreading.Test1)
					: new Action(TasksOnMultithreading.Test2);
				tasks.Add(action);
			}

			foreach (var task in tasks)
			{
				var th = new Thread(new ThreadStart(task));
				th.Start();
			}
			Console.ReadKey();
		}
	}
	public class TasksOnMultithreading
	{
		public static void Test1()
		{
			for (int i = 0; i < 10; i++)
			{
				Console.WriteLine("Method1 is : {0}", i);
			}
		}

		public static void Test2()
		{
			// It prints numbers from 0 to 10
			for (int j = 0; j < 10; j++)
			{
				Console.WriteLine("Method2 is : {0}", j);
			}
		}
	}
}
