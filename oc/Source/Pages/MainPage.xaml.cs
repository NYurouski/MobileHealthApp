using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Acr.UserDialogs;
using oc.Source.Constants;
using oc.Source.Helpers;
using oc.Source.Helpers.Enums;
using oc.Source.Models;
using oc.Source.Models.DataModel;
using oc.Source.Models.ViewModel;
using oc.Source.Pages.ViewHelpers;
using oc.Source.Pages.ViewHelpers.TrackersViews;
using SVG.Forms.Plugin.Abstractions;
using Xamarin.Forms;
using BiometricsView = oc.Source.Pages.ViewHelpers.TrackersViews.BiometricsView;

namespace oc.Source.Pages
{
    public partial class MainPage : ContentPage
    {
        public List<NavigationLink> ControlsList { get; set; }
        private MainViewModel _model;
        private NavigationLink _currentLink;
        private NavigationLink _phrLink;
        private NavigationLink _scoreLink;
        private NavigationLink _eventsLink;
        private NavigationLink _usericonLink;
        private NavigationLink _dashboardLink;
        private NavigationLink _trackersLink;
        private NavigationLink _opportunitiesLink;
        private OpportunityResponseModel _opportunityResponseModel;
        private HomeDataResponseModel _homeDataResponse;
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            _model = new MainViewModel();
            BindingContext = _model;

            #region MessagingCenter
            MessagingCenter.Subscribe<OpportunitiesView, Incentive>(this, "Incentive", AddIncentiveView);
            MessagingCenter.Subscribe<OpportunitiesView, Reward>(this, "Reward", AddRewardView);
            MessagingCenter.Subscribe<OpportunityDetailView>(this, "BackToMainOpportunity", AddMainOpportunity);
            MessagingCenter.Subscribe<SettingView>(this, "WelcomeLetter", AddWelcomeLetterView);
            MessagingCenter.Subscribe<SettingView>(this, "HelpView", AddHelpView);
            MessagingCenter.Subscribe<SettingView>(this, "InformationView", AddInformationView);
            MessagingCenter.Subscribe<SettingView>(this, "ChangePassword", AddChangePassword);
            MessagingCenter.Subscribe<InformationView, AlertModel>(this, "StartAlert", AlertFromInformationView);
            MessagingCenter.Subscribe<ChangePasswordView, AlertModel>(this, "StartAlertCp", AlertFromCpView);
            MessagingCenter.Subscribe<EventView, string>(this, "SubEvent", AddSubEventPage);
            MessagingCenter.Subscribe<SubEventsView, List<string>>(this, "SubEventOnAlertYesNo", AlertAndNewEvents);
            MessagingCenter.Subscribe<PhrView, Chapter>(this, "PHRDetail", AddPhrDetail);
            MessagingCenter.Subscribe<TrackersOverview>(this, "Biometrics", DownloadBiometricsData);
            MessagingCenter.Subscribe<TrackersOverview>(this, "Devices", DownloadDevicesData);
            MessagingCenter.Subscribe<TrackersOverview>(this, "Program", DownloadProgramData);
            MessagingCenter.Subscribe<BiometricsView>(this, "BackToTrackersView", ShowTrackersOverView);
            MessagingCenter.Subscribe<DevicesTracker>(this, "BackToTrackersView", BackToOverviewFromDevices);
            MessagingCenter.Subscribe<ProgramTrackers>(this, "BackToTrackersView", BackToOverviewFromProgram);
            MessagingCenter.Subscribe<BiometricsView, Reading>(this, "EditReadingBio", EditBioReading);
            MessagingCenter.Subscribe<ProgramTrackers, Reading>(this, "EditReadingPro", EditProReading);
            MessagingCenter.Subscribe<ProgramTrackers, string>(this, "DelReadingPro", DelReadingPro);
            MessagingCenter.Subscribe<SubTrackersView, List<string>>(this, "EditReadingPro", SaveReadingChanges);
            MessagingCenter.Subscribe<SubTrackersView, TrackerEnum>(this, "BackToTrackers", ReturnFromEditMode);
            #endregion
        }

        private void ReturnFromEditMode(SubTrackersView view, TrackerEnum pageType)
        {
            if (pageType == TrackerEnum.Biometrics)
            {
                DownloadBiometricsData(null);
            }
            if (pageType == TrackerEnum.Program)
            {
                DownloadProgramData(null);
            }
        }

