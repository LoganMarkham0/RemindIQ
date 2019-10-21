using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RemindIQ.Models
{
    public class RemindDBContext : DbContext
    {
        public RemindDBContext(DbContextOptions options) : base(options)
        {
            //nothing needed here
        }

        public DbSet<Reminder> Reminders { get; set; }
    }
}
