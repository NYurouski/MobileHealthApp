using System;
using System.Threading.Tasks;
using Acr.UserDialogs;
using oc.Source.ApplicationSettings.SettingsConstants;
using oc.Source.Constants;
using oc.Source.Helpers;
using oc.Source.Models;
using Xamarin.Forms;
using AppSettings = oc.Source.ApplicationSettings.Impl.AppSettingsSingleton;

namespace oc.Source.Pages
{
    public partial class LoginInPage : ContentPage
    {
        public LoginInPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            InitViews();

        }


        void InitViews()
        {
            EmailAddressEntry.Text = AppSettingHelper.UserName;
        }

        protected override void OnAppearing()
        {
              
        }


        void OnForgotPasswordTapGestureRecognizerTapped(object sender, EventArgs args)
        {
            Navigation.PushAsync(new ForgotPasswordPage(), true);
        }

        
        private async void LoginButton_OnClicked(object sender, EventArgs e)
        {
            if (Utils.IsEmaiValid(EmailAddressEntry.Text))
            {
                await GetLogin();
            }
            else
            {
                await DisplayAlert("Error!", "Invalid email format", "OK");
            }
        }

        private async Task GetLogin()
        {
            UserDialogs.Instance.ShowLoading("loading");
            var response =
                    await
                        ApiService.Instance.GetDataFromServerTask<SignInRequestModel, LogInResponseModel>(
                            new SignInRequestModel()
                            {
                                UserName = EmailAddressEntry.Text,
                                Password = PasswordEntry.Text
                            }, ApiConstants.SignInUrl);
            UserDialogs.Instance.HideLoading();

            if (response == null)
                {
                    await DisplayAlert("Error", "User credentials are not correct ", "OK");

                }

                else if (response.success)
                {
                    AppSettingHelper.UserName = EmailAddressEntry.Text;
                    AppSettingHelper.ParticipantId = response.data.ParticipantId;
                    AppSettingHelper.ProgramId = response.data.ProgramId;

                    var mainPage = new Source.Pages.MainPage();
                    await mainPage.GetHomeView();
                    await Navigation.PushModalAsync(mainPage);

                }
                else
                {
                    await DisplayAlert("Error!", response.error.Message, "OK");

                }
          }
    }
}

