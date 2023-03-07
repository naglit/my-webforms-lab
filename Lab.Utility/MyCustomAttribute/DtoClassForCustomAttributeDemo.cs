using Newtonsoft.Json;

namespace Lab.Utility.MyCustomAttribute
{
	public class DtoClassForCustomAttributeDemo
	{
		public DtoClassForCustomAttributeDemo(string id, string name)
		{
			this.Id = id;
			this.Name = name;
		}

		[JsonProperty("json_property_id")]
		public string Id { get; set; }
		[JsonProperty("json_property_name")]
		public string Name { get; set; }
	}
}
