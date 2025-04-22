using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBookingSystem.entity;
using TicketBookingSystem.Exception;

namespace TicketBookingSystem.dao
{
    public class BookingSystemServiceProviderImpl : EventServiceProviderImpl, IBookingSystemServiceProvider
    {
        private List<Booking> bookings = new List<Booking>();

        public decimal CalculateBookingCost(int numTickets, Event ev)
        {
            return numTickets * ev.TicketPrice;
        }

        public Booking BookTickets(string eventName, int numTickets, List<Customer> customerList)
        {
            Event selectedEvent = events.FirstOrDefault(e => e.EventName.Equals(eventName, StringComparison.OrdinalIgnoreCase));

            if (selectedEvent == null)
                throw new EventNotFoundException($"Event '{eventName}' not found.");

            if (numTickets > selectedEvent.AvailableSeats)
                throw new System.Exception("Not enough tickets available.");

            var customerSet = new HashSet<Customer>(customerList); // No duplicates
            Booking booking = new Booking(customerSet, selectedEvent, numTickets);
            booking.BookTickets();
            bookings.Add(booking);
            return booking;
        }

        public void CancelBooking(int bookingId)
        {
            Booking booking = bookings.Find(b => b.BookingId == bookingId);

            if (booking == null)
                throw new InvalidBookingIDException($"Booking ID {bookingId} is not valid!");

            booking.CancelBooking();
            bookings.Remove(booking);
        }

        public Booking GetBookingDetails(int bookingId)
        {
            Booking booking = bookings.Find(b => b.BookingId == bookingId);
            if (booking == null)
                throw new System.Exception("Invalid Booking ID");

            return booking;
        }
    }
}