using DatabaseDVLD;
using System;
using System.Data;

namespace BusinessDVLD
{
    public class DetainedLicenseServices : IDetainedLicenseServices
    {
        IDetainedLicenseRepository _detainedLicenseRepository;

        public DetainedLicenseServices(IDetainedLicenseRepository detainedLicenseRepository)
        {
            _detainedLicenseRepository = detainedLicenseRepository;
        }
        public int Add(DetainedLicenseDTO detainedLicenseDTO) => _detainedLicenseRepository.Add(detainedLicenseDTO.ToEntity());
        public bool ReleaseLicense(int detainID, int releasedByUserID, DateTime releaseDate, int releaseApplicationID) =>
            _detainedLicenseRepository.ReleaseLicense(detainID, releaseDate, releasedByUserID, releaseApplicationID);
        public DetainedLicenseDTO GetByLicenseID(int licenseID) => _detainedLicenseRepository.GetByLicenseID(licenseID)?.ToDTO();
        public DataTable GetAll() => _detainedLicenseRepository.GetAllDetainedLicenses();
        public bool IsLicenseDetained(int licenseID) => _detainedLicenseRepository.IsDetained(licenseID);
    }
}
