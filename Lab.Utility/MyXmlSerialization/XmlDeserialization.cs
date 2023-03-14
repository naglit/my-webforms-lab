using System;
using System.Dynamic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Lab.Utility.MyXmlSerialization
{

	public class XmlDeserialization
	{
		private const string XML = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n  <ResultSet totalResultsAvailable=\"1\" totalResultsReturned=\"1\" firstResultPosition=\"1\">\r\n  <Result>\r\n    <Status>OK</Status>\r\n    <OrderInfo>\r\n      <OrderId>testseller-10000001</OrderId>\r\n      <Pay>\r\n        <PayStatus>1</PayStatus>\r\n      </Pay>\r\n    </OrderInfo>\r\n  </Result>\r\n</ResultSet>";

		public static void Deserialize()
		{
			using (var xmlReader = new MemoryStream(Encoding.UTF8.GetBytes(XML)))
			{
				var resultSet = new XmlSerializer(typeof(ResultSetA)).Deserialize(xmlReader);
			}
				
		}
	}
	[Serializable]
	[XmlRoot("ResultSet")]

	public class ResultSetA
	{
		[XmlElement("Result")]
		public Result Result { get; set; }
		[XmlAttribute("totalResultsAvailable")]
		public string TotalResultsAvailable { get; set; }
		[XmlAttribute("totalResultsReturned")]
		public string TotalResultsReturned { get; set; }
		[XmlAttribute("firstResultPosition")]
		public string FirstResultPosition { get; set; }
	}

	[Serializable]
	public class Result
	{
		[XmlElement("Status")]
		public string Status { get; set; }
		[XmlElement("Dummy")]
		public string Dummy { get; set; }
		[XmlElement("OrderInfo")]
		public OrderInfo OrderInfo { get; set; }
	}

	[Serializable]
	public class OrderInfo
	{
		[XmlElement("OrderId")]
		public string OrderId { get; set; }
		[XmlElement("Pay")]
		public Pay Pay { get; set; }
	}

	public class Pay
	{
		[XmlElement("PayStatus")]
		public string PayStatus { get; set; }
	}
}
