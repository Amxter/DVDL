namespace DatabaseDVLD
{
    public interface IApplicationRepository
    {
        int Add(Application application);
        bool Delete(int applicationID);
        Application GetByApplicationID(int applicationID);

    }

}


