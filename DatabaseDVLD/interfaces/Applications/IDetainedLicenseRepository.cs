using System;
using System.Data;

namespace DatabaseDVLD
{
    public interface IDetainedLicenseRepository
    {
        int Add(DetainedLicense detainedLicense);
        bool ReleaseLicense(int detainID, DateTime releaseDate, int releasedByUserID, int releaseApplicationID);
        DataTable GetAllDetainedLicenses();
        DetainedLicense GetByLicenseID(int licenseID);
        bool IsDetained(int licenseID);
    }
}



