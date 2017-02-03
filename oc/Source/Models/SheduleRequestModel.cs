using Newtonsoft.Json;

namespace oc.Source.Models
{
	public class SheduleRequestModel : HomeDataRequestModel
    {
    
        [JsonProperty("registrationeventid")]
        public string RegistrationEventId { get; set; }
    }
}

