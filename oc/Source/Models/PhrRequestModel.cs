using Newtonsoft.Json;

namespace oc.Source.Models
{
    class PhrRequestModel : HomeDataRequestModel
    {
        [JsonProperty("sectionid")]
        public string SectionId { get; set; }
    }
}
