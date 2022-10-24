using System;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace Lab.Utility.CustomAttributes
{
	public class CustomAttributeTesting
	{
		public static void Main()
		{
			var prop = typeof(DtoClassForCustomAttributeDemo).GetProperties(BindingFlags.Public | BindingFlags.Instance)
				.FirstOrDefault(p => p.GetCustomAttributes(typeof(JsonPropertyAttribute), false).Count() == 1);
			Console.WriteLine(prop.GetCustomAttribute<JsonPropertyAttribute>().PropertyName);

		}
	}
}
