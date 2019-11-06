using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RemindIQ.Services;
using System.IO;
using RemindIQ.Views;
using Plugin.LocalNotification;
using System.Collections.Generic;

namespace RemindIQ
{
    public partial class App : Application
    {
        static DatabaseHelper databaseHelper;
        static LocationHelper locationHelper;
        public App()
        {
            InitializeComponent();

            NotificationCenter.Current.NotificationTapped += LoadPageFromNotification;

            MainPage = new NavigationPage(new MainPage());

            //MainPage = new MainPage();
        }

        public static DatabaseHelper DatabaseHelper
        {
            get
            {
                if (databaseHelper == null)
                {
                    databaseHelper = new DatabaseHelper(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "RemindIQ.db3"));
                }
                return databaseHelper;
            }
        }
        public static LocationHelper LocationHelper
        {
            get
            {
                if (locationHelper == null)
                {
                    locationHelper = new LocationHelper();
                }
                return locationHelper;
            }
        }
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        private void LoadPageFromNotification(NotificationTappedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(e.Data))
            {
                return;
            }

            var serializer = new ObjectSerializer<List<string>>();
            var list = serializer.DeserializeObject(e.Data);
            
            if(list.Count != 2)
            {
                return;
            }
            if(list[0] != typeof(NotificationPage).FullName)
            {
                return;
            }

            var count = list[1];

            //((NavigationPage)MainPage).Navigation.PushAsync(new NotificationPage(int.Parse(count)));
        }
    }
}
