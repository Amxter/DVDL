using DatabaseDVLD;

namespace BusinessDVLD
{
    public class TestServices : ITestServices
    {

        readonly ITestRepository _testRepository;
        public TestServices (ITestRepository  testRepository)
        {

            _testRepository = testRepository;

        }
        public int Add(TestDTO dTO) => _testRepository.Add(dTO.ToEntity());
        public TestDTO GetByTestAppointmentID(int testAppointmentID) => _testRepository.GetByTestAppointmentID(testAppointmentID)?.ToDTO() ;
    }

}


