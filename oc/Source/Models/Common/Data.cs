using Newtonsoft.Json;
using oc.Source.Models.DataModel;

namespace oc.Source.Models.Common
{
    public class Data
    {
        [JsonProperty("stage")]
        public string Stage { get; set; }
        [JsonProperty("firstname")]
        public string FirstName { get; set; }
        [JsonProperty("token")]
        public string Token { get; set; }
        [JsonProperty("welcomeletter")]
        public string WelcomeLetter { get; set; }
        [JsonProperty("programid")]
        public string ProgramId { get; set; }
        [JsonProperty("participantid")]
        public string ParticipantId { get; set; }
        [JsonProperty("mystatus")]
        public MyStatus MyStatus { get; set; }
        [JsonProperty("myevents")]
        public MyEvent[] MyEvents { get; set; }
        [JsonProperty("mydailytips")]
        public MyDailyTips MyDailyTips { get; set; }
        [JsonProperty("menuchoices")]
        public MenuChoices MenuChoices { get; set; }
        [JsonProperty("myresultsmessage")]
        public MyResultsMessage MyResultsMessage { get; set; }
        [JsonProperty("myhealthscore")]
        public MyHealthScore MyHealthScore { get; set; }
        [JsonProperty("myhealthrisks")]
        public MyHealthRisks MyHealthRisks { get; set; }
    }
}