using System;
using Lab.Utility.MyXmlSerialization;
using Lab.Utility.SharedConfigurations;
namespace Lab_ConsoleApp
{
	class Program
	{
		static void Main(string[] args)
		{
			var result = DecimalControlConfiguration.Get();
			foreach (var v in result.Variables.GetAll())
			{
				Console.WriteLine($"{v.Name}: {v.RoundingType}");
			}
            Console.ReadLine();
        }
	}
}
