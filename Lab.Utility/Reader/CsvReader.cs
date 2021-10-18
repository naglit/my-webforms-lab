using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lab.Utility.Reader
{
	public class CsvReader<T> where T : IReadDataModel, new()
	{
		public CsvReader(string filePath, bool hasHeader = false)
		{
			this.FilePath = filePath;
			this.HasHeader = hasHeader;
		}

		public IEnumerable<T> Read()
		{
			using (var reader = new StreamReader(this.FilePath, Encoding.GetEncoding("Shift_JIS")))
			{
				var count = 0;
				while (!reader.EndOfStream)
				{
					count++;

					// Split
					var line = reader.ReadLine();
					var row = line.Split(',');

					if (this.HasHeader && (count == 1)) continue;

					// Put the spilitted array data into the property of a generic type model
					var model = new T();
					model.RowData = row;

					yield return model;
				}
				this.TotalRowCount = count;
			}
		}

		private string FilePath { get; set; }
		private bool HasHeader { get;  set; }
		public int TotalRowCount { get; private set; }
	}
}
