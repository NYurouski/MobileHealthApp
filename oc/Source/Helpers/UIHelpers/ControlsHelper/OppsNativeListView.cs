using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using oc.Source.Models;
using Xamarin.Forms;

namespace oc.Source.Helpers.UIHelpers.ControlsHelper
{
    public class OppsNativeListView : ListView
    {
        public static readonly BindableProperty ItemsProperty =
            BindableProperty.Create("Items", typeof(IEnumerable<OpportunitiesModel>), typeof(OppsNativeListView), new List<OpportunitiesModel>());

     
        public IEnumerable<OpportunitiesModel> Items
        {
            get { return (IEnumerable<OpportunitiesModel>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

    
    }
}
