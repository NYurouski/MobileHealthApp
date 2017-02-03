using System;
using System.Collections.Generic;
using System.Linq;
using oc.Source.Helpers;
using oc.Source.Helpers.UIHelpers.ControlsHelper;
using oc.Source.Models.DataModel;
using Xamarin.Forms;

namespace oc.Source.Pages.ViewHelpers.TrackersViews
{
    public partial class BiometricsView : ContentView
    {
        public List<Reading> Opps { get; set; }
        private readonly Color _blueColor = Color.FromHex("#005398");
        public BiometricsView(List<Reading> collect)
        {
            InitializeComponent();
            Opps = collect;
            ArrowLabel.Text = "<";
            InitPage();
        }

        private void InitPage()
        {
            var listOppsView = new List<IList<View>>();
            var listOppsViewColumn = new List<View>();

            var tgr = new TapGestureRecognizer();
            tgr.Tapped += SubTracker_Tapped;
            
            listOppsViewColumn.AddRange(Opps.Select(opp =>
            {
                var stackLayout = new OppStackLayout
                {
                    GestureRecognizers = { tgr },
                    OppId = opp.ReadingType,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    BackgroundColor = _blueColor,
                    Padding = new Thickness(0, 5, 0, 5),
                    Children =
                        {
                            new Label
                            {
                                FontSize = 20,
                                TextColor = Color.White,
                                Text = "Log New",
                                HorizontalOptions = LayoutOptions.CenterAndExpand,
                                VerticalOptions = LayoutOptions.CenterAndExpand,
                            }
                        }
                };
                return new StackLayout
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
                                        Padding = new Thickness(0, 20, 0, 20),
                                        BackgroundColor = Color.White,
                                        VerticalOptions = LayoutOptions.FillAndExpand,
                                        HorizontalOptions = LayoutOptions.FillAndExpand,
                                        Children =
                                        {
                                            new Label
                                            {
                                                FontSize = 24,
                                                TextColor = _blueColor,
                                                Text = opp.ReadingName,
                                                VerticalOptions = LayoutOptions.CenterAndExpand,
                                                HorizontalOptions = LayoutOptions.CenterAndExpand,
                                            }
                                        }
                                    },
                                    new StackLayout
                                    {
                                        Padding = new Thickness(0, 10, 0, 10),
                                        BackgroundColor = Color.White,
                                        VerticalOptions = LayoutOptions.FillAndExpand,
                                        HorizontalOptions = LayoutOptions.FillAndExpand,
                                        Children =
                                        {
                                            new Label
                                            {
                                                FontSize = 18,
                                                TextColor = Color.FromHex("#333333"),
                                                Text = "Last Reading",
                                                HorizontalOptions = LayoutOptions.CenterAndExpand,
                                                VerticalOptions = LayoutOptions.CenterAndExpand
                                            },
                                            new Label
                                            {
                                                FontSize = 18,
                                                TextColor = Color.FromHex("#333333"),
                                                HorizontalOptions = LayoutOptions.CenterAndExpand,
                                                VerticalOptions = LayoutOptions.CenterAndExpand,
                                                Text = opp.ReadingDate
                                            },
                                            new Label
                                            {
                                                FontSize = 15,
                                                TextColor = Color.FromHex("#333333"),
                                                HorizontalOptions = LayoutOptions.CenterAndExpand,
                                                VerticalOptions = LayoutOptions.CenterAndExpand,
                                                Text = opp.Source
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
                                                        FontSize = 30,
                                                        TextColor = _blueColor,
                                                        Text = opp.Value1,
                                                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                                                        VerticalOptions = LayoutOptions.CenterAndExpand,
                                                    },
                                                    new Label
                                                    {
                                                        FontSize = opp.Unit == "" ? 30 : 24,
                                                        TextColor =
                                                            opp.Unit == "" ? _blueColor : Color.FromHex("#999999"),
                                                        Text = opp.Unit == "" ? $"/ {opp.Value2}" : opp.Unit,
                                                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                                                        VerticalOptions = LayoutOptions.CenterAndExpand,
                                                    }
                                                }
                                            },
                                            new StackLayout
                                            {
                                                Padding = new Thickness(20, 0, 20, 0),
                                                Children =
                                                {
                                                    new StackLayout
                                                    {
                                                        BackgroundColor = Color.FromHex("#CCCCCC"),
                                                        Padding = new Thickness(1, 1, 1, 1),
                                                        Children =
                                                        {
                                                            stackLayout
                                                        }
                                                     }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                };
            }));

            listOppsView.Add(new List<View>(listOppsViewColumn));
            var eventsGrid = new CustomGrid<View>(listOppsView);
            TrackersLayoutHealth.Children.Add(eventsGrid.GetGrid);
            listOppsView.Clear();
            listOppsViewColumn.Clear();
        }
        private void SubTracker_Tapped(object sender, EventArgs e)
        {
            var element = (OppStackLayout)sender;
            var oppId = element.OppId;
            MessagingCenter.Send<BiometricsView, Reading>(this, "EditReadingBio", Opps.FirstOrDefault(args => args.ReadingType == oppId));
        }
       
        private void BackArrowToPrevPage(object sender, EventArgs e)
        {
            MessagingCenter.Send<BiometricsView>(this, "BackToTrackersView");
        }
    }
}
