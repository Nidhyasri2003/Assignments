using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBookingSystem.entity;
using TicketBookingSystem.Util;

namespace TicketBookingSystem.dao
{
    public class EventServiceProviderImpl : IEventServiceProvider
    {
        protected HashSet<Event> events = new HashSet<Event>(new EventComparer());



        public virtual Event CreateEvent(string eventName, string date, string time, int totalSeats,
                                  decimal ticketPrice, string eventType, Venue venue)
        {
            DateTime eventDate = DateTime.Parse(date);
            TimeSpan eventTime = TimeSpan.Parse(time);
            Event newEvent;

            switch (eventType.ToLower())
            {
                case "movie":
                    newEvent = new Movie(eventName, eventDate, eventTime, venue, totalSeats, ticketPrice, "Action", "Actor", "Actress");
                    break;
                case "concert":
                    newEvent = new Concert(eventName, eventDate, eventTime, venue, totalSeats, ticketPrice, "Artist", "Rock");
                    break;
                case "sports":
                    newEvent = new Sports(eventName, eventDate, eventTime, venue, totalSeats, ticketPrice, "Cricket", "Team A vs Team B");
                    break;
                default:
                    throw new ArgumentException("Invalid Event Type");
            }

            events.Add(newEvent);
            Console.WriteLine($"Event '{eventName}' created successfully.");
            return newEvent;
        }

        public List<Event> GetEventDetails()
        {
            return events.ToList(); 
        }

        public int GetAvailableNoOfTickets()
        {
            int total = 0;
            foreach (var ev in events)
            {
                total += ev.AvailableSeats;
            }
            return total;
        }
    }
}