using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using oc.Source.ApplicationSettings.Impl;
using oc.Source.ApplicationSettings.SettingsConstants;
using oc.Source.Helpers.Enums;
using oc.Source.Helpers.UIHelpers.ControlsHelper;
using oc.Source.Models;
using oc.Source.Models.DataModel;
using Xamarin.Forms;

namespace oc.Source.Pages.ViewHelpers.TrackersViews
{
    public partial class SubTrackersView : ContentView
    {
        private Reading _reading;
        private CustomFontEntry valueEntry;
        private CustomFontEntry noteEntry;
        private CustomFontEntry systolicEntry;
        private CustomFontEntry diastolicEntry;
        private ExtendedDatePicker datePicker;
        private TrackerEnum pageParamether;
        
        public SubTrackersView(Reading reading, TrackerEnum parameter)
        {
            InitializeComponent();
            pageParamether = parameter;
            _reading = reading;
            TitleLogNew.Text = $"Log New {reading.ReadingName}";
            InitPage();
        }
        private void InitPage()
        {
            ContentLayout.Children.Add(
                new StackLayout
                {
                    HorizontalOptions = LayoutOptions.Start,
                    Children =
                    {
                           new Label()
                        {
                            Text = "Reading Date",
                            Style = (Style) Resources["LabelText"]
                        }
                    }

                }
            );
            datePicker = new ExtendedDatePicker
            {
                Format = "MM/dd/yyyy",
                HeightRequest = 45,
                CornerRadiusExtended = 0,
                BorderWidthExtended = 1,
                BorderColorExtended = Color.FromHex("#CCCCCC")

            };
            ContentLayout.Children.Add(datePicker);

            if (_reading.ReadingType == "1000")
            {
                systolicEntry = new CustomFontEntry
                {
                    PlaceHolderColorExtended = Color.FromHex("#CAC9CE"),
                    Style = (Style)Resources["EntryStyle"],
                    Placeholder = "Systolic"

                };
                diastolicEntry = new CustomFontEntry
                {
                    PlaceHolderColorExtended = Color.FromHex("#CAC9CE"),
                    Style = (Style)Resources["EntryStyle"],
                    Placeholder = "Diastolic"

                };
                ContentLayout.Children.Add(systolicEntry);
                ContentLayout.Children.Add(diastolicEntry);

            }
            else
            {
                Grid grid = new Grid
                {
                    RowSpacing = 0,
                    ColumnSpacing = 0,
                    RowDefinitions =
                     {
                         new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },

                     },
                    ColumnDefinitions =
                     {
                         new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                         new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
                     }
                };

                valueEntry = new CustomFontEntry
                {
                    PlaceHolderColorExtended = Color.FromHex("#CAC9CE"),
                    Style = (Style)Resources["EntryStyle"],
                    Placeholder = "Unit"
                };
                var unitLabel = new Label
                {
                    Text = _reading.Unit,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.Start,
                };
                var wrapperUnitLabel = new StackLayout
                {
                    Padding = new Thickness(10, 0, 0, 0),
                    Children =
                    {
                        unitLabel
                    }
                };

                grid.Children.Add(valueEntry, 0, 0);
                grid.Children.Add(wrapperUnitLabel, 1, 0);
                ContentLayout.Children.Add(grid);

            }
            noteEntry = new CustomFontEntry
            {
                PlaceHolderColorExtended = Color.FromHex("#CAC9CE"),
                Style = (Style)Resources["EntryStyle"],
                Placeholder = "Note"
            };
            ContentLayout.Children.Add(noteEntry);
            var button = new CustomFontButton
            {
                BackgroundColorExtended = Color.FromHex("#37A600"),
                BorderColorExtended = Color.FromHex("#3EC951"),
                BorderWidthExtended = 1,
                CornerRadiusExtended = 7,
                FontSize = 16,
                Text = "Save",
                HeightRequest = 40,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            button.Clicked += ButtonOnClicked;

            ContentLayout.Children.Add(new StackLayout
            {
                Padding = new Thickness(0, 20, 0, 0),
                Children =
                {
                    button
                }
            });
        }
        private void ButtonOnClicked(object sender, EventArgs eventArgs)
        {
            var readingStringList = new List<string>();
            readingStringList.Add(_reading.ReadingType);
            readingStringList.Add(datePicker.Date.ToString("M/d/yyyy"));
            readingStringList.Add(noteEntry.Text);
            readingStringList.Add(_reading.ReadingType == "1000" ? systolicEntry.Text : valueEntry.Text);
            readingStringList.Add(_reading.ReadingType == "1000" ? diastolicEntry.Text : "0");
            
            MessagingCenter.Send<SubTrackersView, List<string>>(this, "EditReadingPro", readingStringList);
         }

        private void BackArrowToPrevPage(object sender, EventArgs e)
        {
            MessagingCenter.Send<SubTrackersView, TrackerEnum>(this, "BackToTrackers", pageParamether);
        }
    }
}
