using Newtonsoft.Json;

namespace Lab.Utility.CustomAttributes
{
	public class DtoClassForCustomAttributeDemo
	{
		[JsonProperty("json_property_id")]
		public string Id { get; set; }
		[JsonProperty("json_property_name")]
		public string Name { get; set; }
	}
}
