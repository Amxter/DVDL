using DatabaseDVLD;

namespace BusinessDVLD
{
    public class ApplicationServices : IApplicationServices
    {
        readonly IApplicationRepository   _applicationRepository;
        public ApplicationServices(IApplicationRepository applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }

        public ApplicationDTO GetByApplicationID(int applicationID) => _applicationRepository.GetByApplicationID(applicationID)?.ToDTO();

        public int Add(ApplicationDTO applicationDTO) => _applicationRepository.Add(applicationDTO.ToEntity());

        public bool Delete(int applicationID) => _applicationRepository.Delete(applicationID);
        public bool UpdateApplicationStatus(int applicationID, int applicationStatus, System.DateTime lastStatusDate) =>
            _applicationRepository.UpdateApplicationStatus(applicationID, applicationStatus, lastStatusDate);
    }
}
