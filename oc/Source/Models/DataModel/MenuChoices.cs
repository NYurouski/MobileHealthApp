using Newtonsoft.Json;

namespace oc.Source.Models.DataModel
{
    public class MenuChoices
    {
        [JsonProperty("welcomeletter")]
        public string WelcomeLetter { get; set; } = bool.FalseString;
        [JsonProperty("viewopportunities")]
        public string ViewOpportunities { get; set; } = bool.FalseString;
        [JsonProperty("viewevents")]
        public string ViewEvents { get; set; } = bool.FalseString;
        [JsonProperty("viewtrackers")]
        public string ViewTrackers { get; set; } = bool.FalseString;
        [JsonProperty("completesurvey")]
        public string CompleteSurvey { get; set; } = bool.FalseString;
        [JsonProperty("viewhealthscore")]
        public string ViewHealthScore { get; set; } = bool.FalseString;
        [JsonProperty("viewphr")]
        public string ViewPhr { get; set; } = bool.FalseString;
    }
}