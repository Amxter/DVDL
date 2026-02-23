using System;
using System.Data;
using System.Data.SqlClient;

namespace DatabaseDVLD
{
    public class CountryRepository : ICountryRepository
    {

        private readonly ILogger _logger;

        public CountryRepository( )
        {
            _logger = new EventLogs();
        }

        public DataTable GetAll()
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection conn = new SqlConnection(DatabaseSittings.ConnectionString))
            {

                string query = @" SELECT * FROM Countries ";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {


                    try
                    {

                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            dataTable.Load(reader);
                        }
                    }
                    catch (Exception ex)
                    { dataTable = new DataTable();
                        _logger.Error("Error while get all countries", ex);
                    }
 
                }
            }
            return dataTable;

        }
        public string GetCountryNameByID(int country )
        {



            string countryName = string.Empty;
            using (SqlConnection conn = new SqlConnection(DatabaseSittings.ConnectionString))
            {
                string query = @" SELECT CountryName FROM Countries WHERE CountryID = @CountryID ";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CountryID", country);
                    try
                    {
                        conn.Open();
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            countryName = result.ToString();
                        }
                    }
                    catch (Exception ex)
                    { countryName = string.Empty;
                        _logger.Error("Error while get country name by ID", ex);
                    }
    
                }
            }
            return countryName;
        }
    }
}
