using DatabaseDVLD;
using System;
using System.Data;

namespace BusinessDVLD
{
    public class LDlApplicationServices : ILDLApplicationServices
    {
        ILDLApplicationRepository _lDLApplicationRepository;
        IApplicationServices _ApplicationServices;
        IApplicationTypesServices _ApplicationTypesServices;
        public LDlApplicationServices(ILDLApplicationRepository repository)
        {
            _lDLApplicationRepository = repository;
            _ApplicationServices = ServiceFactory.CreateApplicationServices();
            _ApplicationTypesServices = ServiceFactory.CreateApplicationTypesServices();
        }
        public int Add(int licenseClassID, int personID)
        {

            ApplicationTypesDTO applicationType = _ApplicationTypesServices.GetApplication("New Local Driving License Service");
            int applicationID = _ApplicationServices.Add(new ApplicationDTO
            {
                ApplicantPersonID = personID,
                ApplicationTypeID = applicationType.ID,
                ApplicationDate = DateTime.Now,
                LastStatusDate = DateTime.Now,
                ApplicationStatus = 1,
                PaidFees = applicationType.Fees,
                CreatedByUserID = CurrentUser.LoggedInUser.UserID


            });

            if (applicationID <= 0)
                return -1;

            ApplicationDTO application = _ApplicationServices.GetByApplicationID(applicationID);

            LDLApplicationDTO dTO = new LDLApplicationDTO
            {

                LicenseClassID = licenseClassID,
                Application = application
            };

            return _lDLApplicationRepository.Add(dTO.ToEntity());
        }
        public int GetPassedTestCount(int lDLApplicationID) => _lDLApplicationRepository.GetPassedTestCount(lDLApplicationID);
        public LDLApplicationDTO GetByID(int iD) => _lDLApplicationRepository.GetByID(iD)?.ToDTO();
        public bool Delete(int LDLApplicationID) => _ApplicationServices.Delete(_lDLApplicationRepository.GetByID(LDLApplicationID).Application.ApplicationID);
        public bool Update(LDLApplicationDTO dTO) => _lDLApplicationRepository.Update(dTO.ToEntity());
        public DataTable GetAll() => _lDLApplicationRepository.GetAll();
        public bool IsExistsApplicationByUsernameAndLicenseClass(int PersonID, int LicenseClassID) => _lDLApplicationRepository.IsExistsApplicationByPersonIDAndLicenseClass(PersonID, LicenseClassID);


    }

}


