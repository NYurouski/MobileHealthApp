using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace oc.Source.Helpers.UIHelpers.ControlsHelper
{
   public class ExtendedDatePicker : DatePicker
    {
        public float CornerRadiusExtended
        {
            get { return (float)GetValue(CornerRadiusExtendedProperty); }

            set { SetValue(CornerRadiusExtendedProperty, value); }
        }

        public static readonly BindableProperty CornerRadiusExtendedProperty = BindableProperty.Create("CornerRadiusExtended",
        typeof(float), typeof(ExtendedDatePicker), 0.0f);

        public static readonly BindableProperty BorderWidthExtendedProperty = BindableProperty.Create("BorderWidthExtended",
      typeof(int), typeof(ExtendedDatePicker), 0);


        public int BorderWidthExtended
        {
            get { return (int)GetValue(BorderWidthExtendedProperty); }

            set { SetValue(BorderWidthExtendedProperty, value); }
        }

        public static readonly BindableProperty BorderColorExtendedProperty = BindableProperty.Create("BorderColorExtended",
            typeof(Color), typeof(ExtendedDatePicker), Color.Black);


        public Color BorderColorExtended
        {
            get { return (Color)GetValue(BorderColorExtendedProperty); }

            set { SetValue(BorderColorExtendedProperty, value); }
        }
    }
}
