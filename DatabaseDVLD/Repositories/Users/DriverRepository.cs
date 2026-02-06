using System;
using System.Data;
using System.Data.SqlClient;

namespace DatabaseDVLD
{
    public class DriverRepository : IDriverRepository
    {
        private readonly ILogger _logger;

        public DriverRepository()
        {
            _logger = new FileLogger();
        }

        public int Add(Driver driver)
        {


            driver.DriverID = -1;

            using (SqlConnection conn = new SqlConnection(DatabaseSittings.connectionString))
            {
                string query = @"
            INSERT INTO [dbo].[Drivers]
           ([PersonID]
           ,[CreatedByUserID]
            ,[CreatedDate])
            VALUES
             
                                (@PersonID , @CreatedByUserID , @CreatedDate  ) ;
                     SELECT SCOPE_IDENTITY();
                                                      ";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PersonID", driver.PersonID);
                    cmd.Parameters.AddWithValue("@CreatedByUserID", driver.CreatedByUserID);
                    cmd.Parameters.AddWithValue("@CreatedDate", driver.CreatedDate);

                    try
                    {
                        conn.Open();
                        object result = cmd.ExecuteScalar();

                        driver.DriverID = Convert.ToInt32(result);

                    }
                    catch (Exception ex)
                    {
                        _logger.Error("Error while adding driver", ex);
                    }



                }
            }

            return driver.DriverID ;

        }

        public DataTable GetAll ()
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection conn = new SqlConnection(DatabaseSittings.connectionString))
            {
                string query = @"  SELECT Drivers.DriverID, Drivers.PersonID, People.NationalNo , ( People.FirstName + ' ' + People.SecondName+ ' ' + People.ThirdName+ ' ' + People.LastName ) as 'Full Name', Drivers.CreatedDate , (
                   SELECT count (Licenses.LicenseID  )
                   FROM     Licenses INNER JOIN
                   Applications ON Licenses.ApplicationID = Applications.ApplicationID INNER JOIN
                   People ON Applications.ApplicantPersonID = People.PersonID  
                   where Licenses.IsActive = 1 and People.PersonID = Drivers.PersonID )  as 'Active Licenses'
                   FROM     Drivers INNER JOIN
                   People ON Drivers.PersonID = People.PersonID ";
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
                        dataTable = null;
                        _logger.Error("Error while retrieving drivers", ex);
                    }
                }
            }
            return dataTable;
        }
        public int IsExistByPersonID(int personID)
        {
            int DriverID = -1;
            using (SqlConnection conn = new SqlConnection(DatabaseSittings.connectionString))
            {
                string query = @" SELECT  DriverID FROM Drivers WHERE PersonID = @PersonID ";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PersonID", personID);
                    try
                    {
                         
                            conn.Open();
                            object result = cmd.ExecuteScalar();

                            DriverID = Convert.ToInt32(result);
                        }
                    catch (Exception ex)
                    {
                        DriverID = -1 ;
                        _logger.Error("Error while checking if driver exists", ex);
                    }
                }
            }
            return DriverID;
        }

    }


}



