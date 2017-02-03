namespace oc.Source.Models
{
    public class MyEventModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string StatusG { get; set; } = "True";

        public string StatusB { get; set; } = "False";

        public string Registered { get; set; }
        public string RegistrationDate { get; set; }
        public int RegistrationEventId { get; set; }
    }
}