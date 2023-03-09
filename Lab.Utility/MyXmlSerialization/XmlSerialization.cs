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
				var req = new Req()
				{
					Target = new Target("000001", new[] { "name", "id", "gender" }),
					SellerId = "sellerId!!",
				};
				var serializer = new XmlSerializer(req.GetType());
				serializer.Serialize(stringwriter, req, xns);
				Console.WriteLine(stringwriter.ToString());
			}
		}
	}

	[XmlRoot("Req")]
	public class Req
	{
		[XmlElement("Target")]
		public Target Target { get; set; }
		[XmlElement("SellerId")]
		public string SellerId { get; set; }
	}

	public class Target
	{
		public Target(){}
		public Target(string orderId, string[] fields)
		{
			this.OrderId = orderId;
			this.Field = string.Join(",", fields);
		}
		[XmlElement("OrderId")]
		public string OrderId { get; set; }
		[XmlElement("Field")]
		public string Field { get; set; }
	}
}
