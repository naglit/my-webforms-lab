using System;
using System.Threading.Tasks;

namespace Lab.Utility.MyMultithreading
{
	public class ConcurrencyTutorial
	{
		public static void Main()
		{
			CalledMethod().GetAwaiter().GetResult();
		}

		private static async Task CalledMethod()
		{
			Console.WriteLine("1. CalledMethod has just run.");
			var waitTask = Wait();
			Console.WriteLine("3. Continue the execution of CalledMethod beside Wait().");
			var msg = await waitTask;
			Console.WriteLine($"{msg}");
		}

		private static async Task<string> Wait()
		{
			Console.WriteLine("2. Wait 5 seconds");
			await Task.Delay(5000);
			return "4. Done.";
		}
	}
}

