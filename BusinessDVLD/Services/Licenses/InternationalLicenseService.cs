using DatabaseDVLD;
using System;
using System.Data;

namespace BusinessDVLD
{
    public class InternationalLicenseService : IInternationalLicenseService
    {
        readonly IInternationalLicensesRepository _internationalLicensesRepository;
        readonly ILicenseService _licenseService;
 
        public InternationalLicenseService(IInternationalLicensesRepository internationalLicensesRepository,
            ILicenseService licenseService   )
        {
            _internationalLicensesRepository = internationalLicensesRepository;
            _licenseService = licenseService;
 
        }
        public int Add(int License , int applicationID)
        {
            InternationalLicenseDTO  license = new InternationalLicenseDTO
            {
                InternationalLicenseID = -1,
                ApplicationID = applicationID ,
                DriverID = _licenseService.GetByID(License).DriverID,
                IssuedUsingLocalLicenseID = License ,
                IssueDate = DateTime.Now,
                ExpirationDate = DateTime.Now.AddYears(1),
                IsActive = true,
                CreatedByUserID = CurrentUser.LoggedInUser.UserID
            };

            return _internationalLicensesRepository.Add(license.ToEntity());
        }
        public int DoesHaveActiveInternationalLicense(int licenseID) => _internationalLicensesRepository.DoesHaveActiveInternationalLicense(licenseID);
        public InternationalLicenseDTO GetByID(int internationalLicenseID) => _internationalLicensesRepository.GetByID(internationalLicenseID)?.ToDTO();
        public DataTable GetAll( ) => _internationalLicensesRepository.GetAll();
    }

}
