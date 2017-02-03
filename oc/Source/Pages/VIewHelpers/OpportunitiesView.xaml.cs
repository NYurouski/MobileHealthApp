using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using oc.Source.Helpers;
using oc.Source.Helpers.UIHelpers.ControlsHelper;
using oc.Source.Models;
using SVG.Forms.Plugin.Abstractions;
using Xamarin.Forms;

namespace oc.Source.Pages.ViewHelpers
{
    public partial class OpportunitiesView : ContentView
    {
        public ObservableCollection<OpportunitiesModel> opps { get; set; }
        private OpportunityResponseModel Response { get; set; }

        public OpportunitiesView(OpportunityResponseModel model)
        {
            Response = model;
            InitializeComponent();
            Resources = new ResourceDictionary
            {
                {
                    "labelStyle", new Style(typeof (Label))
                    {
                        Setters =
                        {
                            new Setter
                            {
                                Property = Label.FontSizeProperty,
                                Value = "13"
                            },
                            new Setter
                            {
                                Property = Label.TextColorProperty,
                                Value = Color.Black
                            },
                            new Setter
                            {
                                Property = View.HorizontalOptionsProperty,
                                Value = LayoutOptions.Start
                            },
                            new Setter
                            {
                                Property = View.VerticalOptionsProperty,
                                Value = LayoutOptions.CenterAndExpand
                            },
                            new Setter
                            {
                                Property = MinimumHeightRequestProperty,
                                Value = 30
                            }

                        }
                    }
                }
            };
            InitOpportunityPage();
        }
        
        private void InitOpportunityPage()
        {
            if (Response.Incentive != null)
            {
                IncentiveLayout.IsVisible = true;
                IncentiveIntroView.IsVisible = true;
                IncentiveIntroView.Source = new HtmlWebViewSource {Html = Response.IncentiveIntro};
                IncentiveTableLayout.IsVisible = true;
                ContentIncentiveWeb.IsVisible = true;

                opps = new ObservableCollection<OpportunitiesModel>();
                foreach (var incentive in Response.Incentive)
                {
                    opps.Add(new OpportunitiesModel
                    {
                        Title = incentive.OpportunityName,
                        IsComplete = int.Parse(incentive.Awarded) == 0,
                        OpportunityId = incentive.OpportunityId
                    });
                }

                #region New Table view

                var listOppsView = new List<IList<View>>();
                var listOppsViewColumn = new List<View>();


                var tgr = new TapGestureRecognizer();
                tgr.Tapped += InsTgr_Tapped;

                #region AddColumn

                var gridForFirstElement = ViewForFirstRow(tgr, opps.First().OpportunityId, opps.First().Title);
                listOppsViewColumn.Add(CreateGridColumn(gridForFirstElement, opps.First().IsComplete));

                #endregion
                var tablesRows = opps.Except(opps.Take(1)).Select(opp =>
                {
                    var oppStackLayout = new OppStackLayout
                    {
                        GestureRecognizers = { tgr },
                        OppId = opp.OpportunityId,
                    };
                    var layout = new OppStackLayout
                    {
                        GestureRecognizers = { tgr },
                        OppId = opp.OpportunityId,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        Padding = new Thickness(0, 13, 0, 13),
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        Children =
                        {
                            new Label
                            {
                                Text = opp.Title,
                                Style = (Style)Resources["labelStyle"],
                                FontSize = 13
                            }
                        }
                    };
                    var item = new Grid
                    {
                        BackgroundColor = Color.White,
                        ColumnSpacing = 0,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        ColumnDefinitions =
                        {
                            new ColumnDefinition
                            {
                                Width = new GridLength (7, GridUnitType.Star)
                            },
                            new ColumnDefinition
                            {
                                Width = new GridLength (1, GridUnitType.Star)
                            }
                        },
                        Children =
                        {
                            { oppStackLayout,1,0 },
                            { layout,0,0 }
                        }
                    };
                    var stackLayout = new StackLayout
                    {
                        Padding = new Thickness(0, 1, 0, 0),
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        BackgroundColor = Color.FromHex("#cfced2"),
                        Children = { item }
                    };
                    return CreateGridColumn(stackLayout, opp.IsComplete);
                });

                listOppsViewColumn.AddRange(tablesRows);
              
                listOppsView.Add(new List<View>(listOppsViewColumn));
                IncentiveTableLayout.Children.Clear();
                var eventsGrid = new CustomGrid<View>(listOppsView, 0);
                IncentiveTableLayout.Children.Add(eventsGrid.GetGrid);
                #endregion
                AchievedLabel.IsVisible = PercentAwardedLabel.IsVisible = Response.PercentAwarded != null;
                PercentAwardedLabel.Text = $"{Response.PercentAwarded}%";
                AchievedeLayout.IsVisible = true;
                AchievedeLayoutSubStack.IsVisible = true;
            }

            if (Response.Reward != null)
            {
                RewardTableLayout.IsVisible = true;
                RewardIntroView.IsVisible = true;
                RewardIntroView.Source = new HtmlWebViewSource { Html = Response.RewardIntro };

                opps = new ObservableCollection<OpportunitiesModel>();
                foreach (var reward in Response.Reward)
                {
                    opps.Add(new OpportunitiesModel
                    {
                        Title = reward.OpportunityName,
                        IsComplete = int.Parse(reward.Awarded) == 0,
                        OpportunityId = reward.OpportunityId
                    });
                }

                #region OppReward table view

                var listOppsView = new List<IList<View>>();
                var listOppsViewColumn = new List<View>();

                var tgr = new TapGestureRecognizer();
                tgr.Tapped += RewardTgr_Tapped;
                
                var gridForFirstElement = ViewForFirstRow(tgr, opps.First().OpportunityId, opps.First().Title);
                listOppsViewColumn.Add(CreateGridColumn(gridForFirstElement, opps.First().IsComplete));
               
                var tablesRows = opps.Except(opps.Take(1)).Select(opp =>
                {
                    var oppStackLayout = new OppStackLayout
                    {
                        GestureRecognizers = { tgr },
                        OppId = opp.OpportunityId,
                    };
                    var layout = new OppStackLayout
                    {
                        GestureRecognizers = { tgr },
                        OppId = opp.OpportunityId,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        Padding = new Thickness(0,13,0,13),
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        Children =
                        {
                            new Label
                            {
                                Text = opp.Title,
                                Style = (Style)Resources["labelStyle"],
                                FontSize = 13
                            }
                        }
                    };
                    var item = new Grid
                    {
                        BackgroundColor = Color.White,
                        ColumnSpacing = 0,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        ColumnDefinitions =
                        {
                            new ColumnDefinition
                            {
                                Width = new GridLength (7, GridUnitType.Star)
                            },
                            new ColumnDefinition
                            {
                                Width = new GridLength (1, GridUnitType.Star)
                            }
                        },
                        Children =
                        {
                            { oppStackLayout,1,0 },
                            { layout,0,0 }
                        }
                    };
                    var stackLayout = new StackLayout
                    {
                        Padding = new Thickness(0, 1, 0, 0),
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        BackgroundColor = Color.FromHex("#cfced2"),
                        Children = { item }
                    };
                    return CreateGridColumn(stackLayout, opp.IsComplete);
                });    
                
                listOppsViewColumn.AddRange(tablesRows);

                listOppsView.Add(new List<View>(listOppsViewColumn));
                var eventsGrid = new CustomGrid<View>(listOppsView, 0);
                RewardTableLayout.Children.Clear();
                RewardTableLayout.Children.Add(eventsGrid.GetGrid);
                #endregion

                AwardedLayout.IsVisible = true;
                CurrencyAwardedLabel.IsVisible = true;
                CurrencyAwardedLabel.Text = Response.CurrencyAwarded;
            }
            else
            {
                RewardTableLayout.IsVisible = false;
                RewardIntroView.IsVisible = false;
            }
            if (Response.ContactInfo != null)
            {
                AdditionalInfoView.IsVisible = true;
                AdditionalInfoView.Source = new HtmlWebViewSource { Html = Response.ContactInfo };
            }
           
            MainStackLayout.Opacity = 1;
        }

