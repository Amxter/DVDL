using System.Data;

namespace DatabaseDVLD
{
    public interface IInternationalLicensesRepository
    {
        int Add(InternationalLicense internationalLicense);
        int DoesHaveActiveInternationalLicense(int licenseID);
        InternationalLicense GetByID(int internationalLicenseID);
        DataTable GetAll();
    }
}