        private async void SaveReadingChanges(SubTrackersView view, List<string> listOfReadingsArgument)
        {
            var response = await ApiService.Instance.GetDataFromServerTask<SaveTrackersRequestModel, LogInResponseModel>(
                              new SaveTrackersRequestModel()
                              {
                                  ParticipantId = AppSettingHelper.ParticipantId,
                                  ReadingTypeId = listOfReadingsArgument[0],
                                  ReadingDate = listOfReadingsArgument[1],
                                  Note = listOfReadingsArgument[2],
                                  Value1 = listOfReadingsArgument[3],
                                  Value2 = listOfReadingsArgument[4]
                              }, ApiConstants.SaveTrackersUrl);
            if (response.success)
            {
                await DisplayAlert("Success!", "Data was saved", "OK");
            }
            else
            {
                await DisplayAlert("Error!", response.error.Message, "OK");

            }
        }

        private async void DelReadingPro(ProgramTrackers view, string readingId)
        {
            using (UserDialogs.Instance.Loading("loading"))
            {
                var response = await ApiService.Instance
                    .GetDataFromServerTask<DeleteTrackerRequestModel, LogInResponseModel>(
                        new DeleteTrackerRequestModel()
                        {
                            ParticipantId = AppSettingHelper.ParticipantId,
                            ParticipantReadingId = readingId
                        }, ApiConstants.DeleteTrackerUrl);
                if (response.success)
                {
                    await DisplayAlert("Success!", "Tracker was deleted", "OK");
                }
                else
                {
                    await DisplayAlert("Error!", response.error.Message, "OK");
                }
            }
        }

        private void EditProReading(ProgramTrackers view, Reading reading)
        {
            ContentStackLayout.Children.Clear();
            var view2 = new SubTrackersView(reading, TrackerEnum.Program);
            ContentStackLayout.Children.Add(view2);
        }

        private void EditBioReading(BiometricsView view, Reading reading)
        {
            ContentStackLayout.Children.Clear();
            var view2 = new SubTrackersView(reading, TrackerEnum.Biometrics);
            ContentStackLayout.Children.Add(view2);
        }

        private void BackToOverviewFromProgram(ProgramTrackers view)
        {
            GetTrackersOverView();
        }

        private void BackToOverviewFromDevices(DevicesTracker view)
        {
            GetTrackersOverView();
        }

        private void ShowTrackersOverView(BiometricsView view)
        {
            GetTrackersOverView();
        }

        private async void DownloadProgramData(TrackersOverview view)
        {
            using (UserDialogs.Instance.Loading("loading"))
            {
                var model = await GetTrackersData();
				var programList = model.Readings.Where(opp => opp.ReadingCategory == ReadingCategoryTypeName.Custom).ToList();
                ContentStackLayout.Children.Clear();
                var view2 = new ProgramTrackers(programList);
                ContentStackLayout.Children.Add(view2);
            }
        }

        private async void DownloadDevicesData(TrackersOverview view)
        {
            using (UserDialogs.Instance.Loading("loading"))
            {
                var model = await GetTrackersData();
				var devicesList = model.Readings.Where(opp => opp.ReadingCategory == ReadingCategoryTypeName.FitBit).ToList();
                ContentStackLayout.Children.Clear();
                var view2 = new DevicesTracker(devicesList);
                ContentStackLayout.Children.Add(view2);
            }
        }

