using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RemindIQ.Services;
using System.IO;
using RemindIQ.Views;

namespace RemindIQ
{
    public partial class App : Application
    {
        static DatabaseHelper databaseHelper;
        static LocationHelper locationHelper;
        public App()
        {
            InitializeComponent();
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
    }
}
