using System.Collections.Generic;
using RemindIQ.Models;
using System.Threading.Tasks;
using SQLite;


namespace RemindIQ.Services
{
    public class DatabaseHelper
    {
        readonly SQLiteAsyncConnection Database;
        
        public DatabaseHelper(string DatabasePath)
        {
            Database = new SQLiteAsyncConnection(DatabasePath);
            Database.CreateTableAsync<Reminder>().Wait();
        }

        public Task<int> AddOrUpdateReminderAsync(Reminder reminder)
        {
            if(reminder.Id != 0)
            {
                return Database.UpdateAsync(reminder);
            }
            return Database.InsertAsync(reminder);
        }

        public Task<int> DeleteReminderAsync(Reminder reminder)
        {
            return Database.DeleteAsync(reminder);
        }

        public Task<Reminder> GetReminderAsync(int id)
        {
            return Database.Table<Reminder>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }
        
        public Task<List<Reminder>> GetAllRemindersAsync()
        {
            return Database.Table<Reminder>().ToListAsync();
        }
        
        public Task<List<Reminder>> GetOpenRemindersAsync()
        {
            return Database.QueryAsync<Reminder>("SELECT * FROM [Reminder] WHERE [Status] = 1");
        }
        
        public Task<List<Reminder>> GetMissedRemindersAsync()
        {
            return Database.QueryAsync<Reminder>("SELECT * FROM [Reminder] WHERE [Status] = 2");
        }
        
        public Task<List<Reminder>> GetClosedRemindersAsync()
        {
            return Database.QueryAsync<Reminder>("SELECT * FROM [Reminder] WHERE [Status] = 3");
        }
    }
}
