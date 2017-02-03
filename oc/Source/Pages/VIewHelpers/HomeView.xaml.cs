using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using oc.Source.Constants;
using oc.Source.Helpers;
using oc.Source.Models;
using Xamarin.Forms;

namespace oc.Source.Pages.ViewHelpers
{
    public partial class HomeView : ContentView
    {
        private HomeDataResponseModel response;
        public HomeView(HomeDataResponseModel response)
        {
            InitializeComponent();
            this.response = response;
        }
        public void InitPage()
        {
            WelcomeLetterLayout.IsVisible = !string.IsNullOrEmpty(response.ResponseData.WelcomeLetter);
            WelcomLetterWebView.Source = new HtmlWebViewSource { Html = response.ResponseData.WelcomeLetter }; ;


            if (response.ResponseData.MyEvents != null && response.ResponseData.MyEvents.Length > 0)
            {
                MyEventsLayout.IsVisible = true;

                if (response.ResponseData.MyEvents.Length > 0)
                {
                    MyEventsOneRegistrationStatusTextLabel.Text = response.ResponseData.MyEvents[0].RegistrationStatus;
                    MyEventsOneRegistrationStatusTextLabel.TextColor = MyEventsOneRegistrationStatusTextLabel.Text == "Registered" ? Color.FromHex("#63934e") : Color.FromHex("#aa0000");
                    MyEventsOneRegistrationEventNameTextLabel.Text = response.ResponseData.MyEvents[0].EventName;
                    MyEventsOneRegistrationDateTextLabel.IsVisible = response.ResponseData.MyEvents[0].RegistrationDate != "NA";
                    MyEventsOneRegistrationDateTextLabel.Text = response.ResponseData.MyEvents[0].RegistrationDate;

                }

                if (response.ResponseData.MyEvents.Length > 1)
                {
                    MyEventsTwoRegistrationStatusTextLabel.Text = response.ResponseData.MyEvents[1].RegistrationStatus;
                    MyEventsTwoRegistrationStatusTextLabel.TextColor = MyEventsOneRegistrationStatusTextLabel.Text == "Registered" ? Color.FromHex("#63934e") : Color.FromHex("#aa0000");

                    MyEventsTwoRegistrationEventNameTextLabel.Text = response.ResponseData.MyEvents[1].EventName;
                    MyEventsTwoRegistrationDateTextLabel.IsVisible = response.ResponseData.MyEvents[1].RegistrationDate != "NA";
                    MyEventsTwoRegistrationDateTextLabel.Text = response.ResponseData.MyEvents[1].RegistrationDate;
                }
                else
                {
                    MyEventsSecondLayout.IsVisible = false;
                }
            }
            else
            {
                MyEventsLayout.IsVisible = false;
            }


            if (response.ResponseData.MyDailyTips != null)
            {

                if (DialyTipTitle != null)
                {
                    DialyTipTitle.Text = response.ResponseData.MyDailyTips.Title;

                }

                if (DialyTipText != null)
                {
                    DialyTipText.Text = response.ResponseData.MyDailyTips.Text;

                }
            }
            else
            {
                DailyTipsContainLayout.IsVisible = false;
            }

            if (response.ResponseData.MyStatus != null)
            {


                var surveyStatus = int.Parse(response.ResponseData.MyStatus.HasSurvey) == 1;

                var surveyImageName = surveyStatus ? "greencirclecheck.png" : "greencirclex.png";



                var BiometricAssessmentStatus = int.Parse(response.ResponseData.MyStatus.HasSurvey) == 1;

                var BiometricAssessmentImageName = BiometricAssessmentStatus ? "greencirclecheck.png" : "greencirclex.png";


                BiometricAssessmentImageChecked.IsVisible = BiometricAssessmentStatus;
                BiometricAssessmentImage.IsVisible = !BiometricAssessmentStatus;



                SurveyImageEnabled.IsVisible = BiometricAssessmentStatus;
                SurveyImage.IsVisible = !BiometricAssessmentStatus;


                SurveyLabel.FormattedText = new FormattedString
                {
                    Spans =
                    {
                        new Span() {Text = "Survey: ", ForegroundColor = Color.White, FontAttributes = FontAttributes.Bold, FontSize = 16},
                        new Span() { Text = surveyStatus ? "Your health survey has been received." : "Your health survey has not been received."}
                    }
                };

                BiometricAssessmentLabel.FormattedText = new FormattedString
                {
                    Spans =
                    {
                        new Span() { Text = "Biometric Assessment: ", ForegroundColor = Color.White, FontAttributes = FontAttributes.Bold, FontSize = 16 },
                        new Span() { Text = surveyStatus ? "Your biometric assessment has been received." : "Your biometric assessment has not been received." }
                    }
                };


            }
            else
            {
                StatusStackLayout.IsVisible = false;
            }


            if (response.ResponseData.MyHealthScore != null)
            {
                // OverAllScore
                var numberString = response.ResponseData.MyHealthScore.OverallScore;
                var numberInt = int.Parse(numberString);
                ScoreNumberLabel.Text = numberString;
                // Color
                var colorString = response.ResponseData.MyHealthScore.RiskColor?.ToString();
                if (colorString != null)
                {
                    ScoreTitleLabel.BackgroundColor = Color.FromHex(colorString);
                    ScoreNumberLabel.BackgroundColor = Color.FromHex(colorString);
                }
                // Risk label
                ScoreRiskLabel.Text = $"{response.ResponseData.MyHealthScore.RiskLevel?.ToString()} Risk";
                //Gender
                var gender = response.ResponseData.MyHealthScore.Gender?.ToString();
                var image = new Image
                {
                    BackgroundColor = Color.Transparent,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Start,
                    WidthRequest = 50,
                    HeightRequest = 30,
                    Source = ImageSource.FromResource(gender == "F" ? "oc.femaleicon.png" : "oc.maleicon.png")
                };
                const int column = 1;
                var row = 0;
                if (numberInt < 51)
                {
                    row = 4;
                }
                else if (numberInt < 61)
                {
                    row = 3;
                }
                else if (numberInt < 71)
                {
                    row = 2;
                }
                else if (numberInt < 86)
                {
                    row = 1;
                }
                HealthScoreStatusGrid.Children.Add(image, column, column + 2, row, row + 1);

            }
            else
            {
                MyHealthScoreLayout.IsVisible = false;
            }

            if (response.ResponseData.MyResultsMessage != null)
            {
                MyResultsLayout.IsVisible = true;
                MyResultFirstMessageText.Text = response.ResponseData.MyResultsMessage?.Msg1;
                MyResultSecondMessageText.Text = response.ResponseData.MyResultsMessage?.Msg2;
            }

            if (response.ResponseData.MyDailyTips != null)
            {
                MyDialyTipHeaderLabel.IsVisible = true;
                DialyTipTitle.Text = response.ResponseData.MyDailyTips.Title;
                DialyTipText.Text = response.ResponseData.MyDailyTips.Text;
            }
            if (response.ResponseData.MyHealthRisks != null)
            {

                BPSignCheckedImage.IsVisible = response.ResponseData.MyHealthRisks.BpSign == "green";
                BPSignRedImage.IsVisible = response.ResponseData.MyHealthRisks.BpSign == "red";
                BPSignUncheckedImage.IsVisible = response.ResponseData.MyHealthRisks.BpSign == "nodata";

                HDLSignCheckedImage.IsVisible = response.ResponseData.MyHealthRisks.HdlSign == "green";
                HDLSignRedImage.IsVisible = response.ResponseData.MyHealthRisks.HdlSign == "red";
                HDLSignUncheckedImage.IsVisible = response.ResponseData.MyHealthRisks.HdlSign == "nodata";


                TriglSignCheckedImage.IsVisible = response.ResponseData.MyHealthRisks.TriglSign == "green";
                TriglSignRedImage.IsVisible = response.ResponseData.MyHealthRisks.TriglSign == "red";
                TriglSignUncheckedImage.IsVisible = response.ResponseData.MyHealthRisks.TriglSign == "nodata";

                GlucoseSignCheckedImage.IsVisible = response.ResponseData.MyHealthRisks.GlucoseSign == "green";
                GlucoseSignRedImage.IsVisible = response.ResponseData.MyHealthRisks.GlucoseSign == "red";
                GlucoseSignUncheckedImage.IsVisible = response.ResponseData.MyHealthRisks.GlucoseSign == "nodata";

                BodySignCheckedImage.IsVisible = response.ResponseData.MyHealthRisks.BodySign == "green";
                BodySignRedImage.IsVisible = response.ResponseData.MyHealthRisks.BodySign == "red";
                BodySignUncheckedImage.IsVisible = response.ResponseData.MyHealthRisks.BodySign == "nodata";

                MyHealthRisksBPTextTextLabel.IsVisible = response.ResponseData.MyHealthRisks.BpSign != "nottested";
                MyHealthRisksBPTextTextLabel.Text = response.ResponseData.MyHealthRisks.BpText;


                MyHealthRisksHDLTextTextLabel.IsVisible = response.ResponseData.MyHealthRisks.HdlSign != "nottested";
                MyHealthRisksHDLTextTextLabel.Text = response.ResponseData.MyHealthRisks.HdlText;

                MyHealthRisksTriglTextTextLabel.IsVisible = response.ResponseData.MyHealthRisks.TriglSign != "nottested";
                MyHealthRisksTriglTextTextLabel.Text = response.ResponseData.MyHealthRisks.TriglText;

                MyHealthRisksGlucoseTextTextLabel.IsVisible = response.ResponseData.MyHealthRisks.GlucoseSign != "nottested";
                MyHealthRisksGlucoseTextTextLabel.Text = response.ResponseData.MyHealthRisks.GlucoseText;

                MyHealthRisksBodyTextTextLabel.IsVisible = response.ResponseData.MyHealthRisks.BodySign != "nottested";
                MyHealthRisksBodyTextTextLabel.Text = response.ResponseData.MyHealthRisks.BodyText;
            }
            else
            {
                MyHealthRisksLayout.IsVisible = false;
            }

            ShieldImage.IsVisible = false;
            MainLayout.Opacity = 1;
         }

        private void MyHealthRiskSummary_OnClicked(object sender, EventArgs e)
        {
           
        }

        private void ViewHealthScore_OnClicked(object sender, EventArgs e)
        {
           
        }

        private void ManageMyEvents_OnClicked(object sender, EventArgs e)
        {
           
        }

        private void MyHealthSurvey_OnClicked(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri(EnvironmentConstants.WebUrl));
        }
    }
}
