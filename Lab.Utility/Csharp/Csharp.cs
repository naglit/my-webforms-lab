
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lab.Utility.Csharp
{
	public class Csharp
	{
		public static void CreateCsv(){
			//before your loop
			var csv = new StringBuilder();

			//in your loop
			
			//Suggestion made by KyleMit
			var newLine = "rsi";
			csv.AppendLine(newLine);

			var nums = Enumerable.Range(1, 100).Select(i => i.ToString()).ToArray();
			foreach (var num in nums)
			{
				csv.AppendLine(num);
			}

			//after your loop
			var webClient = new WebClient();
			var uri = new Uri(@"\\xxxx\yyyy\zzzz\\rsi.csv");
			webClient.UploadFile(uri, @"C:\inetpub\wwwroot\rsi.csv");
			//File.WriteAllText(@"rsi.csv", csv.ToString());
		}

		public static void DeleteFile()
		{
			File.Delete(@"rsi.csv");
		}

		public static void TryStringBuilder(){
			var sb = new StringBuilder();
		}

		public static void CheckIfTheElementisTheLast()
		{
			var numbers = new[] { 1, 2, 3, 4, 5 };
			foreach(var number in numbers)
			{
				numbers.Last();
			}
			var numberInRange = Enumerable.Range(1, 29).ToArray();
			Console.WriteLine(numbers.Last().ToString());
			Console.ReadKey();
		}

		public static void CheckIfToStringMethodCanConvertDBNullToEmptyString()
		{
			var dbNull = DBNull.Value;
			Console.WriteLine(dbNull.ToString());
			Console.WriteLine(dbNull.ToString().GetType());
			Console.WriteLine("a".GetType());
			Console.WriteLine((dbNull.ToString() == "").ToString());
		}

		public static void SubtractDateTime()
		{
			var date1 = DateTime.Today;
			var date2 = new DateTime(2019, 4, 10);
			var span = date1.Subtract(date2);
			Console.WriteLine(span.Days.ToString());

			Console.WriteLine(date1.ToString());
			Console.WriteLine(date2.AddDays(span.Days).ToString());
			Console.WriteLine(new TimeSpan().Days.ToString());
			Console.ReadKey();
		}

		public static void MakeDirectory(){
			// Specify the directory you want to manipulate.
			var path = @"\Desktop\Glenn\00_Temp\rsi.csv";
			var pattern = @"(?:[a-zA-Z0-9]+\.[a-zA-Z0-9]+)";
			var rx = new Regex(pattern);
			var a = rx.Match(path);
			var dirPath = rx.Replace(path, "");
			try
			{
				// Determine whether the directory exists.
				if (Directory.Exists(dirPath))
				{
					Console.WriteLine("That path exists already.");
					return;
				}

				// Try to create the directory.
				DirectoryInfo di = Directory.CreateDirectory(path);
				Console.WriteLine("The directory was created successfully at {0}.", Directory.GetCreationTime(path));

				// Delete the directory.
				di.Delete();
				Console.WriteLine("The directory was deleted successfully.");
			}
			catch (Exception e)
			{
				Console.WriteLine("The process failed: {0}", e.ToString());
			}
			finally { }
		}

		public static void AAA()
		{
			// Specify the directory you want to manipulate.
			var path = @"~\Desktop\Glenn\00_Temp\rsi.csv";
			var pattern = @"(?:[a-zA-Z0-9]+\.[a-zA-Z0-9]+)";
			var rx = new Regex(pattern);
			var a = rx.Match(path);
		}
	}
}
