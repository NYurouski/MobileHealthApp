using Newtonsoft.Json;

namespace oc.Source.Models.DataModel
{

    public class PhrResponseModel
    {
        [JsonProperty("chapter")]
        public Chapter[] Chapter { get; set; }
        [JsonProperty("highlightcolor")]
        public string HighLightColor { get; set; }
    }

    public class Chapter
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("heading")]
        public string Heading { get; set; }
    }
}
