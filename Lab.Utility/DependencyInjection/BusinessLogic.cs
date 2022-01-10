using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Utility.DependencyInjection
{
	public class BusinessLogic
	{
		private readonly IProduct _product;
		public BusinessLogic(IProduct product)
		{
			_product = product;
		}

		public void Insert()
		{
			_product.Insert();
		}
	}
}
