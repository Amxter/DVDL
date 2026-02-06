using System.Data;

namespace BusinessDVLD
{
    public interface ILicenseClassServices
    {
        DataTable GetAll();
        LicenseClassDTO GetByID(int licenseClassID); 
    }
}
