using System.Collections.Generic;
using Newtonsoft.Json;

namespace oc.Source.Models
{
    public class MyEventsResponseModel
    {
        [JsonProperty("event")]
        public List<Event> Events { get; set; } = new List<Event>();
    }
}