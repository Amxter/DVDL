using DatabaseDVLD;
using System.Data;

namespace BusinessDVLD
{
    public class LicenseClassServices : ILicenseClassServices
    {
        ILicenseClassRepository _licenseClassesRepository;
        public LicenseClassServices(ILicenseClassRepository licenseClasses)
        {
            _licenseClassesRepository = licenseClasses;
        }
        public DataTable GetAll() => _licenseClassesRepository.GetAll();
        
    }
}
