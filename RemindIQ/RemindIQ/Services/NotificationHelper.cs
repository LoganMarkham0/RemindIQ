using System;
using System.Collections.Generic;
using System.Text;
using Plugin.LocalNotification;
using RemindIQ.Models;
using RemindIQ.Services;
using System.IO;
using RemindIQ.Views;
using Xamarin.Essentials;
using System.Threading;

namespace RemindIQ.Services
{
    class NotificationHelper
    {
        List<Reminder> fromDatabase;

        public NotificationHelper()
        {
            
        }
        public static void pushNotification(Reminder sender)
        {
            var list = new List<string>
            {
                typeof(MainPage).FullName,
            };

            var serializer = new ObjectSerializer<List<string>>();
            var serializeReturningData = serializer.SerializeObject(list);

            var request = new NotificationRequest//the notification object that will be shown to the user in the end
            {
                NotificationId = 100,
                Title = $"{sender.Name} is within the specified range.",//reminder name should go here
                Description = sender.Notes,//reminder descriptions should go here
                BadgeNumber = 0,
                ReturningData = serializeReturningData,
                Android =
                {
                    //IconName = "my_icon",
                    //AutoCancel = false,
                    //Ongoing = true//not really sure what this boolean does
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

            NotificationCenter.Current.Show(request);//sends object to the notification center, which is in the Android/iOS code
        }
        
        public async void UpdateDistanceManual()
        {
            Location location1 = await LocationHelper.CurrentLocation();
            Location location2 = new Location();
            double distance = 0;
            fromDatabase = await App.DatabaseHelper.GetRemindersAsync(4);
            foreach (Reminder rem in fromDatabase)
            {
                location2.Latitude = rem.Latitude;
                location2.Longitude = rem.Longitude;
                distance = LocationHelper.DistanceBetween(location1, location2);
                rem.DistanceToDestination = distance;
                if (distance < rem.Range)
                {
                    pushNotification(rem);
                }
                await App.DatabaseHelper.UpdateReminderAsync(rem);
            }  
        }
    }
}
