using System;
using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using Acr.UserDialogs;

namespace oc
{
	public class ApiService
	{
		public class ServerApiException : Exception
		{
			public int ErrorCode { get; private set; }

			public ServerApiException() { }
			public ServerApiException(string message) : base(message) { }
			public ServerApiException(string message, Exception inner) : base(message, inner) { }
			public ServerApiException(string message, int errorCode) : base(message)
			{
				ErrorCode = errorCode;
			}
		}

		private static ApiService instance;

		public static ApiService Instance => instance ?? (instance = new ApiService());

	    public ApiService() { }


	    public async Task<TResponseModel> GetDataFromServerTask<TRequestModel, TResponseModel>(TRequestModel model, string url) where TRequestModel : class where TResponseModel : class, new()
	    {
             
	        var response= await WebUtil.GetDataWithPostRequest(url, model);
	        TResponseModel result = default(TResponseModel);
	        if (!string.IsNullOrEmpty(response))
	        {

	            try
	            {
                    
                    result= JsonConvert.DeserializeObject<TResponseModel>(response);
                }
	            catch (Exception ex)
	            {

                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                    
                }
	            
	        }
	        return result;
	    }

	}
}