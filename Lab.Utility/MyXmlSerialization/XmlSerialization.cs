using System;
using System.IO;
using System.Xml.Serialization;

namespace Lab.Utility.MyXmlSerialization
{
	public class XmlSerialization
	{
		public static void Main()
		{
			using (var stringwriter = new StringWriter())
			{
				var xns = new XmlSerializerNamespaces();
				xns.Add("", "");
				var req = new Req();
				var serializer = new XmlSerializer(req.GetType());
				serializer.Serialize(stringwriter, new Req(), xns);
				Console.WriteLine(stringwriter.ToString());
			}
		}
	}

	public class Req
	{
		public Target Target { get; set; } = new Target();
	}

	public class Target
	{
		public Target()
		{
			this.Field = string.Join(",", this.Feilds);
		}
		public string OrderId { get; set; } = "00000001";
		public string Field { get; set; }
		[XmlIgnore]
		public string[] Feilds { get; set; } = { "name", "id", "gender" };
	}
}
