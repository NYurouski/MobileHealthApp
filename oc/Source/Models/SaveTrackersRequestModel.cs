using Newtonsoft.Json;

namespace oc.Source.Models
{
    class SaveTrackersRequestModel
    {
            [JsonProperty("participantid")]
            public string ParticipantId { get; set; }
            [JsonProperty("readingtype")]
            public string ReadingTypeId { get; set; }
            [JsonProperty("readingdate")]
            public string ReadingDate { get; set; }
            [JsonProperty("value1")]
            public string Value1 { get; set; }
            [JsonProperty("value2")]
            public string Value2 { get; set; }
            [JsonProperty("note")]
            public string Note { get; set; }
        
    }
}
