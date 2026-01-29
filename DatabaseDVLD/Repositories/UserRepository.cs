using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;

namespace DatabaseDVLD
{
    public class UserRepository : IUserRepository
    {
        public int Add(User user)
        {
            user.UserID = -1;

            using (SqlConnection conn = new SqlConnection(DatabaseSittings.connectionString))
            {
                string query = @"
                     INSERT INTO [dbo].[Users]
                                ([PersonID]
                                ,[UserName]
                                ,[Password]
                                ,[IsActive])
                          VALUES
                                (@PersonID , @UserName , @Password , @IsActive ) ;
                     SELECT SCOPE_IDENTITY();
                                                      ";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PersonID", user.PersonID);
                    cmd.Parameters.AddWithValue("@UserName", user.UserName);
                    cmd.Parameters.AddWithValue("@Password", user.PasswordHash);
                    cmd.Parameters.AddWithValue("@IsActive", user.IsActive);


                    try
                    {
                        conn.Open();
                        object result = cmd.ExecuteScalar();

                        user.UserID = Convert.ToInt32(result);

                    }
                    catch
                    {

                    }



                }
            }

            return user.UserID;
        }
        public bool Update(User user)
        {


            bool IsUpdate = false;
            using (SqlConnection conn = new SqlConnection(DatabaseSittings.connectionString))
            {

                string query = @"UPDATE [dbo].[Users]
                     SET [PersonID] =  @PersonID
                        ,[UserName] =  @UserName
                        ,[Password] =  @Password
                        ,[IsActive] =  @IsActive
                   WHERE  UserID = @UserID ";



                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", user.UserID);
                    cmd.Parameters.AddWithValue("@PersonID", user.PersonID);
                    cmd.Parameters.AddWithValue("@UserName", user.UserName);
                    cmd.Parameters.AddWithValue("@Password", user.PasswordHash);
                    cmd.Parameters.AddWithValue("@IsActive", user.IsActive);
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
        public bool Delete(int userID)
        {
            bool isDelete = false;
            using (SqlConnection conn = new SqlConnection(DatabaseSittings.connectionString))
            {
                string query = @"DELETE FROM  Users  WHERE  UserID = @UserID ";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userID);
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
        public DataTable GetAll()
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection conn = new SqlConnection(DatabaseSittings.connectionString))
            {

                string query = @"  SELECT Users.UserID, Users.PersonID, (People.FirstName + ' ' + People.SecondName + ' ' + People.ThirdName + ' ' + People.LastName) AS FullName , UserName , Users.IsActive
                                   FROM     Users INNER JOIN
                                   People ON Users.PersonID = People.PersonID
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
        public User GetByID(int userID)
        {
            User user = null;

            using (SqlConnection conn = new SqlConnection(DatabaseSittings.connectionString))
            {

                string query = "Select * from Users  Where UserID = @UserID ";



                using (SqlCommand cmd = new SqlCommand(query, conn))
                {

                    cmd.Parameters.AddWithValue("@UserID", userID);


                    try
                    {

                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                user = new User
                                {
                                    UserID = Convert.ToInt32(reader["UserID"]),
                                    PersonID = Convert.ToInt32(reader["PersonID"]),
                                    IsActive = Convert.ToBoolean(reader["IsActive"]),
                                    UserName = reader["UserName"].ToString(),
                                    PasswordHash = reader["Password"].ToString()
                                };
                            }


                            else
                            {

                                user = null;

                            }




                        }
                    }
                    catch
                    {
                        user = null;
                    }
                }
            }

            return user;

        }
        public bool IsExistsByID(int userID)
        {


            using (SqlConnection conn = new SqlConnection(DatabaseSittings.connectionString))
            {

                string query = @"Select * FROM  Users  WHERE  UserID = @UserID ";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userID);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        return reader.HasRows;
                    }
                }
            }


        }
        public User IsExistsByUserName(string userName)
        {
            User user = null;

            using (SqlConnection conn = new SqlConnection(DatabaseSittings.connectionString))
            {

                string query = @"Select * FROM  Users  WHERE  UserName = @UserName ";



                using (SqlCommand cmd = new SqlCommand(query, conn))
                {

                    cmd.Parameters.AddWithValue("@UserName", userName);


                    try
                    {

                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                user = new User
                                {
                                    UserID = Convert.ToInt32(reader["UserID"]),
                                    PersonID = Convert.ToInt32(reader["PersonID"]),
                                    IsActive = Convert.ToBoolean(reader["IsActive"]),
                                    UserName = reader["UserName"].ToString(),
                                    PasswordHash = reader["Password"].ToString()
                                };
                            }


                            else
                            {

                                user = null;

                            }




                        }
                    }
                    catch
                    {
                        user = null;
                    }
                }
            }

            return user;
        }
        public bool IsExistsByPersonID(int personID)
        {


            using (SqlConnection conn = new SqlConnection(DatabaseSittings.connectionString))
            {

                string query = @"Select * FROM  Users  WHERE  PersonID = @PersonID ";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PersonID", personID);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        return reader.HasRows;
                    }
                }
            }


        }
        public bool IsExistsByUserNameExceptUserID(string userName, int userID)
        {
            using (SqlConnection conn = new SqlConnection(DatabaseSittings.connectionString))
            {

                string query = @"Select * FROM  Users  WHERE  UserName = @UserName  and UserId <> @UserID ";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserName", userName);
                    cmd.Parameters.AddWithValue("@UserID", userID);

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


