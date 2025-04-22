using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBookingSystem.Exception
{
    public class EventNotFoundException : System.Exception
    {
        public EventNotFoundException() : base("Event not found!") { }

        public EventNotFoundException(string message) : base(message) { }
    }
}