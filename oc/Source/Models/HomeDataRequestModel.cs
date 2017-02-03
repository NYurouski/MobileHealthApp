using Newtonsoft.Json;

namespace oc.Source.Models
{
	public class HomeDataRequestModel 
    {
        [JsonProperty("programid")]
        public string ProgramId { get; set; }

        [JsonProperty("participantid")]
        public string ParticipantId { get; set; }
    }
}

