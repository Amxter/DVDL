namespace BusinessDVLD
{
    public interface IApplicationServices
    {
        int Add(ApplicationDTO applicationDTO);
        ApplicationDTO GetByApplicationID(int applicationID);
        bool Delete(int applicationID);
    }
}
