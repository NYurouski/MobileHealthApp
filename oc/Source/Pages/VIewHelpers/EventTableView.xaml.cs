using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using oc.Source.Helpers;
using oc.Source.Helpers.UIHelpers.ControlsHelper;
using oc.Source.Models;
using SVG.Forms.Plugin.Abstractions;
using Xamarin.Forms;

namespace oc.Source.Pages.ViewHelpers
{
    public partial class EventTableView : ContentView
    {
        public ObservableCollection<MyEventModel> MyEventsList { get; set; }
        private MyEventsResponseModel response { get; set; }    

        public EventTableView(MyEventsResponseModel model)
        {
            InitializeComponent();
            response = model;
            InitPage();
        }

        private void InitPage()
        {
            MyEventsList = new ObservableCollection<MyEventModel>();
            if (response?.Events != null)
            {
                foreach (var myevent in response.Events)
                {
                    MyEventsList.Add(new MyEventModel { Title = myevent.EventName, Description = myevent.EventDescription, Registered = myevent.RegistrationStatus, RegistrationEventId = myevent.RegistrationEventId, RegistrationDate = myevent.RegistrationDate });
                }
                foreach (var myEventModel in MyEventsList)
                {
                    View timeStack;
                    if (myEventModel.Registered == "Registered")
                    {
                        timeStack = CreateEventDetail(myEventModel.RegistrationDate);
                    }
                    else
                    {
                        timeStack = new ContentView
                        {
                            Padding = new Thickness(5,20,0,20),
                            HorizontalOptions = LayoutOptions.Center,
                            VerticalOptions = LayoutOptions.Center,
                            Content = new TextContainerFrame
                            {
                                Padding = new Thickness(10),
                                VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Center,
                                CornerRadiusExtended = 10,
                                BorderWidthExtended = 2,
                                BorderColorExtended = Color.FromHex("#45c757"),
                                HasShadow = false,
                                BackgroundColorExtended = Color.FromHex("#3ca41b"),
                                Content = new StackLayout
                                {
                                    Spacing = 0,
                                    HorizontalOptions = LayoutOptions.Center,
                                    Children =
                                    {
                                        new ContentView()
                                        {
                                              HorizontalOptions = LayoutOptions.Center,
                                              Margin = new Thickness(4,0,0,0),
                                              Content =  new SvgImage
                                              {
                                                SvgPath = "oc.Source.Images.register.svg",
                                                SvgAssembly = typeof(App).GetTypeInfo().Assembly,
                                                WidthRequest = 29,
                                                HeightRequest = 26,
                                               }
                                        },
                                        new Label
                                        {
                                            Text = "Register",
                                            TextColor = Color.FromHex("#ffffff"),
                                            VerticalOptions = LayoutOptions.Center,
                                            FontSize = 12
                                         }
                                    }
                                }
                            }
                        };
                    }

                   MainViewStack.Children.Add(CreateEventsBlock(myEventModel.Title, myEventModel.Description, timeStack));

                }
            }
        }
        private StackLayout CreateEventsBlock(string title, string content, View stack)
        {
            var mainStackLayout = new StackLayout
            {
                BackgroundColor = Color.White,
                Padding = new Thickness(15,5,0,5)
            };
            var titleLabel = new Label
            {
                Text = title,
                TextColor = Color.FromHex("#1F6AA5"),
                FontSize = 20
            };
            var gridContent = new Grid();
            gridContent.RowSpacing = 0;
            gridContent.ColumnSpacing = 0;
            gridContent.ColumnDefinitions = new ColumnDefinitionCollection
            {
                new ColumnDefinition
                {
                    Width = new GridLength(3, GridUnitType.Star)
                },
                new ColumnDefinition
                {
                    Width = new GridLength(1, GridUnitType.Absolute)
                },
                new ColumnDefinition
                {
                    Width = new GridLength(1, GridUnitType.Star)
                }
            };
            gridContent.RowDefinitions = new RowDefinitionCollection
            {
                new RowDefinition
                {
                    Height = new GridLength(7, GridUnitType.Absolute)
                },
                new RowDefinition
                {
                    Height = GridLength.Auto
                },
                new RowDefinition
                {
                    Height = new GridLength(7, GridUnitType.Absolute)
                }
            };
            var stackForLabel = new StackLayout
            {
                Padding = new Thickness(0,5,0,5),
                VerticalOptions = LayoutOptions.Center,
                Children =
                {
                    new Label
                    {
                        FontSize = 14,
                        Text = content
                    }
                }
            };
            gridContent.Children.Add(stackForLabel, 0, 1, 0, 3);
            gridContent.Children.Add(new BoxView
            {
                BackgroundColor = Color.FromHex("#E6E6E8"),
                HeightRequest = 3
            },1,1);
            gridContent.Children.Add(stack, 2,3,0,3);
            mainStackLayout.Children.Add(titleLabel);
            mainStackLayout.Children.Add(gridContent);
            return mainStackLayout;
        }
        private StackLayout CreateEventDetail(string date)
        {
            var stackResult = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Padding = new Thickness(5,0,5,0)
            };
            var container = new TextContainerFrame
            {
                BorderColorExtended = Color.FromHex("#E6E6E8"),
                BorderWidthExtended = 1,
                Padding = new Thickness(0,5,0,5),
                HasShadow = false,
                BackgroundColorExtended = Color.White,
                CornerRadiusExtended = 5,
                HorizontalOptions = LayoutOptions.Center,
                Content = new StackLayout
                {
                            Padding = new Thickness(3, 0, 3, 0),
                            Children =
                        {
                            new Label
                            {
                                Text = date,
                                FontSize = 14,
                                FontAttributes = FontAttributes.Bold,
                                HorizontalTextAlignment = TextAlignment.Center
                            }
                         }
                }
             };
            stackResult.Children.Add(container);
            return stackResult;
        }
        
    }
}
