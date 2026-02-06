using DatabaseDVLD;
using System.Data;

namespace BusinessDVLD
{
    public interface ITestAppointmentServices
    {
        DataTable GetAllVisionTestByLDLApplication(int LocalDrivingLicenseApplicationID);
        DataTable GetAllWrittenTestByLDLApplication(int LocalDrivingLicenseApplicationID);
        DataTable GetAllPracticalTestByLDLApplication(int LocalDrivingLicenseApplicationID);
        TestAppointmentDTO GetByID(int id);
        int Add(TestAppointmentDTO dTO , ApplicationDTO application);
        bool Update(TestAppointmentDTO testAppointment);
        bool isActiveAppointment(int LDLApplicationID, int TestTypeID);
        int HowMatchFiledTest(int lDLApplicationID, int TestTypeID);
        bool IsPassedTest(int lDLApplicationID, int testTypeID);
    }

}


