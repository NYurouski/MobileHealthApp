using System;
using System.Linq;
using CoreAnimation;
using CoreGraphics;
using oc.iOS.Renderers;
using oc.Source.Helpers.UIHelpers.ControlsHelper;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(GradientFrame), typeof(GradientRender))]
namespace oc.iOS.Renderers
{
    public class GradientRender : FrameRenderer
    {
        public override CGRect Frame
        {
            get
            {
                return base.Frame;
            }
            set
            {
                if (value.Width > 0 && value.Height > 0)
                {
                    foreach (var layer in Layer.Sublayers.Where(layer => layer is CAGradientLayer))
                        layer.Frame = new CGRect(0, 0, value.Width, value.Height);
                }
                base.Frame = value;
            }
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);

            var element = (GradientFrame)Element;

            if (element != null)
            {
                var gradient = new CAGradientLayer();
                gradient.CornerRadius = Layer.CornerRadius = element.CornerRadiusExtended;
                gradient.Colors = new CGColor[]
                {
                    element.FirstColorExtended.ToCGColor(),
                    element.SecondColorExtended.ToCGColor()
                };
                var layer = Layer.Sublayers.LastOrDefault();
                Layer.InsertSublayerBelow(gradient, layer);
            }
         }
    }
}