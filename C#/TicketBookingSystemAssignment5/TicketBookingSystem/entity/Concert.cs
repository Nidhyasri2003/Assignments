using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBookingSystem.entity
{
    public class Concert : Event
    {
        public string Artist { get; set; }
        public string Type { get; set; }

        public Concert() { }

        public Concert(string eventName, DateTime eventDate, TimeSpan eventTime, Venue venue,
                       int totalSeats, decimal ticketPrice, string artist, string type)
            : base(eventName, eventDate, eventTime, venue, totalSeats, ticketPrice, "Concert")
        {
            Artist = artist;
            Type = type;
        }

        public override void DisplayEventDetails()
        {
            Console.WriteLine($"\n[Concert]");
            Console.WriteLine($"Event: {EventName}, Date: {EventDate.ToShortDateString()}, Time: {EventTime}");
            Venue.DisplayVenueDetails();
            Console.WriteLine($"Artist: {Artist}, Type: {Type}");
            Console.WriteLine($"Seats: {AvailableSeats}/{TotalSeats}, Price: ₹{TicketPrice}");
        }
    }
}