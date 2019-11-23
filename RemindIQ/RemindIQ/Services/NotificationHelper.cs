using System.Collections.Generic;
using Plugin.LocalNotification;
using RemindIQ.Models;
using RemindIQ.Views;

namespace RemindIQ.Services
{
    public class NotificationHelper
    {
        public NotificationHelper()
        {
            
        }
        public void pushNotification(Reminder sender)
        {
            var list = new List<string>
            {
                sender.Id.ToString()
            };
            var serializer = new ObjectSerializer<List<string>>();
            var serializeReturningData = serializer.SerializeObject(list);
            var request = new NotificationRequest
            {
                NotificationId = 100,
                Title = "You are within range of a reminder!",
                Description = $"{sender.Name} is within the specified range.",
                BadgeNumber = 0,
                ReturningData = serializeReturningData,
            };
            NotificationCenter.Current.Show(request);
        }
    }
}
