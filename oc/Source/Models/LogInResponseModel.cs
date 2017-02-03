using oc.Source.Models.Common;

namespace oc.Source.Models
{
    public class LogInResponseModel : DataResponseModel
    {
        public bool success { get; set; }

        public Data data { get; set; }

        public Error error { get; set; }
    }
}







