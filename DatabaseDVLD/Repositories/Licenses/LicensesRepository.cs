using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;

namespace DatabaseDVLD
{
    public class LicensesRepository : ILicensesRepository
    {
        private readonly ILogger _logger;

        public LicensesRepository()
        {
            _logger = new FileLogger();
        }

        public int Add(License license)
        {

            license.LicenseID = -1;

            using (SqlConnection conn = new SqlConnection(DatabaseSittings.connectionString))
            {
                string query = @"
INSERT INTO [dbo].[Licenses]
           ([ApplicationID]
           ,[DriverID]
           ,[LicenseClass]
           ,[IssueDate]
           ,[ExpirationDate]
           ,[Notes]
           ,[PaidFees]
           ,[IsActive]
           ,[IssueReason]
           ,[CreatedByUserID])
     VALUES
 (@ApplicationID , @DriverID , @LicenseClass , @IssueDate, @ExpirationDate , @Notes , @PaidFees , @IsActive , @IssueReason , @CreatedByUserID) ;
                     SELECT SCOPE_IDENTITY();
                                                      ";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ApplicationID", license.ApplicationID);
                    cmd.Parameters.AddWithValue("@DriverID", license.DriverID);
                    cmd.Parameters.AddWithValue("@LicenseClass", license.LicenseClass);
                    cmd.Parameters.AddWithValue("@IssueDate ", license.IssueDate);
                    cmd.Parameters.AddWithValue("@ExpirationDate", license.ExpirationDate);
                    cmd.Parameters.AddWithValue("@PaidFees", license.PaidFees);
                    if (string.IsNullOrEmpty(license.Notes))
                        cmd.Parameters.AddWithValue("@Notes", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@Notes", license.Notes);
                    if (license.IsActive)
                    cmd.Parameters.AddWithValue("@IsActive", license.IsActive);
                    cmd.Parameters.AddWithValue("@IssueReason", license.IssueReason);
                    cmd.Parameters.AddWithValue("@CreatedByUserID", license.CreatedByUserID);

                    try
                    {
                        conn.Open();
                        object result = cmd.ExecuteScalar();

                        license.LicenseID = Convert.ToInt32(result);

                    }
                    catch (Exception ex)
                    {
                        _logger.Error("Error while adding license", ex);
                    }



                }
            }

            return license.LicenseID;





        }
        public DataTable GetAllLocalLicensesByPersonID(int personID)
        {

            DataTable dataTable = new DataTable();
            using (SqlConnection conn = new SqlConnection(DatabaseSittings.connectionString))
            {

                string query = @"  SELECT Licenses.LicenseID, Licenses.ApplicationID, LicenseClasses.ClassName, Licenses.IssueDate, Licenses.ExpirationDate, Licenses.IsActive
                 FROM     Licenses INNER JOIN
                 LicenseClasses ON Licenses.LicenseClass = LicenseClasses.LicenseClassID INNER JOIN
                 Applications ON Licenses.ApplicationID = Applications.ApplicationID
				 where Applications.ApplicantPersonID = @ApplicantPersonID   ";


                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ApplicantPersonID", personID);

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
                        _logger.Error("Error while get all licenses by person ID", ex);
                    }

                }
            }
            return dataTable;

        }
        public DataTable GetAllInternationalLicensesByPersonID(int personID)
        {


            DataTable dataTable = new DataTable();
            using (SqlConnection conn = new SqlConnection(DatabaseSittings.connectionString))
            {

                string query = @"  SELECT InternationalLicenses.InternationalLicenseID, InternationalLicenses.ApplicationID, Licenses.LicenseID, InternationalLicenses.IssueDate, InternationalLicenses.ExpirationDate, InternationalLicenses.IsActive, 
                  Applications.ApplicantPersonID
                  FROM     InternationalLicenses INNER JOIN
                  Licenses ON InternationalLicenses.IssuedUsingLocalLicenseID = Licenses.LicenseID INNER JOIN
                  Applications ON InternationalLicenses.ApplicationID = Applications.ApplicationID 
				  where Applications.ApplicantPersonID = @ApplicantPersonID   ";


                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ApplicantPersonID", personID);

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
                        _logger.Error("Error while get all licenses by person ID", ex);
                    }

                }
            }
            return dataTable;
        }
        public License GetByID(int licenseID)
        {


            License license = null;

            using (SqlConnection conn = new SqlConnection(DatabaseSittings.connectionString))
            {

                string query = "Select * from Licenses Where LicenseID = @LicenseID  ";



                using (SqlCommand cmd = new SqlCommand(query, conn))
                {

                    cmd.Parameters.AddWithValue("@LicenseID ", licenseID);


                    try
                    {



                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                license = new License
                                {
                                    LicenseID = Convert.ToInt32(reader["LicenseID"]),
                                    ApplicationID = Convert.ToInt32(reader["ApplicationID"]),
                                    DriverID = Convert.ToInt32(reader["DriverID"]),
                                    LicenseClass = Convert.ToInt32(reader["LicenseClass"]),
                                    IssueDate = Convert.ToDateTime(reader["IssueDate"]),
                                    ExpirationDate = Convert.ToDateTime(reader["ExpirationDate"]),
                                    PaidFees = Convert.ToDouble(reader["PaidFees"]),
                                    IssueReason = Convert.ToInt32(reader["IssueReason"]),
                                    IsActive = Convert.ToBoolean(reader["IsActive"]),
                                    Notes = reader["Notes"].ToString(),
                                    CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]),
                                };
                            }


                            else
                            {

                                license = null;

                            }




                        }
                    }
                    catch (Exception ex)
                    {
                        license = null;
                        _logger.Error("Error while get user by ID", ex);
                    }

                }
            }

            return license;

        }
        public License GetByApplicationID(int ApplicationID)
        {


            License license = null;

            using (SqlConnection conn = new SqlConnection(DatabaseSittings.connectionString))
            {

                string query = "Select * from Licenses Where Licenses.ApplicationID = @ApplicationID  ; ";



                using (SqlCommand cmd = new SqlCommand(query, conn))
                {

                    cmd.Parameters.AddWithValue("@ApplicationID", ApplicationID);


                    try
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                license = new License
                                {
                                    LicenseID = Convert.ToInt32(reader["LicenseID"]),
                                    ApplicationID = Convert.ToInt32(reader["ApplicationID"]),
                                    DriverID = Convert.ToInt32(reader["DriverID"]),
                                    LicenseClass = Convert.ToInt32(reader["LicenseClass"]),
                                    IssueDate = Convert.ToDateTime(reader["IssueDate"]),
                                    ExpirationDate = Convert.ToDateTime(reader["ExpirationDate"]),
                                    PaidFees = Convert.ToDouble(reader["PaidFees"]),
                                    IssueReason = Convert.ToInt32(reader["IssueReason"]),
                                    IsActive = Convert.ToBoolean(reader["IsActive"]),
                                    Notes = reader["Notes"].ToString(),
                                    CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]),
                                };
                            }


                            else
                            {

                                license = null;

                            }




                        }
                    }
                    catch (Exception ex)
                    {
                        license = null;
                        _logger.Error("Error while get user by ID", ex);
                    }

                }
            }

            return license;

        }

    }
}

