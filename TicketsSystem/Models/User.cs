﻿namespace TicketsSystem.Models
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int UserRole { get; set; }
        public int TypeOfCard { get; set; }
    }
}
