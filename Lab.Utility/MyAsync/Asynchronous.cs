using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Utility.MyAsync
{
	public class Asynchronous
	{
		public static void Main()
		{
			SomeMethod();
			Console.WriteLine("hey");
		}

		public static async void SomeMethod()
		{
			Console.WriteLine("start");
			await Task.Delay(5000);
			Console.WriteLine("end");
		}
	}
}
