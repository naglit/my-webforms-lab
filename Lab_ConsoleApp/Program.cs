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

			EncryptColumnData.IsSensitiveDataColumn();
			Console.ReadLine();

		}
	}
}
