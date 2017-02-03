using System.ComponentModel;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using oc.iOS.Renderers;
using oc.Source.Helpers.UIHelpers.ControlsHelper;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomFontEntry), typeof(CustomEntryRenderer))]

namespace oc.iOS.Renderers
{
    public class CustomEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            var element = (CustomFontEntry)Element;


            base.OnElementChanged(e);
            if (Control != null)
            {
                var btnGradient = new CAGradientLayer
                {
                    Frame = Control.Bounds,
                    Colors = new CGColor[] { Color.Blue.ToCGColor() },
                    Locations = new NSNumber[] { 0.0f, 0.1f }
                };


                Control.Layer.AddSublayer(btnGradient);
                Control.Layer.MasksToBounds = true;
                if (element != null)
                {


                    Control.Layer.BorderColor = element.BorderColorExtended.ToCGColor();
                    Control.Layer.BorderWidth = element.BorderWidthExtended;
                    var cornerRadius = element.CornerRadiusExtended;
                    if (cornerRadius == 0.0f)
                    {
                        Control.BorderStyle = UITextBorderStyle.None;
                        Control.Layer.BackgroundColor = Color.White.ToCGColor();
                        Control.LeftView = new UIView(new CGRect(0, 0, 5, 0));
                        Control.LeftViewMode = UITextFieldViewMode.Always;
                    }
                    else
                    {
                        Control.Layer.CornerRadius = element.CornerRadiusExtended;
                    }

                }
                if (e.NewElement != null)
                    e.NewElement.PlaceholderColor = element.PlaceHolderColorExtended;


                Control.Layer.SetNeedsDisplay();
            }
        }
    }
}