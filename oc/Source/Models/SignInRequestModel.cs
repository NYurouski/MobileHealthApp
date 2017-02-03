using Newtonsoft.Json;

namespace oc.Source.Models
{
	public class SignInRequestModel : DataRequestModel
	{
        [JsonProperty("password")]
        public string Password { get; set;}
	}
}

