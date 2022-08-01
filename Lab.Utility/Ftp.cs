
using System;
using System.IO;
using System.Net;

namespace Lab.Utility
{
	public class Ftp
	{
		public Ftp(string destinationPath, string sourcePath)
		{
			this.DestinationPath = destinationPath;
			this.SourcePath = sourcePath;
		}

		public void Copy()
		{
			// Get the object used to communicate with the server.
			var request = (FtpWebRequest)WebRequest.Create(this.DestinationPath);
			request.Method = WebRequestMethods.Ftp.UploadFile;

			// This example assumes the FTP site uses anonymous logon.
			request.Credentials = new NetworkCredential("anonymous", "a@example.com");

			// Copy the contents of the file to the request stream.
			using (var fileStream = File.Open(this.SourcePath, FileMode.Open, FileAccess.Read))
			{
				using (var requestStream = request.GetRequestStream())
				{
					fileStream.CopyTo(requestStream);
					using (var response = (FtpWebResponse)request.GetResponse())
					{
						Console.WriteLine($"Upload File Complete, status {response.StatusDescription}");
					}
				}
			}
		}

		private string DestinationPath { get; set; }

		private string SourcePath { get; set; }
	}
}
