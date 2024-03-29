﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace Lab.Utility.MyCsharp
{
	public class Csharp
	{
		public static void ImportCsv(string filePath)
		{
			using (var reader = new StreamReader(filePath, Encoding.GetEncoding("shift_jis")))
			{
				var listA = new List<string>();
				var listB = new List<string>();
				while (!reader.EndOfStream)
				{
					var line = reader.ReadLine();
					var values = line.Split(',');

					listA.Add(values[0]);
					listB.Add(values[1]);
				}
			}
		}

		public static void SplitArray()
		{
			var ha = Enumerable.Range(0, 100).Select(
				i => new Hashtable
				{
					{ i, i }
				}).ToArray();

			var a = ha.Select(
				(x, i) => new
				{
					Index = i,
					Value = x
				}).GroupBy(x => x.Index / 12).Select(x => x.Select(v => v.Value).ToList()).ToList();
		}

		public static void CreateCsv()
		{
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

		public static void TryStringBuilder()
		{
			var sb = new StringBuilder();
		}

		public static void CheckIfTheElementisTheLast()
		{
			var numbers = new[] { 1, 2, 3, 4, 5 };
			foreach (var number in numbers)
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

		public static void MakeDirectory()
		{
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
			finally
			{
			}
		}

		public static void AAA()
		{
			// Specify the directory you want to manipulate.
			var path = @"~\Desktop\Glenn\00_Temp\rsi.csv";
			var pattern = @"(?:[a-zA-Z0-9]+\.[a-zA-Z0-9]+)";
			var rx = new Regex(pattern);
			var a = rx.Match(path);
		}

		public static void Regex()
		{
			// Define a regular expression for repeated words.
			var rx = new Regex(@"\b(?<word>\w+)\s+(\k<word>)\b", (RegexOptions.Compiled | RegexOptions.IgnoreCase));

			// Define a test string.
			var text = "The the quick brown fox  fox jumps over the lazy dog dog.";

			// Find matches.
			var matches = rx.Matches(text);

			// Report the number of matches found.
			Console.WriteLine("{0} matches found in:\n   {1}", matches.Count, text);

			// Report on each match.
			foreach (Match match in matches)
			{
				var groups = match.Groups;
				Console.WriteLine(
					"'{0}' repeated at positions {1} and {2}",
					groups["word"].Value,
					groups[0].Index,
					groups[1].Index);
			}
		}

		public static void GetCaptureGroup()
		{
			var rx = new Regex(@"(?:!=ENC=!).+(?:!=IV=!)(.+)");
			var match = rx.Match("!=ENC=!adasdas==!=IV=!asdasfv==");

			var group = match.Groups;
			Console.WriteLine(group[1].Value);
		}


		public static void FindTableName()
		{
			var rx = new Regex(@"(?:INSERT|UPDATE)[\s\r\n\t]+([^\s\r\n\t].+_.+[^\s])");
			var matches = rx.Matches(SqlStmt);
			foreach (Match match in matches)
			{
				var groups = match.Groups;
			}
		}

		public string GenerateRandomString(Random random ,int length)
		{
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
			return new string(Enumerable.Repeat(chars, length)
				.Select(s => s[random.Next(s.Length)])
				.ToArray());
		}

		public static string SqlStmt
		{
			get
			{
				return @"INSERT  t_Product
									(
										order_id,
										shop_id,
										product_id,
										variation_id
									)
							VALUES	
									(
										@order_id,
										@shop_id,
										@product_id,
										@variation_id
									)";
			}
		}

		public static void TryAddRange()
		{
			string[] input = { "Brachiosaurus", "Amargasaurus", "Mamenchisaurus" };

			List<string> dinosaurs = new List<string>(input);

			Console.WriteLine("\nCapacity: {0}", dinosaurs.Capacity);

			Console.WriteLine();
			foreach (string dinosaur in dinosaurs)
			{
				Console.WriteLine(dinosaur);
			}

			Console.WriteLine("\nAddRange(dinosaurs)");
			dinosaurs.AddRange(dinosaurs);

			Console.WriteLine();
			foreach (string dinosaur in dinosaurs)
			{
				Console.WriteLine(dinosaur);
			}

			Console.WriteLine("\nRemoveRange(2, 2)");
			dinosaurs.RemoveRange(2, 2);

			Console.WriteLine();
			foreach (string dinosaur in dinosaurs)
			{
				Console.WriteLine(dinosaur);
			}

			input = new string[] { "Tyrannosaurus", "Deinonychus", "Velociraptor" };

			Console.WriteLine("\nInsertRange(3, input)");
			dinosaurs.InsertRange(3, input);

			Console.WriteLine();
			foreach (string dinosaur in dinosaurs)
			{
				Console.WriteLine(dinosaur);
			}

			Console.WriteLine("\noutput = dinosaurs.GetRange(2, 3).ToArray()");
			string[] output = dinosaurs.GetRange(2, 3).ToArray();

			Console.WriteLine();
			foreach (string dinosaur in output)
			{
				Console.WriteLine(dinosaur);
			}
		}

		protected static IEnumerable<string> YeildReturnHeader()
		{
			var a = Enumerable.Range(0, 100);
			yield return "header";

			foreach (var i in a)
			{
				yield return i.ToString();
			}
		}

		public static void ImplicitlyConvertNumberIntoCharSetNo()
		{
			GetDateOfBusinessDay(DateTime.Now, '2', true);
		}

		public static DateTime GetDateOfBusinessDay(DateTime baseDate, int businessDayCount, bool isGetAfterDate)
		{
			Console.WriteLine(businessDayCount);
			return DateTime.Now;
		}

		public static void TestProperty()
		{
			var item = new Item();
		}

		public static void DoubtSkip()
		{
			var list = Enumerable.Range(0, 10);
			foreach (var i in Enumerable.Range(0, 1000))
			{
				var s = new List<string>();
				foreach (var j in list.Skip(1)) s.Add(j.ToString());
				Console.WriteLine(string.Join(" ", s));
			}
		}

		static string Aaa()
		{
			var strs = new[] { "1234", "123", "12" };
			if (strs.Length == 0) return "";

			var prefix = strs[0];
			for (int i = 1; i < strs.Length; i++)
			{
				while (strs[i].IndexOf(prefix) != 0)
				{
					prefix = prefix.Substring(0, prefix.Length - 1);
					if (string.IsNullOrEmpty(prefix)) return "";
				}
			}

			return prefix;
		}

		public static void ParseTest()
		{
			var num = 101 / 100;
			var a = 101;
			var c = (a - num * 100) / 10;
			var aaaa = 0f;
			var d = float.TryParse("1099", out aaaa);
		}

		public static void ExperimentRefType()
		{
			var list = new List<string>()
			{
				"a",
				"b",
				"c",
			};

			var newarray = new string[3];
			list.CopyTo(newarray);
			newarray[0] = "d";
			list.ForEach(Console.WriteLine);
			foreach (var x in newarray) Console.WriteLine(x);
		}


	}

	public class Item
	{
		public Point Location { get; set; }
	}

	public struct Point
	{
		public int X { get; set; }
		public int Y { get; set; }
	}

	/// <summary>
	/// a base class which has a property to be hidden by one of a derived class
	/// </summary>
	public class BaseClass
	{
		public string ConcealedProperty { get; set; } = "base";
	}

	/// <summary>
	/// a derived class which has a property hiding one of the base class
	/// </summary>
	public class DerivedClass : BaseClass
	{
		public string ConcealedProperty { get; set; } = "derived";
	}

	/// <summary>
	/// This is for research for C# Build Alert Caused by Hiding any property.
	/// </summary>
	public class HiddenPropertyCaller
	{
		public void CallHiddenProperty()
		{
			var baseClass = new BaseClass();
			Console.WriteLine(baseClass.ConcealedProperty);

			var derivedClass = new DerivedClass();
			Console.WriteLine(derivedClass.ConcealedProperty);
		}
	}

	public class PointerHandler
	{
		public void Main()
		{
			//unsafe{
			//	fixed (char* p = &list[0].ToCharArray()[0]){
			//		char* p2 = p;
			//		Console.WriteLine(*p);
			//	}
			//}
		}
	}
}
