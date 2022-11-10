using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Lab.Utility.JsonSerializationLab
{
	public class JsonSerialization
	{
		public static void Main()
		{
			var dict = new Dictionary<string, string>
			{
				{ "key1", "value1" },
				{ "key2", "value2" },
				{ "key3", "value3" },
			};
			var hashtable = new Hashtable
			{
				{ "key1", "value1" },
				{ "key2", "value2" },
				{ "key3", "value3" },
			};
			var serializedDict= JsonConvert.SerializeObject(dict);
			var serializedHashtable = JsonConvert.SerializeObject(hashtable);
			Console.WriteLine(serializedDict);
			Console.WriteLine(serializedHashtable);
		}
}
}
