using System.Data.Entity;

namespace Timekeeping
{
    public class TimeRecordContext : DbContext
    {
        public DbSet<TimeRecord> TimeRecords { get; set; }
    }
}
