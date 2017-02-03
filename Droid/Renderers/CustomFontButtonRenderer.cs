using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using oc.Source.Helpers.UIHelpers.ControlsHelper;
using oc.Droid.Renderers;
using oc.Droid.Renderers.Extentions;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Button = Xamarin.Forms.Button;
using Color = Android.Graphics.Color;

[assembly: ExportRenderer(typeof(CustomFontButton), typeof(DroidCustomFontButtonRenderer))]

namespace oc.Droid.Renderers
{
    class DroidCustomFontButtonRenderer : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);


            if (Control != null)
            {
                var element = (CustomFontButton) Element;

                if (e.NewElement != null)
                {
                    e.NewElement.WidthRequest = element.WidthRequest;
                    e.NewElement.HeightRequest = element.HeightRequest;
                }

                var draw = new GradientDrawable();
                draw.SetColor(element.BackgroundColorExtended.ToAndroid());
                draw.SetStroke(element.BorderWidthExtended, element.BorderColorExtended.ToAndroid());
                draw.SetCornerRadius(element.CornerRadiusExtended);

                var sld = new StateListDrawable();
                sld.AddState(new int[] { }, draw);
                Control.SetBackground(sld);
                Control.SetAllCaps(false);
                Control.SetTextColor(element.TextColorExtended.ToAndroid());
             }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == CustomFontButton.VerticalContentAlignmentProperty.PropertyName ||
                e.PropertyName == CustomFontButton.HorizontalContentAlignmentProperty.PropertyName)
            {
                UpdateAlignment();
            }
          

            base.OnElementPropertyChanged(sender, e);
        }

        private void UpdateAlignment()
        {
            var element = this.Element as CustomFontButton;

            if (element == null || this.Control == null)
            {
                return;
            }

            this.Control.Gravity = element.VerticalContentAlignment.ToDroidVerticalGravity() |
                element.HorizontalContentAlignment.ToDroidHorizontalGravity();
        }
    }
}