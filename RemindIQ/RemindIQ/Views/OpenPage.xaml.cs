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
    public partial class OpenPage : ContentPage
    {
        List<Reminder> OpenReminders = new List<Reminder>();
        public OpenPage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            OpenReminders.Clear();
            OpenReminders = await App.DatabaseHelper.GetOpenRemindersAsync();
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