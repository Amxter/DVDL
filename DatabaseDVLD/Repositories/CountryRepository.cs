using System.Data;
using System.Data.SqlClient;

namespace DatabaseDVLD
{
    public class CountryRepository : ICountryRepository
    {
        public DataTable GetAll()
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection conn = new SqlConnection(DatabaseSittings.connectionString))
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
                    catch
                    {
                        dataTable = new DataTable();

                    }
                }
            }
            return dataTable;

        }

        public string GetCountryNameByID(int country )
        {



            string countryName = string.Empty;
            using (SqlConnection conn = new SqlConnection(DatabaseSittings.connectionString))
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
                    catch
                    {
                        countryName = string.Empty;
                    }
                }
            }
            return countryName;
        }
    }
}
