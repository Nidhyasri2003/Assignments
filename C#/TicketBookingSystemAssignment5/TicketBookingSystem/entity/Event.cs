using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBookingSystem.entity
{
    public abstract class Event
    {
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public TimeSpan EventTime { get; set; }
        public Venue Venue { get; set; } // Reference to Venue object
        public int TotalSeats { get; set; }
        public int AvailableSeats { get; set; }
        public decimal TicketPrice { get; set; }
        public string EventType { get; set; }

        public Event() { }

        public Event(string eventName, DateTime eventDate, TimeSpan eventTime, Venue venue,
                     int totalSeats, decimal ticketPrice, string eventType)
        {
            EventName = eventName;
            EventDate = eventDate;
            EventTime = eventTime;
            Venue = venue;
            TotalSeats = totalSeats;
            AvailableSeats = totalSeats;
            TicketPrice = ticketPrice;
            EventType = eventType;
        }

        public decimal CalculateTotalRevenue()
        {
            return (TotalSeats - AvailableSeats) * TicketPrice;
        }

        public int GetBookedNoOfTickets()
        {
            return TotalSeats - AvailableSeats;
        }

        public void BookTickets(int numTickets)
        {
            if (AvailableSeats >= numTickets)
            {
                AvailableSeats -= numTickets;
                Console.WriteLine($"{numTickets} ticket(s) booked.");
            }
            else
            {
                Console.WriteLine("Not enough available seats.");
            }
        }

        public void CancelBooking(int numTickets)
        {
            AvailableSeats += numTickets;
            if (AvailableSeats > TotalSeats)
                AvailableSeats = TotalSeats;
            Console.WriteLine($"{numTickets} ticket(s) cancelled.");
        }

        public abstract void DisplayEventDetails();
    }
}