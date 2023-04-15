using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
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
                var table = db.GetCollection<TicketsDetails>("Tickets");
                var rows = table.FindAll();

                if (rows != null)
                    tickets = rows.ToList();
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
    }
}
