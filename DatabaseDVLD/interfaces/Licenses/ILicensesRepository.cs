using System.Data;

namespace DatabaseDVLD
{
   public interface ILicensesRepository
    {
        int Add(License license);
        bool DeactivateLicense(int licenseID);
        DataTable GetAllInternationalLicensesByPersonID(int personID);
        DataTable GetAllLocalLicensesByPersonID(int personID);
        License GetByID(int licenseID);
        License GetByApplicationID(int ApplicationID);
        bool IsExistsLicenseByLDLApplication(int lDLApplication);
        bool IsExists(int license);
        bool IsExpirationDateLicense(int license);
    }
}

