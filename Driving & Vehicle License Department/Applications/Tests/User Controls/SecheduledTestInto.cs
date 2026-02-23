using BusinessDVLD;
using DatabaseDVLD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Driving___Vehicle_License_Department.Applications.Tests.User_Controls
{
    public partial class ScheduledTestInto : UserControl
    {
 
        ILDLApplicationServices _lDLApplicationServices;
        ILicenseClassServices _licenseClassServices;
        IPersonServices _personServices;
        ITestAppointmentServices _testAppointmentServices;
        public ScheduledTestInto()
        {

            InitializeComponent();
            _lDLApplicationServices = ServiceFactory.CreateLDLApplicationServices();
            _licenseClassServices = ServiceFactory.CreateLicenseClassServices();
            _personServices = ServiceFactory.CreatePersonServices();
            _testAppointmentServices = ServiceFactory.CreateTestAppointmentServices();


        }

        private void LoadControl (int TestID )
        {
            if (TestID == TestTypesIDs.VisionTestID )
            {
                pbTestTypeImage.Image = Properties.Resources.Vision_512;
            }
            else if (TestID == TestTypesIDs.WrittenTestID)
            {
                pbTestTypeImage.Image = Properties.Resources.Written_Test_512;
            }
            else if (TestID == TestTypesIDs.PracticalTestID)
            {
                pbTestTypeImage.Image = Properties.Resources.driving_test_512;
            }

        }
        public void LoadData(int TestID , int TestAppointment )
        {
            TestAppointmentDTO _testAppointmentDTO = _testAppointmentServices.GetByID(TestAppointment);
            LDLApplicationDTO _lDLApplicationDTO = _lDLApplicationServices.GetByID(_testAppointmentDTO.LocalDrivingLicenseApplicationID);


            LoadControl(TestID); 
            lblLocalDrivingLicenseAppID.Text = _testAppointmentDTO.LocalDrivingLicenseApplicationID.ToString();
            lblDrivingClass.Text = _licenseClassServices.GetByID(_lDLApplicationDTO.LicenseClassID).ClassName.ToString();
            lblFullName.Text = _personServices.GetByID(_lDLApplicationDTO.Application.ApplicantPersonID).FullName.ToString();
            lblDate.Text = _testAppointmentDTO.AppointmentDate.ToShortDateString();
            lblFees.Text = _testAppointmentDTO.PaidFees.ToString();
            lblTestID.Text = TestID.ToString();
            int filedTests = _testAppointmentServices.HowMatchFiledTest(_lDLApplicationDTO.LocalDrivingLicenseApplicationID, Convert.ToInt32(_testAppointmentDTO.TestTypeID));
            lblTrial.Text = filedTests.ToString();

 
        }
    }
}
