using System;
using System.Data;
using System.Data.SqlClient;

namespace DatabaseDVLD
{
    public class TestAppointmentRepository : ITestAppointmentRepository
    {
        private readonly ILogger _logger;


        public TestAppointmentRepository()
        {
            _logger = new FileLogger();
        }
        private DataTable GetAllByTestType(int testTypeID, int LocalDrivingLicenseApplicationID)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection conn = new SqlConnection(DatabaseSittings.connectionString))
            {

                string query = @"  SELECT TestAppointmentID, AppointmentDate, PaidFees, IsLocked
FROM     TestAppointments
where TestAppointments.TestTypeID = @TestTypeID and TestAppointments.LocalDrivingLicenseApplicationID  = @LocalDrivingLicenseApplicationID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TestTypeID", testTypeID);
                    cmd.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

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
                        _logger.Error("Error while get all by test type", ex);
                    }

                }
            }
            return dataTable;


        }
        public DataTable GetAllVisionTestByLDLApplication(int LocalDrivingLicenseApplicationID)
        {
            return GetAllByTestType(TestTypes.VisionTestID, LocalDrivingLicenseApplicationID);
        }
        public DataTable GetAllWrittenTestByLDLApplication(int LocalDrivingLicenseApplicationID)
        {
            return GetAllByTestType(TestTypes.WrittenTestID, LocalDrivingLicenseApplicationID);
        }
        public DataTable GetAllPracticalTestByLDLApplication(int LocalDrivingLicenseApplicationID)
        {
            return GetAllByTestType(TestTypes.PracticalTestID, LocalDrivingLicenseApplicationID);
        }
        public TestAppointment GetByID(int testAppointmentID)
        {
            TestAppointment testAppointment = null;

            using (SqlConnection conn = new SqlConnection(DatabaseSittings.connectionString))
            {
                string query = @"
            SELECT 
                TestAppointmentID,
                TestTypeID,
                LocalDrivingLicenseApplicationID,
                AppointmentDate,
                PaidFees,
                CreatedByUserID,
                IsLocked,
                RetakeTestApplicationID
            FROM TestAppointments
            WHERE TestAppointmentID = @TestAppointmentID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TestAppointmentID", testAppointmentID);

                    try
                    {
                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                testAppointment = new TestAppointment();

                                testAppointment.TestAppointmentID = reader.GetInt32(reader.GetOrdinal("TestAppointmentID"));
                                testAppointment.TestTypeID = reader.GetInt32(reader.GetOrdinal("TestTypeID"));
                                testAppointment.LocalDrivingLicenseApplicationID = reader.GetInt32(reader.GetOrdinal("LocalDrivingLicenseApplicationID"));
                                testAppointment.AppointmentDate = reader.GetDateTime(reader.GetOrdinal("AppointmentDate"));
                                testAppointment.PaidFees = Convert.ToDouble( reader["PaidFees"]  );
                                testAppointment.CreatedByUserID = reader.GetInt32(reader.GetOrdinal("CreatedByUserID"));
                                testAppointment.IsLocked = reader.GetBoolean(reader.GetOrdinal("IsLocked"));
                                testAppointment.RetakeTestApplicationID = reader.IsDBNull(reader.GetOrdinal("RetakeTestApplicationID"))
                                        ? -1
                                        : reader.GetInt32(reader.GetOrdinal("RetakeTestApplicationID"));
                                 
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.Error($"Error while getting TestAppointment by ID = {testAppointmentID}", ex);
                    }
                }
            }

            return testAppointment;
        }
        public bool isActiveAppointment(int LDLApplicationID, int TestTypeID)
        {
            bool count = false;
            using (SqlConnection conn = new SqlConnection(DatabaseSittings.connectionString))
            {
                string query = @"SELECT count ( LocalDrivingLicenseApplicationID )
                          FROM     TestAppointments    
                          where LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID  and TestAppointments.TestTypeID = @TestTypeID and TestAppointments.IsLocked = 0 ";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LDLApplicationID);
                    cmd.Parameters.AddWithValue("@TestTypeID", TestTypeID);
                    try
                    {
                        conn.Open();
                        object result = cmd.ExecuteScalar();
                        count = Convert.ToBoolean(result);
                    }
                    catch (Exception ex)
                    {
                        count = false;
                        _logger.Error("Error while Get is Active Appointment", ex);
                    }
                }
            }
            return count;
        }
        public int HowMatchFiledTest(int lDLApplicationID, int TestTypeID)
        {
            int count = -1 ;
            using (SqlConnection conn = new SqlConnection(DatabaseSittings.connectionString))
            {
                string query = @"SELECT  count (TestAppointments.LocalDrivingLicenseApplicationID )FROM   Tests INNER JOIN
                  TestAppointments ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID  
                  where TestAppointments.LocalDrivingLicenseApplicationID=@lDLApplicationID and Tests.TestResult = 0 and 
				  TestAppointments.TestTypeID = @TestTypeID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@lDLApplicationID", lDLApplicationID);
                    cmd.Parameters.AddWithValue("@TestTypeID", TestTypeID);
                    try
                    {
                        conn.Open();
                        object result = cmd.ExecuteScalar();

                        count = Convert.ToInt32(result);

                    }
                    catch (Exception ex)
                    {
                        count = -1 ;
                        _logger.Error("Error while Get is Filed Test", ex);
                    }
                }


            }

            return count;
        }
        public bool IsPassedTest(int lDLApplicationID, int testTypeID)
        {
            bool count = false;
            using (SqlConnection conn = new SqlConnection(DatabaseSittings.connectionString))
            {
                string query = @" SELECT count (TestAppointments.LocalDrivingLicenseApplicationID  )
                  FROM     Tests INNER JOIN
                  TestAppointments ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID
				  where TestAppointments.LocalDrivingLicenseApplicationID = @lDLApplicationID and 
				  TestAppointments.TestTypeID = @testTypeID and
				  Tests.TestResult = 1 ;";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@lDLApplicationID", lDLApplicationID);
                    cmd.Parameters.AddWithValue("@testTypeID", testTypeID);
                    try
                    {
                        conn.Open();
                        object result = cmd.ExecuteScalar();

                        count = Convert.ToBoolean(result);

                    }
                    catch (Exception ex)
                    {
                        count = false;
                        _logger.Error("Error while Get is Filed Test", ex);
                    }
                }
            }
            return count; 
        }
        public int Add(TestAppointment testAppointment)
        {
 
            testAppointment.TestAppointmentID = -1;

            using (SqlConnection conn = new SqlConnection(DatabaseSittings.connectionString))
            {
                string query = @"
                   INSERT INTO [dbo].[TestAppointments]
           ([TestTypeID]
           ,[LocalDrivingLicenseApplicationID]
           ,[AppointmentDate]
           ,[PaidFees]
           ,[CreatedByUserID]
           ,[IsLocked]
           ,[RetakeTestApplicationID])
     VALUES
       (@TestTypeID , @LocalDrivingLicenseApplicationID , @AppointmentDate , @PaidFees , @CreatedByUserID , @IsLocked, @RetakeTestApplicationID ) ;
                     SELECT SCOPE_IDENTITY();
                                                      ";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TestTypeID", testAppointment.TestTypeID);
                    cmd.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", testAppointment.LocalDrivingLicenseApplicationID);
                    cmd.Parameters.AddWithValue("@AppointmentDate", testAppointment.AppointmentDate);
                    cmd.Parameters.AddWithValue("@PaidFees", testAppointment.PaidFees);
                    cmd.Parameters.AddWithValue("@CreatedByUserID", testAppointment.CreatedByUserID);
                    cmd.Parameters.AddWithValue("@IsLocked", testAppointment.IsLocked);

                    if (testAppointment.RetakeTestApplicationID == -1)
                        cmd.Parameters.AddWithValue("@RetakeTestApplicationID", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@RetakeTestApplicationID", testAppointment.RetakeTestApplicationID);



                    try
                    {
                        conn.Open();
                        object result = cmd.ExecuteScalar();

                        testAppointment.TestAppointmentID = Convert.ToInt32(result);

                    }
                    catch (Exception ex)
                    {
                        _logger.Error("Error while adding Test Appointment", ex);
                    }




                }
            }

            return testAppointment.TestAppointmentID;
        }
        public bool Update(TestAppointment testAppointment)
        {


            bool IsUpdate = false;
            using (SqlConnection conn = new SqlConnection(DatabaseSittings.connectionString))
            {

                string query = @"UPDATE [dbo].[TestAppointments]
   SET [TestTypeID] =  @TestTypeID
      ,[LocalDrivingLicenseApplicationID] = @LocalDrivingLicenseApplicationID 
      ,[AppointmentDate] =  @AppointmentDate
      ,[PaidFees] =  @PaidFees
      ,[CreatedByUserID] =  @CreatedByUserID
      ,[IsLocked] =  @IsLocked
      ,[RetakeTestApplicationID] =  @RetakeTestApplicationID
                   WHERE  TestAppointmentID = @TestAppointmentID ";



                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TestTypeID", testAppointment.TestTypeID);
                    cmd.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", testAppointment.LocalDrivingLicenseApplicationID);
                    cmd.Parameters.AddWithValue("@AppointmentDate", testAppointment.AppointmentDate);
                    cmd.Parameters.AddWithValue("@PaidFees", testAppointment.PaidFees);
                    cmd.Parameters.AddWithValue("@CreatedByUserID", testAppointment.CreatedByUserID);
                    cmd.Parameters.AddWithValue("@IsLocked", testAppointment.IsLocked);

                    if (testAppointment.RetakeTestApplicationID == -1)
                        cmd.Parameters.AddWithValue("@RetakeTestApplicationID", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@RetakeTestApplicationID", testAppointment.RetakeTestApplicationID);
                    cmd.Parameters.AddWithValue("@TestAppointmentID", testAppointment.TestAppointmentID);



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
                        _logger.Error("Error while update test appointment", ex);
                    }

                }
            }

            return IsUpdate;
        }
        public bool Delete(int testAppointmentID)
        {
            bool isDelete = false;
            using (SqlConnection conn = new SqlConnection(DatabaseSittings.connectionString))
            {
                string query = @"DELETE FROM  TestAppointments  WHERE  TestAppointmentID = @TestAppointmentID ";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TestAppointmentID", testAppointmentID);
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
                        _logger.Error("Error while delete test appointment", ex);
                    }

                }
            }
            return isDelete;
        }

    }
}
