using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RemindIQ.Models;

namespace RemindIQ.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewReminderPage : ContentPage
    {
        Reminder NewReminder = new Reminder();
        public NewReminderPage()
        {
            InitializeComponent();
        }

        async void Back_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private void AddReminder_Clicked(object sender, EventArgs e)
        {
            NewReminder.Name = Name.Text;
            NewReminder.DestinationAddress = Location.Text;
            NewReminder.DestinationLatitude = 7;//Replace with the method to get the destination latiture
            NewReminder.DestinationLongitude = 7;//Replace with the method to get the destination longitude
            NewReminder.Range = RangeSlider.Value;
            NewReminder.DistanceToDestination = 7;//Replace with the method to calculate distance
            NewReminder.Notes = Notes.Text;
            NewReminder.Status = 0;

            //Create the Reminder entry in the database
        }

        private void RangeSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            RangeLabel.Text = String.Format("{0:##0.0} {1}", e.NewValue.ToString(), "Miles");
        }
    }
}