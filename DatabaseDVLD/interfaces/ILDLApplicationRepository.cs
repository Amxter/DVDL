using System.Data;

namespace DatabaseDVLD
{
    public interface ILDLApplicationRepository
    {
        int Add(LDLApplication application);
        bool IsExistsApplicationByUsernameAndLicenseClass(int PersonID, int LicenseClassID);
        DataTable GetAll();
        LDLApplication GetByID(int localDrivingLicenseApplicationID);
        bool Update(LDLApplication application); 


    }

}


