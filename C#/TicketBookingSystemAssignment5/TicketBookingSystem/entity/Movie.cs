using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBookingSystem.entity
{
    public class Movie : Event
    {
        public string Genre { get; set; }
        public string ActorName { get; set; }
        public string ActressName { get; set; }

        public Movie() { }

        public Movie(string eventName, DateTime eventDate, TimeSpan eventTime, Venue venue,
                     int totalSeats, decimal ticketPrice, string genre, string actorName, string actressName)
            : base(eventName, eventDate, eventTime, venue, totalSeats, ticketPrice, "Movie")
        {
            Genre = genre;
            ActorName = actorName;
            ActressName = actressName;
        }

        public override void DisplayEventDetails()
        {
            Console.WriteLine($"\n[Movie]");
            Console.WriteLine($"Event: {EventName}, Date: {EventDate.ToShortDateString()}, Time: {EventTime}");
            Venue.DisplayVenueDetails();
            Console.WriteLine($"Genre: {Genre}, Actor: {ActorName}, Actress: {ActressName}");
            Console.WriteLine($"Seats: {AvailableSeats}/{TotalSeats}, Price: ₹{TicketPrice}");
        }
    }
}