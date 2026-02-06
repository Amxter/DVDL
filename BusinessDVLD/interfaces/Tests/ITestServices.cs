using DatabaseDVLD;

namespace BusinessDVLD
{
    public interface ITestServices
    {
        int Add(TestDTO dTO);
        TestDTO GetByTestAppointmentID(int testAppointmentID); 
    }
}


