using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TicketsSystem.Commands;
using TicketsSystem.Models;
using TicketsSystem.Services;

namespace TicketsSystem.ViewModels
{
    public class ReservationViewModel : BaseViewModel
    {
        private const string path = "C:\\Users\\ivich\\Documents\\TU\\ВВПС\\Tickets.json";
        private string _fromDestination;
        private string _toDestination;
        private string _strDate;
        private bool _isOneWay;
        private bool _isTwoWay;
        private bool _hasChild;
        private bool _isFamily;
        private bool _isPensioner;
        private ObservableCollection<TicketstInformation> _calculatedTicketstInformation;

        private DelegateCommand searchCommand;

        public string FromDestination 
        { 
            get { return _fromDestination; } 
            set { _fromDestination = value; } 
        }

        public string ToDestination 
        { 
            get { return _toDestination; }
            set { _toDestination = value; }
        }

        public string StrDate
        {
            get { return _strDate; }
            set { _strDate = value; }
        }

        public bool IsOneWay
        {
            get { return _isOneWay; }
            set { _isOneWay = value; }
        }

        public bool IsTwoWay
        {
            get { return _isTwoWay; }
            set { _isTwoWay = value; }
        }

        public bool HasChild
        {
            get { return _hasChild; }
            set { _hasChild = value; }
        }

        public bool IsPensioner
        {
            get { return _isPensioner; }
            set { _isPensioner = value; }
        }

        public bool IsFamily
        {
            get { return _isFamily; }
            set { _isFamily = value; }
        }

        public ObservableCollection<TicketstInformation> CalculatedTicketsInformation
        {
            get
            {
                return _calculatedTicketstInformation ?? 
                    (_calculatedTicketstInformation = new ObservableCollection<TicketstInformation>());
            }
        }

        public DelegateCommand SearchCommand
        {
            get
            {
                if (searchCommand == null)
                    searchCommand = new DelegateCommand(Search);
                return searchCommand;
            }
        }
       
        private void Search(object o)
        {
            DateTime.TryParse(StrDate, out DateTime fromDate);
            //DateTime.TryParse(ToDate, out DateTime toDate);

            DatabaseService databaseService = new DatabaseService();
            IList<TicketsDetails> list = databaseService.GetTicketsDetails(FromDestination, ToDestination, fromDate);
            if (list != null && list.Count > 0)
                CalculateTickets(list);
        }

        private void CalculateTickets(IList<TicketsDetails> ticketsDetails)
        {
            CalculatedTicketsInformation.Clear();
            foreach (var ticket in ticketsDetails)
            {
                TicketstInformation ticketstInformation = new TicketstInformation();
                ticketstInformation.StartTime = ticket.StartTime;
                ticketstInformation.EndTime = ticket.EndTime;

                if ((ticket.StartTime.TimeOfDay >= new DateTime(1, 1, 1, 7, 30, 0).TimeOfDay &&
                    ticket.StartTime.TimeOfDay <= new DateTime(1, 1, 1, 9, 29, 59).TimeOfDay) ||
                    (ticket.StartTime.TimeOfDay >= new DateTime(1, 1, 1, 16, 0, 0).TimeOfDay &&
                    ticket.StartTime.TimeOfDay <= new DateTime(1, 1, 1, 19, 29, 59).TimeOfDay))
                {
                    ticketstInformation.Price = ticket.Price;
                    ticketstInformation.Discount = 0;
                }
                else
                {
                    decimal price = ticket.Price * (decimal)0.95;
                    ticketstInformation.Discount = 5;

                    if (IsFamily)
                    {
                        price *= (decimal)0.5;
                        ticketstInformation.Discount += 50;
                    }
                    else if (IsPensioner)
                    {
                        price *= (decimal)0.66;
                        ticketstInformation.Discount += 34;
                    }
                    else if (HasChild)
                    {
                        price *= (decimal)0.90;
                        ticketstInformation.Discount += 10;
                    }

                    ticketstInformation.Price = price;
                }

                CalculatedTicketsInformation.Add(ticketstInformation);
                OnPropertyChanged("CalculatedTicketsInformation");
            }
        }

        public struct TicketstInformation
        {
            public DateTime StartTime { get; set; }
            public DateTime EndTime { get; set; }
            public decimal Price { get; set; }
            public int Discount { get; set; }
        }
    }
}
