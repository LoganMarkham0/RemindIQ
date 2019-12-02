using System;
using Xamarin.Forms;
using RemindIQ.Services;
using System.IO;
using RemindIQ.Views;
using System.Threading;
using Plugin.LocalNotification;
using System.Collections.Generic;
using RemindIQ.Models;

namespace RemindIQ
{
    public partial class App : Application
    {
        public static int RefreshInterval = 10000;
        public static DatabaseHelper databaseHelper = new DatabaseHelper(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "RemindIQ.db3"));
        public static LocationHelper locationHelper = new LocationHelper();
        public static NotificationHelper notificationHelper = notificationHelper = new NotificationHelper();
        public App()
        {
            InitializeComponent();
            locationHelper.UpdateAllDistances();
            NotificationCenter.Current.NotificationTapped += LoadPageFromNotification;
            MainPage = new NavigationPage(new MainPage());
            new Thread(new ThreadStart(UpdaterThread)).Start();
        }
        private void UpdaterThread()
        {
            Thread.Sleep(RefreshInterval);
            while (true)
            {
                Thread.Sleep(RefreshInterval);
                locationHelper.UpdateAllDistances();
            }
        }
        private async void LoadPageFromNotification(NotificationTappedEventArgs e)
        {
            var serializer = new ObjectSerializer<List<string>>();
            var list = serializer.DeserializeObject(e.Data);
            Reminder tappedReminder = await databaseHelper.GetReminderAsync(int.Parse(list[0]));
            await MainPage.Navigation.PushModalAsync(new NavigationPage(new ReminderPage(tappedReminder)));
        }
    }
}
