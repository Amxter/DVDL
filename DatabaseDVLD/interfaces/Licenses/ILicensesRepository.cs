using System.Data;

namespace DatabaseDVLD
{
   public interface ILicensesRepository
    {
        int Add(License license);
        DataTable GetAllInternationalLicensesByPersonID(int personID);
        DataTable GetAllLocalLicensesByPersonID(int personID);
        License GetByID(int licenseID);
        License GetByApplicationID(int ApplicationID);
    }
}

