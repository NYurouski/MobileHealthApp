using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace oc.Source.Helpers.UIHelpers.ControlsHelper
{
    public class ForgotMyPasswordLabel : Label
        {
        public static readonly BindableProperty IsUnderlineProperty = BindableProperty.Create("IsUnderlineProperty",
            typeof (bool), typeof (ForgotMyPasswordLabel), true);
            public bool IsUnderlined
            {
                get { return (bool)GetValue(IsUnderlineProperty); }
                set { SetValue(IsUnderlineProperty, value); }
            }

        }

}
