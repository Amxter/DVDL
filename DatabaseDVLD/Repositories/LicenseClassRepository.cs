using System;
using System.Data;
using System.Data.SqlClient;

namespace DatabaseDVLD
{
    public class LicenseClassRepository : ILicenseClassRepository
    {

        private readonly ILogger _logger;

        public LicenseClassRepository( )
        {
            _logger = new FileLogger();
        }
 
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
                    catch (Exception ex)
                    {dataTable = new DataTable();
                        _logger.Error("Error while get all license class", ex);
                    }
    
                }
            }
            return dataTable;
        }
      
    }
}


