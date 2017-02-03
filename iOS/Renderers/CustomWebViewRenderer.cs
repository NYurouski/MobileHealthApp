using oc.iOS.Renderers;
using oc.Source.Helpers.UIHelpers.ControlsHelper;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomWebView), typeof(CustomWebViewRenderer))]
namespace oc.iOS.Renderers
{
  

  
        public class CustomWebViewRenderer : WebViewRenderer
        {
            protected override void OnElementChanged(VisualElementChangedEventArgs e)
            {
                base.OnElementChanged(e);
                Delegate = new CustomUIWebViewDelegate(this);
            }
        }

    public class CustomUIWebViewDelegate : UIWebViewDelegate
    {
        CustomWebViewRenderer webViewRenderer;

        public CustomUIWebViewDelegate(CustomWebViewRenderer _webViewRenderer = null)
        {
            webViewRenderer = _webViewRenderer ?? new CustomWebViewRenderer();
        }

        public override async void LoadingFinished(UIWebView webView)
        {
            var wv = webViewRenderer.Element as CustomWebView;
            if (wv != null)
            {
                await System.Threading.Tasks.Task.Delay(100); // wait here till content is rendered

                wv.HeightRequest = (double)webView.ScrollView.ContentSize.Height;

            }
        }
    }

}