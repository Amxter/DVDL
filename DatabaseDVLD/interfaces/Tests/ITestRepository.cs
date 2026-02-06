namespace DatabaseDVLD
{
    public interface ITestRepository
    {
        int Add(Test test);
        Test GetByTestAppointmentID(int testAppointmentID);
    }
}





