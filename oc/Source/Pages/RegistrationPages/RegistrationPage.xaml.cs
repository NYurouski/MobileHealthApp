using System;
using Acr.UserDialogs;
using oc.Source.Constants;
using oc.Source.Models;
using Xamarin.Forms;


namespace oc
{
	public partial class RegistrationPage : ContentPage
	{
		private int restrictCount = 15;
		private ConfirmPasswordBehavior passwordValidator;


		public RegistrationPage()
		{
			InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, true);
            Title = "Registration";
            NavigationPage.SetBackButtonTitle(this, "Back");
            InitViews();
		}

		private void InitViews()
		{

		    ContinueToFirstStepButton.Clicked += async (sender, args) =>
		    {
                UserDialogs.Instance.ShowLoading("loading");
                var response = await ApiService.Instance.GetDataFromServerTask<RegistrationRequestModel, LogInResponseModel>(new RegistrationRequestModel()
                {
                   InvitationCode = InvitationCodeEntry.Text
                }, ApiConstants.RegistrationUrl);

                UserDialogs.Instance.HideLoading();
                await Navigation.PushAsync(new RegistrationPageFirstStep(), false);
		    };

		  
		}


		void OnTextChanged(object sender, EventArgs e)
		{
			var entry = sender as Entry;
			var val = entry.Text; 

			if (val.Length > restrictCount)
			{
				val = val.Remove(val.Length - 1);
				entry.Text = val; 
			}
		}
	}
}

