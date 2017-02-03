using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using ModernHttpClient;
using Newtonsoft.Json;

namespace oc
{
	public static class WebUtil
	{

		public static async Task<string> GetDataWithPostRequest<T>(string url, T requestModel)
		{
			string response = null;
		    try
			{
				var httpClient = new HttpClient(new NativeMessageHandler());
                httpClient.Timeout = TimeSpan.FromMinutes(2);
                HttpResponseMessage data = await httpClient.PostAsync(url, new StringContent(JsonConvert.SerializeObject(requestModel, Formatting.Indented), Encoding.UTF8, "application/json"));
                
				if (data.StatusCode == HttpStatusCode.OK)
				{
					response = await data.Content.ReadAsStringAsync();
				}

			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.ToString());
				return null;
			}

			return response;
		}


	}
}
