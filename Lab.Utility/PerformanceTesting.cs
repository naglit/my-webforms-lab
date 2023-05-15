using System;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace Lab.Utility
{
	public class PerformanceTesting
	{
		public static void TestPerformance(Action action)
		{
			var watch = Stopwatch.StartNew();
			action.Invoke();
			watch.Stop();

			Console.WriteLine($"Time Taken: {watch.ElapsedMilliseconds} ms.");
			Console.ReadLine();
		}

		public static void TestRegex()
		{
			TestPerformance(
				() =>
				{
					var random = new Random();
					var randomStrings = Enumerable.Range(0, 100)
						.Select(_ => new MyCsharp.Csharp().GenerateRandomString(random, 50)).ToArray();
					foreach (var _ in Enumerable.Range(0, 80))
					{
						var pattern = @"^\w";
						var filePaths = randomStrings.Where(str => Regex.IsMatch(str, pattern)).ToArray();
						foreach (var filePath in filePaths) Console.WriteLine(filePath);
					}
				});
		}
	}
}
