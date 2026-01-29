using System.Data;

namespace BusinessDVLD
{
    public interface IApplicationTypesServices
    {
        DataTable GetAll();
        bool Upgrade(ApplicationTypesDTO applicationDTO);
        ApplicationTypesDTO GetApplication(int applicationTypeID);
        ApplicationTypesDTO GetApplication(string applicationTypeTitle); 
    }
}
