using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using oc.Source.Helpers;
using oc.Source.Helpers.UIHelpers.ControlsHelper;
using oc.Source.Models.DataModel;
using Xamarin.Forms;

namespace oc.Source.Pages.ViewHelpers.TrackersViews
{
    public partial class ProgramTrackers : ContentView
    {
        private List<Reading> Opps { get; set; }
        private readonly Color _orangeColor = Color.FromHex("#EA862C");
        public ProgramTrackers(List<Reading> list)
        {
            InitializeComponent();
            ArrowLabel.Text = "<";
            Opps = list;
            InitData();
        }

        private void InitData()
        {
            var tgr = new TapGestureRecognizer();
            tgr.Tapped += SubTracker_Tapped;
            var trash = new TapGestureRecognizer();
            trash.Tapped += Trash_Tapped;
            var listOppsView = new List<IList<View>>();
            var listOppsViewColumn = new List<View>();
            listOppsViewColumn.AddRange(Opps.Where(opp => opp.ReadingCategory == "custom").Select(opp => new StackLayout
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
                                    FontSize = 24,
                                    TextColor = _orangeColor,
                                    Text = opp.ReadingName,
                                    VerticalOptions = LayoutOptions.CenterAndExpand,
                                    HorizontalOptions = LayoutOptions.CenterAndExpand,
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
                                    FontSize= 18,
                                    TextColor= Color.FromHex("#333333"),
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
                                  Text =  opp.ReadingDate ?? "No Recording"
                                },
                                new Label
                                {
                                  FontSize = 15,
                                  TextColor = Color.FromHex("#333333"),
                                  HorizontalOptions = LayoutOptions.CenterAndExpand,
                                  VerticalOptions = LayoutOptions.CenterAndExpand,
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
                                            FontSize = 30,
                                            TextColor = _orangeColor,
                                            Text = opp.Value1 ?? "NA",
                                            HorizontalOptions = LayoutOptions.CenterAndExpand,
                                            VerticalOptions = LayoutOptions.CenterAndExpand,
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
                                new StackLayout
                                {
                                    Padding = new Thickness(20,0,20,0),
                                    Spacing = 20,
                                    Orientation = StackOrientation.Horizontal,
                                    Children =
                                    {
                                        new StackLayout
                                        {
                                            BackgroundColor = Color.FromHex("#CCCCCC"),
                                            Padding = new Thickness(1,1,1,1),
                                            Children =
                                            {
                                                new OppStackLayout
                                                {
                                                    GestureRecognizers = { tgr },
                                                    OppId = opp.ReadingType,
                                                    VerticalOptions = LayoutOptions.FillAndExpand,
                                                    HorizontalOptions = LayoutOptions.FillAndExpand,
                                                    Children =
                                                    {
                                                        new Label
                                                        {
                                                            FontSize = 20,
                                                            TextColor = Color.White,
                                                            Text = "Log New",
                                                            WidthRequest = 240,
                                                            HeightRequest = 40,
                                                            BackgroundColor = _orangeColor,
                                                            HorizontalOptions = LayoutOptions.Start,
                                                            VerticalOptions = LayoutOptions.Start,
                                                            HorizontalTextAlignment = TextAlignment.Center,
                                                            VerticalTextAlignment = TextAlignment.Center
                                                        }
                                                    }
                                                }

                                            }
                                       },
                                       new OppStackLayout
                                       {
                                           GestureRecognizers = { trash },
                                           OppId = opp.ReadingType,
                                           Children =
                                           {
                                               new Image
                                               {
                                                  WidthRequest = 30,
                                                  HeightRequest = 30,
                                                  Source = ImageSource.FromResource("oc.snipimage.jpg")
                                               }
                                           }

                                       }

                                     }
                                }
                             }
                        }
                    }
                  }
                }
            }));
            listOppsView.Add(new List<View>(listOppsViewColumn));
            TrackersLayoutCustom.Children.Add(new CustomGrid<View>(listOppsView).GetGrid);
        }
        private void SubTracker_Tapped(object sender, EventArgs e)
        {
            var element = (OppStackLayout)sender;
            var oppId = element.OppId;
            MessagingCenter.Send<ProgramTrackers, Reading>(this, "EditReadingPro", Opps.FirstOrDefault(args => args.ReadingType == oppId));
        }
        private void Trash_Tapped(object sender, EventArgs e)
        {
            var element = (OppStackLayout)sender;
            MessagingCenter.Send<ProgramTrackers, string>(this, "DelReadingPro", element.OppId);
        }

        private void BackArrowToPrevPage(object sender, EventArgs e)
        {
            MessagingCenter.Send<ProgramTrackers>(this, "BackToTrackersView");
        }
    }
}
