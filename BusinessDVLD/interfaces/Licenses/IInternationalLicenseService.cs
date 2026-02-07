using System.Data;

namespace BusinessDVLD
{
    public interface IInternationalLicenseService
    {
        int Add(int licenseID, int application);
        int DoesHaveActiveInternationalLicense(int licenseID);
        InternationalLicenseDTO GetByID(int internationalLicenseID);
        DataTable GetAll();
    }

}
