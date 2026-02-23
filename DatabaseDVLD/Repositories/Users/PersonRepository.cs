using System;
using System.Data;
using System.Data.SqlClient;

namespace DatabaseDVLD
{
    public class PersonRepository : IPersonRepository
    {
        private readonly ILogger _logger;

        public PersonRepository()
        {
            _logger = new EventLogs();
        }

        private  void AddPersonParameters(SqlCommand cmd, Person person, bool includeId)
        {
            if (includeId)
                cmd.Parameters.AddWithValue("@PersonID", person.PersonID);

            cmd.Parameters.AddWithValue("@NationalNo", person.NationalNo);
            cmd.Parameters.AddWithValue("@FirstName", person.FirstName);
            cmd.Parameters.AddWithValue("@SecondName", person.SecondName);

            cmd.Parameters.AddWithValue("@ThirdName",
                string.IsNullOrWhiteSpace(person.ThirdName) ? (object)DBNull.Value : person.ThirdName);

            cmd.Parameters.AddWithValue("@LastName", person.LastName);
            cmd.Parameters.AddWithValue("@DateOfBirth", person.DateOfBirth);
            cmd.Parameters.AddWithValue("@Gendor", person.Gendor);
            cmd.Parameters.AddWithValue("@Address", person.Address);
            cmd.Parameters.AddWithValue("@Phone", person.Phone);

            cmd.Parameters.AddWithValue("@Email",
                string.IsNullOrWhiteSpace(person.Email) ? (object)DBNull.Value : person.Email);

            cmd.Parameters.AddWithValue("@NationalityCountryID", person.NationalityCountryID);

            cmd.Parameters.AddWithValue("@ImagePath",
                string.IsNullOrWhiteSpace(person.ImagePath) ? (object)DBNull.Value : person.ImagePath);
        }
        private  Person MapPerson(SqlDataReader reader)
        {
            return new Person
            {
                PersonID = Convert.ToInt32(reader["PersonID"]),
                NationalNo = reader["NationalNo"].ToString(),
                FirstName = reader["FirstName"].ToString(),
                SecondName = reader["SecondName"].ToString(),
                ThirdName = reader["ThirdName"] == DBNull.Value ? null : reader["ThirdName"].ToString(),
                LastName = reader["LastName"].ToString(),
                DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
                Gendor = Convert.ToInt16(reader["Gendor"]),
                Address = reader["Address"].ToString(),
                Phone = reader["Phone"].ToString(),
                Email = reader["Email"] == DBNull.Value ? null : reader["Email"].ToString(),
                NationalityCountryID = Convert.ToInt32(reader["NationalityCountryID"]),
                ImagePath = reader["ImagePath"] == DBNull.Value ? null : reader["ImagePath"].ToString()
            };
        }

        public int Add(Person person)
        {
            person.PersonID = -1;

            using (SqlConnection conn = new SqlConnection(DatabaseSittings.ConnectionString))
            {
                string query = @"
                           INSERT INTO dbo.People
                           (
                               NationalNo, FirstName, SecondName, ThirdName, LastName,
                               DateOfBirth, Gendor, Address, Phone, Email,
                               NationalityCountryID, ImagePath
                           )
                           VALUES
                           (
                               @NationalNo, @FirstName, @SecondName, @ThirdName, @LastName,
                               @DateOfBirth, @Gendor, @Address, @Phone, @Email,
                               @NationalityCountryID, @ImagePath
                           );
                           
                           SELECT SCOPE_IDENTITY();
                           ";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    AddPersonParameters(cmd, person, false);
                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    person.PersonID = Convert.ToInt32(result);
                }
            }

            return person.PersonID;
        }
        public bool Update(Person person)
        {


            bool IsUpdate = false;
            using (SqlConnection conn = new SqlConnection(DatabaseSittings.ConnectionString))
            {

                string query = @"UPDATE [dbo].[People]
   SET [NationalNo] =  @NationalNo 
      ,[FirstName] =   @FirstName
      ,[SecondName] =  @SecondName
      ,[ThirdName] =  @ThirdName 
      ,[LastName] =  @LastName 
      ,[DateOfBirth] = @DateOfBirth
      ,[Gendor] = @Gendor
      ,[Address] = @Address
      ,[Phone] = @Phone
      ,[Email] = @Email
      ,[NationalityCountryID] = @NationalityCountryID
      ,[ImagePath] =   @ImagePath 
 WHERE  PersonID = @PersonID ";



                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    AddPersonParameters(cmd, person, true);

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
                        _logger.Error("Error while update person", ex);
                    }

                }
            }

            return IsUpdate;
        }
        public bool Delete(int personID)
        {

            bool isDelete = false;
            using (SqlConnection conn = new SqlConnection(DatabaseSittings.ConnectionString))
            {

                string query = @"DELETE FROM  People  WHERE  PersonID = @PersonID ";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PersonID", personID);
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
                        _logger.Error("Error while delete person", ex);
                    }

                }
            }

            return isDelete;
        }
        public DataTable GetAll()
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection conn = new SqlConnection(DatabaseSittings.ConnectionString))
            {

                string query = @" 
SELECT People.PersonID, People.NationalNo,(FirstName + ' ' + SecondName + ' ' + COALESCE(ThirdName,'') + ' ' + LastName) AS FullName ,People.DateOfBirth,
    CASE 
        WHEN Gendor = 0 THEN 'Male'
        WHEN Gendor = 1 THEN 'Female'
        ELSE 'Unknown'
    END AS Gendor , People.Address, People.Phone, People.Email, Countries.CountryName as Nationality  
                 
FROM     People INNER JOIN
                  Countries ON People.NationalityCountryID = Countries.CountryID ";

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
                        _logger.Error("Error while get all person", ex);
                    }

                }
            }
            return dataTable;
        }
        public Person GetByID(int personID)
        {
            Person person = null;

            using (SqlConnection conn = new SqlConnection(DatabaseSittings.ConnectionString))
            {

                string query = "Select * from People  Where PersonID = @PersonID ";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {

                    cmd.Parameters.AddWithValue("@PersonID", personID);
                    try
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                person = MapPerson(reader);
                            }
                            else
                            {
                                person = null;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        person = null;
                        _logger.Error("Error while get by person ID ", ex);
                    }

                }
            }

            return person;

        }
        public bool IsExistsByID(int personID)
        {


            using (SqlConnection conn = new SqlConnection(DatabaseSittings.ConnectionString))
            {

                string query = @"Select 1 FROM  People  WHERE  PersonID = @PersonID ";

                try
                {
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
                catch (Exception ex)
                {
                    _logger.Error("Error while checking exists by ID", ex);
                    return false;
                }
            }

        }
        public bool IsExistsByNationalNo(string nationalNo)
        {
            using (SqlConnection conn = new SqlConnection(DatabaseSittings.ConnectionString))
            {
                string query = @"SELECT NationalNo FROM People WHERE NationalNo = @NationalNo";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@NationalNo", nationalNo);
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        return reader.HasRows;
                    }
                }
            }
        }
        public Person GetByNationalNo(string nationalNo)

        {
            Person person = null;

            using (SqlConnection conn = new SqlConnection(DatabaseSittings.ConnectionString))
            {

                string query = "Select * from People  Where NationalNo = @nationalNo ";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {

                    cmd.Parameters.AddWithValue("@nationalNo", nationalNo);
                    try
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                person = MapPerson(reader);
                            }
                            else
                            {
                                person = null;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        person = null;
                        _logger.Error("Error while get by national No", ex);
                    }

                }
            }

            return person;

        }

    }

}


