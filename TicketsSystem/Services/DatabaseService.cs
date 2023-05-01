using LiteDB;
using System;
using System.Collections.Generic;
using TicketsSystem.Models;

namespace TicketsSystem.Services
{
    public class DatabaseService
    {
        private const string path = "C:\\Users\\ivich\\Documents\\TU\\ВВПС\\Tickets.db";

        public IList<TicketsDetails> GetTicketsDetails(string fromDestination, string toDestination, DateTime date)
        {
            IList<TicketsDetails> tickets = null;

            using (var db = new LiteDatabase(path))
            {
                var collection = db.GetCollection<TicketsDetails>("Tickets");
                var rows = collection.Query()
                                     .Where(i => i.FromDestination == fromDestination && 
                                                 i.ToDestination == toDestination &&
                                                 i.StartTime.Date == date)
                                     .ToList();

                if (rows != null)
                    tickets = rows;
            }
            return tickets;
        }

        public void AddNewOrder(TicketsDetails order)
        {
            using (var db = new LiteDatabase(path))
            {
                var table = db.GetCollection<TicketsDetails>("Tickets");
                table.Insert(order);
            }
        }

        private const string pathUsers = "C:\\Users\\ivich\\Documents\\TU\\ВВПС\\Users.db";
        public void AddUser(User user)
        {
            using (var db = new LiteDatabase(pathUsers))
            {
                var table = db.GetCollection<User>("Users");
                table.Insert(user);
            }
        }

        public User FindUser(string username, string password)
        {
            User user = null;

            using (var db = new LiteDatabase(pathUsers))
            {
                var table = db.GetCollection<User>("Users");
                var userFound = table.FindOne(u => u.Username == username && u.Password == password);

                if (userFound != null)
                    user = userFound;
            }
                
            return user;
        }
    }
}
