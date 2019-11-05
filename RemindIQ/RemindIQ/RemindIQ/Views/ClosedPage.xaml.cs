using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RemindIQ.Models;
using RemindIQ.ViewModels;
using Xamarin.Essentials;

namespace RemindIQ.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClosedPage : ContentPage
    {
        public ClosedPage()
        {
            InitializeComponent();
            ClosedListView.RefreshCommand = new Command(() => { OnAppearing(); ClosedListView.IsRefreshing = false; });
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var ClosedReminders = await App.DatabaseHelper.GetClosedRemindersAsync();
            if (ClosedReminders != null)
            {
                ClosedListView.ItemsSource = ClosedReminders;
            }
            ClosedListView.IsRefreshing = false;
        }
        private void Left_Swiped(object sender, SwipedEventArgs e)
        {
            //DELETE
        }
        private void Right_Swiped(object sender, SwipedEventArgs e)
        {
            //REOPEN REMINDER
        }
        private void Show_Reminder(object sender, EventArgs e)
        {
            //SHOW
        }
    }
}