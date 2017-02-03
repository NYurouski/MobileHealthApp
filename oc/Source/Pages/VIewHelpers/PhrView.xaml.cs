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
using oc.Source.Models.DataModel;
using Xamarin.Forms;

namespace oc.Source.Pages.ViewHelpers
{
    public partial class PhrView : ContentView
    {
        private PhrResponseModel Response { get; set; }
       public ObservableCollection<Chapter> opps { get; set; }
        public PhrView()
        {
            InitializeComponent();
        }
        public async Task InitHealthScoreDetailPage()
        {
            if (Response == null)
            {
                Response = await ApiService.Instance.GetDataFromServerTask<HomeDataRequestModel, PhrResponseModel>(new HomeDataRequestModel()
                {

                    ProgramId = AppSettingHelper.ProgramId,
                    ParticipantId = AppSettingHelper.ParticipantId

                }, ApiConstants.GetSectionMobileUrl);

                opps = new ObservableCollection<Chapter>();

                if (Response.Chapter != null)
                {
                    foreach (var chapter in Response.Chapter)
                    {
                        opps.Add(new Chapter
                        {
                            Id = chapter.Id,
                            Heading = chapter.Heading
                        });
                    }
                }

                var listOppsView = new List<IList<View>>();
                var listOppsViewColumn = new List<View>();

                listOppsViewColumn.AddRange(opps.Select(opp => CreateGridColumn(new Label
                {
                    Text = opp.Heading,
                    TextColor = Color.FromHex("#333333"),
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    MinimumHeightRequest = 30,
                    FontSize = 14
                }, opp.Id)));

                listOppsView.Add(new List<View>(listOppsViewColumn));
                var eventsGrid = new CustomGrid<View>(listOppsView);
                HealthReportMenu.Children.Add(eventsGrid.GetGrid);

                MainLayout.IsVisible = true;
              }
        }

        private View CreateGridColumn(View cellView, string oppId, double widthRequest = 0)
        {
            var tgr = new TapGestureRecognizer();
            tgr.Tapped += ChapterId_Tapped;
            var bodyStackLayout = new OppStackLayout
            {
                Padding = new Thickness(5, 5, 5, 5),
                BackgroundColor = Color.White,
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                MinimumHeightRequest = 30,
                OppId = oppId

            };

            if (widthRequest > 0)
                bodyStackLayout.WidthRequest = widthRequest;

            bodyStackLayout.Children.Add(cellView);
            bodyStackLayout.Children.Add(new Label
            {
                Text = ">",
                TextColor = Color.FromHex("#333333"),
                HorizontalOptions = LayoutOptions.End,
                MinimumHeightRequest = 30,
                FontSize = 14
            });
            bodyStackLayout.GestureRecognizers.Add(tgr);
            return bodyStackLayout;
        }

        private void ChapterId_Tapped(object sender, EventArgs e)
        {
           
            var element = (OppStackLayout)sender;
            var oppId = element.OppId;

            if (Response.Chapter != null)
                MessagingCenter.Send<PhrView, Chapter>(this, "PHRDetail", Response.Chapter.FirstOrDefault(args => args.Id == oppId));
          
         }

    }
}
