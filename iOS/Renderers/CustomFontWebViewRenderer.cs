using Foundation;
using oc.iOS.Renderers;
using oc.Source.Helpers.UIHelpers.ControlsHelper;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
[assembly: Dependency(typeof(BaseUrl_iOS))]
[assembly: ExportRenderer(typeof(CustomFontWebView), typeof(CustomFontWebViewRenderer))]
namespace oc.iOS.Renderers
{
    public interface IBaseUrl { string Get(); }
    public class BaseUrl_iOS : IBaseUrl
    {
        public string Get()
        {
            return NSBundle.MainBundle.BundlePath;
        }
    }

    public class CustomFontWebViewRenderer : WebViewRenderer
        {
            protected override void OnElementChanged(VisualElementChangedEventArgs e)
            {
                base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                this.Delegate = new CustomFontUIWebViewDelegate(this,true);
            }

         
        }
        }

    public class CustomFontUIWebViewDelegate : UIWebViewDelegate
    {
        CustomFontWebViewRenderer webViewRenderer;
        private bool Redraw;

        public CustomFontUIWebViewDelegate(CustomFontWebViewRenderer _webViewRenderer, bool redraw)
        {
            webViewRenderer = _webViewRenderer ?? new CustomFontWebViewRenderer();
            Redraw = redraw;
        }

       
        public override async void LoadingFinished(UIWebView webView)
        {
            var wv = webViewRenderer.Element as CustomFontWebView;
            if (wv != null && Redraw)
            {
                Redraw = false;
                
                var oldHtml = (wv.Source as HtmlWebViewSource).Html;

                var htmlSource = new HtmlWebViewSource
                {
                    Html = "<style type=\"text/css\">" +
                          "p {" +
                          $"font-size: {wv.FontSizeExtended};" +
                          $"text-align: {wv.TextAlighmentExtended};" +
                          "}" +
                          "div {" +
                          $"font-size: {wv.FontSizeExtended};" +
                          $"text-align: {wv.TextAlighmentExtended};" +
                          "}" +
                          "ul {" +
                          $"font-size: {wv.FontSizeExtended};" +
                          $"text-align: {wv.TextAlighmentExtended}" +
                          "}" +
                          "li {" +
                          $"font-size: {wv.FontSizeExtended};" +
                          $"text-align: {wv.TextAlighmentExtended};" +
                          "}" +
                          "</style>" + oldHtml,
                    BaseUrl = DependencyService.Get<IBaseUrl>().Get()
                };
                wv.Source = htmlSource;
                webView.Opaque = false;
                webView.BackgroundColor = UIColor.Clear;
                await System.Threading.Tasks.Task.Delay(100); 
                wv.HeightRequest =  (double)webView.ScrollView.ContentSize.Height;

            }
        }
    }

}