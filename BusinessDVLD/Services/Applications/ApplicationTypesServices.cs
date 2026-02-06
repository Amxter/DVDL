using DatabaseDVLD;
using System.Data;

namespace BusinessDVLD
{
    public class ApplicationTypesServices : IApplicationTypesServices
    {
        IApplicationTypesRepository _applicationTypesRepository;
        public ApplicationTypesServices(IApplicationTypesRepository applicationTypes)
        {
            _applicationTypesRepository = applicationTypes;

        }
        public DataTable GetAll() => _applicationTypesRepository.GetAll();
        public ApplicationTypesDTO GetApplication(string applicationTypeTitle) => _applicationTypesRepository.GetApplication(applicationTypeTitle).ToDTO();
        public ApplicationTypesDTO GetApplication(int applicationTypeID) => _applicationTypesRepository.GetApplication(applicationTypeID).ToDTO();
        public bool Upgrade(ApplicationTypesDTO applicationDTO) => _applicationTypesRepository.Update(applicationDTO.ToEntity());
    }
}
