using System.Data;

namespace DatabaseDVLD
{
    public interface ITestAppointmentRepository
    {
        int Add(TestAppointment testAppointment);
        bool Update(TestAppointment testAppointment);
        bool Delete(int testAppointmentID);
        DataTable GetAllVisionTestByLDLApplication(int LocalDrivingLicenseApplicationID);
        DataTable GetAllWrittenTestByLDLApplication(int LocalDrivingLicenseApplicationID);
        DataTable GetAllPracticalTestByLDLApplication(int LocalDrivingLicenseApplicationID);
        TestAppointment GetByID(int testAppointmentID);
        int HowMatchFiledTest(int LDLApplication, int TestTypeID);
        bool IsPassedTest(int lDLApplicationID, int testTypeID);
        bool isActiveAppointment(int LDLApplicationID, int TestTypeID);
         

    }


}





