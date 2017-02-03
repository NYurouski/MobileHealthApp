using Newtonsoft.Json;

namespace oc.Source.Models.DataModel
{
    public class MyHealthScore
    {
        [JsonProperty("overallscore")]
        public string OverallScore { get; set; }
        [JsonProperty("risklevel")]
        public object RiskLevel { get; set; }
        [JsonProperty("riskcolor")]
        public object RiskColor { get; set; }
        [JsonProperty("gender")]
        public object Gender { get; set; }
    }
}