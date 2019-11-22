using System.Collections.Generic;
using RemindIQ.Models;
using System.Threading.Tasks;
using SQLite;


namespace RemindIQ.Services
{
    public class DatabaseHelper
    {
        SQLiteAsyncConnection Database;

        public DatabaseHelper(string DatabasePath)
        {
            Database = new SQLiteAsyncConnection(DatabasePath);
            Database.CreateTableAsync<Reminder>().Wait();
        }

        public Reminder Reminder
        {
            get => default(Reminder);
            set
            {
            }
        }

        public Task<int> AddReminderAsync(Reminder reminder)
        {
            return Database.InsertAsync(reminder);
        }
        public Task<int> UpdateReminderAync(Reminder reminder)
        {
            return Database.UpdateAsync(reminder);
        }

        public Task<int> DeleteReminderAsync(Reminder reminder)
        {
            return Database.DeleteAsync(reminder);
        }

        public Task<Reminder> GetReminderAsync(int id)
        {
            return Database.Table<Reminder>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        
        public Task<List<Reminder>> GetRemindersAsync(int x)
        {
            if(x == 4)
            {
                return App.DatabaseHelper.Database.Table<Reminder>().ToListAsync();
            }
            return App.DatabaseHelper.Database.Table<Reminder>().Where(i => i.Status.Equals(x)).ToListAsync();
        }

    }
}
