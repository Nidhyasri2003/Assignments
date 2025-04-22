using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBookingSystem.entity;

namespace TicketBookingSystem.dao
{
    public interface IBookingSystemServiceProvider
    {
        decimal CalculateBookingCost(int numTickets, Event ev);
        Booking BookTickets(string eventName, int numTickets, List<Customer> customers);
        void CancelBooking(int bookingId);
        Booking GetBookingDetails(int bookingId);
    }
}