using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using oc.Source.Models;
using Xamarin.Forms;

namespace oc.Source.Helpers.UIHelpers.ControlsHelper
{
    public class NativeListView : ListView
    {
        public static readonly BindableProperty ItemsProperty =
            BindableProperty.Create("Items", typeof(IEnumerable<MyEventModel>), typeof(NativeListView), new List<MyEventModel>());

     
        public IEnumerable<MyEventModel> Items
        {
            get { return (IEnumerable<MyEventModel>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

    
    }
}
