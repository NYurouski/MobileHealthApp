using System;
using System.Collections.Generic;
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


[assembly: ExportRenderer(typeof(CustomFontWebView), typeof(CustomFontWebViewRenderer))]

namespace oc.Droid.Renderers
{
    class CustomFontWebViewRenderer : WebViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<WebView> e)
        {
        
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                // lets get a reference to the native control
                var webView = (global::Android.Webkit.WebView) Control;
                
                // do whatever you want to the WebView here!
                //webView.VerticalScrollBarEnabled = false;
                //webView.VerticalScrollBarEnabled = false;
                //webView.SetInitialScale(-1);
            }
        }
    }
}