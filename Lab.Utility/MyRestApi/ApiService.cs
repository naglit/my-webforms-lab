using System;
using System.Net;
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
			var getResponse = HttpGet(
				"https://api.open-meteo.com/v1/forecast?latitude=35.6785&longitude=139.6823&timezone=Asia%2FTokyos",
				new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"),
				"{\"username\":\"abc\",\"password\":\"abc\"}",
				Encoding.UTF8);

			// POST
			var postResponse = HttpPost(
				"https://api.restful-api.dev/objects",
				"{\"username\":\"abc\",\"password\":\"abc\"}",
				Encoding.UTF8,
				"application/json");
		}

		public async Task<ApiResponseBody> HttpGet(
			string url,
			MediaTypeWithQualityHeaderValue mediaType,
			string content,
			Encoding encoding)
		{
			// Add an Accept header for JSON format.
			s_httpClient.DefaultRequestHeaders.Accept.Clear();
			s_httpClient.DefaultRequestHeaders.Accept.Add(mediaType);

			// Get data response
			var response = new HttpResponseMessage();
			try
			{
				response = await s_httpClient.GetAsync(url);
			}
			catch (Exception ex)
			{
				// Log
			}
			var responseBody = new ApiResponseBody(
				response.StatusCode,
				response.Content?.ReadAsStringAsync().Result,
				response.ReasonPhrase);
			return responseBody;
		}

		public async Task<ApiResponseBody> HttpPost(string url, string content, Encoding encoding, string contentType)
		{
			s_httpClient.DefaultRequestHeaders.Accept.Clear();

			var stringContent = new StringContent(content, encoding, contentType);
			// Get data response
			var response = new HttpResponseMessage();
			try
			{
				response = await s_httpClient.PostAsync(url, stringContent);
			}
			catch (Exception ex)
			{
				// Log
			}
			response.EnsureSuccessStatusCode();
			var responseBody = new ApiResponseBody(
				response.StatusCode,
				response.Content?.ReadAsStringAsync().Result,
				response.ReasonPhrase);
			return responseBody;
		}
	}

	public class ApiResponseBody
	{
		public ApiResponseBody(HttpStatusCode statusCode, string content, string reasonPhrase)
		{
			this.StatusCode = statusCode;
			this.Content = content;
			this.ReasonPhrase = reasonPhrase;
		}

		public HttpStatusCode StatusCode { get; }
		public string Content { get; }
		public string ReasonPhrase { get; }
	}
}
