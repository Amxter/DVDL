using System;
using System.Data;
using System.Data.SqlClient;

namespace DatabaseDVLD
{
    public class ManageTestTypesRepository : IManageTestTypesRepository  
    {
        private readonly ILogger _logger;

        public ManageTestTypesRepository( )
        {
            _logger = new FileLogger();
        }

        public DataTable GetAll()
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection conn = new SqlConnection(DatabaseSittings.connectionString))
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
        public bool Update(Test test)
        {


            bool IsUpdate = false;
            using (SqlConnection conn = new SqlConnection(DatabaseSittings.connectionString))
            {

                string query = @"UPDATE [dbo].[TestTypes]
                    SET [TestTypeTitle] =  @Title 
                   ,[TestTypeDescription] =  @Description
                   ,[TestTypeFees] =  @Fees
                   WHERE  TestTypeID = @TestTypeID ";



                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TestTypeID", test.ID);
                    cmd.Parameters.AddWithValue("@Title", test.Title);
                    cmd.Parameters.AddWithValue("@Description", test.Description);
                    cmd.Parameters.AddWithValue("@Fees", test.Fees);

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
    }
}


