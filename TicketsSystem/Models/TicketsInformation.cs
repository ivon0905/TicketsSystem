using System;

namespace TicketsSystem.Models
{
    public class TicketsInformation
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal Price { get; set; }
        public int Discount { get; set; }
        public bool Reserved { get; set; }
    }
}
