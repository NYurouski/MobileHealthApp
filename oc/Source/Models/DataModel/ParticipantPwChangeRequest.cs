using Newtonsoft.Json;

namespace oc.Source.Models.DataModel
{
    public class ParticipantPwChangeRequest
    {
            [JsonProperty("key")]
            public string Key { get; set; }
            [JsonProperty("participantid")]
            public string ParticipantId { get; set; }
            [JsonProperty("currentpassword")]
            public string CurrentPassword { get; set; }
            [JsonProperty("newpassword")]
            public string NewPassword { get; set; }
        
    }
}