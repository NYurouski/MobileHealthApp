using System;
using oc.Source.ApplicationSettings.SettingsConstants;
using oc.Source.Helpers;
using oc.Source.Helpers.UIHelpers.ControlsHelper;
using oc.Source.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppSettings = oc.Source.ApplicationSettings.Impl.AppSettingsSingleton;

namespace oc
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Application.Current.MainPage = new NavigationPage(new LoginInPage());
        }
        protected override void OnStart()
        {
           
        }

        protected override void OnSleep()
        {
            AppSettingHelper.SaveCurrentTime();
        }

        protected override void OnResume()
        {
            if (AppSettingHelper.IsSleepTimeLessThan15Minutes) return;
            App.Current.MainPage = new NavigationPage(new Source.Pages.LoginInPage());
        }
    }
}