using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RemindIQ.Services
{
    public interface IDatabaseHelper<T>
    {
        Task<bool> AddReminderAsync(T reminder);
        Task<bool> UpdateReminderAsync(T reminder);
        Task<bool> DeleteReminderAsync(string id);
        Task<T> GetReminderAsync(string id);
        Task<IEnumerable<T>> GetRemindersAsync(bool forceRefresh = false);
    }
}
