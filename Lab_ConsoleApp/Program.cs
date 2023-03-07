using System;
using Lab.Utility;
using Lab.Utility.Csharp;
using Lab.Utility.CsharpExperiment;
using Lab.Utility.MyCsharp;
using Lab.Utility.MyCustomAttribute;

namespace Lab_ConsoleApp
{
	class Program
	{
		static void Main(string[] args)
		{
			CustomAttributeTesting.Test(new DtoClassForCustomAttributeDemo("0","name!"));
			Console.ReadLine();
		}
	}
}
