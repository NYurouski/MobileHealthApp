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
    public partial class OpportunityDetailView : ContentView
    {
        public Reward RewardDetail { get; set; }
        public Incentive IncentiveDetail { get; set; }
        public ObservableCollection<OpportunitiesModel> opps { get; set; }

        public OpportunityDetailView(Incentive incentiveDetail)
        {
            IncentiveDetail = incentiveDetail;
            InitializeComponent();
            ArrowLabel.Text = "<";
        }

        public OpportunityDetailView(Reward rewardDetail)
        {
            RewardDetail = rewardDetail;
            InitializeComponent();
            ArrowLabel.Text = "<";
        }
        private void BackArrowToPrevPage(object sender, EventArgs e)
        {
            MessagingCenter.Send<OpportunityDetailView>(this, "BackToMainOpportunity");
        }

        public void InitOpportunitiesDetail()
        {
            opps = new ObservableCollection<OpportunitiesModel>();
            
            if (IncentiveDetail != null)
                foreach (var category in IncentiveDetail.Category)
                {
                    opps.Add(new OpportunitiesModel
                    {
                        Title = category.Description,
                        IsComplete = int.Parse(category.Value) == 0
                    });
                }

            if (RewardDetail != null)
                foreach (var category in RewardDetail.Category)
                {
                    opps.Add(new OpportunitiesModel
                    {
                        Title = category.Description,
                        IsComplete = int.Parse(category.Value) == 0
                    });
                }

            var categoryMinimum = RewardDetail != null ? RewardDetail.CategoryMinimum : IncentiveDetail.CategoryMinimum;
            var categoryCount = RewardDetail != null ? RewardDetail.CategoryCount : IncentiveDetail.CategoryCount;

            TitleTable.Text = $"Achieve {categoryMinimum} of {categoryCount}";

            var listOppsView = new List<IList<View>>();
            var listOppsViewColumn = new List<View>();


            listOppsViewColumn.Add(CreateGridColumn(new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    new StackLayout
                    {
                        BackgroundColor = Color.White,
                        Padding = new Thickness(0,13,0,13),
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        Children =
                        {
                            new Label
                            {
                                Text = opps.First().Title,
                                TextColor = Color.Black,
                                VerticalOptions = LayoutOptions.CenterAndExpand,
                                HorizontalOptions = LayoutOptions.FillAndExpand,
                                MinimumHeightRequest = 30,
                                FontSize = 14
                            }
                        }
                    }

                }
            }, opps.First().IsComplete));

            listOppsViewColumn.AddRange(opps.Except(opps.Take(1)).Select(opp =>
            CreateGridColumn(new StackLayout
            {
                Padding = new Thickness(0, 1, 0, 0),
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.FromHex("#cfced2"),
                Children =
                {
                    new StackLayout
                    {
                        BackgroundColor = Color.White,
                        Padding = new Thickness(0,13,0,13),
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        Children =
                        {
                            new Label
                            {
                                Text = opp.Title,
                                TextColor = Color.Black,
                                VerticalOptions = LayoutOptions.CenterAndExpand,
                                HorizontalOptions = LayoutOptions.FillAndExpand,
                                MinimumHeightRequest = 30,
                                FontSize = 14
                            }
                        }
                    }

                }
            }, opp.IsComplete)));

            listOppsView.Add(new List<View>(listOppsViewColumn));
            var eventsGrid = new CustomGrid<View>(listOppsView, 0);
            IncentiveTableLayout.Children.Add(eventsGrid.GetGrid);
            MainStacklayout.Opacity = 1;
        }

        private View CreateGridColumn(View cellView, bool oppId)
        {
            var gridView = new Grid
            {
                BackgroundColor = Color.White,
                Padding = new Thickness(10, 0, 0, 0),
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
                },
            };
            gridView.Children.Add(new ContentView
            {
                Content = new SvgImage
                {
                    SvgPath = oppId ? "oc.greenthumbsup.svg" : "oc.redthumbsdown.svg",
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
    }
}
