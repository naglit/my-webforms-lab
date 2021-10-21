using System;
using System.Globalization;
using System.IO;

namespace Lab.Utility
{
	public class FileWithCreationDate
	{
		public FileWithCreationDate(string filePath)
		{
			this.FilePath = filePath;
			RetrieveDate();
			this.Archived = (this.CreationDate <= DateTime.Today.AddDays(-26));
		}

		private void RetrieveDate()
		{
			DateTime creationDate;
			var hasDateInFileName = DateTime.TryParseExact(
				this.FileName.Substring(17, 8),
				"yyyyMMdd",
				null,
				DateTimeStyles.None,
				out creationDate);
			if (hasDateInFileName == false)
			{
				Console.WriteLine("A datetime info cannot be retrieved from {0}", this.FilePath);
			}

			this.HasDateInFileName = hasDateInFileName;
			this.CreationDate = creationDate;
		}

		public string FilePath { get; set; }
		public string FileName { get { return Path.GetFileName(this.FilePath); } }
		public DateTime CreationDate { get; set; }
		public bool HasDateInFileName { get; set; }
		public bool Archived { get; set; }
	}
}
