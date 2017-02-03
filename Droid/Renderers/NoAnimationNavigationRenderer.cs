using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using oc.Droid.Renderers;
using oc.Source.Helpers.UIHelpers.ControlsHelper;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

//[assembly: ExportRenderer(typeof(NavigationPage), typeof(NoAnimationNavigationRenderer))]

namespace oc.Droid.Renderers
{
    public class NoAnimationNavigationRenderer : NavigationRenderer
    {
        public NoAnimationNavigationRenderer() : base()
        {
        }

        protected override Task<bool> OnPopViewAsync(Page page, bool animated)
        {
            return base.OnPopViewAsync(page, false);
        }

        protected override Task<bool> OnPushAsync(Page view, bool animated)
        {
            Page p = view;
            return base.OnPushAsync((Page)p, false);
        }

        protected override Task<bool> OnPopToRootAsync(Page page, bool animated)
        {
            return base.OnPopToRootAsync(page, false);
        }

    }
}