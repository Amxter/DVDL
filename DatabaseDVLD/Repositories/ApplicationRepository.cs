using System;
using System.Data.SqlClient;

namespace DatabaseDVLD
{
    public class ApplicationRepository : IApplicationRepository
    {
        public int Add(Application application)
        {


            application.ApplicationID = -1;

            using (SqlConnection conn = new SqlConnection(DatabaseSittings.connectionString))
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
                    catch
                    {

                    }



                }
            }

            return application.ApplicationID;


        }

        public Application GetByApplicationID(int applicationID)
        {

            Application application = null;

            using (SqlConnection conn = new SqlConnection(DatabaseSittings.connectionString))
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
                                    LastStatusDate = Convert.ToDateTime(reader["LastStatusDate"] )  ,
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
                    catch
                    {
                        application = null;
                    }
                }
            }

            return application;


        }
        public bool Delete(int applicationID)
        {
            bool isDelete = false;
            using (SqlConnection conn = new SqlConnection(DatabaseSittings.connectionString))
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
                    catch
                    {
                        isDelete = false;
                    }
                }
            }
            return isDelete;
        }

        //public DataTable GetAll()
        //{
        //    DataTable dataTable = new DataTable();
        //    using (SqlConnection conn = new SqlConnection(DatabaseSittings.connectionString))
        //    {

        //        string query = @"  SELECT Users.UserID, Users.PersonID, (People.FirstName + ' ' + People.SecondName + ' ' + People.ThirdName + ' ' + People.LastName) AS FullName , UserName , Users.IsActive
        //                           FROM     Users INNER JOIN
        //                           People ON Users.PersonID = People.PersonID
        //                                 ";

        //        using (SqlCommand cmd = new SqlCommand(query, conn))
        //        {


        //            try
        //            {

        //                conn.Open();

        //                using (SqlDataReader reader = cmd.ExecuteReader())
        //                {
        //                    dataTable.Load(reader);
        //                }
        //            }
        //            catch
        //            {
        //                dataTable = new DataTable();

        //            }
        //        }
        //    }
        //    return dataTable;
        //}
    }

}


