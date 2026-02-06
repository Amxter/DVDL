namespace BusinessDVLD
{
    public interface IApplicationServices
    {
        int Add(ApplicationDTO applicationDTO);
        ApplicationDTO GetByApplicationID(int applicationID);
        bool Delete(int applicationID);
        bool UpdateApplicationStatus(int applicationID, int applicationStatus, System.DateTime lastStatusDate); 
    }
}
