using System;
using System.Data;
using System.Data.SqlClient;

namespace DatabaseDVLD
{
    public class DetainedLicenseRepository : IDetainedLicenseRepository
    {
        private readonly ILogger _logger;
        public DetainedLicenseRepository()
        {
            _logger = new EventLogs();
        }
        public int Add(DetainedLicense detainedLicense)
        {
            detainedLicense.DetainID = -1;

            using (SqlConnection conn = new SqlConnection(DatabaseSittings.connectionString))
            {
                string query = @"
INSERT INTO [dbo].[DetainedLicenses]
           ([LicenseID]
           ,[DetainDate]
           ,[FineFees]
           ,[CreatedByUserID]
           ,[IsReleased]
           ,[ReleaseDate]
           ,[ReleasedByUserID]
           ,[ReleaseApplicationID])
                          VALUES
                                (@LicenseID , @DetainDate , @FineFees , @CreatedByUserID, @IsReleased , @ReleaseDate , @ReleasedByUserID , @ReleaseApplicationID) ;
                     SELECT SCOPE_IDENTITY();
                                                      ";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@LicenseID", detainedLicense.LicenseID);
                    cmd.Parameters.AddWithValue("@DetainDate", detainedLicense.DetainDate);
                    cmd.Parameters.AddWithValue("@FineFees", detainedLicense.FineFees);
                    cmd.Parameters.AddWithValue("@CreatedByUserID", detainedLicense.CreatedByUserID);
                    cmd.Parameters.AddWithValue("@IsReleased", detainedLicense.IsReleased);

                    if (detainedLicense.ReleaseDate == null)
                        cmd.Parameters.AddWithValue("@ReleaseDate", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@ReleaseDate", detainedLicense.ReleaseDate);

                    if (detainedLicense.ReleasedByUserID == null)
                        cmd.Parameters.AddWithValue("@ReleasedByUserID", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@ReleasedByUserID", detainedLicense.ReleasedByUserID);

                    if (detainedLicense.ReleaseApplicationID == null)
                        cmd.Parameters.AddWithValue("@ReleaseApplicationID", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@ReleaseApplicationID", detainedLicense.ReleaseApplicationID);


                    try
                    {
                        conn.Open();
                        object result = cmd.ExecuteScalar();

                        detainedLicense.DetainID = Convert.ToInt32(result);

                    }
                    catch (Exception ex)
                    {
                        _logger.Error("Error while adding detained license", ex);
                    }



                }
            }

            return detainedLicense.DetainID;


        }
        public bool ReleaseLicense(int detainID, DateTime releaseDate, int releasedByUserID, int releaseApplicationID)
        {
            bool isReleased = false;
            using (SqlConnection conn = new SqlConnection(DatabaseSittings.connectionString))
            {
                string query = @"update DetainedLicenses set  DetainedLicenses.IsReleased = 1 , DetainedLicenses.ReleaseDate = @ReleaseDate , DetainedLicenses.ReleasedByUserID = @ReleasedByUserID , DetainedLicenses.ReleaseApplicationID = @ReleaseApplicationID where DetainedLicenses.DetainID = @DetainID ";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@DetainID", detainID);
                    cmd.Parameters.AddWithValue("@ReleaseDate", releaseDate);
                    cmd.Parameters.AddWithValue("@ReleasedByUserID", releasedByUserID);
                    cmd.Parameters.AddWithValue("@ReleaseApplicationID", releaseApplicationID);
                    try
                    {
                        conn.Open();
                        int result = cmd.ExecuteNonQuery();
                        if (result != 0)
                            isReleased = true;
                    }
                    catch (Exception ex)
                    {
                        isReleased = false;
                        _logger.Error("Error while releasing license", ex);
                    }
                }
            }
            return isReleased;
        }
        public DetainedLicense GetByLicenseID(int licenseID)
        {
            DetainedLicense detainedLicense = new DetainedLicense();
            using (SqlConnection conn = new SqlConnection(DatabaseSittings.connectionString))
            {
                string query = @" SELECT *
                                   FROM DetainedLicenses
                                   WHERE LicenseID = @LicenseID  and IsReleased = 0 ";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@LicenseID", licenseID);
                    try
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {

                                detainedLicense.DetainID = System.Convert.ToInt32(reader["DetainID"]);
                                detainedLicense.DetainDate = System.Convert.ToDateTime(reader["DetainDate"]);
                                detainedLicense.FineFees = System.Convert.ToDouble(reader["FineFees"]);
                                detainedLicense.CreatedByUserID = System.Convert.ToInt32(reader["CreatedByUserID"]);
                                detainedLicense.IsReleased = System.Convert.ToBoolean(reader["IsReleased"]);
                                if (reader["ReleaseDate"] != DBNull.Value)
                                    detainedLicense.ReleaseDate = System.Convert.ToDateTime(reader["ReleaseDate"]);
                                else
                                    detainedLicense.ReleaseDate = null;
                                if (reader["ReleasedByUserID"] != DBNull.Value)
                                    detainedLicense.ReleasedByUserID = System.Convert.ToInt32(reader["ReleasedByUserID"]);
                                else
                                    detainedLicense.ReleasedByUserID = null;
                                if (reader["ReleaseApplicationID"] != DBNull.Value)
                                    detainedLicense.ReleaseApplicationID = System.Convert.ToInt32(reader["ReleaseApplicationID"]);
                                else
                                    detainedLicense.ReleaseApplicationID = null;

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        detainedLicense = null;
                        _logger.Error("Error while get detain by ID", ex);
                    }

                }
            }
            return detainedLicense;
        }
        public DataTable GetAllDetainedLicenses()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(DatabaseSittings.connectionString))
            {
                string query = @"select * from detainedLicenses_View order by IsReleased ,DetainID;";
                
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    try
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                    catch (Exception ex)
                    {
                        dt = null;
                        _logger.Error("Error while get all detained licenses", ex);
                    }
                }
            }
            return dt;
        }
        public bool IsDetained(int licenseID)
        {
            bool isDetained = false;
            using (SqlConnection conn = new SqlConnection(DatabaseSittings.connectionString))
            {
                string query = @"SELECT COUNT(*) FROM DetainedLicenses WHERE LicenseID = @LicenseID AND IsReleased = 0";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@LicenseID", licenseID);
                    try
                    {
                        conn.Open();
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        isDetained = count > 0;
                    }
                    catch (Exception ex)
                    {
                        isDetained = false;
                        _logger.Error("Error while checking if license is detained", ex);
                    }
                }
            }
            return isDetained;
        }
    }
}



