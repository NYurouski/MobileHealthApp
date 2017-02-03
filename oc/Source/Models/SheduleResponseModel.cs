using Newtonsoft.Json;

namespace oc.Source.Models
{
   public class SheduleResponseModel
    {
        [JsonProperty("registrationeventid")]
        public string RegistrationEventId { get; set; }
        [JsonProperty("bcompletesurvey")]
        public string BCompleteSurvey { get; set; }
        [JsonProperty("registrationeventname")]
        public string RegistrationEventName { get; set; }
        [JsonProperty("registrationeventdescription")]
        public string RegistrationEventDescription { get; set; }
        [JsonProperty("participantscreeningsubperiodid")]
        public int ParticipantScreeningSubPeriodId { get; set; }
        [JsonProperty("participantscreeningtime")]
        public string ParticipantScreeningTime { get; set; }
        [JsonProperty("screeningperiods")]
        public ScreeningPeriod[] ScreeningPeriods { get; set; }
    }

    public class ScreeningPeriod
    {
        [JsonProperty("screeningperiodid")]
        public string ScreeningPeriodId { get; set; }
        [JsonProperty("location")]
        public string Location { get; set; }
        [JsonProperty("starttime")]
        public string StartTime { get; set; }
        [JsonProperty("screeningsubperiods")]
        public ScreeningSubPeriod[] ScreeningSubPeriods { get; set; }
    }

    public class ScreeningSubPeriod
    {
        [JsonProperty("screeningsubperiodid")]
        public string ScreeningSubPeriodId { get; set; }
        [JsonProperty("screeningtime")]
        public string ScreeningTime { get; set; }
        [JsonProperty("full")]
        public int Full { get; set; }
        [JsonProperty("active")]
        public int Active { get; set; }

        [JsonProperty("balreadyregistered")]
        public int BAlreadyRegistered { get; set; }
    }
}