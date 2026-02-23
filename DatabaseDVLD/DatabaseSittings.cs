
 
using System.Configuration;
namespace DatabaseDVLD
{

    public static class DatabaseSittings
    {
        //public static string connectionString = ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString;


        public static string ConnectionString => ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString;
    }
}
