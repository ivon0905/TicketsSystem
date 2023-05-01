using System;
using System.Collections.Generic;
using static TicketsSystem.ViewModels.ReservationViewModel;
using TicketsSystem.Models;
using System.Collections.ObjectModel;

namespace TicketsSystem.Services
{
    public class CalculationsService
    {
        public CalculationsService() { }

        public ObservableCollection<TicketsInformation> CalculateTicketsPrices(IList<TicketsDetails> tickets, 
            bool hasChild, int typeOfCard)
        {
            ObservableCollection<TicketsInformation> list = new ObservableCollection<TicketsInformation>();
            foreach (var ticket in tickets)
            {
                TicketsInformation ticketstInformation = new TicketsInformation();
                ticketstInformation.StartTime = ticket.StartTime;
                ticketstInformation.EndTime = ticket.EndTime;

                //Rush hour
                if (IsRushTime(ticket.StartTime))
                {
                    ticketstInformation.Price = ticket.Price;
                    ticketstInformation.Discount = 0;
                }
                //Off rush hour
                else
                {
                    //standard discount
                    decimal price = ticket.Price * (decimal)0.95; 
                    ticketstInformation.Discount = 5;

                    //family
                    if (typeOfCard == 1) 
                    {
                        price *= (decimal)0.5;
                        ticketstInformation.Discount += 50;
                    }
                    //pensioner
                    else if (typeOfCard == 2) 
                    {
                        price *= (decimal)0.66;
                        ticketstInformation.Discount += 34;
                    }
                    //standard with child
                    else if (typeOfCard == 0 && hasChild) 
                    {
                        price *= (decimal)0.90;
                        ticketstInformation.Discount += 10;
                    }

                    ticketstInformation.Price = price;
                }
                list.Add(ticketstInformation);
            }

            return list;
        }

        public bool IsRushTime(DateTime startTime)
        {
            return (startTime.TimeOfDay >= new DateTime(1, 1, 1, 7, 30, 0).TimeOfDay &&
                    startTime.TimeOfDay <= new DateTime(1, 1, 1, 9, 29, 59).TimeOfDay) ||
                    (startTime.TimeOfDay >= new DateTime(1, 1, 1, 16, 0, 0).TimeOfDay &&
                    startTime.TimeOfDay <= new DateTime(1, 1, 1, 19, 29, 59).TimeOfDay);
        }
    }
}
