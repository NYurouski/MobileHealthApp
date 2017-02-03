using Newtonsoft.Json;

namespace oc.Source.Models
{
	public class RegistrationRequestModel : DataRequestModel
	{
        [JsonProperty("invitationcode")]
        public string InvitationCode { get; set;}
	}
}

