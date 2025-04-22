using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBookingSystem.entity;

namespace TicketBookingSystem.Util
{
    public class EventComparer : IEqualityComparer<Event>, IComparer<Event>
    {
        public bool Equals(Event x, Event y)
        {
            return x.EventName.Equals(y.EventName) && x.Venue.VenueName.Equals(y.Venue.VenueName);
        }

        public int GetHashCode(Event obj)
        {
            return obj.EventName.GetHashCode() ^ obj.Venue.VenueName.GetHashCode();
        }

        public int Compare(Event x, Event y)
        {
            int nameCompare = x.EventName.CompareTo(y.EventName);
            if (nameCompare != 0)
                return nameCompare;
            return x.Venue.VenueName.CompareTo(y.Venue.VenueName);
        }
    }
}