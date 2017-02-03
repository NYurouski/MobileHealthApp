using System.Collections.Generic;

namespace oc.Source.Constants
{
	public static class ApiConstants
	{
        const string BaseUrl = EnvironmentConstants.ApiBaseUrl;
        const string SignIn = "checkpassword2.php";
		const string SignUp = "register";
        const string ForgotPassword = "resetpassword2.php";
        const string Registration = "checkinvitationcodemobile.php";
        const string HomeData = "gethomedata.php";
        const string OpportunityData = "getopportunitiesmobile.php";
        const string MyEventsData = "geteventsmobile.php";
        const string MySheduleData = "getschedulemobile.php";
	    const string WelcomeLetterData = "getwelcomelettermobile.php";
	    const string AddAppointment = "addappointmentmobile.php";
        const string CancelAppointment = "cancelappointmentmobile.php";
        const string GetScoreDetails = "getscoredetailsmobile.php";
        const string GetSectionMobile = "getphrsectionsmobile.php";
        const string GetSingleSectionMobile = "getsinglephrsectionmobile.php";
        const string GetTrackersMobile = "gettrackersmobile.php";
        const string GetParticipantProgramsMobile = "getparticipantprogramsmobile.php";
        const string SaveParticipantInfoMobile = "saveparticipantinfomobile.php";
        const string GetParticipantInfoMobile = "getparticipantinfomobile.php";
        const string SetParticipantpw = "setparticipantpw.php";
        const string SaveTrackers = "savetrackermobile.php";
        const string DeleteTracker = "deletetrackermobile.php";

        public static string SignInUrl => $"{BaseUrl}{SignIn}";
        public static string RegistrationUrl => $"{BaseUrl}{Registration}";
        public static string HomeDataUrl => $"{BaseUrl}{HomeData}";
        public static string SingUpUrl => $"{BaseUrl}{SignUp}";
	    public static string ForgotPasswordUrl => $"{BaseUrl}{ForgotPassword}";
        public static string OpportunityPageUrl => $"{BaseUrl}{OpportunityData}";
        public static string MyEventsPageUrl => $"{BaseUrl}{MyEventsData}";
        public static string ShedulePageUrl => $"{BaseUrl}{MySheduleData}";
        public static string WelcomeLetterUrl=> $"{BaseUrl}{WelcomeLetterData}";
	    public static string AddAppointmentUrl => $"{BaseUrl}{AddAppointment}";
        public static string CancelAppointmentUrl => $"{BaseUrl}{CancelAppointment}";
        public static string GetScoreDetailsUrl => $"{BaseUrl}{GetScoreDetails}";
        public static string GetSectionMobileUrl => $"{BaseUrl}{GetSectionMobile}";
        public static string GetSingleSectionMobileUrl => $"{BaseUrl}{GetSingleSectionMobile}";
        public static string GetTrackersMobileUrl => $"{BaseUrl}{GetTrackersMobile}";
        public static string GetParticipantProgramsMobileUrl => $"{BaseUrl}{GetParticipantProgramsMobile}";
        public static string SaveParticipantInfoMobileUrl => $"{BaseUrl}{SaveParticipantInfoMobile}";
        public static string GetParticipantInfoMobileUrl => $"{BaseUrl}{GetParticipantInfoMobile}";
        public static string SetParticipantPwUrl => $"{BaseUrl}{SetParticipantpw}";
        public static string SaveTrackersUrl => $"{BaseUrl}{SaveTrackers}";
        public static string DeleteTrackerUrl => $"{BaseUrl}{DeleteTracker}";

        public static readonly Dictionary<string, string> listStates =  new Dictionary<string, string>
            {
                {"Alabama", "AL"},
                {"Alaska", "AK"},
                {"Arizona", "AZ"},
                {"Arkansas", "AR"},
                {"California", "CA"},
                {"Colorado", "CO"},
                {"Connecticut", "CT"},
                {"Delaware", "DE"},
                {"Florida", "FL"},
                {"Georgia", "GA"},
                {"Hawaii", "HI"},
                {"Idaho", "ID"},
                {"Illinois", "IL"},
                {"Indiana", "IN"},
                {"Iowa", "IA"},
                {"Kansas", "KS"},
                {"Kentucky", "KY"},
                {"Louisiana", "LA"},
                {"Maine", "ME"},
                {"Maryland", "MD"},
                {"Massachusetts", "MA"},
                {"Michigan", "MI"},
                {"Minnesota", "MN"},
                {"Mississippi", "MS"},
                {"Missouri", "MO"},
                {"Montana", "MT"},
                {"Nebraska", "NE"},
                {"Nevada", "NV"},
                {"New Hampshire", "NH"},
                {"New Jersey", "NJ"},
                {"New Mexico", "NM"},
                {"New York", "NY"},
                {"North Carolina", "NC"},
                {"North Dakota", "ND"},
                {"Ohio", "OH"},
                {"Oklahoma", "OK"},
                {"Oregon", "OR"},
                {"Pennsylvania", "PA"},
                {"Rhode Island", "RI"},
                {"South Carolina", "SC"},
                {"South Dakota", "SD"},
                {"Tennessee", "TN"},
                {"Texas", "TX"},
                {"Utah", "UT"},
                {"Vermont", "VT"},
                {"Virginia", "VA"},
                {"Washington", "WA"},
                {"West Virginia", "WV"},
                {"Wisconsin", "WI"},
                {"Wyoming", "WY"}
            };
    }
}

