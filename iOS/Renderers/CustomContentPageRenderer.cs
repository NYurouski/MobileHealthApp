using System;
using oc.iOS.Renderers;
using oc.Source.Pages;
using Xamarin.Forms;

using Xamarin.Forms.Platform.iOS;
using UIKit;

[assembly: ExportRenderer(typeof(ContentPage), typeof(CustomContentPageRenderer))]
namespace oc.iOS.Renderers
{
    class CustomContentPageRenderer : PageRenderer
    {
        public CustomContentPageRenderer()
        {
        }

        public override void WillMoveToParentViewController(UIViewController parent)
        {
            base.WillMoveToParentViewController(parent);

            var page = (ContentPage)Element;

        }
    }
}