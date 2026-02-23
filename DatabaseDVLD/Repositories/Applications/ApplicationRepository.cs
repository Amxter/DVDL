using System;
using System.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;

namespace DatabaseDVLD
{
    public class ApplicationRepository : IApplicationRepository
    {

        private readonly ILogger _logger;

        public ApplicationRepository()
        {
            _logger = new EventLogs();
        }

        public int Add(Application application)
        {


            application.ApplicationID = -1;

            using (SqlConnection conn = new SqlConnection(DatabaseSittings.ConnectionString))
            {
                string query = @"
INSERT INTO [dbo].[Applications]
           ([ApplicantPersonID]
           ,[ApplicationDate]
           ,[ApplicationTypeID]
           ,[ApplicationStatus]
           ,[LastStatusDate]
           ,[PaidFees]
           ,[CreatedByUserID])
                          VALUES
                                (@ApplicantPersonID , @ApplicationDate , @ApplicationTypeID , @ApplicationStatus, @LastStatusDate , @PaidFees , @CreatedByUserID ) ;
                     SELECT SCOPE_IDENTITY();
                                                      ";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ApplicantPersonID", application.ApplicantPersonID);
                    cmd.Parameters.AddWithValue("@ApplicationDate", application.ApplicationDate);
                    cmd.Parameters.AddWithValue("@ApplicationTypeID", application.ApplicationTypeID);
                    cmd.Parameters.AddWithValue("@ApplicationStatus", application.ApplicationStatus);
                    cmd.Parameters.AddWithValue("@LastStatusDate", application.LastStatusDate);
                    cmd.Parameters.AddWithValue("@PaidFees", application.PaidFees);
                    cmd.Parameters.AddWithValue("@CreatedByUserID", application.CreatedByUserID);



                    try
                    {
                        conn.Open();
                        object result = cmd.ExecuteScalar();

                        application.ApplicationID = Convert.ToInt32(result);

                    }
                    catch (Exception ex)
                    {
                        _logger.Error("Error while adding Application", ex);
                    }



                }
            }

            return application.ApplicationID;


        }
        public Application GetByApplicationID(int applicationID)
        {

            Application application = null;

            using (SqlConnection conn = new SqlConnection(DatabaseSittings.ConnectionString))
            {

                string query = "Select * from Applications  Where ApplicationID = @ApplicationID ";



                using (SqlCommand cmd = new SqlCommand(query, conn))
                {

                    cmd.Parameters.AddWithValue("@ApplicationID", applicationID);


                    try
                    {

                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                application = new Application
                                {
                                    ApplicationID = Convert.ToInt32(reader["ApplicationID"]),
                                    ApplicantPersonID = Convert.ToInt32(reader["ApplicantPersonID"]),
                                    ApplicationTypeID = Convert.ToInt32(reader["ApplicationTypeID"]),
                                    ApplicationStatus = Convert.ToInt32(reader["ApplicationStatus"]),
                                    ApplicationDate = Convert.ToDateTime(reader["ApplicationDate"].ToString()),
                                    LastStatusDate = Convert.ToDateTime(reader["LastStatusDate"]),
                                    PaidFees = Convert.ToDouble(reader["PaidFees"]),
                                    CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"])

                                };
                            }


                            else
                            {

                                application = null;

                            }




                        }
                    }
                    catch (Exception ex)
                    {
                        application = null;
                        _logger.Error("Error while Grt Application By ID", ex);
                    }

                }
            }

            return application;


        }
        public bool Delete(int applicationID)
        {
            bool isDelete = false;
            using (SqlConnection conn = new SqlConnection(DatabaseSittings.ConnectionString))
            {
                string query = @"update Applications set  Applications.ApplicationStatus = 2 where Applications.ApplicationID = @ApplicationID ";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ApplicationID", applicationID);
                    try
                    {
                        conn.Open();
                        int result = cmd.ExecuteNonQuery();
                        if (result != 0)
                            isDelete = true;
                    }
                    catch (Exception ex)
                    {
                        isDelete = false;

                        _logger.Error("Error while Delete Application", ex);
                    }

                }
            }
            return isDelete;
        }
        public bool UpdateApplicationStatus(int applicationID, int applicationStatus, DateTime lastStatusDate)
        {
            bool isUpdated = false;
            using (SqlConnection conn = new SqlConnection(DatabaseSittings.ConnectionString))
            {
                string query = @"update Applications set  Applications.ApplicationStatus = @ApplicationStatus , Applications.LastStatusDate = @LastStatusDate  where Applications.ApplicationID = @ApplicationID ";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ApplicationID", applicationID);
                    cmd.Parameters.AddWithValue("@ApplicationStatus", applicationStatus);
                    cmd.Parameters.AddWithValue("@LastStatusDate", lastStatusDate);
                    try
                    {
                        conn.Open();
                        int result = cmd.ExecuteNonQuery();
                        if (result != 0)
                            isUpdated = true;
                    }
                    catch (Exception ex)
                    {
                        isUpdated = false;
                        _logger.Error("Error while Update Application Status", ex);
                    }
                }
            }
            return isUpdated;
        }

    }
}



