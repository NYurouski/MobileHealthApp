using System;
using System.Collections.Generic;
using System.Reflection;
using oc.Source.Models.ViewModel;
using SVG.Forms.Plugin.Abstractions;
using Xamarin.Forms;

namespace oc.Source.Pages.ViewHelpers
{
    public partial class TrackersOverview : ContentView
    {
        public TrackersOverview()
        {
            InitializeComponent();
            BiometricsImage.Children.Add(new SvgImage
            {
                SvgPath = "oc.Source.Images.biometrics.svg",
                SvgAssembly = typeof(App).GetTypeInfo().Assembly,
                HeightRequest = 50,
                WidthRequest = 50,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
            });
            DevicesImage.Children.Add(new SvgImage
            {
                SvgPath = "oc.Source.Images.devices.svg",
                SvgAssembly = typeof(App).GetTypeInfo().Assembly,
                HeightRequest = 60,
                WidthRequest = 60,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
            });
            ProgramImage.Children.Add(new SvgImage
            {
                SvgPath = "oc.Source.Images.program.svg",
                SvgAssembly = typeof(App).GetTypeInfo().Assembly,
                HeightRequest = 50,
                WidthRequest = 50,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
            });
        }

        private void BiometrickOnTapped(object sender, EventArgs e)
        {
            MessagingCenter.Send<TrackersOverview>(this, "Biometrics");
        }

        private void DevicesOnTapped(object sender, EventArgs e)
        {
            MessagingCenter.Send<TrackersOverview>(this, "Devices");
        }

        private void ProgramOnTapped(object sender, EventArgs e)
        {
            MessagingCenter.Send<TrackersOverview>(this, "Program");
        }
    }
}
