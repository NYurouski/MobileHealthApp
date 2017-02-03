using Newtonsoft.Json;

namespace oc.Source.Models
{
    class AppointmentMobileRequestModel : HomeDataRequestModel
    {
        [JsonProperty("screeningsubperiodid")]
        public string ScreeningSubperiodId { get; set; }

    }
}
