using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using oc.Source.ApplicationSettings.Impl;
using oc.Source.ApplicationSettings.SettingsConstants;
using oc.Source.Constants;
using oc.Source.Helpers;
using oc.Source.Helpers.UIHelpers.ControlsHelper;
using oc.Source.Models;
using Xamarin.Forms;

namespace oc.Source.Pages.ViewHelpers
{
    public partial class SubEventsView : ContentView
    {
        private string RegistrationEventId { get; set; }
        private string Title { get; set; }
        private bool isTimeAlreadyChosen;
        public ObservableCollection<MySheduleModel> opps { get; set; }
       
        public SubEventsView()
        {
            InitializeComponent();
        }
        public SubEventsView(string registrationEventId)
        {
            RegistrationEventId = registrationEventId;
            InitializeComponent();
        }
        
        public async Task ListViewAttemp()
        {
           var response =
                 await
                     ApiService.Instance.GetDataFromServerTask<HomeDataRequestModel, SheduleResponseModel>(
                         new SheduleRequestModel
                         {
                             ProgramId =
                                 AppSettingsSingleton.GetInstance().GetByKey(AppSettingName.ProgramId, string.Empty),
                             ParticipantId =
                                 AppSettingsSingleton.GetInstance().GetByKey(AppSettingName.ParticipantId, string.Empty),
                             RegistrationEventId = RegistrationEventId

                         }, ApiConstants.ShedulePageUrl);

            Title = response.RegistrationEventName;

            opps = new ObservableCollection<MySheduleModel>();

            SheduleLayout.IsVisible = (bool)response?.ScreeningPeriods?.Select(args => args.ScreeningSubPeriods).Any();
            
            foreach (var screeningSubPeriod in response.ScreeningPeriods)
            {
                var sheduleModel = new MySheduleModel { Title = screeningSubPeriod.Location };

                foreach (var subPeriod in screeningSubPeriod.ScreeningSubPeriods.Where(args => args.Active == 1))
                {
                    sheduleModel.ShedulePlans.Add(new ShedulePlan
                    {
                        isEnabled = subPeriod.Full == 0,
                        ScreeningTimeTitle = subPeriod.ScreeningTime,
                        ScreeningSubperiodId = subPeriod.ScreeningSubPeriodId,
                        BAlreadyRegistered = subPeriod.BAlreadyRegistered
                    });
                }

                opps.Add(sheduleModel);
            }

            foreach (var mySheduleModel in opps)
            {

                SheduleLayout.Children.Add(CreateGridHeader(mySheduleModel.Title));

                var listEventsView = new List<IList<View>>();
                var firstListEventsViewColumn = new List<View>();
                var secondListEventsViewColumn = new List<View>();
                var thirdListEventsViewColumn = new List<View>();

                for (int i = 0; i < mySheduleModel.ShedulePlans.Count; i += 3)
                {
                    firstListEventsViewColumn.Add(CreateGridCellBody(mySheduleModel.ShedulePlans[i].ScreeningTimeTitle, mySheduleModel.ShedulePlans[i].isEnabled, mySheduleModel.ShedulePlans[i].ScreeningSubperiodId, mySheduleModel.ShedulePlans[i].BAlreadyRegistered));
                }

                for (int y = 1; y < mySheduleModel.ShedulePlans.Count; y += 3)
                {
                    try
                    {
                        secondListEventsViewColumn.Add(
                                      CreateGridCellBody(mySheduleModel.ShedulePlans[y].ScreeningTimeTitle,
                                          mySheduleModel.ShedulePlans[y].isEnabled, mySheduleModel.ShedulePlans[y].ScreeningSubperiodId, mySheduleModel.ShedulePlans[y].BAlreadyRegistered));
                    }
                    catch (Exception)
                    {

                    }
                }
                for (int u = 2; u < mySheduleModel.ShedulePlans.Count; u += 3)
                {
                    try
                    {
                        thirdListEventsViewColumn.Add(
                                       CreateGridCellBody(mySheduleModel.ShedulePlans[u].ScreeningTimeTitle,
                                           mySheduleModel.ShedulePlans[u].isEnabled, mySheduleModel.ShedulePlans[u].ScreeningSubperiodId, mySheduleModel.ShedulePlans[u].BAlreadyRegistered));
                    }
                    catch (Exception)
                    {

                    }
                }

                listEventsView.Add(firstListEventsViewColumn);

                if (secondListEventsViewColumn.Count > 0)
                    listEventsView.Add(secondListEventsViewColumn);

                if (thirdListEventsViewColumn.Count > 0)
                    listEventsView.Add(thirdListEventsViewColumn);

                var eventsGrid = new CustomGrid<View>(listEventsView) { GetGrid = { VerticalOptions = LayoutOptions.Fill, BackgroundColor = Color.FromHex("#ffffff") } };

                SheduleLayout.Children.Add(eventsGrid.GetGrid);

            }
        }
        private View CreateGridHeader(string header)
        {
            var headerStackLayout = new StackLayout
            {
                BackgroundColor = Color.FromHex("#ffffff"),
                Orientation = StackOrientation.Vertical,
                Padding = new Thickness(12, 20, 0, 0)
            };
            headerStackLayout.Children.Add(new Label
            {
                Text = header,
                TextColor = Color.Black,
                VerticalOptions = LayoutOptions.End,
                HorizontalOptions = LayoutOptions.Start,
                LineBreakMode = LineBreakMode.WordWrap,
                FontSize = 14
            });
            return headerStackLayout;
        }

        private View CreateGridCellBody(string screeningTimeTitle, bool isFull, string screeningSubperiodId, int bAlreadyRegistered)

        {
            var bodyStackLayout = new StackLayout
            {
                BackgroundColor = Color.FromHex("#ffffff"),
                Spacing = 0,
                Orientation = StackOrientation.Vertical
            };

            var button = new ScreeningSubperiodIdButton
            {
                Text = screeningTimeTitle,
                TextColor = isFull ? Color.Black : Color.FromHex("#414141"),
                BorderColor = Color.Black,
                BorderWidth = 2,
                BorderRadius = 0,
                BackgroundColor = isFull ? Color.White : Color.FromHex("#cccccc"),
                ScreeningSubperiodId = screeningSubperiodId,
                Margin = 12
            };
            if (isFull && bAlreadyRegistered == 0)
                button.Clicked += Button_Clicked;
            if (bAlreadyRegistered == 1)
            {
                button.BackgroundColor = Color.Red;
                isTimeAlreadyChosen = true;
            }

            bodyStackLayout.Children.Add(button);

            return bodyStackLayout;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
           if (isTimeAlreadyChosen)
            {
                var response = await ApiService.Instance.GetDataFromServerTask<SheduleRequestModel, AppointmentMobileResponseModel>(new SheduleRequestModel
                {
                    RegistrationEventId = this.RegistrationEventId,
                    ProgramId =
                        AppSettingsSingleton.GetInstance().GetByKey(AppSettingName.ProgramId, string.Empty),
                    ParticipantId =
                        AppSettingsSingleton.GetInstance().GetByKey(AppSettingName.ParticipantId, string.Empty)
                }, ApiConstants.CancelAppointmentUrl);
            }
            OnAlertYesNoClicked(sender, e);
        }
        private void OnAlertYesNoClicked(object sender, EventArgs e)
        {
            var button = (ScreeningSubperiodIdButton)sender;
            var sendInfo = new List<string>
            {
               Title,
               button.Text,
               button.ScreeningSubperiodId
            };
            MessagingCenter.Send<SubEventsView, List<string>>(this, "SubEventOnAlertYesNo", sendInfo);
          }
    }
}
