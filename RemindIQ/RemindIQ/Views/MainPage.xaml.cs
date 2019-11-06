using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using RemindIQ.Models;
using Plugin.LocalNotification;

namespace RemindIQ.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        int currentPage;

        private int _count;

        public MainPage()
        {
            InitializeComponent();

            //NotifyDatePicker.MinimumDate = DateTime.Today;
            //NotifyTimePicker.Time = DateTime.Now.TimeOfDay.Add(TimeSpan.FromSeconds(10));
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            Title = "Open";
            currentPage = 0;
            reminderListView.ItemsSource = await App.DatabaseHelper.GetRemindersAsync(currentPage);
        }
        private async void Refresh()
        {
            reminderListView.ItemsSource = await App.DatabaseHelper.GetRemindersAsync(currentPage);
        }
        private void Load_Page(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (button.Text == "Open")
            {
                Title = "Open";
                currentPage = 0;
                Refresh();
            }
            if (button.Text == "Missed")
            {
                Title = "Missed";
                currentPage = 1;
                Refresh();
            }
            if (button.Text == "Closed")
            {
                Title = "Closed";
                currentPage = 2;
                Refresh();
            }

        }
        private async void Menu_Item(object sender, EventArgs e)
        {
            ToolbarItem toolbarItem = (ToolbarItem)sender;
            if (toolbarItem.Text == "Add")
            {
                await Navigation.PushModalAsync(new NavigationPage(new ReminderPage()));
            }
            if (toolbarItem.Text == "Settings")
            {
                await Navigation.PushModalAsync(new NavigationPage(new SettingsPage()));
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
                await App.DatabaseHelper.UpdateReminderAync(reminder);

            }
            if (menuItem.Text == "Delete")
            {
                await App.DatabaseHelper.DeleteReminderAsync(reminder);
            }
            Refresh();
        }


        private async void Show_Reminder(object sender, SelectedItemChangedEventArgs e)
        {
            Reminder reminder = (Reminder)e.SelectedItem;
            await Navigation.PushModalAsync(new NavigationPage(new ReminderPage(reminder)));
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            _count++;

            var list = new List<string>
            {
                typeof(NotificationPage).FullName,
                _count.ToString()
            };

            var serializer = new ObjectSerializer<List<string>>();
            var serializeReturningData = serializer.SerializeObject(list);

            var request = new NotificationRequest
            {
                NotificationId = 100,
                Title = "Test",
                Description = $"Tap Count: {_count}",
                BadgeNumber = _count,
                ReturningData = serializeReturningData,
                Android =
                {
                    //IconName = "my_icon",
                    //AutoCancel = false,
                    //Ongoing = true
                },
            };

            // if not specified, default sound will be played.
            /*
            if (CustomSoundSwitch.IsToggled)
            {
                request.Sound = Device.RuntimePlatform == Device.Android
                    ? "good_things_happen"
                    : "good_things_happen.aiff";
            }
            */

            // if not specified, notification will show immediately.
            /*
            if (UseNotifyTimeSwitch.IsToggled)
            {
                var notifyDateTime = NotifyDatePicker.Date.Add(NotifyTimePicker.Time);
                if (notifyDateTime <= DateTime.Now)
                {
                    notifyDateTime = DateTime.Now.AddSeconds(10);
                }
                request.NotifyTime = notifyDateTime;
            }
            */

            NotificationCenter.Current.Show(request);
        }
    }
}
