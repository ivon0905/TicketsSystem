using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsSystem.Models;

namespace TicketsSystem.ViewModels
{
    public class CartViewModel : BaseViewModel
    {
        private ObservableCollection<TicketsDetails> _tickets;
        private decimal _sum;

        public CartViewModel(ObservableCollection<TicketsDetails> tickets, decimal sum)
        {
            _tickets = tickets;
            _sum = sum;
            
        }

        public ObservableCollection<TicketsDetails> Tickets
        {
            get
            {
                if (_tickets == null)
                    _tickets = new ObservableCollection<TicketsDetails>();
                return _tickets;
            }
        }

        public string StrTotalAmount
        {
            get { return "Общо: " + _sum.ToString("N2") + " лв."; }
        }
    }
}