        private async void DownloadBiometricsData(TrackersOverview trackersOverview)
        {
            using (UserDialogs.Instance.Loading("loading"))
            {
                var model = await GetTrackersData();
				var healthList = model.Readings.Where(opp => opp.ReadingCategory == ReadingCategoryTypeName.Health).ToList();
                ContentStackLayout.Children.Clear();
                var view2 = new BiometricsView(healthList);
                ContentStackLayout.Children.Add(view2);
            }
        }
        private async Task<TrackersResponseModel> GetTrackersData()
        {
            await Task.Delay(1);
            return await
                        ApiService.Instance.GetDataFromServerTask<HomeDataRequestModel, TrackersResponseModel>(
                            new HomeDataRequestModel()
                            {

                                ProgramId = AppSettingHelper.ProgramId,
                                ParticipantId = AppSettingHelper.ParticipantId

                            }, ApiConstants.GetTrackersMobileUrl);
        
        }
        private async void AddPhrDetail(PhrView view, Chapter chapter)
        {
            using (UserDialogs.Instance.Loading("loading"))
            {
                await Task.Delay(1);
                ContentStackLayout.Children.Clear();
                var view2 = new PhrDetailView(chapter);
                await view2.InitHealthReportSectionPage();
                ContentStackLayout.Children.Add(view2);
            }
        }
        private async void AlertAndNewEvents(SubEventsView view, List<string> listOfAppointmentCredentials)
        {
            var answer = await DisplayAlert("onecommunity.com says:", $"Are you sure you want to make an appointment for {listOfAppointmentCredentials[0]} at {listOfAppointmentCredentials[1]}", "OK", "Cancel");
            if (!answer) return;
            await ApiService.Instance.GetDataFromServerTask<AppointmentMobileRequestModel, AppointmentMobileResponseModel>(new AppointmentMobileRequestModel()
            {
                ScreeningSubperiodId = listOfAppointmentCredentials[2],
                ProgramId = AppSettingHelper.ProgramId,
                ParticipantId = AppSettingHelper.ParticipantId

            }, ApiConstants.AddAppointmentUrl);
            await GetEventView();
        }
        private async void AddSubEventPage(EventView view, string subEventTitle)
        {
            using (UserDialogs.Instance.Loading("loading"))
            {
                await Task.Delay(1);
                ContentStackLayout.Children.Clear();
                var view2 = new SubEventsView(subEventTitle);
                await view2.ListViewAttemp();
                ContentStackLayout.Children.Add(view2);
            }
        }
        private void InitNavigationLinksObjects()
        {
            _phrLink = new NavigationLink
            {
                MainStackLayout = PhrButton,
                ContentOfImage = PhrContent,
                Image = PhrSvgImage,
                Label = PhrLabel,
                ActiveImageSource = "oc.Source.Images.phractive.svg",
                InactiveImageSource = "oc.Source.Images.phr.svg",
                LinkStatus = _homeDataResponse.ResponseData.MenuChoices.ViewPhr
            };
            _scoreLink = new NavigationLink
            {
                MainStackLayout = ScoreButton,
                ContentOfImage = ScoreContent,
                Image = ScoreSvgImage,
                Label = ScoreLabel,
                ActiveImageSource = "oc.Source.Images.scoreactive.svg",
                InactiveImageSource = "oc.Source.Images.score.svg",
                LinkStatus = _homeDataResponse.ResponseData.MenuChoices.ViewHealthScore
            };
            _eventsLink = new NavigationLink
            {
                MainStackLayout = EventsButton,
                ContentOfImage = EventsContent,
                Image = EventsSvgImage,
                Label = EventsLabel,
                ActiveImageSource = "oc.Source.Images.eventsactive.svg",
                InactiveImageSource = "oc.Source.Images.events.svg",
                LinkStatus = _homeDataResponse.ResponseData.MenuChoices.ViewEvents
            };
            _trackersLink = new NavigationLink
            {
                MainStackLayout = TrackersButton,
                ContentOfImage = TrackersContent,
                Image = TrackersSvgImage,
                Label = TrackersLabel,
                ActiveImageSource = "oc.Source.Images.trackeractive.svg",
                InactiveImageSource = "oc.Source.Images.tracker.svg",
                LinkStatus = _homeDataResponse.ResponseData.MenuChoices.ViewTrackers
            };
            _opportunitiesLink = new NavigationLink
            {
                MainStackLayout = OpportunitiesButton,
                ContentOfImage = OpportunitiesContent,
                Image = OpportunitiesSvgImage,
                Label = OpportunitiesLabel,
                ActiveImageSource = "oc.Source.Images.opportunitiesactive.svg",
                InactiveImageSource = "oc.Source.Images.opportunities.svg",
                LinkStatus = _homeDataResponse.ResponseData.MenuChoices.ViewOpportunities
            };
            _dashboardLink = new NavigationLink
            {
                MainStackLayout = DashboardButton,
                ContentOfImage = DashboardContent,
                Image = DashboardSvgImage,
                ActiveImageSource = "oc.Source.Images.dashboardactive.svg",
                InactiveImageSource = "oc.Source.Images.dashboard.svg"
            };
            _usericonLink = new NavigationLink
            {
                MainStackLayout = UserButton,
                ContentOfImage = UserContent,
                Image = UserSvgImage,
                ActiveImageSource = "oc.Source.Images.useractive.svg",
                InactiveImageSource = "oc.Source.Images.user.svg"
            };
        }
        private async void AlertFromCpView(ChangePasswordView view, AlertModel alertModel)
        {
            await DisplayAlert(alertModel.Title, alertModel.Content, alertModel.AcceptMessage);
        }
        private async void AddChangePassword(SettingView view)
        {
            MakeButtonInactive();
            using (UserDialogs.Instance.Loading("loading"))
            {
                await Task.Delay(1);
                PageTitle.Text = "Change Password";
                ContentStackLayout.Children.Clear();
                var view2 = new ChangePasswordView();
                ContentStackLayout.Children.Add(view2);
            }
        }
        private async void AlertFromInformationView(InformationView informationView, AlertModel alertModel)
        {
            await DisplayAlert(alertModel.Title, alertModel.Content, alertModel.AcceptMessage);
        }
        private async void AddInformationView(SettingView view)
        {
            MakeButtonInactive();
            using (UserDialogs.Instance.Loading("loading"))
            {
                await Task.Delay(1);
                PageTitle.Text = "Personal Info";
                ContentStackLayout.Children.Clear();
                var view2 = new InformationView();
                await view2.InitPage();
                ContentStackLayout.Children.Add(view2);
            }
        }
        private async void AddHelpView(SettingView view)
        {
            MakeButtonInactive();
            using (UserDialogs.Instance.Loading("loading"))
            {
                await Task.Delay(1);
                PageTitle.Text = "Help";
                ContentStackLayout.Children.Clear();
                var view2 = new HelpView();
                ContentStackLayout.Children.Add(view2);
            }
        }

