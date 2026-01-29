using System.Data;
using System.Data.SqlClient;

namespace DatabaseDVLD
{
    public class LicenseClassRepository : ILicenseClassRepository
    {
        public DataTable GetAll()
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection conn = new SqlConnection(DatabaseSittings.connectionString))
            {

                string query = @" SELECT[LicenseClassID]
      ,[ClassName]
      ,[ClassDescription]
      ,[MinimumAllowedAge]
      ,[DefaultValidityLength]
      ,[ClassFees]
  FROM [DVLD].[dbo].[LicenseClasses] ";

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
      
    }
}


