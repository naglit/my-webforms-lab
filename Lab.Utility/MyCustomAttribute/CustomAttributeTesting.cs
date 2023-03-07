using System;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;

namespace Lab.Utility.MyCustomAttribute
{
	public class CustomAttributeTesting
	{
		public static void Main()
		{
			var properties =
				typeof(DtoClassForCustomAttributeDemo).GetProperties(BindingFlags.Public | BindingFlags.Instance);
			foreach (var property in properties)
			{
				var attributes = (property.GetCustomAttributes(typeof(JsonPropertyAttribute), false));
				foreach (JsonPropertyAttribute attribute in attributes)
				{
					Console.WriteLine(attribute.PropertyName);
				}
			}
			//var property = properties.FirstOrDefault(
			//	p => p.GetCustomAttributes(typeof(JsonPropertyAttribute), false).Count() == 1);
		}

		public static void Test<T>(T obj)
		{
			var properties =
				typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
			var parameters = properties.ToDictionary(
				p =>
				{
					var attributes = p.GetCustomAttributes(typeof(JsonPropertyAttribute), false);
					if (attributes.Any() == false) return "aa";
					var attribute = attributes.First();
					return ((JsonPropertyAttribute)attribute).PropertyName;
				},
				p => p.GetValue(obj));
			foreach (var parameter in parameters) Console.WriteLine($"{parameter.Key}: {parameter.Value}");
			var queryString = string.Join(
				"&",
				parameters.Select(p => $"{p.Key}={p.Value}")); // skip url encoding
			var url = $"https://www.naglit/?{queryString}";
			Console.WriteLine(url);
		}
	}
}
