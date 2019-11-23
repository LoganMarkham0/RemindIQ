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
        public static DatabaseHelper databaseHelper;
        public static LocationHelper locationHelper;
        public static NotificationHelper notificationHelper;
        public App()
        {
            InitializeComponent();
            NotificationCenter.Current.NotificationTapped += LoadPageFromNotification;
            MainPage = new NavigationPage(new MainPage());
            databaseHelper = new DatabaseHelper(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "RemindIQ.db3"));
            locationHelper = new LocationHelper();
            notificationHelper = new NotificationHelper();
            new Thread(new ThreadStart(UpdaterThread)).Start();
        }
        private void UpdaterThread()
        {
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
