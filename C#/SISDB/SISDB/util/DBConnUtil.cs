using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISDB.util
{
    public static class DBConnUtil
    {
        public static SqlConnection GetConnection()
        {
            string connStr = "Data Source=JAYASRI;Initial Catalog=SISDB;Integrated Security=True";
            return new SqlConnection(connStr);
        }
    }
}
