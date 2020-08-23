using System.Configuration;
using System.Data.SqlClient;

namespace JSONWebAPI
{
    public class DBConnect
    {
        private static SqlConnection NewCon = new SqlConnection(@"Data Source=182.50.133.110;Persist Security Info=True;User ID=SmartPark;Password=prodromos!@#");

        public static SqlConnection getConnection()
        {
            return NewCon;
        }

        public DBConnect()
        {

        }
    }
}