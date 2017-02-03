using Newtonsoft.Json;

namespace oc.Source.Models.DataModel
{
    public class MyHealthRisks
    {
        [JsonProperty("highlightcolor")]
        public string HighlightColor { get; set; }
        [JsonProperty("bpsign")]
        public string BpSign { get; set; }
        [JsonProperty("bptext")]
        public string BpText { get; set; }
        [JsonProperty("hdlsign")]
        public string HdlSign { get; set; }
        [JsonProperty("hdltext")]
        public string HdlText { get; set; }
        [JsonProperty("triglsign")]
        public string TriglSign { get; set; }
        [JsonProperty("trigltext")]
        public string TriglText { get; set; }
        [JsonProperty("glucosesign")]
        public string GlucoseSign { get; set; }
        [JsonProperty("glucosetext")]
        public string GlucoseText { get; set; }
        [JsonProperty("bodysign")]
        public string BodySign { get; set; }
        [JsonProperty("bodytext")]
        public string BodyText { get; set; }
    }
}