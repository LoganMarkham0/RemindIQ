using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RemindIQ.Models
{
    public class RemindRepo : IRemindRepo
    {
        private RemindDBContext _db;

        public RemindRepo(RemindDBContext db)
        {
            _db = db;
        }

        public Reminder Create(Reminder reminder)
        {
            _db.Reminders.Add(reminder);
            _db.SaveChanges();
            return reminder;
        }

        public void Delete(int id)
        {
            Reminder reminderToDelete = Read(id);
            _db.Reminders.Remove(reminderToDelete);
            _db.SaveChanges();
        }

        public Reminder Read(int id)
        {
            return _db.Reminders.FirstOrDefault(r => r.Id == id);
        }

        public IQueryable<Reminder> ReadAll()
        {
            return _db.Reminders;
        }

        public void Update(int id, Reminder reminder)
        {
            Reminder reminderToUpdate = Read(id);

            reminderToUpdate.Name = reminder.Name;
            reminderToUpdate.Notes = reminder.Notes;
            reminderToUpdate.Range = reminder.Range;
            reminderToUpdate.Status = reminder.Status;
            reminderToUpdate.DestinationAddress = reminder.DestinationAddress;
            reminderToUpdate.DestinationLatitude = reminder.DestinationLatitude;
            reminderToUpdate.DestinationLongitude = reminder.DestinationLongitude;
            reminderToUpdate.DistanceToDestination = reminder.DistanceToDestination;
            _db.SaveChanges();
        }
    }
}
