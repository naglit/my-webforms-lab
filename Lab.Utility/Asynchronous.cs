using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Utility
{
	public class Asynchronous
	{
		public async void TestAsync(){
			await Task.Run(() => new Test().WriteNumbers(1000));

		}

		public class Test{
			public void WriteNumbers(int no)
			{
				foreach (var i in Enumerable.Range(0, no))
				{
					Console.WriteLine(i);
				}
			}
		}
	}
}
