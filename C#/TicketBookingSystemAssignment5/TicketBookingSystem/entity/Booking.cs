using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBookingSystem.entity
{
    public class Booking
    {
        public static int BookingCounter = 1000;
        public int BookingId { get; set; }
        public HashSet<Customer> Customers { get; set; }
        public Event Event { get; set; }
        public int NumTickets { get; set; }
        public decimal TotalCost { get; set; }
        public DateTime BookingDate { get; set; }

        public Booking(Customer customer, Movie movie, int numTickets)
        {
            NumTickets = numTickets;
        }

        public Booking(HashSet<Customer> customers, Event ev, int numTickets)
        {
            BookingId = ++BookingCounter;
            Customers = customers;
            Event = ev;
            NumTickets = numTickets;
            TotalCost = CalculateBookingCost(numTickets);
            BookingDate = DateTime.Now;
        }

        public decimal CalculateBookingCost(int numTickets)
        {
            return numTickets * Event.TicketPrice;
        }

        public void BookTickets()
        {
            Event.BookTickets(NumTickets);
            Console.WriteLine($"Booking ID: {BookingId}, Total Cost: ₹{TotalCost}");
        }

        public void CancelBooking()
        {
            Event.CancelBooking(NumTickets);
            Console.WriteLine($"Booking ID {BookingId} cancelled.");
        }

        public void DisplayBookingDetails()
        {
            Console.WriteLine($"\n--- Booking Details ---");
            Console.WriteLine($"Booking ID: {BookingId}\nDate: {BookingDate}");
            Event.DisplayEventDetails();
            Console.WriteLine($"Tickets: {NumTickets}, Total Cost: ₹{TotalCost}");
            Console.WriteLine("Customers:");
            foreach (var c in Customers)
            {
                c.DisplayCustomerDetails();
            }
        }
    }
}