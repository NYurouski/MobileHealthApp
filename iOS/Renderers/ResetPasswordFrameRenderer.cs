using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using CoreGraphics;
using oc.Droid.Renderers;
using oc.Source.Helpers.UIHelpers.ControlsHelper;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;


[assembly: ExportRenderer(typeof(ResetPasswordFrame), typeof(ResetPasswordFrameRenderer))]
namespace oc.Droid.Renderers
{
    public class ResetPasswordFrameRenderer : FrameRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);

         
            this.Layer.CornerRadius = 10;
            this.Layer.Bounds.Inset(1, 1);
            Layer.BorderColor = UIColor.White.CGColor;
            Layer.BorderWidth = (float)0.5;
            Layer.BackgroundColor=new CGColor(255,255,255,(nfloat) 0.5);

        }
    }
}