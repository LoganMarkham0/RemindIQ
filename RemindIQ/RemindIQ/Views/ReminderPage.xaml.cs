using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RemindIQ.Models;

namespace RemindIQ.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReminderPage : ContentPage
    {
        private bool isNew;
        private Reminder reminder = new Reminder();

        public ReminderPage()
        {
            InitializeComponent();
            BindingContext = this;
            isNew = true;
            Title = "New Reminder";
            AddReminder.Text = "Add Reminder";
            reminder.HasBeenNotified = false;
            reminder.Status = 0;
        }

        public ReminderPage(Reminder reminder)
        {
            InitializeComponent();
            BindingContext = this;
            isNew = false;
            this.reminder = reminder;
            Title = "Edit Reminder";
            AddReminder.Text = "Update Reminder";
            NameField.Text = reminder.Name;
            LocationField.Text = reminder.DestinationAddress;
            RangeSliderField.Value = reminder.Range;
            NotesField.Text = reminder.Notes;
        }

        async void AddReminder_Clicked(object sender, EventArgs e)
        {
            reminder.Name = NameField.Text;
            reminder.DestinationAddress = LocationField.Text;
            reminder.Range = RangeSliderField.Value;
            reminder.Notes = NotesField.Text;

            // if user enters necessary input, proceed with calculations
            if (CorrectUserInput())
            {
                bool addressFound = true;

                // if address can not be found using GeoCoding, alert user
                try
                {
                    await App.locationHelper.GetRemoteLocation(LocationField.Text);
                }
                catch (Exception)
                {
                    addressFound = false;
                    LocationField.Text = null;
                    LocationField.PlaceholderColor = Color.Red;
                    LocationField.Placeholder = "Address not found";
                }

                if (addressFound)
                {
                    reminder.Latitude = (await App.locationHelper.GetRemoteLocation(LocationField.Text)).Latitude;
                    reminder.Longitude = (await App.locationHelper.GetRemoteLocation(LocationField.Text)).Longitude;
                    reminder.DistanceToDestination = App.locationHelper.GetDistanceBetween(await App.locationHelper.GetCurrentLocation(), await App.locationHelper.GetRemoteLocation(LocationField.Text));
                    reminder.Notes = NotesField.Text;
                    reminder.Status = 0;
                    if (isNew)
                    {
                        await App.databaseHelper.AddReminderAsync(reminder);
                    }
                    else
                    {
                        await App.databaseHelper.UpdateReminderAsync(reminder);
                    }

                    await Navigation.PopModalAsync();
                }
            }
        }

        async void Back_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private void RangeSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            RangeLabelField.Text = String.Format("{0:##0.0} {1}", e.NewValue.ToString(), "Miles");
        }

        /// <summary>
        /// Checks to see if the user input entered is functional.
        /// </summary>
        /// <returns>Will return true if user input does not cause any errors</returns>
        private bool CorrectUserInput()
        {
            bool isCorrect = true;

            // Check to make sure the reminder object has a name
            if (reminder.Name == null)
            {
                isCorrect = false;
                NameField.PlaceholderColor = Color.Red;
                NameField.Placeholder = "Reminder Name cannot be empty";
            }

            // Check to make sure the user enters a location
            if (reminder.DestinationAddress == null)
            {
                isCorrect = false;
                LocationField.PlaceholderColor = Color.Red;
                LocationField.Placeholder = "Reminder location cannot be empty";
            }

            // If the user did not select a range
            if (reminder.Range == 0)
            {
                isCorrect = false;
                RangeErrorMessage.Text = "Please choose a range for your reminder.";
            }
            else
            {
                RangeErrorMessage.Text = null;
            }

            return isCorrect;
        }
    }
}