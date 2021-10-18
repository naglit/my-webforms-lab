using Lab.Utility.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Utility.Reader
{
	class TestCsvReader
	{
		public void TestToRead()
		{
			var a = new CsvReader<SampleReadDataModel>(@".\test.csv");
			var b = a.Read();

			foreach (var c in b)
			{
				Console.WriteLine(c.Name);
			}
			foreach (var e in b)
			{
				Console.WriteLine(e.Id);
			}
		}
	}
}
