using System;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Lab.Utility
{
	public class FileSweeper
	{
		public FileSweeper()
		{
			//FileController.CreateDirectory(Constants.ACTIVE_DIR_PATH);
		}

		public void SweepFiles()
		{
			GetFilesToBeArchived();

			SweepOut();
		}

		private void GetFilesToBeArchived()
		{
			var src = @"C:\inetpub\wwwroot\Batch\FileUpload\backup";

			var filePaths = Directory.GetFiles(src);
			this.FilesToBeArchived = filePaths.Select(path => new FileWithCreationDate(path)).Where(f => f.Archived).ToArray();
		}

		private void SweepOut()
		{
			// Archive
			foreach (var file in this.FilesToBeArchived)
			{
				
			}
		}

		private FileWithCreationDate[] FilesToBeArchived { get; set; }
	}

	internal class FileWithCreationDate
	{
		internal FileWithCreationDate(string filePath)
		{
			this.FilePath = filePath;
			RetrieveDate();
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

		public void Move ()
		{
			var dist = @"C:\inetpub\wwwroot\Batch\FileUpload\backup\archive";
			var distFilePath = Path.Combine(dist, this.FileName);
			Console.WriteLine("{0} => {1}", this.FilePath, distFilePath);
			try
			{
				File.Move(this.FilePath, distFilePath);
			}
			catch (Exception e)
			{
				var errMsg = string.Format(
					"ファイルの移動に失敗しました。既に同名のファイルが移動先に存在する可能性があります。filename={0}",
					this.FileName);
				Console.WriteLine(errMsg);
			}
			

			Console.WriteLine("{0} => {1}", this.FilePath, Path.Combine(dist, this.FileName));
			File.Move(this.FilePath, Path.Combine(dist, this.FileName));
		}

		internal string FilePath { get; set; }
		internal string FileName { get { return Path.GetFileName(this.FilePath); } }
		private DateTime CreationDate { get; set; }
		private bool HasDateInFileName { get; set; }
		public bool Archived { get { return (this.CreationDate <= DateTime.Today.AddDays(-26)); } }
	}
}