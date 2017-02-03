using Newtonsoft.Json;

namespace oc.Source.Models.DataModel
{
    class SettingsDataRequestModel
    {
        private string _email;

        [JsonProperty("participantid")]
        public string ParticipantId { get; set; }
        [JsonProperty("fn")]
        public string FirstName { get; set; }
        [JsonProperty("ln")]
        public string LastName { get; set; }
        [JsonProperty("dob")]
        public string Date { get; set; }

        [JsonProperty("email")]
        public string Email
        {
            get { return _email.ToLower(); }
            set { _email = value; }
        }

        [JsonProperty("addr")]
        public string Address { get; set; }
        [JsonProperty("city")]
        public string City { get; set; }
        [JsonProperty("state")]
        public string State { get; set; }
        [JsonProperty("zip")]
        public string Zip { get; set; }

        [JsonProperty("emailmarketing")]
        public int EmailMarketing
        {
            get; set;
        }

      
    }
}
