using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Utility.DependencyInjection
{
	public class DataAccessLayer : IProduct
	{
		private readonly IProduct _product;

		public void Insert()
		{
			Console.WriteLine("Insert");
		}
	}
}
