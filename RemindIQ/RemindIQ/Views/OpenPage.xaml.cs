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
using System.Windows.Input;

namespace RemindIQ.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OpenPage : ContentPage
    {
        public OpenPage()
        {
            InitializeComponent();
            OpenListView.RefreshCommand = new Command(() => { OnAppearing(); OpenListView.IsRefreshing = false; });
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var OpenReminders = await App.DatabaseHelper.GetOpenRemindersAsync();
            if(OpenReminders != null)
            {
                OpenListView.ItemsSource = OpenReminders;
            }
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