        private async void AddWelcomeLetterView(SettingView settingView)
        {
            MakeButtonInactive();
            using (UserDialogs.Instance.Loading("loading"))
            {
                await Task.Delay(1);
                PageTitle.Text = "Welcome Letter";
                ContentStackLayout.Children.Clear();
                var view2 = new WelcomeLetterView();
                await view2.InitPage();
                ContentStackLayout.Children.Add(view2);
            }
        }

        private async void AddMainOpportunity(OpportunityDetailView view)
        {
            await GetOppView();
        }

        private void AddRewardView(OpportunitiesView view, Reward reward)
        {
            using (UserDialogs.Instance.Loading("loading"))
            {
                ContentStackLayout.Children.Clear();
                var view1 = new OpportunityDetailView(reward);
                view1.InitOpportunitiesDetail();
                ContentStackLayout.Children.Add(view1);
            }
        }

        private void AddIncentiveView(OpportunitiesView view, Incentive incentive)
        {
            using (UserDialogs.Instance.Loading("loading"))
            {
                ContentStackLayout.Children.Clear();
                var view1 = new OpportunityDetailView(incentive);
                view1.InitOpportunitiesDetail();
                ContentStackLayout.Children.Add(view1);
            }
        }

        private async void Dashboard_OnTapped(object sender, EventArgs e)
        {
            TurnOnButton(_dashboardLink);
            await GetHomeView();
        }

        protected internal async Task GetHomeView()
        {
            using (UserDialogs.Instance.Loading("loading"))
            {
                await Task.Delay(1);
                ContentStackLayout.Children.Clear();
                PageTitle.Text = "Dashboard";
                _homeDataResponse = await ApiService.Instance.GetDataFromServerTask<HomeDataRequestModel, HomeDataResponseModel>(new HomeDataRequestModel()
                {
                    ProgramId = AppSettingHelper.ProgramId,
                    ParticipantId = AppSettingHelper.ParticipantId

                }, ApiConstants.HomeDataUrl);

                var view2 = new HomeView(_homeDataResponse);
                view2.InitPage();
                ContentStackLayout.Children.Add(view2);
                InitNavigationLinksObjects();
                RefreshNavigationButtons();
            }

        }

        private void RefreshNavigationButtons()
        {
            PhrButton.IsEnabled = _phrLink.IsEnableStatus;
            PhrSvgImage.Opacity = _phrLink.OpacityStatus;
            OpportunitiesButton.IsEnabled = _opportunitiesLink.IsEnableStatus;
            OpportunitiesSvgImage.Opacity = _opportunitiesLink.OpacityStatus;
            TrackersButton.IsEnabled = _trackersLink.IsEnableStatus;
            TrackersSvgImage.Opacity = _trackersLink.OpacityStatus;
            EventsButton.IsEnabled = _eventsLink.IsEnableStatus;
            EventsSvgImage.Opacity = _eventsLink.OpacityStatus;
            ScoreButton.IsEnabled = _scoreLink.IsEnableStatus;
            ScoreSvgImage.Opacity = _scoreLink.OpacityStatus;
        }

