using System;
using Newtonsoft.Json;


namespace oc.Source.Models
{

    public class ScoreDetailsResponseModel
    {
        [JsonProperty("participantscore")]
        public Participantscore ParticipantScore { get; set; }
        [JsonProperty("compare")]
        public Compare Compare { get; set; }
        [JsonProperty("scoretable")]
        public Scoretable[] Scoretable { get; set; }
    }

    public class Participantscore
    {
        [JsonProperty("highlightcolor")]
        public string HighlightColor { get; set; }
        [JsonProperty("healthscoretypeid")]
        public int HealthScoreTypeId { get; set; }
        [JsonProperty("overallscore")]
        public int OveralScore { get; set; }
        [JsonProperty("maxnicotinescore")]
        public int MaxNicotineScore { get; set; }
        [JsonProperty("nicotinescore")]
        public int NicotineScore { get; set; }
        [JsonProperty("bodyweightscore")]
        public int BodyWeightScore { get; set; }
        [JsonProperty("bodyfatscore")]
        public int BodyfatScore { get; set; }
        [JsonProperty("bloodpressurescore")]
        public int BloodPressureScore { get; set; }
        [JsonProperty("waistscore")]
        public int WaistScore { get; set; }
        [JsonProperty("cholesterolscore")]
        public int CholesterolScore { get; set; }
        [JsonProperty("ldlscore")]
        public int LdlScore { get; set; }
        [JsonProperty("hdlscore")]
        public int HdlScore { get; set; }
        [JsonProperty("cholesterolratioscor")]
        public int CholesterolRatioScore { get; set; }
        [JsonProperty("triglyceridesscore")]
        public int TriglyceridesScore { get; set; }
        [JsonProperty("glucosescore")]
        public int GlucoseScore { get; set; }
        [JsonProperty("GGTscor")]
        public int GGTscore { get; set; }
        [JsonProperty("healthybehaviorscore")]
        public int HealthyBehaviorScore { get; set; }
        [JsonProperty("healthybehaviormaxscore")]
        public string HealthyBehaviorMaxScore { get; set; }
        [JsonProperty("bodycompscore")]
        public int BodyCompScore { get; set; }
        [JsonProperty("bodycompmaxscore")]
        public string BodyCompMaxScore { get; set; }
        [JsonProperty("healthstatusscore")]
        public int HealthStatusScore { get; set; }
        [JsonProperty("healthstatusmaxscore")]
        public string HealthStatusMaxScore { get; set; }
        [JsonProperty("screeningresultsscore")]
        public int ScreeningResultsScore { get; set; }
        [JsonProperty("screeningresultsmaxscore")]
        public string ScreeningResultsMaxScore { get; set; }
        [JsonProperty("preventativescore")]
        public int PreventativeScore { get; set; }
        [JsonProperty("preventativemaxscore")]
        public string PreventativeMaxScore { get; set; }
        [JsonProperty("bloodworkscore")]
        public object BloodWorkScore { get; set; }
        [JsonProperty("bmiscore")]
        public object BmiScore { get; set; }
        [JsonProperty("risklevel")]
        public string RiskLevel { get; set; }
        [JsonProperty("riskcolor")]
        public string RiskColor { get; set; }
        [JsonProperty("riskmessage")]
        public string RiskMessage { get; set; }
        [JsonProperty("gender")]
        public string Gender { get; set; }
        [JsonProperty("firstname")]
        public object FirstName { get; set; }
        [JsonProperty("scoredate")]
        public string ScoreDate { get; set; }
        [JsonProperty("clientname")]
        public string ClientName { get; set; }
    }

    public class Compare
    {
     

        [JsonProperty("nationalscore")]
        public string NationalScore { get; set; }
        [JsonProperty("programscore")]
        public string ProgramScore { get; set; }
        [JsonProperty("marketscore")]
        public string MarketScore { get; set; }
        public double ProgramScoreDecimal => Convert.ToDouble(ProgramScore);
    }

    public class Scoretable
    {
        [JsonProperty("scoredate")]
        public string ScoreDate { get; set; }
        [JsonProperty("scorevalue")]
        public int ScoreValue { get; set; }
        [JsonProperty("scoreprogram")]
        public string ScoreProgram { get; set; }
    }

}
