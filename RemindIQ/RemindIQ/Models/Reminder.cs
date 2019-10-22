

namespace RemindIQ.Models
{
    public class Reminder 
        {
            public string Id { get; set; }

            public string Name { get; set; }

            public string DestinationAddress { get; set; }

            public double DestinationLatitude { get; set; }

            public double DestinationLongitude { get; set; }

            public double Range { get; set; }

            public double DistanceToDestination { get; set; }

            public string Notes { get; set; }

            public int Status { get; set; }


        }
    
}
