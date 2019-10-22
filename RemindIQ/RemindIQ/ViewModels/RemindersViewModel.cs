using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using RemindIQ.Models;
using RemindIQ.Views;
using System.Threading.Tasks;
using System.Diagnostics;

namespace RemindIQ.ViewModels
{
    class RemindersViewModel : BaseViewModel
    {
        public ObservableCollection<Reminder> Open { get; set; }
        public ObservableCollection<Reminder> Missed { get; set; }
        public ObservableCollection<Reminder> Closed { get; set; }
        public Command LoadOpenCommand { get; set; }
        public Command LoadMissedCommand { get; set; }
        public Command LoadClosedCommand { get; set; }

        public RemindersViewModel()
        {
            Open = new ObservableCollection<Reminder>();
            Missed = new ObservableCollection<Reminder>();
            Closed = new ObservableCollection<Reminder>();
            LoadOpenCommand = new Command(async () => await ExecuteLoadOpenCommand());
            LoadMissedCommand = new Command(async () => await ExecuteLoadMissedCommand());
            LoadClosedCommand = new Command(async () => await ExecuteLoadClosedCommand());

            MessagingCenter.Subscribe<NewReminderPage, Reminder>(this, "AddItem", async (obj, Reminder) =>
            {
                var newReminder = Reminder as Reminder;
                if (Reminder.Status == 0)
                {
                    Open.Add(newReminder);
                }
                if (Reminder.Status == 1)
                {
                    Missed.Add(newReminder);
                }
                if (Reminder.Status == 2)
                {
                    Closed.Add(newReminder);
                }
                await Database.AddReminderAsync(newReminder);
            });
        }

        async Task ExecuteLoadOpenCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Open.Clear();
                var Reminders = await Database.GetRemindersAsync(true);
                foreach (var Reminder in Reminders)
                {
                    if (Reminder.Status == 0)
                    {
                        Open.Add(Reminder);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
        async Task ExecuteLoadMissedCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Missed.Clear();
                var Reminders = await Database.GetRemindersAsync(true);
                foreach (var Reminder in Reminders)
                {
                    if (Reminder.Status == 1)
                    {
                        Missed.Add(Reminder);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
        async Task ExecuteLoadClosedCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Closed.Clear();
                var Reminders = await Database.GetRemindersAsync(true);
                foreach (var Reminder in Reminders)
                {
                    if (Reminder.Status == 2)
                    {
                        Closed.Add(Reminder);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
