
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Lab.Utility.Encryption;

namespace Lab.Utility.Csharp
{
	public class Csharp
	{
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

		public static void Regex()
        {
			// Define a regular expression for repeated words.
			var rx = new Regex(@"\b(?<word>\w+)\s+(\k<word>)\b",
			  (RegexOptions.Compiled | RegexOptions.IgnoreCase));

			// Define a test string.
			var text = "The the quick brown fox  fox jumps over the lazy dog dog.";

			// Find matches.
			var matches = rx.Matches(text);

			// Report the number of matches found.
			Console.WriteLine("{0} matches found in:\n   {1}",
							  matches.Count,
							  text);

			// Report on each match.
			foreach (Match match in matches)
			{
				var groups = match.Groups;
				Console.WriteLine("'{0}' repeated at positions {1} and {2}",
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
			foreach(Match match in matches)
            {
				var groups = match.Groups;		
			}
        }

		public static string GenerateRandomString(int length)
		{
			var random = new Random();
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
			return new string(Enumerable.Repeat(chars, length)
			  .Select(s => s[random.Next(s.Length)]).ToArray());
		}

		public static string SqlStmt 
		{ 
			get { 
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
	}
}
