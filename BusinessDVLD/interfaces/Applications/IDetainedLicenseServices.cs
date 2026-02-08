using System;
using System.Data;

namespace BusinessDVLD
{
    public interface IDetainedLicenseServices
    {
        int Add(DetainedLicenseDTO detainedLicenseDTO);
        bool ReleaseLicense(int detainID, int releasedByUserID, DateTime releaseDate, int releaseApplicationID);
        DetainedLicenseDTO GetByLicenseID(int licenseID);
        DataTable GetAll();
        bool IsLicenseDetained(int licenseID);
    }
}
