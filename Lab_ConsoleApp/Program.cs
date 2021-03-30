using Lab.Utility.Configuration;
using Lab.Utility.Csharp;
using Lab.Utility.Encryption;
using Lab.Utility.Multithreading;
using System;
using System.Linq;

namespace Lab_ConsoleApp
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine(37 + (16 / 16 + 1) * 16);
			var a = SensitiveDataColumnsSetting.GetInstance;
			Console.WriteLine(Encryption.GenerateIV());
			Console.ReadLine();

		}
	}
}
