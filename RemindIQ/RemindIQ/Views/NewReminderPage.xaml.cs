using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RemindIQ.Models;
using Xamarin.Essentials;
namespace RemindIQ.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewReminderPage : ContentPage
    {
        public Reminder Reminder { get; set; }

        public NewReminderPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        async void AddReminder_Clicked(object sender, EventArgs e)
        {
            Reminder = new Reminder
            {
                Name = NameField.Text,
                DestinationAddress = LocationField.Text,
                Latitude = (await App.LocationHelper.GetRemoteLocation(LocationField.Text)).Latitude,
                Longitude = (await App.LocationHelper.GetRemoteLocation(LocationField.Text)).Longitude,
                Range = RangeSliderField.Value,
                DistanceToDestination = App.LocationHelper.GetDistanceBetween(await App.LocationHelper.GetCurrentLocation(), await App.LocationHelper.GetRemoteLocation(LocationField.Text)),
                Notes = NotesField.Text,
                Status = 0
            };
            await App.DatabaseHelper.AddOrUpdateReminderAsync(Reminder);            
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