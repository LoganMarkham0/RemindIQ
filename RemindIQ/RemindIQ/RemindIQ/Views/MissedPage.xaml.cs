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
        public MissedPage()
        {
            InitializeComponent();
            MissedListView.RefreshCommand = new Command(() => { OnAppearing(); MissedListView.IsRefreshing = false; });
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var MissedReminders = await App.DatabaseHelper.GetMissedRemindersAsync();
            if (MissedReminders != null)
            {
                MissedListView.ItemsSource = MissedReminders;
            }
            MissedListView.IsRefreshing = false;
        }
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
    }
}