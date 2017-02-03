using System;
using Acr.UserDialogs;
using Xamarin.Forms;


namespace oc
{
	public partial class RegistrationPageFourStep : ContentPage
	{
		private int restrictCount = 15; 



		public RegistrationPageFourStep()
		{
			InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, true);
            Title = "Registration Step 4";
            NavigationPage.SetBackButtonTitle(this, "Back");
          
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