        private Grid ViewForFirstRow(TapGestureRecognizer tgr, string opportunitiesId, string opportunitiesTitle)
        {
            var gridForFirstElement = new Grid
            {
                BackgroundColor = Color.White,
                ColumnSpacing = 0,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                ColumnDefinitions =
                {
                    new ColumnDefinition
                    {
                        Width = new GridLength(7, GridUnitType.Star)
                    },
                    new ColumnDefinition
                    {
                        Width = new GridLength(1, GridUnitType.Star)
                    }
                }
            };
            gridForFirstElement.Children.Add(new OppStackLayout
            {
                GestureRecognizers = {tgr},
                OppId = opportunitiesId,
            }, 1, 0);
            gridForFirstElement.Children.Add(new OppStackLayout
            {
                GestureRecognizers = {tgr},
                HorizontalOptions = LayoutOptions.FillAndExpand,
                OppId = opportunitiesId,
                Padding = new Thickness(0, 13, 0, 13),
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Children =
                {
                    new Label
                    {
                        Text = opportunitiesTitle,
                        Style = (Style) Resources["labelStyle"],
                        FontSize = 13
                    }
                }
            }, 0, 0);
            return gridForFirstElement;
        }
        
        private View CreateGridColumn(View cellView, bool IsComplete)
        {
            var gridView = new Grid
            {
                BackgroundColor = Color.White,
                Padding = new Thickness(10, 0, 0, 0),
                ColumnSpacing = 0,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                ColumnDefinitions =
                {
                  new ColumnDefinition
                  {
                      Width = new GridLength (1, GridUnitType.Star)
                  },
                  new ColumnDefinition
                  {
                      Width = new GridLength (7, GridUnitType.Star)
                  }
                }
            };
            gridView.Children.Add(new ContentView
            {
                Content =
                new SvgImage
                {
                    SvgPath = IsComplete ? "oc.greenthumbsup.svg" : "oc.redthumbsdown.svg",
                    SvgAssembly = typeof(App).GetTypeInfo().Assembly,
                    WidthRequest = 25,
                    HeightRequest = 25
                },
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            }, 0, 0);
            gridView.Children.Add(cellView, 1, 0);

            return gridView;
        }
        private void RewardTgr_Tapped(object sender, EventArgs e)
        {
            var element = (OppStackLayout)sender;
            var oppId = element.OppId;

            if (Response.Reward != null)
            {
                MessagingCenter.Send<OpportunitiesView, Reward>(this, "Reward", Response.Reward.FirstOrDefault(args => args.OpportunityId == oppId));
            }
               
        }
        private void InsTgr_Tapped(object sender, EventArgs e)
        {
            var element = (OppStackLayout)sender;
            var oppId = element.OppId;
            if (Response.Incentive != null)
            {
                MessagingCenter.Send<OpportunitiesView, Incentive>(this, "Incentive", Response.Incentive.FirstOrDefault(args => args.OpportunityId == oppId));
            }
               
        }
    }
}
