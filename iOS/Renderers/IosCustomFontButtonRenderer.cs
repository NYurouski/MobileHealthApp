using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using oc.iOS.Renderers;
using oc.iOS.Renderers.Extentions;
using oc.Source.Helpers.UIHelpers.ControlsHelper;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomFontButton), typeof(IosCustomFontButtonRenderer))]
namespace oc.iOS.Renderers
{
    class IosCustomFontButtonRenderer : ButtonRenderer
    {
        private UIButton _newButton;
        private UIButton _oldButton;

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            var element = (CustomFontButton)Element;
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                e.NewElement.WidthRequest = element.WidthRequest;
                e.NewElement.HeightRequest = element.HeightRequest;
            }
            if (e.OldElement == null)
            {
                _oldButton = (UIButton)Control;
                _oldButton.Bounds = new CGRect(element.X, element.Y, element.WidthRequest, element.HeightRequest);
            }
        }

        private void DrawButton(UIButton button)
        {
            var element = (CustomFontButton)Element;
            var btnGradient = new CAGradientLayer
            {
                Frame = button.Bounds,
                Colors = new CGColor[] { element.BackgroundColorExtended.ToCGColor(), element.BackgroundColorExtended.ToCGColor() }
            };
            button.Layer.InsertSublayer(btnGradient, 0);
            button.Layer.MasksToBounds = true;
            button.Layer.BorderColor = element.BorderColorExtended.ToCGColor();
            button.Layer.BorderWidth = element.BorderWidthExtended;
            button.Layer.CornerRadius = element.CornerRadiusExtended;
            button.SetTitleColor(element.TextColorExtended.ToUIColor(), UIControlState.Normal);
        }
        
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            
            switch (e.PropertyName)
            {
                case "VerticalContentAlignment":
                    this.Control.VerticalAlignment = this.Element.VerticalContentAlignment.ToContentVerticalAlignment();
                    break;
                case "HorizontalContentAlignment":
                    this.Control.HorizontalAlignment = this.Element.HorizontalContentAlignment.ToContentHorizontalAlignment();
                    break;
                default:
                    base.OnElementPropertyChanged(sender, e);
                    break;
            }
        }

        public new CustomFontButton Element
        {
            get
            {
                return base.Element as CustomFontButton;
            }
        }



        public override void Draw(CGRect rect)
        {
            base.Draw(rect);
            DrawButton(_oldButton);
        }
    }
}
