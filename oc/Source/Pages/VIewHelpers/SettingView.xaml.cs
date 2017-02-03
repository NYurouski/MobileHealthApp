using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace oc.Source.Pages.ViewHelpers
{
    public partial class SettingView : ContentView
    {
        public SettingView()
        {
            InitializeComponent();
        }

        private void WelcomeLetter_OnTapped(object sender, EventArgs e)
        {
            MessagingCenter.Send<SettingView>(this, "WelcomeLetter");
        }

        private void LogOut_OnTapped(object sender, EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new LoginInPage());
        }

        private void Help_OnTapped(object sender, EventArgs e)
        {
            MessagingCenter.Send<SettingView>(this, "HelpView");
        }

        private void Information_OnTapped(object sender, EventArgs e)
        {
            MessagingCenter.Send<SettingView>(this, "InformationView");
        }

        private void ChangePassword_OnTapped(object sender, EventArgs e)
        {
            MessagingCenter.Send<SettingView>(this, "ChangePassword");
        }
    }
}
