using System;
using System.Data;
using System.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;

namespace DatabaseDVLD
{
    public class LicenseClassRepository : ILicenseClassRepository
    {

        private readonly ILogger _logger;

        public LicenseClassRepository()
        {
            _logger = new EventLogs();
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
                    {
                        dataTable = new DataTable();
                        _logger.Error("Error while get all license class", ex);
                    }

                }
            }
            return dataTable;
        }
        public LicenseClass GetByID(int licenseClassID)
        {

            LicenseClass licenseClass = null;
            using (SqlConnection conn = new SqlConnection(DatabaseSittings.connectionString))
            {

                string query = "Select * from LicenseClasses   Where LicenseClassID = @LicenseClassID ";



                using (SqlCommand cmd = new SqlCommand(query, conn))
                {

                    cmd.Parameters.AddWithValue("@LicenseClassID", licenseClassID);


                    try
                    {

                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {



                                licenseClass = new LicenseClass
                                {
                                    LicenseClassID = Convert.ToInt32(reader["LicenseClassID"]),
                                    ClassName = reader["ClassName"].ToString(),
                                    ClassDescription = reader["ClassDescription"].ToString(),
                                    MinimumAllowedAge = Convert.ToInt32(reader["MinimumAllowedAge"]),
                                    DefaultValidityLength = Convert.ToInt32(reader["DefaultValidityLength"]),
                                    ClassFees = Convert.ToDecimal(reader["ClassFees"])
                                };
                            }


                            else
                            {

                                licenseClass = null;


                            }




                        }
                    }
                    catch (Exception ex)
                    {
                        licenseClass = null;
                        _logger.Error("Error while get license class by ID", ex);
                    }

                }
            }

            return licenseClass;
        }
    }




}

