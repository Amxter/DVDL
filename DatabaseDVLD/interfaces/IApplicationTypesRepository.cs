using System.Data;

namespace DatabaseDVLD
{
    public interface IApplicationTypesRepository
    {
        DataTable GetAll();
        bool Update(ApplicationTypes application);
        ApplicationTypes GetApplication(string applicationTypeTitle);
        ApplicationTypes GetApplication(int applicationTypeID);
    }
}


