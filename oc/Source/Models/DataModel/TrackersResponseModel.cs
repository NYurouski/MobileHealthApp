using Newtonsoft.Json;

namespace oc.Source.Models.DataModel
{
    class TrackersResponseModel
    {
        [JsonProperty("readings")]
        public Reading[] Readings { get; set; }
        [JsonProperty("highlightcolor")]
        public string HighLightColor { get; set; }
    }
   
    public class Reading
    {
        [JsonProperty("readingname")]
        public string ReadingName { get; set; }
        [JsonProperty("unit")]
        public string Unit { get; set; }
        [JsonProperty("readingcategory")]
        public string ReadingCategory { get; set; }
        [JsonProperty("readingtype")]
        public string ReadingType { get; set; }
        [JsonProperty("Source")]
        public string Source { get; set; }
        [JsonProperty("value1")]
        public string Value1 { get; set; }
        [JsonProperty("value2")]
        public string Value2 { get; set; }
        [JsonProperty("readingdate")]
        public string ReadingDate { get; set; }
        [JsonProperty("graphingdate")]
        public string GraphingDate { get; set; }
    }
}
