using Newtonsoft.Json;
using oc.Source.Models.Common;

namespace oc.Source.Models
{
    public class HomeDataResponseModel : DataResponseModel
    {
        [JsonProperty("success")]
        public bool Success { get; set; }
        [JsonProperty("data")]
        public Data ResponseData { get; set; }
        [JsonProperty("error")]
        public Error Error { get; set; }
    }
}
