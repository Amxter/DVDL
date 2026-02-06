using System.Data;

namespace BusinessDVLD
{
    public interface ILicenseService
    {
        int Add(LicenseDTO licenseDTO);
        DataTable GetAllLocalLicensesByPersonID(int personID);
        DataTable GetAllInternationalLicensesByPersonID(int personID);
        LicenseDTO GetByID(int licenseID);
        LicenseDTO GetByApplicationID(int ApplicationID);
        bool IsExistsLicenseByLDLApplication(int lDLApplication);
    }

 
}
