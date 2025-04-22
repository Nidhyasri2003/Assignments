using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBookingSystem.entity
{
    public class Venue
    {
        public string VenueName { get; set; }
        public string Address { get; set; }

        // Default Constructor
        public Venue()
        {
        }

        // Overloaded Constructor
        public Venue(string venueName, string address)
        {
            VenueName = venueName;
            Address = address;
        }

        // Display method
        public void DisplayVenueDetails()
        {
            Console.WriteLine($"\nVenue Name: {VenueName}\nAddress: {Address}\n");
        }
    }
}