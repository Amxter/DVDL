using System;
using System.Data;
using System.Data.SqlClient;

namespace DatabaseDVLD
{
    public class TestTypesRepository : ITestTypesRepository  
    {
        private readonly ILogger _logger;

        public TestTypesRepository( )
        {
            _logger = new EventLogs();
        }

        public DataTable GetAll()
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection conn = new SqlConnection(DatabaseSittings.ConnectionString))
            {

                string query = @" SELECT  [TestTypeID]
                                       ,[TestTypeTitle]
                                       ,[TestTypeDescription]
                                       ,[TestTypeFees]
                                   FROM [DVLD].[dbo].[TestTypes] ";

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
                    { dataTable = new DataTable();
                        _logger.Error("Error while get all manage test types", ex);
                    }
      
                }
            }
            return dataTable;
        }
        public bool Update(TestType test)
        {


            bool IsUpdate = false;
            using (SqlConnection conn = new SqlConnection(DatabaseSittings.ConnectionString))
            {

                string query = @"UPDATE [dbo].[TestTypes]
                    SET [TestTypeTitle] =  @Title 
                   ,[TestTypeDescription] =  @Description
                   ,[TestTypeFees] =  @Fees
                   WHERE  TestTypeID = @TestTypeID ";



                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TestTypeID", test.TestTypeID);
                    cmd.Parameters.AddWithValue("@Title", test.TestTypeTitle);
                    cmd.Parameters.AddWithValue("@Description", test.TestTypeDescription);
                    cmd.Parameters.AddWithValue("@Fees", test.TestTypeFees);

                    try
                    {
                        conn.Open();
                        int result = cmd.ExecuteNonQuery();

                        if (result != 0)
                            IsUpdate = true;

                    }
                    catch (Exception ex)
                    {IsUpdate = false;
                        
                        _logger.Error("Error while update manage test types", ex);
                    }
 
                }
            }

            return IsUpdate;
        }
        public TestType GetByID(int TestTypeID)
        {

            TestType testTypeID = null;

            using (SqlConnection conn = new SqlConnection(DatabaseSittings.ConnectionString))
            {

                string query = "Select * from TestTypes  Where TestTypeID = @TestTypeID ";



                using (SqlCommand cmd = new SqlCommand(query, conn))
                {

                    cmd.Parameters.AddWithValue("@TestTypeID", TestTypeID);


                    try
                    {

                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                testTypeID = new TestType
                                {
                                    TestTypeID = Convert.ToInt32(reader["TestTypeID"]),
                                    TestTypeTitle = reader["TestTypeTitle"].ToString(),
                                    TestTypeDescription = reader["TestTypeDescription"].ToString(),
                                    TestTypeFees = Convert.ToDouble(reader["TestTypeFees"]),


                                };
                            }


                            else
                            {

                                testTypeID = null;

                            }




                        }
                    }
                    catch (Exception ex)
                    {
                        testTypeID = null;
                        _logger.Error("Error while get by test type ID ", ex);
                    }

                }
            }

            return testTypeID;

        }
    }
}


