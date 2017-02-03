using Newtonsoft.Json;

namespace oc.Source.Models.DataModel
{
    public class MyDailyTips
    {
        [JsonProperty("category")]
        public string Category { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}