using System;
using System.Data;
using System.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;

namespace DatabaseDVLD
{
    public class LDLApplicationRepository : ILDLApplicationRepository
    {
        private readonly ILogger _logger;

        public LDLApplicationRepository()
        {
            _logger = new EventLogs();
        }

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
                    catch (Exception ex)
                    {
                        _logger.Error("Error while adding LDLApplication", ex);
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
                    catch (Exception ex)
                    {
                        IsUpdate = false;
                        _logger.Error("Error while update LDLApplication", ex);
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
                    catch (Exception ex)
                    {
                        dataTable = new DataTable();
                        _logger.Error("Error while get all LDLapplication", ex);
                    }

                }
            }
            return dataTable;
        }
        public LDLApplication GetByID(int localDrivingLicenseApplicationID)
        {
            LDLApplication ldl = null;

            string query = @"
SELECT 
    l.LocalDrivingLicenseApplicationID,
    l.LicenseClassID,
    l.ApplicationID,

    a.ApplicationID AS App_ApplicationID,
    a.ApplicantPersonID,
    a.ApplicationDate,
    a.ApplicationTypeID,
    a.ApplicationStatus,
    a.LastStatusDate,
    a.PaidFees,
    a.CreatedByUserID
FROM LocalDrivingLicenseApplications l
INNER JOIN Applications a ON a.ApplicationID = l.ApplicationID
WHERE l.LocalDrivingLicenseApplicationID = @ID;
";

            try
            {
                using (SqlConnection connection = new SqlConnection(DatabaseSittings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@ID", SqlDbType.Int).Value = localDrivingLicenseApplicationID;

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // ابنِ Application من نفس الـ reader
                            var app = new Application
                            {
                                ApplicationID = Convert.ToInt32(reader["App_ApplicationID"]),
                                ApplicantPersonID = Convert.ToInt32(reader["ApplicantPersonID"]),
                                ApplicationDate = Convert.ToDateTime(reader["ApplicationDate"]),
                                ApplicationTypeID = Convert.ToInt32(reader["ApplicationTypeID"]),
                                ApplicationStatus = Convert.ToInt32(reader["ApplicationStatus"]),
                                LastStatusDate = Convert.ToDateTime(reader["LastStatusDate"]),
                                PaidFees = Convert.ToDouble(reader["PaidFees"]),
                                CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"])
                            };

                            // ابنِ LDLApplication
                            ldl = new LDLApplication
                            {
                                LocalDrivingLicenseApplicationID = Convert.ToInt32(reader["LocalDrivingLicenseApplicationID"]),
                                LicenseClassID = Convert.ToInt32(reader["LicenseClassID"]),
                                Application = app
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // لا تبلع الخطأ بصمت (أقل شيء Debug)
                System.Diagnostics.Debug.WriteLine($"[LDLApplicationRepository.GetByID] {ex}");
                // أو ارجع null مثل أسلوبك الحالي
            }

            return ldl;
        }
        public int GetPassedTestCount(int lDLApplicationID)
        {
            int count = 0;
            using (SqlConnection conn = new SqlConnection(DatabaseSittings.connectionString))
            {
                string query = @"SELECT count ( TestAppointments.LocalDrivingLicenseApplicationID )  
                         FROM     Tests INNER JOIN
                  TestAppointments ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID INNER JOIN
                  LocalDrivingLicenseApplications ON TestAppointments.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID
				  where TestAppointments.LocalDrivingLicenseApplicationID = @lDLApplicationID   and Tests.TestResult = 1";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@lDLApplicationID", lDLApplicationID);



                    try
                    {
                        conn.Open();
                        object result = cmd.ExecuteScalar();

                        count = Convert.ToInt32(result);

                    }
                    catch (Exception ex)
                    {
                        count = 0;
                        _logger.Error("Error while Get Passed Test Count", ex);
                    }
                }


            }

            return count;
        }
        public bool IsExistsApplicationByPersonIDAndLicenseClass(int PersonID, int LicenseClassID)
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



    }
}





