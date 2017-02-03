using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using oc.iOS.Renderers;
using oc.Source.Helpers.UIHelpers.ControlsHelper;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ExtendedPicker), typeof(CustomPickerRender))]

namespace oc.iOS.Renderers
{
    class CustomPickerRender : PickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            var element = (ExtendedPicker)Element;


            base.OnElementChanged(e);
            if (Control != null)
            {
                
                if (element != null)
                {
                    Control.Layer.BorderColor = element.BorderColorExtended.ToCGColor();
                    Control.Layer.BorderWidth = element.BorderWidthExtended;
                    var cornerRadius = element.CornerRadiusExtended;
                    if (cornerRadius == 0.0f)
                    {
                        Control.BorderStyle = UITextBorderStyle.Line;
                        Control.Layer.BackgroundColor = Color.White.ToCGColor();
                        Control.LeftView = new UIView(new CGRect(0, 0, 5, 0));
                        Control.LeftViewMode = UITextFieldViewMode.Always;
                    }
                    else
                    {
                        Control.Layer.CornerRadius = element.CornerRadiusExtended;
                    }

                }

                Control.Layer.SetNeedsDisplay();
            }
        
        }
    }
}