using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBookingSystem.entity
{
    public class Sports : Event
    {
        public string SportName { get; set; }
        public string TeamsName { get; set; }

        public Sports() { }

        public Sports(string eventName, DateTime eventDate, TimeSpan eventTime, Venue venue,
                      int totalSeats, decimal ticketPrice, string sportName, string teamsName)
            : base(eventName, eventDate, eventTime, venue, totalSeats, ticketPrice, "Sports")
        {
            SportName = sportName;
            TeamsName = teamsName;
        }

        public override void DisplayEventDetails()
        {
            Console.WriteLine($"\n[Sports]");
            Console.WriteLine($"Event: {EventName}, Date: {EventDate.ToShortDateString()}, Time: {EventTime}");
            Venue.DisplayVenueDetails();
            Console.WriteLine($"Sport: {SportName}, Teams: {TeamsName}");
            Console.WriteLine($"Seats: {AvailableSeats}/{TotalSeats}, Price: ₹{TicketPrice}");
        }
    }
}