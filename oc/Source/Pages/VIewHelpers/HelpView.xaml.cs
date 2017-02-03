using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using oc.Source.Helpers;
using Xamarin.Forms;

namespace oc.Source.Pages.ViewHelpers
{
    public partial class HelpView : ContentView
    {
        public HelpView()
        {
            InitializeComponent();
            UserEmail.Text = AppSettingHelper.UserName;
        }
    }
}
