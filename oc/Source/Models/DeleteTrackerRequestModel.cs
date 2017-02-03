using Newtonsoft.Json;

namespace oc.Source.Models
{
    class DeleteTrackerRequestModel
    {
        [JsonProperty("participantreadingid")]
        public string ParticipantReadingId { get; set; }
        [JsonProperty("participantid")]
        public string ParticipantId { get; set; }
       
    }
}
