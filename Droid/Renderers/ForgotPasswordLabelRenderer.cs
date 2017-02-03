using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Util;
using oc.Droid.Renderers;
using oc.Source.Helpers.UIHelpers.ControlsHelper;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ForgotMyPasswordLabel), typeof(ForgotPasswordLabelRenderer))]
namespace oc.Droid.Renderers
{
    public class ForgotPasswordLabelRenderer : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            var view = (ForgotMyPasswordLabel)Element;
            var control = Control;

            UpdateUi(view, control);
        }

       
        static void UpdateUi(ForgotMyPasswordLabel view, TextView control)
        {
           
            if (view.IsUnderlined)
            {
               
                control.PaintFlags = control.PaintFlags | PaintFlags.UnderlineText | PaintFlags.FakeBoldText;
              
            }

        }
    }
}