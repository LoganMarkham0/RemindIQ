using System.Collections.Generic;
using RemindIQ.Models;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Linq;

namespace RemindIQ.Services
{
    class DatabaseHelper : IDatabaseHelper<Reminder>
    {
        readonly List<Reminder> Reminders;
        public DatabaseHelper()
        {
            Reminders = new List<Reminder>();
            //populate internal list from database
        }

        public async Task<bool> AddReminderAsync(Reminder reminder)
        {
            Reminders.Add(reminder);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateReminderAsync(Reminder reminder)
        {
            var oldReminder = Reminders.Where((Reminder arg) => arg.Id == reminder.Id).FirstOrDefault();
            Reminders.Remove(oldReminder);
            Reminders.Add(reminder);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteReminderAsync(string id)
        {
            var oldReminder = Reminders.Where((Reminder arg) => arg.Id == id).FirstOrDefault();
            Reminders.Remove(oldReminder);
            return await Task.FromResult(true);
        }

        public async Task<Reminder> GetReminderAsync(string id)
        {
            return await Task.FromResult(Reminders.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Reminder>> GetRemindersAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(Reminders);
        }
    }
}
