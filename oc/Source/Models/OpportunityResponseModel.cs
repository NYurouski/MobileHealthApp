using Newtonsoft.Json;

namespace oc.Source.Models
{
    public class OpportunityResponseModel : DataResponseModel
    {
        [JsonProperty("incentive")]
        public Incentive[] Incentive { get; set; }
        [JsonProperty("percentawarded")]
        public string PercentAwarded { get; set; }
        [JsonProperty("rewardintro")]
        public string RewardIntro { get; set; }
        [JsonProperty("incentiveintro")]
        public string IncentiveIntro { get; set; }
        [JsonProperty("currency")]
        public string Currency { get; set; }
        [JsonProperty("highlightcolor")]
        public string HighlightColor { get; set; }
        [JsonProperty("contactinfo")]
        public string ContactInfo { get; set; }
        [JsonProperty("participanttype")]
        public Participanttype ParticipantType { get; set; }
        [JsonProperty("reward")]
        public Reward[] Reward { get; set; }
        [JsonProperty("currencyawarded")]
        public string CurrencyAwarded { get; set; }
    }

    public class Participanttype
    {
    }

    public class Incentive
    {
        [JsonProperty("opportunityid")]
        public string OpportunityId { get; set; }
        [JsonProperty("opportunityname")]
        public string OpportunityName { get; set; }
        [JsonProperty("categorycount")]
        public string CategoryCount { get; set; }
        [JsonProperty("categoryminimum")]
        public string CategoryMinimum { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("awarded")]
        public string Awarded { get; set; }
        [JsonProperty("categories")]
        public Category[] Category { get; set; }
    }

    public class Reward
    {
        [JsonProperty("opportunityid")]
        public string OpportunityId { get; set; }
        [JsonProperty("opportunityname")]
        public string OpportunityName { get; set; }
        [JsonProperty("categorycount")]
        public string CategoryCount { get; set; }
        [JsonProperty("categoryminimum")]
        public string CategoryMinimum { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("rewardedvalue")]
        public string RewardedValue { get; set; }
        [JsonProperty("rewardvalue")]
        public string RewardValue { get; set; }
        [JsonProperty("awarded")]
        public string Awarded { get; set; }
        [JsonProperty("category")]
        public Category[] Category { get; set; }

    }

    public class Category
    {
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}