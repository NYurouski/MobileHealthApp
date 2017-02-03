using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using oc.Droid.Renderers;
using oc.Source.Helpers.UIHelpers.ControlsHelper;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


[assembly: ExportRenderer(typeof(CustomFontEntry), typeof(CustomEntryRenderer))]
namespace oc.Droid.Renderers
{
    public class CustomEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            var element = (CustomFontEntry) Element;

            base.OnElementChanged( e);
            if (Control != null)
            {
                if (e.NewElement != null)
                {
                    e.NewElement.PlaceholderColor=element.PlaceHolderColorExtended;
                }
                var customBg = new GradientDrawable();
                customBg.SetColor(element.BackgroundColorExtended.ToAndroid());
                customBg.SetCornerRadius(element.CornerRadiusExtended);
                
                customBg.SetStroke(element.BorderWidthExtended, element.BorderColorExtended.ToAndroid());
                this.Control.SetBackground(customBg);
            }
        }
    }
}