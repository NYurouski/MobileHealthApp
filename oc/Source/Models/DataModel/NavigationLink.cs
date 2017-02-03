using SVG.Forms.Plugin.Abstractions;
using Xamarin.Forms;

namespace oc.Source.Models.DataModel
{
    public class NavigationLink
    {
        private string _linkStatus;
        private StackLayout _mainStackLayout;
        private SvgImage _image;

        public StackLayout MainStackLayout
        {
            get
            {
                _mainStackLayout.IsEnabled = IsEnableStatus;
                return _mainStackLayout;
            }
            set { _mainStackLayout = value; }
        }

        public StackLayout ContentOfImage { get; set; }

        public SvgImage Image
        {
            get
            {
                _image.Opacity = OpacityStatus;
                return _image;
            }
            set { _image = value; }
        }

        public Label Label { get; set; }
        public string ActiveImageSource { get; set; }
        public string InactiveImageSource { get; set; }
        public string LinkStatus
        {
            get { return _linkStatus; }
            set
            {
                _linkStatus = value;
                if (bool.Parse(_linkStatus)) return;
                OpacityStatus = 0.4;
                IsEnableStatus = false;
            }
        }
        public double OpacityStatus { get; set; } = 1;
        public bool IsEnableStatus { get; set; } = true;
    }
}
