using System;
using System.Data;
using System.Data.SqlClient;

namespace DatabaseDVLD
{
    public class LDLApplicationRepository : ILDLApplicationRepository
    {
        public int Add(LDLApplication application)
        {


            application.LocalDrivingLicenseApplicationID = -1;

            using (SqlConnection conn = new SqlConnection(DatabaseSittings.connectionString))
            {
                string query = @"
INSERT INTO [dbo].[LocalDrivingLicenseApplications]
           ([ApplicationID]
           ,[LicenseClassID])
                          VALUES
                                (@ApplicationID , @LicenseClassID ) ;
                     SELECT SCOPE_IDENTITY();
                                                      ";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ApplicationID", application.Application.ApplicationID);
                    cmd.Parameters.AddWithValue("@LicenseClassID", application.LicenseClassID);
                    try
                    {
                        conn.Open();
                        object result = cmd.ExecuteScalar();
                        application.LocalDrivingLicenseApplicationID = Convert.ToInt32(result);

                    }
                    catch
                    {

                    }



                }
            }

            return application.LocalDrivingLicenseApplicationID;


        }
        public bool Update(LDLApplication application)
        {


            bool IsUpdate = false;
            using (SqlConnection conn = new SqlConnection(DatabaseSittings.connectionString))
            {

                string query = @"
UPDATE [dbo].[LocalDrivingLicenseApplications]
   SET [ApplicationID] =  @ApplicationID
      ,[LicenseClassID] =  @LicenseClassID
 WHERE  LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID  ";



                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", application.LocalDrivingLicenseApplicationID);
                    cmd.Parameters.AddWithValue("@ApplicationID", application.Application.ApplicationID);
                    cmd.Parameters.AddWithValue("@LicenseClassID", application.LicenseClassID);
                    try
                    {
                        conn.Open();
                        int result = cmd.ExecuteNonQuery();

                        if (result != 0)
                            IsUpdate = true;

                    }
                    catch
                    {
                        IsUpdate = false;
                    }
                }
            }

            return IsUpdate;

        }
        public DataTable GetAll()
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection conn = new SqlConnection(DatabaseSittings.connectionString))
            {

                string query = @"  SELECT *
                                   FROM GetAllLocalDrivingLicenseApplications
                                         ";

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
        public LDLApplication GetByID(int localDrivingLicenseApplicationID)
        {
            LDLApplication lDLApplication = new LDLApplication();

            lDLApplication.LocalDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
            using (SqlConnection conn = new SqlConnection(DatabaseSittings.connectionString))
            {

                string query = "Select * from LocalDrivingLicenseApplications  Where LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID ";



                using (SqlCommand cmd = new SqlCommand(query, conn))
                {

                    cmd.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", lDLApplication.LocalDrivingLicenseApplicationID);


                    try
                    {

                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {

                                ApplicationRepository applicationRepository = new ApplicationRepository();
                                Application application = applicationRepository.GetByApplicationID(Convert.ToInt32(reader["ApplicationID"]));

                                lDLApplication = new LDLApplication
                                {
                                    Application = application,
                                    LicenseClassID = Convert.ToInt32(reader["LicenseClassID"]),
                                };
                            }
                            else
                            {

                                lDLApplication = null;

                            }




                        }
                    }
                    catch
                    {
                        lDLApplication = null;
                    }
                }
            }

            return lDLApplication;
        }
        public bool IsExistsApplicationByUsernameAndLicenseClass(int PersonID, int LicenseClassID)
        {


            using (SqlConnection conn = new SqlConnection(DatabaseSittings.connectionString))
            {

                string query = @"SELECT Applications.ApplicantPersonID, LocalDrivingLicenseApplications.LicenseClassID
FROM     Applications INNER JOIN
                  LocalDrivingLicenseApplications ON Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID
				  where Applications.ApplicantPersonID = @ApplicantPersonID  and LocalDrivingLicenseApplications.LicenseClassID = @LicenseClassID  and Applications.ApplicationStatus in(1,3) ";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ApplicantPersonID", PersonID);
                    cmd.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        return reader.HasRows;
                    }
                }
            }


        }
        //public bool Delete(int applicationID)
        //{
        //    bool isDelete = false;
        //    using (SqlConnection conn = new SqlConnection(DatabaseSittings.connectionString))
        //    {
        //        string query = @"DELETE FROM  Applications  WHERE  ApplicationID = @ApplicationID ";
        //        using (SqlCommand cmd = new SqlCommand(query, conn))
        //        {
        //            cmd.Parameters.AddWithValue("@ApplicationID", applicationID);
        //            try
        //            {
        //                conn.Open();
        //                int result = cmd.ExecuteNonQuery();
        //                if (result != 0)
        //                    isDelete = true;
        //            }
        //            catch
        //            {
        //                isDelete = false;
        //            }
        //        }
        //    }
        //    return isDelete;
        //}

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


