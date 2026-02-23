using System;
using System.Data.SqlClient;

namespace DatabaseDVLD
{
    public class TestRepository : ITestRepository
    {

        private readonly ILogger _logger;

        public TestRepository()
        {
            _logger = new EventLogs();
        }

        public int Add(Test test)
        {

            test.TestID = -1;

            using (SqlConnection conn = new SqlConnection(DatabaseSittings.ConnectionString))
            {
                string query = @"

  Update TestAppointments set TestAppointments.IsLocked =  1 
  where TestAppointments.TestAppointmentID = @TestAppointmentID1

INSERT INTO [dbo].[Tests]
           ([TestAppointmentID]
           ,[TestResult]
           ,[Notes]
           ,[CreatedByUserID])
      
                          VALUES
                                (@TestAppointmentID , @TestResult  , @Notes , @CreatedByUserID ) ;
                     SELECT SCOPE_IDENTITY();
                                                      ";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TestAppointmentID", test.TestAppointmentID);
                    cmd.Parameters.AddWithValue("@TestAppointmentID1", test.TestAppointmentID);
                    cmd.Parameters.AddWithValue("@TestResult", test.TestResult);
                    if (test.Notes == null || test.Notes == "")
                        cmd.Parameters.AddWithValue("@Notes", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@Notes", test.Notes);
                    cmd.Parameters.AddWithValue("@CreatedByUserID", test.CreatedByUserID);
 
                    try
                    {
                        conn.Open();
                        object result = cmd.ExecuteScalar();
                        test.TestID = Convert.ToInt32(result);

                    }
                    catch (Exception ex)
                    {
                        _logger.Error("Error while adding LDLApplication", ex);
                    }



                }
            }

            return test.TestID;


        }
        public Test GetByTestAppointmentID(int testAppointmentID)
        {
            Test test = null;
            using (SqlConnection conn = new SqlConnection(DatabaseSittings.ConnectionString))
            {
                string query = @"
                SELECT [TestID]
                      ,[TestAppointmentID]
                      ,[TestResult]
                      ,[Notes]
                      ,[CreatedByUserID]
                    
                  FROM [dbo].[Tests]
                  WHERE TestAppointmentID = @TestAppointmentID
                ";
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
                                test = new Test
                                {
                                    TestID = Convert.ToInt32(reader["TestID"]),
                                    TestAppointmentID = Convert.ToInt32(reader["TestAppointmentID"]),
                                    TestResult = Convert.ToBoolean( reader["TestResult"] ),
                                    Notes = reader["Notes"] == DBNull.Value ? null : reader["Notes"].ToString(),
                                    CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]) 
                                     
                                };
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.Error("Error while retrieving Test by TestAppointmentID", ex);
                    }
                }
            }
            return test;
        }

    }
}





