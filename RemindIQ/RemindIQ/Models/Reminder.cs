using SQLite;

namespace RemindIQ.Models
{
    public class Reminder
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string DestinationAddress { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Range { get; set; }
        public double DistanceToDestination { get; set; }
        public string Notes { get; set; }
        public int Status { get; set; }
        public bool HasBeenNotified { get; set; } 
    }
}
