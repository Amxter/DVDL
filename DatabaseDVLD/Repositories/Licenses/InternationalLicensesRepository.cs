using System;
using System.Data;
using System.Data.SqlClient;

namespace DatabaseDVLD
{
    public class InternationalLicensesRepository : IInternationalLicensesRepository
    {

        private readonly ILogger _logger;

        public InternationalLicensesRepository()
        {
            _logger = new EventLogs();
        }


        public int Add(InternationalLicense internationalLicense)
        {

            internationalLicense.InternationalLicenseID = -1;

            using (SqlConnection conn = new SqlConnection(DatabaseSittings.connectionString))
            {
                string query = @"
                   INSERT INTO [dbo].[InternationalLicenses]
                              ([ApplicationID]
                              ,[DriverID]
                              ,[IssuedUsingLocalLicenseID]
                              ,[IssueDate]
                              ,[ExpirationDate]
                              ,[IsActive]
                              ,[CreatedByUserID])
                        VALUES
                    (@ApplicationID , @DriverID , @IssuedUsingLocalLicenseID,  @IssueDate, @ExpirationDate  , @IsActive ,   @CreatedByUserID) ;
                     SELECT SCOPE_IDENTITY();
                                                      ";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ApplicationID", internationalLicense.ApplicationID);
                    cmd.Parameters.AddWithValue("@DriverID", internationalLicense.DriverID);
                    cmd.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", internationalLicense.IssuedUsingLocalLicenseID);
                    cmd.Parameters.AddWithValue("@IssueDate", internationalLicense.IssueDate);
                    cmd.Parameters.AddWithValue("@ExpirationDate", internationalLicense.ExpirationDate);
                    cmd.Parameters.AddWithValue("@IsActive", internationalLicense.IsActive);
                    cmd.Parameters.AddWithValue("@CreatedByUserID", internationalLicense.CreatedByUserID);

                    try
                    {
                        conn.Open();
                        object result = cmd.ExecuteScalar();

                        internationalLicense.InternationalLicenseID = Convert.ToInt32(result);

                    }
                    catch (Exception ex)
                    {
                        _logger.Error("Error while adding international license", ex);
                    }
                }
            }

            return internationalLicense.InternationalLicenseID;
        }
        public int DoesHaveActiveInternationalLicense(int licenseID)
        {
            int hasActiveLicense = -1 ;
            using (SqlConnection conn = new SqlConnection(DatabaseSittings.connectionString))
            {
                string query = @"
                    SELECT InternationalLicenseID
                    FROM [dbo].[InternationalLicenses] 
                    WHERE IssuedUsingLocalLicenseID = @IssuedUsingLocalLicenseID AND IsActive = 1";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", licenseID);
                    try
                    {
                        conn.Open();
                        object result = cmd.ExecuteScalar();

                        hasActiveLicense = Convert.ToInt32(result);
                    }
                    catch (Exception ex)
                    {
                        hasActiveLicense = -1;
                        _logger.Error("Error while checking active international license", ex);
                    }
                }
            }
            return hasActiveLicense;
        }
        public InternationalLicense GetByID(int internationalLicenseID)
        {
            InternationalLicense internationalLicense = null;
            using (SqlConnection conn = new SqlConnection(DatabaseSittings.connectionString))
            {
                string query = @"
                    SELECT InternationalLicenseID, ApplicationID, DriverID, IssuedUsingLocalLicenseID, IssueDate, ExpirationDate, IsActive, CreatedByUserID
                    FROM [dbo].[InternationalLicenses]
                    WHERE InternationalLicenseID = @InternationalLicenseID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@InternationalLicenseID", internationalLicenseID);
                    try
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                internationalLicense = new InternationalLicense
                                {
                                    InternationalLicenseID = reader.GetInt32(0),
                                    ApplicationID = reader.GetInt32(1),
                                    DriverID = reader.GetInt32(2),
                                    IssuedUsingLocalLicenseID = reader.GetInt32(3),
                                    IssueDate = reader.GetDateTime(4),
                                    ExpirationDate = reader.GetDateTime(5),
                                    IsActive = reader.GetBoolean(6),
                                    CreatedByUserID = reader.GetInt32(7)
                                };
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.Error("Error while retrieving international license by ID", ex);
                    }
                }
            }
            return internationalLicense;


        }

        public DataTable GetAll ()
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection conn = new SqlConnection(DatabaseSittings.connectionString))
            {

                string query = @" SELECT InternationalLicenseID, ApplicationID, DriverID, IssuedUsingLocalLicenseID, IssueDate, ExpirationDate, IsActive
                    FROM     InternationalLicenses";


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
                        _logger.Error("Error while get all international licenses", ex);
                    }

                }
            }
            return dataTable;


        }
    }

}

