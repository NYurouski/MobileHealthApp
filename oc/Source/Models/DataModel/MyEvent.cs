using Newtonsoft.Json;

namespace oc.Source.Models.DataModel
{
    public class MyEvent
    {
        [JsonProperty("highlightcolor")]
        public string HighlightColor { get; set; }
        [JsonProperty("registrationeventid")]
        public string RegistrationEventId { get; set; }
        [JsonProperty("eventname")]
        public string EventName { get; set; }
        [JsonProperty("eventdescription")]
        public string EventDescription { get; set; }
        [JsonProperty("registrationstatus")]
        public string RegistrationStatus { get; set; }
        [JsonProperty("screeningsubperiodid")]
        public string ScreeningSubperiodId { get; set; }
        [JsonProperty("registrationdate")]
        public string RegistrationDate { get; set; }
    }
}