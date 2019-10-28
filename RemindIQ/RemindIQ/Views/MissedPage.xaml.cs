using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RemindIQ.Models;
using RemindIQ.ViewModels;


namespace RemindIQ.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MissedPage : ContentPage
    {
        List<Reminder> MissedReminders = new List<Reminder>();
        public MissedPage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            MissedReminders.Clear();
            MissedReminders = await App.DatabaseHelper.GetMissedRemindersAsync();
        }
        /*
        private void Left_Swiped(object sender, SwipedEventArgs e)
        {
            //DELETE
        }
        private void Right_Swiped(object sender, SwipedEventArgs e)
        {
            //COMPLETE
        }
        private void Show_Reminder(object sender, EventArgs e)
        {
            //SHOW
        }
        */
    }
}