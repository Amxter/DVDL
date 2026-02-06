using System.Data;

namespace DatabaseDVLD
{
    public interface ILicenseClassRepository
    {
        DataTable GetAll();
        LicenseClass GetByID(int licenseClassID);
    }
}


