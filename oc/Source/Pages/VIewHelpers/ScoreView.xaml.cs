using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using oc.Source.Constants;
using oc.Source.Helpers;
using oc.Source.Helpers.UIHelpers.ControlsHelper;
using oc.Source.Models;
using Xamarin.Forms;

namespace oc.Source.Pages.ViewHelpers
{
    public partial class ScoreView : ContentView
    {
        private ScoreDetailsResponseModel Response { get; set; }
        public ObservableCollection<Scoretable> opps { get; set; }
        public ScoreView()
        {
            InitializeComponent();
        }
        public async Task InitHealthScoreDetailPage()
        {
            if (Response == null)
            {
                Response =
                    await
                        ApiService.Instance.GetDataFromServerTask<HomeDataRequestModel, ScoreDetailsResponseModel>(
                            new HomeDataRequestModel()
                            {
                                ProgramId = AppSettingHelper.ProgramId,
                                ParticipantId = AppSettingHelper.ParticipantId

                            }, ApiConstants.GetScoreDetailsUrl);
                BindingContext = Response;
                // Risk Message Title
                ScoredRangeLabel.Text = $"{Response.ParticipantScore.FirstName}, you scored in the {Response.ParticipantScore.RiskLevel} Risk range.";
                // OverAllScore
                var numberOveralScore = Response.ParticipantScore.OveralScore;
                // "Your Score"'

                var height = 0.0;
                if (numberOveralScore < 51)
                {
                    height = 1 + numberOveralScore * 0.6;
                }

                else if (numberOveralScore < 61)
                {
                    height = 32 + (numberOveralScore - 50) * 3;
                }
                else if (numberOveralScore < 71)
                {
                    height = 63 + (numberOveralScore - 60) * 3;
                }
                else if (numberOveralScore < 86)
                {
                    height = 94 + (numberOveralScore - 70) * 2;
                }
                else
                {
                    height = 125 + (numberOveralScore - 85) * 2;
                }
                YourRectangle.HeightRequest = height;

                var heightProgram = 0.0;
                var heightScore = Response.Compare.ProgramScoreDecimal;
                string color;
                if (heightScore < 51)
                {
                    heightProgram = 1 + heightScore * 0.6;
                    color = "#AA0000";
                }
                else if (heightScore < 61)
                {
                    heightProgram = 32 + (heightScore - 50) * 3;
                    color = "#EA862C";
                }
                else if (heightScore < 71)
                {
                    heightProgram = 63 + (heightScore - 60) * 3;
                    color = "#E1BE18";
                }
                else if (heightScore < 86)
                {
                    heightProgram = 94 + (heightScore - 70) * 2;
                    color = "#B1B149";
                }
                else
                {
                    heightProgram = 125 + (heightScore - 85) * 2;
                    color = "#63934E";
                }
                ProgramRectangle.HeightRequest = Math.Round(heightProgram);
                ProgramRectangle.BackgroundColor = Color.FromHex(color);
                // Dynamic Table
                opps = new ObservableCollection<Scoretable>();

                if (Response.Scoretable != null)
                {
                    foreach (var score in Response.Scoretable)
                    {
                        opps.Add(new Scoretable
                        {
                            ScoreDate = score.ScoreDate,
                            ScoreProgram = score.ScoreProgram,
                            ScoreValue = score.ScoreValue
                        });
                    }
                }

                var listOppsView = new List<IList<View>>();

                var listOppsViewColumn = new List<View>
                {
                     CreateGridHeader("Date Recorded", LayoutOptions.StartAndExpand)
                };
                listOppsViewColumn.AddRange(opps.Select(opp => CreateGridColumn(new Label
                {
                    Text = opp.ScoreDate,
                    TextColor = Color.FromHex("#333333"),
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    MinimumHeightRequest = 30,
                    FontSize = 12
                }, 125)));

                listOppsView.Add(new List<View>(listOppsViewColumn));
                listOppsViewColumn.Clear();

                listOppsViewColumn.Add(CreateGridHeader("Score", LayoutOptions.StartAndExpand));
                listOppsViewColumn.AddRange(opps.Select(opp => CreateGridColumn(new Label
                {
                    Text = opp.ScoreValue.ToString(),
                    TextColor = Color.FromHex("#333333"),
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    MinimumHeightRequest = 30,
                    FontSize = 12
                }, 55)));

                listOppsView.Add(new List<View>(listOppsViewColumn));
                listOppsViewColumn.Clear();

                listOppsViewColumn.Add(CreateGridHeader("Program", LayoutOptions.StartAndExpand));
                listOppsViewColumn.AddRange(opps.Select(opp => CreateGridColumn(new Label
                {
                    Text = opp.ScoreProgram,
                    TextColor = Color.FromHex("#333333"),
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    MinimumHeightRequest = 30,
                    FontSize = 12
                })));

                listOppsView.Add(new List<View>(listOppsViewColumn));
                var eventsGrid = new CustomGrid<View>(listOppsView);
                HistotyLogLayout.Children.Add(eventsGrid.GetGrid);
                MainLayout.IsVisible = true;
            }
        }

        private View CreateGridHeader(string headerName, LayoutOptions layOutOptions)
        {
            var headerStackLayout = new StackLayout
            {
                BackgroundColor = Color.FromHex("#eeeeee"),
                Padding = new Thickness(5, 5, 5, 1),
                Orientation = StackOrientation.Vertical
            };

            headerStackLayout.Children.Add(new Label
            {
                Text = headerName,
                TextColor = Color.FromHex("#333333"),
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = layOutOptions,
                BackgroundColor = Color.FromHex("#eeeeee")
            });
            return headerStackLayout;
        }

        private View CreateGridColumn(View cellView, double widthRequest = 0)
        {
            var bodyStackLayout = new OppStackLayout
            {
                Padding = new Thickness(5, 5, 5, 1),
                BackgroundColor = Color.White,
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                MinimumHeightRequest = 30
            };

            if (widthRequest > 0)
                bodyStackLayout.WidthRequest = widthRequest;

            bodyStackLayout.Children.Add(cellView);
            return bodyStackLayout;
        }
    }
}
