using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBookingSystem.Exception
{
    public static class HandleNullPointer
    {
        public static void SafeExecute(Action action)
        {
            try
            {
                action.Invoke();
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine("Null reference occurred: " + ex.Message);
            }
        }
    }
}