using System.Configuration;

namespace TicketBookingSystem.Util
{
    public static class DBPropertyUtil
    {
        

        public static string GetConnectionString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}
