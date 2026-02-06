using System.Data;

namespace DatabaseDVLD
{
    public interface ILDLApplicationRepository
    {
        int Add(LDLApplication application);
        bool IsExistsApplicationByPersonIDAndLicenseClass(int PersonID, int LicenseClassID);
        DataTable GetAll();
        LDLApplication GetByID(int localDrivingLicenseApplicationID);
        bool Update(LDLApplication application);

        int GetPassedTestCount(int lDLApplicationID);


    }

}


