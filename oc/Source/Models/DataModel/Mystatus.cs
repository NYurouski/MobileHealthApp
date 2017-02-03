using Newtonsoft.Json;

namespace oc.Source.Models.DataModel
{
    public class MyStatus
    {
        [JsonProperty("hassurvey")]
        public string HasSurvey { get; set; }
        [JsonProperty("hasbiometrics")]
        public string HasBiometrics { get; set; }
        [JsonProperty("biometricdate")]
        public string BiometricDate { get; set; }
        [JsonProperty("highlightcolor")]
        public string HighlightColor { get; set; }
        [JsonProperty("programover")]
        public string ProgramOver { get; set; }
    }
}