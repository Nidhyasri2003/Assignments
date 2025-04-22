using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;

namespace CourierManagementSystem.Util
{
    public static class DBConnUtil
    {
        public static SqlConnection GetConnection()
        {
            
            return new SqlConnection("Server=JAYASRI ;Database=Couriermanagement;Integrated Security=True;TrustServerCertificate=True");
        }
    }
}