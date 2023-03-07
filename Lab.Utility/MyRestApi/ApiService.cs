using System;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Utility.MyRestApi
{
	public class ApiService
	{
		public ApiService()
		{
		}

		private static readonly HttpClient s_httpClient = new HttpClient();

		public void Test()
		{
			// GET
			var apiGetRequestParams = new ApiRequestParams(
				new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"),
				"",
				"https://api.open-meteo.com/v1/forecast?latitude=35.6785&longitude=139.6823&timezone=Asia%2FTokyo");
			HttpGet(apiGetRequestParams).GetAwaiter().GetResult();

			// POST
			var apiPostRequestParams = new ApiRequestParams(
				new MediaTypeWithQualityHeaderValue("application/json"),
				authorization: "",
				url: "https://api.restful-api.dev/objects",
				content: "{\"username\":\"abc\",\"password\":\"abc\"}");

			var a = HttpPost(apiPostRequestParams);
		}

		public async Task HttpGet(ApiRequestParams parameters)
		{
			// Add an Accept header for JSON format.
			s_httpClient.DefaultRequestHeaders.Accept.Clear();
			s_httpClient.DefaultRequestHeaders.Accept.Add(parameters.MediaTypeWithQualityHeaderValue);

			// Get data response
			var response = s_httpClient.GetAsync(parameters.Url).Result;
			if (response.IsSuccessStatusCode)
			{
				// Parse the response body
				var dataObjects = response.Content.ReadAsStringAsync().Result;
				foreach (var d in dataObjects)
				{
				}
			}
			else
			{
				Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
			}
		}

		public async Task HttpPost(ApiRequestParams parameters)
		{
			s_httpClient.DefaultRequestHeaders.Accept.Clear();
			var stringContent = new StringContent(parameters.Content, Encoding.UTF8, "application/json");
			var response = await s_httpClient.PostAsync(parameters.Url, stringContent);
			response.EnsureSuccessStatusCode();

			// return URI of the created resource.
		}
	
}

	public class ApiRequestParams
	{
		public ApiRequestParams(MediaTypeWithQualityHeaderValue mediaTypeWithQualityHeaderValue, string authorization, string url, string content = "")
		{
			this.MediaTypeWithQualityHeaderValue= mediaTypeWithQualityHeaderValue;
			this.Authorization = authorization;
			this.Url = url;
			this.Content = content;
		}

		public MediaTypeWithQualityHeaderValue MediaTypeWithQualityHeaderValue { get; }
		public string Authorization { get; }
		public string Url { get; }
		public string Content { get; }
	}
}
