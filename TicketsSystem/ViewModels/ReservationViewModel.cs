using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using TicketsSystem.Commands;
using TicketsSystem.Models;
using TicketsSystem.Services;
using TicketsSystem.Views;

namespace TicketsSystem.ViewModels
{
    public class ReservationViewModel : BaseViewModel
    {
        private string _fromDestination;
        private string _toDestination;
        private string _direction;
        private string _oppositeDirection;
        private DateTime _fromDate;
        private DateTime _toDate;
        private bool _isOneWay;
        private bool _isTwoWay;
        private bool _hasChild;
        private User _user;
        private TicketsInformation _selectedTicket;
        private CalculationsService _calculationsService;
        private List<string> _listDestinations;
        private ObservableCollection<TicketsInformation> _calculatedTicketstInformation;
        private ObservableCollection<TicketsInformation> _calculatedTicketstInformationOppositeDirection;
        private ObservableCollection<TicketsInformation> SelectedTickets;

        private DelegateCommand searchCommand;
        private DelegateCommand addTicketCommand;
        private DelegateCommand clearCartCommand;
        private DelegateCommand viewReservedTicketsCommand;

        public ReservationViewModel(User user)
        {
            _user = user;
            _listDestinations = new List<string> { "София", "Сандански", "Пловдив", "Бургас"};
            _calculationsService = new CalculationsService();
            SelectedTickets = new ObservableCollection<TicketsInformation>();
            _fromDate = DateTime.Now;
            _toDate = DateTime.Now;
        }

        public string FromDestination 
        { 
            get { return _fromDestination; } 
            set { _fromDestination = value; OnPropertyChanged(); } 
        }

        public string ToDestination 
        { 
            get { return _toDestination; }
            set { _toDestination = value; OnPropertyChanged(); }
        }

        public DateTime FromDate
        {
            get { return _fromDate; }
            set { _fromDate = value; OnPropertyChanged("FromDate"); }
        }

        public DateTime ToDate
        {
            get { return _toDate; }
            set { _toDate = value; OnPropertyChanged(); }
        }

        public string Direction
        {
            get { return _direction; }
            set { _direction = value; OnPropertyChanged("Direction"); }
        }

        public string OppositeDirection
        {
            get { return _oppositeDirection; }
            set { _oppositeDirection = value; OnPropertyChanged("OppositeDirection"); }
        }

        public bool IsOneWay
        {
            get { return _isOneWay; }
            set { _isOneWay = value; }
        }

        public bool IsTwoWay
        {
            get { return _isTwoWay; }
            set { _isTwoWay = value; OnPropertyChanged("IsTwoWay"); }
        }

        public bool HasChild
        {
            get { return _hasChild; }
            set { _hasChild = value; OnPropertyChanged("HasChild"); }
        }

        public TicketsInformation SelectedTicket
        {
            get { return _selectedTicket; }
            set
            {
                _selectedTicket = value;
                OnPropertyChanged();
            }
        }

        public List<string> ListDestinations
        {
            get { return _listDestinations; }
        }

        public ObservableCollection<TicketsInformation> CalculatedTicketsInformation
        {
            get
            {
                return _calculatedTicketstInformation ?? 
                    (_calculatedTicketstInformation = new ObservableCollection<TicketsInformation>());
            }
        }

        public ObservableCollection<TicketsInformation> CalculatedTicketsInformationOppositeDirection
        {
            get
            {
                return _calculatedTicketstInformationOppositeDirection ??
                    (_calculatedTicketstInformationOppositeDirection = new ObservableCollection<TicketsInformation>());
            }
        }

        public DelegateCommand AddTicketCommand
        {
            get
            {
                return addTicketCommand ??
                    (addTicketCommand = new DelegateCommand(AddTicket));
            }
        }

        public DelegateCommand ClearCartCommand
        {
            get
            {
                return clearCartCommand ??
                    (clearCartCommand = new DelegateCommand(ClearCart));
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

        public DelegateCommand ViewReservedTicketsCommand
        {
            get
            {
                if (viewReservedTicketsCommand == null)
                    viewReservedTicketsCommand = new DelegateCommand(ViewReservedTickets);
                return viewReservedTicketsCommand;
            }
        }


        private void Search(object o)
        {
            DatabaseService databaseService = new DatabaseService();
            IList<TicketsDetails> list = databaseService.GetTicketsDetails(FromDestination, ToDestination, FromDate);
            if (list != null && list.Count > 0)
            {
                CalculatedTicketsInformation.Clear();
                _calculatedTicketstInformation = _calculationsService.CalculateTicketsPrices(list, HasChild, _user.TypeOfCard);
                OnPropertyChanged("CalculatedTicketsInformation");
                Direction = string.Format("{0} -> {1}", FromDestination, ToDestination);
            }
            else
            {
                Direction = "Не са намерени данни!";
                CalculatedTicketsInformation.Clear();
                OnPropertyChanged("CalculatedTicketsInformation");
            }

            if (IsTwoWay)
            {
                IList<TicketsDetails> listOppositeDirection = databaseService.GetTicketsDetails(ToDestination, FromDestination, ToDate);
                if (listOppositeDirection != null && listOppositeDirection.Count > 0)
                {
                    CalculatedTicketsInformationOppositeDirection.Clear();
                    _calculatedTicketstInformationOppositeDirection = _calculationsService.CalculateTicketsPrices(listOppositeDirection, HasChild, _user.TypeOfCard);
                    OnPropertyChanged("CalculatedTicketsInformationOppositeDirection");
                    OppositeDirection = string.Format("{0} -> {1}", ToDestination, FromDestination);
                }
                else
                {
                    OppositeDirection = "Не са намерени данни!";
                    CalculatedTicketsInformationOppositeDirection.Clear();
                    OnPropertyChanged("CalculatedTicketsInformationOppositeDirection");
                }
            }
        }       

        public void ViewReservedTickets(object o)
        {
            ObservableCollection<TicketsDetails> tickets = new ObservableCollection<TicketsDetails>();
            decimal sum = 0;

            if (SelectedTicket.StartTime.Year != 1)
            {
                foreach (var ticket in SelectedTickets)
                {
                    TicketsDetails details = new TicketsDetails();
                    details.FromDestination = FromDestination;
                    details.ToDestination = ToDestination;
                    details.StartTime = ticket.StartTime;
                    details.EndTime = ticket.EndTime;
                    details.Price = ticket.Price;
                    tickets.Add(details);
                    sum += ticket.Price;
                }
            }            

            if (tickets.Count > 0)
            {
                CartView view = new CartView();
                view.DataContext = new CartViewModel(tickets, sum);
                view.Show();
            }
            else
            {
                MessageBox.Show("Не са избрани билети!");
            }
        }

        private void AddTicket(object o)
        {
            SelectedTickets.Add(SelectedTicket);
        }

        private void ClearCart(object o)
        {
            SelectedTickets.Clear();
            SelectedTicket = new TicketsInformation();
        }  
    }
}
