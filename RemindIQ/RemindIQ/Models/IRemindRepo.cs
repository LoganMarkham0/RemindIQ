using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RemindIQ.Models
{
    public interface IRemindRepo
    {
        Reminder Create(Reminder reminder);

        Reminder Read(int id);

        IQueryable<Reminder> ReadAll();

        void Update(int id, Reminder person);

        void Delete(int id);

    }
}
