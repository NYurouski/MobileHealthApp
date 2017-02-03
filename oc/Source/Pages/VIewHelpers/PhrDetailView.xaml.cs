using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using oc.Source.ApplicationSettings.Impl;
using oc.Source.ApplicationSettings.SettingsConstants;
using oc.Source.Constants;
using oc.Source.Models;
using oc.Source.Models.DataModel;
using Xamarin.Forms;

namespace oc.Source.Pages.ViewHelpers
{
    public partial class PhrDetailView : ContentView
    {
        private PhrSectionResponseModel ResponseContent { get; set; }
        private Chapter Section { get; set; }
        public PhrDetailView(Chapter chapter)
        {
            Section = chapter;
            InitializeComponent();
        }
        public async Task InitHealthReportSectionPage()
        {
            ResponseContent = await ApiService.Instance.GetDataFromServerTask<PhrRequestModel, PhrSectionResponseModel>(new PhrRequestModel()
            {
                ProgramId = AppSettingsSingleton.GetInstance().GetByKey(AppSettingName.ProgramId, string.Empty),
                ParticipantId =
                AppSettingsSingleton.GetInstance().GetByKey(AppSettingName.ParticipantId, string.Empty),
                SectionId = Section.Id
            }, ApiConstants.GetSingleSectionMobileUrl);
            HealthReportContentView.Source = new HtmlWebViewSource { Html = ResponseContent.Html };
        }
    }
}
