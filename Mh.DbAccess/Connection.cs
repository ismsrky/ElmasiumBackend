using System.Data.SqlClient;

namespace Mh.DbAccess
{
    public class Connection
    {
        public static string ConnectionString { get; set; }

        public static SqlConnection GetConn()
        {
            return new SqlConnection(ConnectionString);
        }

        public bool IsBussy { get; set; }
    }
}