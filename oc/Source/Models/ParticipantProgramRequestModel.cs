using Newtonsoft.Json;

namespace oc.Source.Models
{
    class ParticipantProgramRequestModel
    {
        [JsonProperty("participantid")]
        public string ParticipantId { get; set; }
    }
}