        private async void Opportunites_OnTapped(object sender, EventArgs e)
        {
            TurnOnButton(_opportunitiesLink);
            _opportunityResponseModel = null;
            await GetOppView();
        }

        private async Task GetOppView()
        {
            using (UserDialogs.Instance.Loading("loading"))
            {
                PageTitle.Text = "Opportunities";
                ContentStackLayout.Children.Clear();
                if (_opportunityResponseModel == null)
                {
                    _opportunityResponseModel = await ApiService.Instance.GetDataFromServerTask<HomeDataRequestModel, OpportunityResponseModel>(
                    new HomeDataRequestModel()
                    {
                        ProgramId = AppSettingHelper.ProgramId,
                        ParticipantId = AppSettingHelper.ParticipantId
                    }, ApiConstants.OpportunityPageUrl);
                }
                var view1 = new OpportunitiesView(_opportunityResponseModel);
                ContentStackLayout.Children.Add(view1);
            }
        }
        private async void Events_OnTapped(object sender, EventArgs e)
        {
            TurnOnButton(_eventsLink);
            await GetEventView();
        }

        private async Task GetEventView()
        {
            using (UserDialogs.Instance.Loading("loading"))
            {
                PageTitle.Text = "Events";
                ContentStackLayout.Children.Clear();
                var eventsResponseModel = await ApiService.Instance.GetDataFromServerTask<HomeDataRequestModel, MyEventsResponseModel>(
                   new HomeDataRequestModel()
                   {
                       ProgramId = AppSettingHelper.ProgramId,
                       ParticipantId = AppSettingHelper.ParticipantId
                   }, ApiConstants.MyEventsPageUrl);

                var view1 = new EventTableView(eventsResponseModel);
                ContentStackLayout.Children.Add(view1);
            }
        }

        private async void Phr_OnTapped(object sender, EventArgs e)
        {
            TurnOnButton(_phrLink);
            using (UserDialogs.Instance.Loading("loading"))
            {
                await Task.Delay(1);
                PageTitle.Text = "Personal Health Report";
                ContentStackLayout.Children.Clear();
                var view1 = new PhrView();
                await view1.InitHealthScoreDetailPage();
                ContentStackLayout.Children.Add(view1);
            }
        }

        private async void Score_OnTapped(object sender, EventArgs e)
        {
            TurnOnButton(_scoreLink);
            using (UserDialogs.Instance.Loading("loading"))
            {
                await Task.Delay(1);
                PageTitle.Text = "Health Score";
                ContentStackLayout.Children.Clear();
                var view1 = new ScoreView();
                await view1.InitHealthScoreDetailPage();
                ContentStackLayout.Children.Add(view1);
            }
        }

        private void Trackers_OnTapped(object sender, EventArgs e)
        {
            TurnOnButton(_trackersLink);
            GetTrackersOverView();
        }

        private void GetTrackersOverView()
        {
            PageTitle.Text = "Health Tracker";
            ContentStackLayout.Children.Clear();
            var view1 = new TrackersOverview();
            ContentStackLayout.Children.Add(view1);
        }

        private async void Settings_OnTapped(object sender, EventArgs e)
        {
            TurnOnButton(_usericonLink);
            using (UserDialogs.Instance.Loading("loading"))
            {
                await Task.Delay(1);
                PageTitle.Text = "User Settings";
                ContentStackLayout.Children.Clear();
                var view1 = new SettingView();
                ContentStackLayout.Children.Add(view1);
            }
        }

        private void TurnOnButton(NavigationLink link)
        {
            ChangeImage(link.ContentOfImage, link.ActiveImageSource);
            link.MainStackLayout.IsEnabled = false;
            if (link.Label != null)
            {
                link.Label.TextColor = Color.FromHex("#365989");
            }
            if (_currentLink != null) MakeButtonInactive();
            _currentLink = link;

        }

        private void MakeButtonInactive()
        {
            ChangeImage(_currentLink.ContentOfImage, _currentLink.InactiveImageSource);
            _currentLink.MainStackLayout.IsEnabled = true;
            if (_currentLink.Label != null)
            {
                _currentLink.Label.TextColor = Color.FromHex("#bbbdc0");
            }

        }

        private void ChangeImage(StackLayout container, string imageSource)
        {
            container.Children.Clear();
            container.Children.Add(new SvgImage
            {
                SvgPath = imageSource,
                SvgAssembly = typeof(App).GetTypeInfo().Assembly,
                HeightRequest = 30,
                WidthRequest = 30,
                BackgroundColor = Color.White,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
            });
        }
    }
}
