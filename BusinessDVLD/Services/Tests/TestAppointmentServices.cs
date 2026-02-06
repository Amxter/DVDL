using DatabaseDVLD;
using System;
using System.Data;

namespace BusinessDVLD
{
    public class TestAppointmentServices : ITestAppointmentServices
    {
        ITestAppointmentRepository _testAppointmentRepository;
        IApplicationServices _applicationServices;
        ILDLApplicationServices _lDLApplicationServices;
        public TestAppointmentServices(ITestAppointmentRepository testAppointmentRepository, IApplicationServices applicationServices , ILDLApplicationServices lDLApplicationServices)
        {
            _testAppointmentRepository = testAppointmentRepository;
            _applicationServices = applicationServices;
            _lDLApplicationServices = lDLApplicationServices;
        }
        public DataTable GetAllVisionTestByLDLApplication(int LocalDrivingLicenseApplicationID) => _testAppointmentRepository.GetAllVisionTestByLDLApplication(LocalDrivingLicenseApplicationID);
        public DataTable GetAllWrittenTestByLDLApplication(int LocalDrivingLicenseApplicationID) => _testAppointmentRepository.GetAllWrittenTestByLDLApplication(LocalDrivingLicenseApplicationID);
        public DataTable GetAllPracticalTestByLDLApplication(int LocalDrivingLicenseApplicationID) => _testAppointmentRepository.GetAllPracticalTestByLDLApplication(LocalDrivingLicenseApplicationID);
        public TestAppointmentDTO GetByID(int id) => _testAppointmentRepository.GetByID(id).ToDTO()   ;
        public int Add(TestAppointmentDTO dTO, ApplicationDTO application )
        {
            if ( 0 ==_testAppointmentRepository.HowMatchFiledTest(dTO.LocalDrivingLicenseApplicationID , dTO.TestTypeID ))
            {
                _testAppointmentRepository.Add(dTO.ToEntity()); 
     
                    return -1; 
            }


            application.ApplicantPersonID = _lDLApplicationServices.GetByID(dTO.LocalDrivingLicenseApplicationID).Application.ApplicantPersonID;
            application.ApplicationDate = DateTime.Now;
            application.ApplicationTypeID = 7;
            application.ApplicationStatus = 3;
            application.LastStatusDate = DateTime.Now;
          //  application.PaidFees = dTO.PaidFees;
            application.CreatedByUserID = dTO.CreatedByUserID;
            

            dTO.RetakeTestApplicationID = _applicationServices.Add(application);

            if (_testAppointmentRepository.Add(dTO.ToEntity()) != -1)
                return dTO.RetakeTestApplicationID;
            else 
                return -1;


        }
        public bool Update(TestAppointmentDTO testAppointment) => _testAppointmentRepository.Update(testAppointment.ToEntity());
        public bool isActiveAppointment(int LDLApplicationID, int TestTypeID) => _testAppointmentRepository.isActiveAppointment(LDLApplicationID, TestTypeID);
        public int HowMatchFiledTest(int lDLApplicationID, int TestTypeID ) => _testAppointmentRepository.HowMatchFiledTest(lDLApplicationID , TestTypeID);
        public bool IsPassedTest(int lDLApplicationID, int testTypeID) => _testAppointmentRepository.IsPassedTest(lDLApplicationID, testTypeID);
    }

}


