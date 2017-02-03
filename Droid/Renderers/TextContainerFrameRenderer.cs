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
using Java.Util;
using oc.Droid.Renderers;
using oc.Source.Helpers.UIHelpers.ControlsHelper;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Color = Android.Graphics.Color;

[assembly: ExportRenderer(typeof(TextContainerFrame), typeof(TextContainerFrameRenderer))]
namespace oc.Droid.Renderers
{
    public class TextContainerFrameRenderer : FrameRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);
            var element = (TextContainerFrame)Element;
            var draw = new GradientDrawable();
            draw.SetColor(element.BackgroundColorExtended.MultiplyAlpha(element.OpasityColorExtended).ToAndroid());
            draw.SetStroke(element.BorderWidthExtended, element.BorderColorExtended.ToAndroid());
            draw.SetCornerRadius(element.CornerRadiusExtended);
           
            var sld = new StateListDrawable();
            sld.AddState(new int[] { }, draw);
            SetBackgroundDrawable(sld);

         }
        
    }
}