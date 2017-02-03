using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace oc
{
	public static class TextUtils
	{
		public static string GenerateQuery(string data)
		{
			var jObj = (JObject)JsonConvert.DeserializeObject(data);

			var query = String.Join("&",
							jObj.Children().Cast<JProperty>()
									.Select(jp => jp.Name + "=" + jp.Value.ToString()));
			return query;
		}
	}
}

