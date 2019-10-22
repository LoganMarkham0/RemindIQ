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
        public Reminder Reminder { get; set; }

        public NewReminderPage()
        {
            InitializeComponent();


            BindingContext = this;
        }

        private void RangeSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            RangeLabel.Text = String.Format("{0:##0.0} {1}", e.NewValue.ToString(), "Miles");
        }

        async void Back_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        async void AddReminder_Clicked(object sender, EventArgs e)
        {
            Reminder = new Reminder
            {
                Name = Name.Text,
                DestinationAddress = Location.Text,
                DestinationLatitude = 7,//Replace with the method to get the destination latiture
                DestinationLongitude = 7,//Replace with the method to get the destination longitude
                Range = RangeSlider.Value,
                DistanceToDestination = 7,//Replace with the method to calculate distance
                Notes = Notes.Text,
                Status = 0
            };


            MessagingCenter.Send(this, "AddItem", Reminder);
            
            await Navigation.PopModalAsync();
        }


    }
}