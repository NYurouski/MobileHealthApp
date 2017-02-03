using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using oc.Source.ApplicationSettings.Impl;
using oc.Source.ApplicationSettings.SettingsConstants;
using oc.Source.Constants;
using oc.Source.Models;
using Xamarin.Forms;

namespace oc.Source.Pages.ViewHelpers
{
    public partial class WelcomeLetterView : ContentView
    {
        public WelcomeLetterView()
        {
            InitializeComponent();
        }
         public async Task InitPage()
        {
            var response = await ApiService.Instance.GetDataFromServerTask<HomeDataRequestModel, WelcomeLetterResponseModel>(new HomeDataRequestModel()
            {
                ProgramId = AppSettingsSingleton.GetInstance().GetByKey(AppSettingName.ProgramId, string.Empty),
                ParticipantId = AppSettingsSingleton.GetInstance().GetByKey(AppSettingName.ParticipantId, string.Empty)

            }, ApiConstants.WelcomeLetterUrl);

            if (response != null)
            {
                var htmlSource = new HtmlWebViewSource { Html = response.WelcomeLetter };

                WelcomLetterWebView.Source = htmlSource;
                StackWebView.Opacity = 1;
            }
            
        }
    }
}
