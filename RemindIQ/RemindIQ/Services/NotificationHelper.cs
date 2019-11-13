using System;
using System.Collections.Generic;
using System.Text;
using Plugin.LocalNotification;
using RemindIQ.Models;
using RemindIQ.Services;
using System.IO;
using RemindIQ.Views;

namespace RemindIQ.Services
{
    class NotificationHelper
    {
        public static void pushNotification(object sender)
        {
            var list = new List<string>
            {
                typeof(NotificationPage).FullName,
                //_count.ToString()
            };

            var serializer = new ObjectSerializer<List<string>>();
            var serializeReturningData = serializer.SerializeObject(list);

            var request = new NotificationRequest//the notification object that will be shown to the user in the end
            {
                NotificationId = 100,//think this has to match the channel id
                Title = "Test",//reminder name should go here
                Description = "test",//$"Tap Count: {_count}",//reminder descriptions should go here
                BadgeNumber = 0,//_count, //not sure about this one
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
    }
}
