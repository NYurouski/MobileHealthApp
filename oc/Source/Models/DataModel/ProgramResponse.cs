namespace oc.Source.Models.DataModel
{
    public class ProgramResponse
    {
        public Program[] programs { get; set; }
        
    }
    public class Program
    {
        public string programid { get; set; }
        public string programname { get; set; }
        public string programtoparticipantid { get; set; }
    }
}