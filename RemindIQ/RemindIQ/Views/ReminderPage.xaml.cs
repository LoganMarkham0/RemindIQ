using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using RemindIQ;
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
            reminder.Latitude = (await App.LocationHelper.GetRemoteLocation(LocationField.Text)).Latitude;
            reminder.Longitude = (await App.LocationHelper.GetRemoteLocation(LocationField.Text)).Longitude;
            reminder.Range = RangeSliderField.Value;
            reminder.DistanceToDestination = App.LocationHelper.GetDistanceBetween(await App.LocationHelper.GetCurrentLocation(), await App.LocationHelper.GetRemoteLocation(LocationField.Text));
            reminder.Notes = NotesField.Text;
            reminder.Status = 0;
            if (isNew)
            {
                await App.DatabaseHelper.AddReminderAsync(reminder);
            }
            else
            {
                await App.DatabaseHelper.UpdateReminderAsync(reminder);
            }
            await Navigation.PopModalAsync();
        }

        async void Back_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private void RangeSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            RangeLabelField.Text = String.Format("{0:##0.0} {1}", e.NewValue.ToString(), "Miles");
        }
    }
}