using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace oc.Source.Helpers.UIHelpers.ControlsHelper
{
    public class CustomFontButton : Button
    {
        public static readonly BindableProperty BackgroundColorProperty =
            BindableProperty.Create("BackgroundColorProperty", typeof (Color), typeof (CustomFontButton), Color.Black);
               
               

        public Color BackgroundColorExtended
        {
            get { return (Color)GetValue(BackgroundColorProperty); }

            set { SetValue(BackgroundColorProperty, value); }
        }



     
        public static readonly BindableProperty TextColorProperty = BindableProperty.Create("TextColorProperty",
            typeof(Color), typeof(CustomFontButton), Color.White);


        public Color TextColorExtended
        {
            get { return (Color)GetValue(TextColorProperty); }

            set { SetValue(TextColorProperty, value); }
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

        public static readonly BindableProperty VerticalContentAlignmentProperty =
            BindableProperty.Create("VerticalContentAlignmentProperty", typeof(TextAlignment), typeof(CustomFontButton), TextAlignment.Center);

        public static readonly BindableProperty HorizontalContentAlignmentProperty =
         BindableProperty.Create("HorizontalContentAlignmentProperty", typeof(TextAlignment), typeof(CustomFontButton), TextAlignment.Center);

        public TextAlignment VerticalContentAlignment
        {
            get { return (TextAlignment)GetValue(VerticalContentAlignmentProperty); }
            set { this.SetValue(VerticalContentAlignmentProperty, value); }
        }

        public TextAlignment HorizontalContentAlignment
        {
            get { return (TextAlignment)GetValue(HorizontalContentAlignmentProperty); }
            set { this.SetValue(HorizontalContentAlignmentProperty, value); }
        }
    }
}
