using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using oc.Source.ApplicationSettings.SettingsConstants;
using oc.Source.Constants;
using oc.Source.Helpers;
using oc.Source.Models;
using oc.Source.Models.DataModel;
using Xamarin.Forms;

namespace oc.Source.Pages.ViewHelpers
{
    public partial class ChangePasswordView : ContentView
    {
        private const string Key = "abc123";
        private HomeDataResponseModel SaveResponse { get; set; }
        public ChangePasswordView()
        {
            InitializeComponent();
            EmailLabel.Text = AppSettingHelper.UserName;
        }
        private async void SaveButton_onTapped(object sender, EventArgs e)
        {
            var password = string.Empty;
            if (NewPasswordEntry.Text == ReEnterPassEntry.Text)
            {
                password = NewPasswordEntry.Text;
            }
            else
            {
                MessagingCenter.Send<ChangePasswordView, AlertModel>(this, "StartAlertCp", new AlertModel
                {
                    Title = "Error",
                    Content = "Passwords don't match!",
                    AcceptMessage = "OK"
                });
                return;
            }

            SaveResponse =
                    await
                        ApiService.Instance.GetDataFromServerTask<ParticipantPwChangeRequest, HomeDataResponseModel>(new ParticipantPwChangeRequest()
                        {
                            ParticipantId = AppSettingHelper.ParticipantId,
                            Key = Key,
                            NewPassword = password,
                            CurrentPassword = PasswordEntry.Text

                        }, ApiConstants.SetParticipantPwUrl);

            MessagingCenter.Send<ChangePasswordView, AlertModel>(this, "StartAlertCp", new AlertModel
            {
                Title = SaveResponse.Success ? "Success" : "Error",
                Content = SaveResponse.Success ? "Password was changed" : SaveResponse.Error.Message,
                AcceptMessage = "OK"
            });
        }
    }
}
