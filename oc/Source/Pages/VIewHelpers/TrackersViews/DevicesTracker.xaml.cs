using System;
using System.Collections.Generic;
using System.Linq;
using oc.Source.Helpers;
using oc.Source.Helpers.UIHelpers.ControlsHelper;
using oc.Source.Models.DataModel;
using Xamarin.Forms;

namespace oc.Source.Pages.ViewHelpers.TrackersViews
{
    public partial class DevicesTracker : ContentView
    {
        private List<Reading> Opps { get; set; }
        private readonly Color _greenColor = Color.FromHex("#63934E");
        public DevicesTracker(List<Reading> list)
        {
            InitializeComponent();
            ArrowLabel.Text = "<";
            Opps = list;
            Resources = new ResourceDictionary
            {
                { "labelStyle", new Style(typeof(Label))
                    {
                        Setters =
                        {
                            new Setter
                            {
                                Property = Label.TextColorProperty,
                                Value = Color.FromHex("#333333")
                            },
                            new Setter
                            {
                                Property = View.HorizontalOptionsProperty,
                                Value = LayoutOptions.CenterAndExpand
                            },
                            new Setter
                            {
                                Property = View.VerticalOptionsProperty,
                                Value = LayoutOptions.CenterAndExpand
                            },

                        }
                    }
                }
            };
            InitData();
        }

        private void InitData()
        {
            var listOppsView = new List<IList<View>>();
            var listOppsViewColumn = new List<View>();
            // FitBit
            listOppsViewColumn.AddRange(Opps.Select(opp => new StackLayout
            {
                Padding = new Thickness(0, 20, 0, 0),
                BackgroundColor = Color.White,
                Children =
                  {
                    new StackLayout
                    {
                    Padding = new Thickness(1, 1, 1, 1),
                    BackgroundColor = Color.FromHex("#aaaaaa"),
                    Spacing = 1,
                    Children =
                    {
                        new StackLayout
                        {
                             Padding = new Thickness(0,20,0,20),
                             BackgroundColor = Color.White,
                             VerticalOptions = LayoutOptions.FillAndExpand,
                             HorizontalOptions = LayoutOptions.FillAndExpand,
                             Children =
                             {
                                new Label
                                 {
                                    Style = (Style)Resources["labelStyle"],
                                    FontSize = 24,
                                    TextColor = _greenColor,
                                    Text = opp.ReadingName
                                 }
                             }
                        },
                        new StackLayout
                        {
                             Padding = new Thickness(0,10,0,10),
                             BackgroundColor = Color.White,
                             VerticalOptions = LayoutOptions.FillAndExpand,
                             HorizontalOptions = LayoutOptions.FillAndExpand,
                             Children =
                             {
                                new Label
                                {
                                    Style = (Style)Resources["labelStyle"],
                                    FontSize= 18,
                                    Text = "Last Reading"
                                },
                                new Label
                                {
                                  Style = (Style)Resources["labelStyle"],
                                  FontSize= 18,
                                  Text =  opp.ReadingDate ?? "No Recording"
                                },
                                new Label
                                {
                                  Style = (Style)Resources["labelStyle"],
                                  FontSize = 15,
                                  TextColor = Color.FromHex("#333333"),
                                  Text =  opp.Source ?? "NA"
                                },
                                new StackLayout
                                {
                                    Orientation = StackOrientation.Horizontal,
                                    VerticalOptions = LayoutOptions.Center,
                                    HorizontalOptions = LayoutOptions.Center,
                                    Children =
                                    {
                                        new Label
                                        {
                                            Style = (Style)Resources["labelStyle"],
                                            FontSize = 30,
                                            TextColor = _greenColor,
                                            Text = opp.Value1 ?? "NA",
                                         },
                                        new Label
                                        {
                                            FontSize =  24,
                                            TextColor = Color.FromHex("#999999"),
                                            Text = opp.Unit ?? "NA",
                                            HorizontalOptions = LayoutOptions.CenterAndExpand,
                                            VerticalOptions = LayoutOptions.CenterAndExpand,
                                         }
                                    }
                                },
                             }
                        }
                    }
                  }
                }
            }));
            listOppsView.Add(new List<View>(listOppsViewColumn));
            TrackersDevices.Children.Add(new CustomGrid<View>(listOppsView).GetGrid);
        }

        private void BackArrowToPrevPage(object sender, EventArgs e)
        {
            MessagingCenter.Send<DevicesTracker>(this, "BackToTrackersView");
        }
    }
}
