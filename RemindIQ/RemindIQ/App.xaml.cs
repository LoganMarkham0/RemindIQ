using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RemindIQ.Services;
using System.IO;
using RemindIQ.Views;
using Plugin.LocalNotification;
using System.Collections.Generic;
using RemindIQ.Models;
using System.ComponentModel;
using System.Threading;
using Xamarin.Essentials;

namespace RemindIQ
{
    public partial class App : Application
    {
        static DatabaseHelper databaseHelper;
        static LocationHelper locationHelper;
        //static NotificationHelper notificationHelper;
        

        public App()
        {
            InitializeComponent();

            NotificationCenter.Current.NotificationTapped += LoadPageFromNotification;//part of the notification sample

            MainPage = new NavigationPage(new MainPage());

            if (locationHelper == null)
            {
                locationHelper = new LocationHelper();
            }
            /*
            if (notificationHelper == null)
            {
                notificationHelper = new NotificationHelper();
            }
            */
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

        //also part of the notification sample, allows the user to go straight to the notification page through clicking the notification
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
            if(list[0] != typeof(MainPage).FullName)
            {
                return;
            }

            string rem = list[1];

            //this part is broken for some reason
            //((NavigationPage)MainPage).Navigation.PushAsync(new NotificationPage(rem));
        }
        /*
        public static AsyncOperation CheckForNotifications()
        {
            locationHelper.UpdateDistance();
            return null;
        }
        */
    }
}
