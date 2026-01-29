using System.Data;
using System.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;

namespace DatabaseDVLD
{
    public class ApplicationTypesRepository : IApplicationTypesRepository
    {
        public DataTable GetAll()
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection conn = new SqlConnection(DatabaseSittings.connectionString))
            {

                string query = @" SELECT [ApplicationTypeID]
                                   ,[ApplicationTypeTitle]
                                   ,[ApplicationFees]
                                   FROM [DVLD].[dbo].[ApplicationTypes] ";

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
        public bool Update(ApplicationTypes application)
        {


            bool IsUpdate = false;
            using (SqlConnection conn = new SqlConnection(DatabaseSittings.connectionString))
            {

                string query = @"UPDATE [dbo].[ApplicationTypes]
                   SET [ApplicationTypeTitle] =  @Title
                      ,[ApplicationFees] =  @Fees
                   WHERE  ApplicationTypeID = @ApplicationTypeID ";



                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ApplicationTypeID", application.ID);
                    cmd.Parameters.AddWithValue("@Title", application.Title);
                    cmd.Parameters.AddWithValue("@Fees", application.Fees);

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

        public ApplicationTypes GetApplication (string applicationTypeTitle)
        {

            ApplicationTypes applicationTypes =  new ApplicationTypes() ;
            using (SqlConnection conn = new SqlConnection(DatabaseSittings.connectionString))
            {
                string query = @" SELECT *
                                   FROM [DVLD].[dbo].[ApplicationTypes]
                                   WHERE ApplicationTypeTitle = @ApplicationTypeTitle ";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ApplicationTypeTitle", applicationTypeTitle);
                    try
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                applicationTypes.Fees = System.Convert.ToDouble(reader["ApplicationFees"]);
                                applicationTypes.ID = System.Convert.ToInt32(reader["ApplicationTypeID"]);
                                applicationTypes.Title = reader["ApplicationTypeTitle"].ToString();
                            }
                        }
                    }
                    catch
                    {
                        applicationTypes = null;


                    }
                }
            }
            return applicationTypes;
        }
        public ApplicationTypes GetApplication(int applicationTypeID)
        {

            ApplicationTypes applicationTypes = new ApplicationTypes();
            using (SqlConnection conn = new SqlConnection(DatabaseSittings.connectionString))
            {
                string query = @" SELECT *
                                   FROM [DVLD].[dbo].[ApplicationTypes]
                                   WHERE ApplicationTypeID = @ApplicationTypeID ";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ApplicationTypeID", applicationTypeID);
                    try
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                applicationTypes.Fees = System.Convert.ToDouble(reader["ApplicationFees"]);
                                applicationTypes.ID = System.Convert.ToInt32(reader["ApplicationTypeID"]);
                                applicationTypes.Title = reader["ApplicationTypeTitle"].ToString();
                            }
                        }
                    }
                    catch
                    {
                        applicationTypes = null;


                    }
                }
            }
            return applicationTypes;
        }
    }


}


