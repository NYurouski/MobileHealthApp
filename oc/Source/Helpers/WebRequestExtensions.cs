using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace oc
{
	public static class WebRequestExtensions
	{
		public static Task<WebResponse> GetResponseAsync(this WebRequest request)
		{
			return Task.Factory.FromAsync<WebResponse>(request.BeginGetResponse, request.EndGetResponse, null);
		}

		public static Task<Stream> GetRequestStreamAsync(this WebRequest request)
		{
			return Task.Factory.FromAsync<Stream>(request.BeginGetRequestStream, request.EndGetRequestStream, null);
		}
	}
}

