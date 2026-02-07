using System.Data;

namespace BusinessDVLD
{
    public interface ILicenseService
    {
        int Add(LicenseDTO licenseDTO);
        bool DeactivateLicense(int licenseID);
        DataTable GetAllLocalLicensesByPersonID(int personID);
        DataTable GetAllInternationalLicensesByPersonID(int personID);
        LicenseDTO GetByID(int licenseID);
        LicenseDTO GetByApplicationID(int ApplicationID);
        bool IsExistsLicenseByLDLApplication(int lDLApplication);
        bool IsExists(int license);
        bool IsExpirationDateLicense(int license);
    }

 
}
