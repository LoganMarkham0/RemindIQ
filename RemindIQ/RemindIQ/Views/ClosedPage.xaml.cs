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
        List<Reminder> ClosedReminders = new List<Reminder>();
        public ClosedPage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            ClosedReminders.Clear();
            ClosedReminders = await App.DatabaseHelper.GetClosedRemindersAsync();
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