using Newtonsoft.Json;

namespace oc.Source.Models.DataModel
{
    public class MyResultsMessage
    {
        [JsonProperty("msg1")]
        public string Msg1 { get; set; }
        [JsonProperty("msg2")]
        public string Msg2 { get; set; }
    }
}