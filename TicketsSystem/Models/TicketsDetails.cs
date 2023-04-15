using System;

namespace TicketsSystem.Models
{
    public class TicketsDetails
    {
        public string FromDestination { get; set; }
        public string ToDestination { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal Price { get; set; }
    }
}
