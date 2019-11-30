using System;
using System.ComponentModel;
using Xamarin.Forms;
using RemindIQ.Models;
using System.Threading;

namespace RemindIQ.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        int currentPage;
        public MainPage()
        {
            InitializeComponent();

            new Thread(() =>
            {
                Thread.Sleep(App.RefreshInterval);
                Thread.Sleep(App.RefreshInterval / 2);
                while (true)
                {
                    Thread.Sleep(App.RefreshInterval);
                    reminderListView.Dispatcher.BeginInvokeOnMainThread((Action)(async () => reminderListView.ItemsSource = await App.databaseHelper.GetRemindersAsync(currentPage)));
                }
            }).Start();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            Title = "Open";
            currentPage = 0;
            reminderListView.ItemsSource = await App.databaseHelper.GetRemindersAsync(currentPage);
        }
        private async void Load_Page(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (button.Text == "Open")
            {
                Title = "Open";
                currentPage = 0;
            }
            if (button.Text == "Missed")
            {
                Title = "Missed";
                currentPage = 1;
            }
            if (button.Text == "Closed")
            {
                Title = "Closed";
                currentPage = 2;
            }
            reminderListView.ItemsSource = await App.databaseHelper.GetRemindersAsync(currentPage);
        }
        private async void Menu_Item(object sender, EventArgs e)
        {
            ToolbarItem toolbarItem = (ToolbarItem)sender;
            if (toolbarItem.Text == "Add")
            {
                await Navigation.PushModalAsync(new NavigationPage(new ReminderPage()));
            }
            if (toolbarItem.Text == "About")
            {
                await Navigation.PushModalAsync(new NavigationPage(new AboutPage()));
            }
        }
        private async void Context_Clicked(object sender, EventArgs e)
        {
            var menuItem = (MenuItem)sender;
            Reminder reminder = (Reminder)menuItem.BindingContext;
            if (menuItem.Text == "Complete")
            {
                reminder.Status = 2;
                await App.databaseHelper.UpdateReminderAsync(reminder);

            }
            if (menuItem.Text == "Delete")
            {
                await App.databaseHelper.DeleteReminderAsync(reminder);
            }
            reminderListView.ItemsSource = await App.databaseHelper.GetRemindersAsync(currentPage);
        }
        private async void Show_Reminder(object sender, SelectedItemChangedEventArgs e)
        {
            Reminder reminder = (Reminder)e.SelectedItem;
            await Navigation.PushModalAsync(new NavigationPage(new ReminderPage(reminder)));
        }
        private async void Refresh_Clicked(object sender, EventArgs e)
        {
            reminderListView.ItemsSource = await App.databaseHelper.GetRemindersAsync(currentPage);
        }
    }
}
