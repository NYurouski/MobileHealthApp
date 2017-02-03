using System;
using Acr.UserDialogs;
using oc.Source.Constants;
using oc.Source.Models;
using Xamarin.Forms;

namespace oc.Source.Pages
{
	public partial class ForgotPasswordPage : ContentPage
	{
		public ForgotPasswordPage()
		{
            InitializeComponent();
		    NavigationPage.SetHasNavigationBar(this, false);
			
		}
        
       
	    private async void Reset_OnClicked(object sender, EventArgs e)
	    {
	        var email = EmailAddressEntry.Text;
            if (string.IsNullOrEmpty(email))
	        {
                await DisplayAlert("Error!", "Email input is empty", "OK");
                return;
            }
            if (Utils.IsEmaiValid(email))
            {
                UserDialogs.Instance.ShowLoading("Loading");
                var response = await ApiService.Instance.GetDataFromServerTask<ForgotPasswordRequestModel, ForgotPassworResponseModel>(new ForgotPasswordRequestModel()
                {
                    UserName = EmailAddressEntry.Text
                }, ApiConstants.ForgotPasswordUrl);


                UserDialogs.Instance.HideLoading();

                if (response != null && response.Success)
                {

                    await DisplayAlert("Info!", "OneCommunity has sent a temporary password to your email address. Please follow the instructions in the email to login.", "OK");

                    await Navigation.PopToRootAsync();
                }
                else
                {
                    await DisplayAlert("Error!", response != null ? "Message : " + response.Error.ToString() : "Data error", "OK");
                }

            }
            else
            {
                await DisplayAlert("Error!", "Invalid email format", "OK");
            }
        }

	    private void OnForgotPasswordTapGestureRecognizerTapped(object sender, EventArgs e)
	    {
            Navigation.PopAsync();
        }
	}
}

