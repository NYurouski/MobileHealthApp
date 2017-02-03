using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using oc.Source.Constants;
using oc.Source.Helpers;
using oc.Source.Models;
using oc.Source.Models.DataModel;
using Xamarin.Forms;

namespace oc.Source.Pages.ViewHelpers
{
    public partial class InformationView : ContentView
    {
        private ProgramResponse Response { get; set; }
        private HomeDataResponseModel SaveResponse { get; set; }
        private PickersSettingsHelper pick;
        private SettingsDataRequestModel ParticipantDataResponse;
        public InformationView()
        {
            InitializeComponent();
        }

        public async Task InitPage()
        {

            ParticipantDataResponse =
                   await
                       ApiService.Instance.GetDataFromServerTask<ParticipantProgramRequestModel, SettingsDataRequestModel>(
                           new ParticipantProgramRequestModel()
                           {
                               ParticipantId = AppSettingHelper.ParticipantId

                           }, ApiConstants.GetParticipantInfoMobileUrl);
            var programId = AppSettingHelper.ProgramId;
            Response =
                    await
                        ApiService.Instance.GetDataFromServerTask<ParticipantProgramRequestModel, ProgramResponse>(
                            new ParticipantProgramRequestModel()
                            {
                                ParticipantId = AppSettingHelper.ParticipantId

                            }, ApiConstants.GetParticipantProgramsMobileUrl);

            pick = new PickersSettingsHelper { Model = ParticipantDataResponse };
            pick.Model.ParticipantId = AppSettingHelper.ParticipantId;
            pick.Program = Response.programs.FirstOrDefault(pro => pro.programid == programId).programname;
            foreach (var state in ApiConstants.listStates)
            {
                pick.StatesList.Add(state.Key);
            }

            foreach (var programname in Response.programs)
            {
                pick.ProgramNameList.Add(programname.programname);
            }
            BindingContext = pick;
            MainStackLayout.Opacity = 1;
        }

        private async void SaveInfoTapped(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(pick.Model.FirstName) || string.IsNullOrWhiteSpace(pick.Model.LastName) || string.IsNullOrWhiteSpace(pick.Model.Address) || string.IsNullOrWhiteSpace(pick.Model.City) || string.IsNullOrWhiteSpace(pick.Model.Date) || string.IsNullOrWhiteSpace(pick.Model.Zip) || string.IsNullOrWhiteSpace(pick.Model.Email))
            {
                MessagingCenter.Send<InformationView, AlertModel>(this, "StartAlert", new AlertModel
                {
                    Title = "Error",
                    Content = "Please fill in the remaining entries.",
                    AcceptMessage = "Ok"
                });
                return;
            }
            using (UserDialogs.Instance.Loading("Loading..."))
            {
                SaveResponse = await ApiService.Instance.GetDataFromServerTask<SettingsDataRequestModel, HomeDataResponseModel>(pick.Model, ApiConstants.SaveParticipantInfoMobileUrl);
            }
            
            MessagingCenter.Send<InformationView, AlertModel>(this, "StartAlert", new AlertModel
            {
                Title = SaveResponse.Success ? "Success" : "Error",
                Content = SaveResponse.Success ? "Information was saved" : SaveResponse.Error.Message,
                AcceptMessage = "Ok"
            });
        }
    }
}
