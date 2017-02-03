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
    public partial class EventView : ContentView
    {
        public EventView()
        {
            InitializeComponent();
        }
        public ObservableCollection<MyEventModel> opps { get; set; }

        public async Task ListViewAttempt()
        {
            var response =
                await
                    ApiService.Instance.GetDataFromServerTask<HomeDataRequestModel, MyEventsResponseModel>(
                        new HomeDataRequestModel()
                        {
                            ProgramId = AppSettingHelper.ProgramId,
                            ParticipantId = AppSettingHelper.ParticipantId
                         }, ApiConstants.MyEventsPageUrl);
            opps = new ObservableCollection<MyEventModel>();
            if (response?.Events != null)
            {
                foreach (var myevent in response.Events)
                {
                    opps.Add(new MyEventModel { Title = myevent.EventName, Description = myevent.EventDescription, Registered = myevent.RegistrationStatus, RegistrationEventId = myevent.RegistrationEventId, RegistrationDate = myevent.RegistrationDate });
                }

                var listEventsView = new List<IList<View>>();
                var listEventsViewColumn = new List<View> { CreateGridHeader() };
                listEventsViewColumn.AddRange(opps.Select(opp => CreateGridCellBody(opp.Title, opp.Description, opp.Registered, opp.RegistrationEventId, opp.RegistrationDate)));
                listEventsView.Add(listEventsViewColumn);
                var eventsGrid = new CustomGrid<View>(listEventsView) { GetGrid = { BackgroundColor = Color.White } };

                EventsLayout.Children.Clear();
                EventsLayout.Children.Add(eventsGrid.GetGrid);
                EventsLayout.IsVisible = opps.Any();
            }
        }

        private View CreateGridHeader()
        {
            var headerStackLayout = new StackLayout
            {
                BackgroundColor = Color.White,
                Orientation = StackOrientation.Vertical
            };

            headerStackLayout.Children.Add(new Label
            {
                Text = "Events Management",
                TextColor = Color.Black,
                VerticalOptions = LayoutOptions.End,
                HorizontalOptions = LayoutOptions.Start,
                FontSize = 18
            });

            return headerStackLayout;
        }

        private View CreateGridCellBody(string titleName, string desccriptionName, string registerStatus, int registrationEventId, string registrationDate)
        {

            var bodyStackLayout = new StackLayout
            {
                BackgroundColor = Color.White,
                Spacing = 0,
                Orientation = StackOrientation.Vertical
            };

            var registerStackLayout = new StackLayout
            {
                BackgroundColor = Color.White,
                Spacing = 3,
                Orientation = StackOrientation.Horizontal,
            };


            bodyStackLayout.Children.Add(new BoxView { HorizontalOptions = LayoutOptions.FillAndExpand, BackgroundColor = Color.Transparent, HeightRequest = 40 });
            bodyStackLayout.Children.Add(new Label
            {
                Text = titleName,
                TextColor = Color.Black,
               FontSize = 14

            });
            bodyStackLayout.Children.Add(new BoxView { HorizontalOptions = LayoutOptions.FillAndExpand, BackgroundColor = Color.Transparent, HeightRequest = 10 });
            bodyStackLayout.Children.Add(new Label
            {
                Text = desccriptionName,
                TextColor = Color.Black,
                FontSize = 14

            });

            registerStackLayout.Children.Add(new Label
            {
                Text = registerStatus,
                TextColor = Color.FromHex("#660000"),
                VerticalOptions = LayoutOptions.End,
                FontSize = 14,

            });

            if (registerStatus == "Not Registered")
            {
                var label = new RegistrationEventLabel
                {
                    Text = "Register",
                    TextColor = Color.FromHex("#428bca"),
                    RegistrationEventId = registrationEventId,
                    FontSize = 14,
                    HorizontalOptions = LayoutOptions.Start

                };

                var tap = new TapGestureRecognizer();
                tap.Tapped += EventPage_Tapped;
                label.GestureRecognizers.Add(tap);
                registerStackLayout.Children.Add(label);
            }
            else
            {
                registerStackLayout.Orientation = StackOrientation.Vertical;
                registerStackLayout.Children.Add(new Label
                {
                    Text = $"You are currently registrated for this event. Your appointment is {registrationDate}",
                    TextColor = Color.FromHex("#660000"),
                    VerticalOptions = LayoutOptions.End,
                    FontSize = 14
                });
                var registratedStackLayout = new StackLayout
                {
                    BackgroundColor = Color.White,
                    Spacing = 20,
                    Orientation = StackOrientation.Horizontal,
                };
                var cancelLabel = new RegistrationEventLabel
                {
                    Text = "Cancel",
                    RegistrationEventId = registrationEventId,
                    TextColor = Color.FromHex("#428bca"),
                    FontSize = 14,
                    HorizontalOptions = LayoutOptions.Start
                };
                var tapOnCancel = new TapGestureRecognizer();
                tapOnCancel.Tapped += CancelEvent_Tapped;
                cancelLabel.GestureRecognizers.Add(tapOnCancel);
                registratedStackLayout.Children.Add(cancelLabel);

                var changeLabel = new RegistrationEventLabel
                {
                    Text = "Change",
                    RegistrationEventId = registrationEventId,
                    TextColor = Color.FromHex("#428bca"),
                    FontSize = 14,
                    HorizontalOptions = LayoutOptions.Start
                };

                var tapOnChange = new TapGestureRecognizer();
                tapOnChange.Tapped += TapOnChange_Tapped; ;
                changeLabel.GestureRecognizers.Add(tapOnChange);
                registratedStackLayout.Children.Add(changeLabel);
                registerStackLayout.Children.Add(registratedStackLayout);

            }

            bodyStackLayout.Children.Add(new BoxView { HorizontalOptions = LayoutOptions.FillAndExpand, BackgroundColor = Color.Transparent, HeightRequest = 10 });
            bodyStackLayout.Children.Add(registerStackLayout);


            return bodyStackLayout;
        }

        private void TapOnChange_Tapped(object sender, EventArgs e)
        {

            EventPage_Tapped(sender, e);

        }

        private void EventPage_Tapped(object sender, EventArgs e)
        {
            EventsLayout.Children.Clear();
            MessagingCenter.Send<EventView, string>(this, "SubEvent", (((RegistrationEventLabel)sender).RegistrationEventId).ToString());
        }
        private async void CancelEvent_Tapped(object sender, EventArgs e)
        {
            OnCancelTapped(sender, e);
            EventsLayout.Children.Clear();
           await ListViewAttempt();

        }

        async void OnCancelTapped(object sender, EventArgs e)
        {

            var label = (RegistrationEventLabel)sender;
            var response = await ApiService.Instance.GetDataFromServerTask<SheduleRequestModel, AppointmentMobileResponseModel>(new SheduleRequestModel
            {
                RegistrationEventId = label.RegistrationEventId.ToString(),
                ProgramId =
                    AppSettingsSingleton.GetInstance().GetByKey(AppSettingName.ProgramId, string.Empty),
                ParticipantId =
                    AppSettingsSingleton.GetInstance().GetByKey(AppSettingName.ParticipantId, string.Empty)
            }, ApiConstants.CancelAppointmentUrl);
        }
    }
}
