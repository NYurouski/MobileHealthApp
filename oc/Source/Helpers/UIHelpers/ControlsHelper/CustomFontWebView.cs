using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace oc.Source.Helpers.UIHelpers.ControlsHelper
{
    public class CustomFontWebView : WebView
    {
        public static readonly BindableProperty FontSizeProperty =
           BindableProperty.Create("FontSizeExtended", typeof(string), typeof(CustomFontWebView), "medium");


        public string FontSizeExtended
        {
            get { return (string)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }

        private static readonly BindableProperty TextAlighmentProperty =
           BindableProperty.Create("TextAlighmentExtended", typeof(string), typeof(CustomFontWebView), "left");


        public string TextAlighmentExtended
        {
            get { return (string)GetValue(TextAlighmentProperty); }
            set { SetValue(TextAlighmentProperty, value); }
        }
    }
}
