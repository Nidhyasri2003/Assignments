using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;

namespace BankingSystem.util
{
    public class DBUtil
    {
        public static SqlConnection GetConnection()
        {
            string connStr = "Server=JAYASRI;Database=HMBank;Trusted_Connection=True;";
            return new SqlConnection(connStr);
        }
    }
}