using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace Lab.Utility.DependencyInjection
{
	public class DependencyInjection
	{
		public static void Inject()
		{
			var container = new UnityContainer();
			container.RegisterType<DataAccessLayer>();
			container.RegisterType<BusinessLogic>();
			// RegisterType<From, To>();
			container.RegisterType<IProduct, DataAccessLayer>();

			var dl = container.Resolve<BusinessLogic>();
			dl.Insert();
		}
		public static void InjectToTest()
		{
			var container = new UnityContainer();
			container.RegisterType<TestDataAccessLayer>();
			container.RegisterType<BusinessLogic>();
			// RegisterType<From, To>();
			container.RegisterType<IProduct, TestDataAccessLayer>();

			var dl = container.Resolve<BusinessLogic>();
			dl.Insert();
		}
	}
}
