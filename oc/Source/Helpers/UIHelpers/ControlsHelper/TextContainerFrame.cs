using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace oc.Source.Helpers.UIHelpers.ControlsHelper
{
    public class TextContainerFrame : Frame
        {
        public static readonly BindableProperty BackgroundColorProperty =
            BindableProperty.Create("BackgroundColorProperty", typeof(Color), typeof(CustomFontButton), Color.Black);



        public Color BackgroundColorExtended
        {
            get { return (Color)GetValue(BackgroundColorProperty); }

            set { SetValue(BackgroundColorProperty, value); }
        }



        public static readonly BindableProperty BorderWidthProperty = BindableProperty.Create("BorderWidthProperty",
 typeof(int), typeof(CustomFontButton), 0);


        public int BorderWidthExtended
        {
            get { return (int)GetValue(BorderWidthProperty); }

            set { SetValue(BorderWidthProperty, value); }
        }

        
        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create("BorderColorProperty",
            typeof(Color), typeof(CustomFontButton), Color.Black);


        public Color BorderColorExtended
        {
            get { return (Color)GetValue(BorderColorProperty); }

            set { SetValue(BorderColorProperty, value); }
        }


        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create("CornerRadiusProperty",
  typeof(float), typeof(CustomFontButton), 0.0f);


        public float CornerRadiusExtended
        {
            get { return (float)GetValue(CornerRadiusProperty); }

            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly BindableProperty OpasityColorProperty = BindableProperty.Create("OpasityColorProperty", typeof(float), typeof(CustomFontButton), 1.0f);

        public float OpasityColorExtended
        {
            get { return (float)GetValue(OpasityColorProperty); }

            set { SetValue(OpasityColorProperty, value); }
        }
    }

}
