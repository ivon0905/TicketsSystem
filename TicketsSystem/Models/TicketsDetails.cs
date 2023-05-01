using System;

namespace TicketsSystem.Models
{
    public class TicketsDetails
    {
        public TicketsDetails(string fromDestination, string toDestination, DateTime startTime, DateTime endTime, 
                              decimal price)
        {
            FromDestination = fromDestination;
            ToDestination = toDestination;
            StartTime = startTime;
            EndTime = endTime;
            Price = price;
        }

        public TicketsDetails() { }

        public string FromDestination { get; set; }
        public string ToDestination { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal Price { get; set; }
    }
}
