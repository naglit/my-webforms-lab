
namespace Lab.Utility.Model
{
	public class SampleReadDataModel : IReadDataModel
	{
		public SampleReadDataModel()
		{
			this.RowData = new string[2];
		}

		public string Id
		{
			get { return this.RowData[0]; }
		}
		public string Name
		{
			get { return this.RowData[1]; }
		}
		public string[] RowData { get; set; }
		
	}
}
