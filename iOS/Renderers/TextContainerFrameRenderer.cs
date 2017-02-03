using System;
using CoreGraphics;
using oc.iOS.Renderers;
using oc.Source.Helpers.UIHelpers.ControlsHelper;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(TextContainerFrame), typeof(TextContainerFrameRenderer))]
namespace oc.iOS.Renderers
{
    public class TextContainerFrameRenderer : FrameRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);

            var element = (TextContainerFrame)Element;

            if (element != null)
            {
                this.Layer.CornerRadius = element.CornerRadiusExtended;
                this.Layer.Bounds.Inset(1, 1);
                Layer.BorderColor = element.BorderColorExtended.ToCGColor();
                Layer.BorderWidth = (float)element.BorderWidthExtended;
                Layer.BackgroundColor = element.BackgroundColorExtended.MultiplyAlpha(element.OpasityColorExtended).ToCGColor();
            }
         }
    }
}