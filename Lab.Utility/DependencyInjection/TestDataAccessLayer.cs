using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Utility.DependencyInjection
{
	public class TestDataAccessLayer : IProduct
	{
		public void Insert()
		{
			Console.WriteLine("Insert on Dependency Injection");
		}
	}
}
