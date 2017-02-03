using System;
using Acr.UserDialogs;
using Xamarin.Forms;


namespace oc
{
	public partial class RegistrationPageFirstStep : ContentPage
	{
		private int restrictCount = 15; 



		public RegistrationPageFirstStep()
		{
			InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, true);
            Title = "Registration Step 1";
            NavigationPage.SetBackButtonTitle(this, "Back");
            InitViews();
		}

		private void InitViews()
		{

		    ContinueToSecondStepButton.Clicked += (sender, args) =>
		    {
		        Navigation.PushAsync(new RegistrationPageSecondStep(), false);
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

