using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace TestAdoNet
{
    public class DbProvider
    {
        string connectionString;
        protected SqlConnection SqlConnection;
        public DbProvider()
        {
            connectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
            SqlConnection = new SqlConnection(connectionString);
        }

    }
}
