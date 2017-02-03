using System.Collections.Generic;

namespace oc.Source.Models
{
    public class MySheduleModel
    {

        public string Title { get; set; }
        public List<ShedulePlan> ShedulePlans { get; set; }= new List<ShedulePlan>();


    }

    public class ShedulePlan
    {

        public string ScreeningTimeTitle { get; set; }


        public bool isEnabled { get; set; } = false;

        public string ScreeningSubperiodId { get; set; }

        public int BAlreadyRegistered { get; set; }
    }
}
