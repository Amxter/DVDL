using DatabaseDVLD;
using System.Data;

namespace BusinessDVLD
{
    public class LicenseClassServices : ILicenseClassServices
    {
        readonly ILicenseClassRepository _licenseClassesRepository;
        public LicenseClassServices(ILicenseClassRepository licenseClasses)
        {
            _licenseClassesRepository = licenseClasses;
        }
        public DataTable GetAll() => _licenseClassesRepository.GetAll();
        public LicenseClassDTO GetByID(int licenseClassID) => _licenseClassesRepository.GetByID(licenseClassID)?.ToDTO()  ;

    }

 
}
