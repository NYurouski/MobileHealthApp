using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace oc.Source.Helpers.UIHelpers.ControlsHelper
{
   public class GradientFrame : TextContainerFrame
    {
        public static readonly BindableProperty FirstColorProperty =
           BindableProperty.Create("FirstColorProperty", typeof(Color), typeof(GradientFrame), Color.Black);



        public Color FirstColorExtended
        {
            get { return (Color)GetValue(FirstColorProperty); }

            set { SetValue(FirstColorProperty, value); }
        }
        
        public static readonly BindableProperty SecondColorProperty =
          BindableProperty.Create("SecondColorProperty", typeof(Color), typeof(GradientFrame), Color.Black);


        public Color SecondColorExtended
        {
            get { return (Color)GetValue(SecondColorProperty); }

            set { SetValue(SecondColorProperty, value); }
        }

    }
}
