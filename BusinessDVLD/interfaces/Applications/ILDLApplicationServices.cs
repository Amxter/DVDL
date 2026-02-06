using System.Data;

namespace BusinessDVLD
{
    public interface ILDLApplicationServices
    {
        int Add(int licenseClassID, int personID);
        DataTable GetAll();
        LDLApplicationDTO GetByID(int iD);
        bool Update(LDLApplicationDTO dTO);
        bool Delete(int LDLApplicationID);
        bool IsExistsApplicationByUsernameAndLicenseClass(int PersonID, int LicenseClassID);
        int GetPassedTestCount(int lDLApplicationID); 
    }
}


