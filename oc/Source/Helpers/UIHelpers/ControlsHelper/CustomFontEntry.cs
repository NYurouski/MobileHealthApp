using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace oc.Source.Helpers.UIHelpers.ControlsHelper
{
    public class CustomFontEntry : Entry
    {
        public static readonly BindableProperty BackgroundColorExtendedProperty =
             BindableProperty.Create("BackgroundColorExtended", typeof(Color), typeof(CustomFontEntry), Color.Black);
        
        public Color BackgroundColorExtended
        {
            get { return (Color)GetValue(BackgroundColorExtendedProperty); }

            set { SetValue(BackgroundColorExtendedProperty, value); }
        }
        
        public static readonly BindableProperty TextColorExtendedProperty = BindableProperty.Create("TextColorExtended",
            typeof(Color), typeof(CustomFontEntry), Color.Black);


        public Color TextColorExtended
        {
            get { return (Color)GetValue(TextColorExtendedProperty); }

            set { SetValue(TextColorExtendedProperty, value); }
        }
        
        public static readonly BindableProperty BorderWidthExtendedProperty = BindableProperty.Create("BorderWidthExtended",
      typeof(int), typeof(CustomFontEntry), 0);


        public int BorderWidthExtended
        {
            get { return (int)GetValue(BorderWidthExtendedProperty); }

            set { SetValue(BorderWidthExtendedProperty, value); }
        }
        
        public static readonly BindableProperty BorderColorExtendedProperty = BindableProperty.Create("BorderColorExtended",
            typeof(Color), typeof(CustomFontEntry), Color.Black);


        public Color BorderColorExtended
        {
            get { return (Color)GetValue(BorderColorExtendedProperty); }

            set { SetValue(BorderColorExtendedProperty, value); }
        }
        
        public static readonly BindableProperty CornerRadiusExtendedProperty = BindableProperty.Create("CornerRadiusExtended",
   typeof(float), typeof(CustomFontEntry), 0.0f);


        public float CornerRadiusExtended
        {
            get { return (float)GetValue(CornerRadiusExtendedProperty); }

            set { SetValue(CornerRadiusExtendedProperty, value); }
        }
        
        public static readonly BindableProperty PlaceHolderColorExtendedProperty = BindableProperty.Create("PlaceHolderColorExtended",
   typeof(Color), typeof(CustomFontEntry), Color.Black);


        public Color PlaceHolderColorExtended
        {
            get { return (Color)GetValue(PlaceHolderColorExtendedProperty); }

            set { SetValue(PlaceHolderColorExtendedProperty, value); }
        }
    }
}
