using Lab.Utility;
using System;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Lab_ConsoleApp
{
	class Program
	{
		static void Main(string[] args)
		{
			var src = @"C:\inetpub\wwwroot\";
			var filePaths = Directory.GetFiles(src);
			var filesWithCreationDate = filePaths.Select(path => new FileWithCreationDate(path)).ToArray();

			// Archive
			var dist = @"C:\inetpub\wwwroot\archive";
			foreach (var file in filesWithCreationDate.Where(f => f.Archived))
			{
				Console.WriteLine("{0} => {1}", file.FilePath, Path.Combine(dist, file.FileName));
				File.Move(file.FilePath, Path.Combine(dist, file.FileName));
			}

			Console.ReadLine();
		}
	}
}
