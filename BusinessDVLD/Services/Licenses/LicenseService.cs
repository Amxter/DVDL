using DatabaseDVLD;
using System.Data;

namespace BusinessDVLD
{
    public class LicenseService : ILicenseService
    {


        ILicensesRepository _licensesRepository;
        public LicenseService(ILicensesRepository licensesRepository )
        {
 
            _licensesRepository = licensesRepository;
        }
        public int Add (LicenseDTO licenseDTO) => _licensesRepository.Add(licenseDTO.ToEntity());
        public bool DeactivateLicense(int licenseID) => _licensesRepository.DeactivateLicense(licenseID);
        public DataTable GetAllInternationalLicensesByPersonID(int personID) => _licensesRepository.GetAllInternationalLicensesByPersonID(personID);
        public DataTable GetAllLocalLicensesByPersonID(int personID) => _licensesRepository.GetAllLocalLicensesByPersonID(personID);
        public LicenseDTO GetByID(int licenseID)  => _licensesRepository.GetByID(licenseID)?.ToDTO();
        public LicenseDTO GetByApplicationID(int ApplicationID) => _licensesRepository.GetByApplicationID(ApplicationID)?.ToDTO();
        public bool IsExistsLicenseByLDLApplication(int lDLApplication) => _licensesRepository.IsExistsLicenseByLDLApplication(lDLApplication);
        public bool IsExists(int licenseID) => _licensesRepository.IsExists(licenseID);
        public bool IsExpirationDateLicense(int license) => _licensesRepository.IsExpirationDateLicense(license);



    }

}
