using BusinessDVLD;
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
    public partial class ScheduleTest : UserControl
    {

        
        IApplicationServices _applicationServices;
        IApplicationTypesServices _applicationTypesServices;
        ILDLApplicationServices _lDLApplicationServices;
        ILicenseClassServices _licenseClassServices;
        IPersonServices _personServices;
        ITestTypesServices _testTypeServices;
        ITestAppointmentServices _testAppointmentServices;
        public enum Mode
        {
            Add, Update
        }
        public enum TestType
        {
            Vision = 1, Written, Practical
        }

        private Mode _mode;
        double _fees;
        TestType _TestType;
        int _testAppointmentID;
        LDLApplicationDTO _lDLApplicationDTO;
        TestAppointmentDTO _testAppointmentDTO;
        double _AppointmentFees;

        public bool IsDataEnabled
        {
            get { return dtpTestDate.Enabled ; }
            set { dtpTestDate.Enabled = value; }
        }

        public bool IsSaveEnabled
        {
            get { return btnSave.Enabled; }
            set { btnSave.Enabled = value; }
        }

        public ScheduleTest()
        {



            InitializeComponent();



            _lDLApplicationServices = ServiceFactory.CreateLDLApplicationServices();
            _licenseClassServices = ServiceFactory.CreateLicenseClassServices();
            _personServices = ServiceFactory.CreatePersonServices();
            _testTypeServices = ServiceFactory.CreateManageTestTypesServices();
            _testAppointmentServices = ServiceFactory.CreateTestAppointmentServices();
             _applicationServices = ServiceFactory.CreateApplicationServices();
            _applicationTypesServices = ServiceFactory.CreateApplicationTypesServices();
        }
        private void _LoadVision()
        {
            this.Text = "Schedule Vision Test";
            gbTestType.Text = "Vision Test";
            pbTestTypeImage.Image = Properties.Resources.Vision_512;

        }
        private void _LoadWritten()
        {
            this.Text = "Schedule Written Test";
            gbTestType.Text = "Written Test";
            pbTestTypeImage.Image = Properties.Resources.Written_Test_321;
        }
        private void _LoadPractical()
        {
            this.Text = "Schedule Practical Test";
            gbTestType.Text = "Practical Test";
            pbTestTypeImage.Image = Properties.Resources.driving_test_512;
        }
        private void _LoadForm(TestType testType)
        {
            switch (testType)
            {
                case TestType.Vision:
                    _LoadVision();
                    break;
                case TestType.Written:
                    _LoadWritten();
                    break;
                case TestType.Practical:
                    _LoadPractical();
                    break;
                default:
                    break;
            }

        }
        private void _LoadDataFromAdd(int LDLApplication)
        {
            _lDLApplicationDTO = _lDLApplicationServices.GetByID(LDLApplication);
            lblLocalDrivingLicenseAppID.Text = _lDLApplicationDTO.LocalDrivingLicenseApplicationID.ToString();
            lblDrivingClass.Text = _licenseClassServices.GetByID(_lDLApplicationDTO.LicenseClassID).ClassName.ToString();
            lblFullName.Text = _personServices.GetByID(_lDLApplicationDTO.Application.ApplicantPersonID).FullName.ToString();
            dtpTestDate.Value = DateTime.Now;
            _fees = _testTypeServices.GetByID(Convert.ToInt32(_TestType)).TestTypeFees;
            lblFees.Text = _fees.ToString();

             _AppointmentFees = 0;
            int filedTests = _testAppointmentServices.HowMatchFiledTest(_lDLApplicationDTO.LocalDrivingLicenseApplicationID, Convert.ToInt32(_TestType));
            lblTrial.Text = filedTests.ToString();


            if (filedTests != 0 )
            {
                _AppointmentFees = _applicationTypesServices.GetApplication("Retake Test").Fees;
            }

            lblRetakeAppFees.Text = _AppointmentFees.ToString();
            //_fees = _fees + AppointmentFees;
            lblTotalFees.Text = (_fees + _AppointmentFees).ToString();

        }
        private void  LockedForm ()
        {
            IsDataEnabled = false;
            IsSaveEnabled = false;
            lblUserMessage.Enabled = true;
        }
        private void _LoadDataFromUpdate(int LDLApplication)
        {
            _lDLApplicationDTO = _lDLApplicationServices.GetByID(LDLApplication);
            _testAppointmentDTO = _testAppointmentServices.GetByID(_testAppointmentID );

            if (_testAppointmentDTO.IsLocked )
            {
                LockedForm();
            }

            lblLocalDrivingLicenseAppID.Text = _lDLApplicationDTO.LocalDrivingLicenseApplicationID.ToString();
            lblDrivingClass.Text = _licenseClassServices.GetByID(_lDLApplicationDTO.LicenseClassID).ClassName.ToString();
            lblFullName.Text = _personServices.GetByID(_lDLApplicationDTO.Application.ApplicantPersonID).FullName.ToString();
            dtpTestDate.MinDate = _testAppointmentDTO.AppointmentDate;
            dtpTestDate.Value = _testAppointmentDTO.AppointmentDate ;  
            _fees = _testAppointmentDTO.PaidFees;
            lblFees.Text = _fees.ToString();

            int filedTests = _testAppointmentServices.HowMatchFiledTest(_lDLApplicationDTO.LocalDrivingLicenseApplicationID, Convert.ToInt32(_TestType));
            lblTrial.Text = filedTests.ToString(); 

            if (filedTests <= 0)
            {
                lblTotalFees.Text = _fees.ToString();
                lblRetakeAppFees.Text = "0";
                lblRetakeTestAppID.Text = "[??]";

            }
            else
            {

                ApplicationDTO applicationDTO = _applicationServices.GetByApplicationID(_testAppointmentDTO.RetakeTestApplicationID);

                lblTotalFees.Text = (_fees + applicationDTO.PaidFees).ToString();
                lblRetakeTestAppID.Text = _testAppointmentDTO.RetakeTestApplicationID.ToString();
                lblRetakeAppFees.Text = applicationDTO.PaidFees.ToString();
            }





        }
        public void LoadScheduleTest(Mode mode, TestType testType, int LDLApplication )
        {

            _LoadForm(testType);
            _TestType = testType;
            _mode = mode;
             
            if (mode == Mode.Add)
            {
                _LoadDataFromAdd(LDLApplication);
            }
            else if (mode == Mode.Update)
            {
                _LoadDataFromUpdate(LDLApplication);
            }

        }
        public void LoadScheduleTest(Mode mode, TestType testType, int LDLApplication , int testAppointment )
        {

            _LoadForm(testType);
            _TestType = testType;
            _mode = mode;
            _testAppointmentID = testAppointment;
            if (mode == Mode.Add)
            {
                _LoadDataFromAdd(LDLApplication);
            }
            else if (mode == Mode.Update)
            {
                _LoadDataFromUpdate(LDLApplication);
            }

        }
        private void ScheduleTest_Load(object sender, EventArgs e)
        {
            dtpTestDate.MinDate = DateTime.Now;
        }
        async void ShowMessage()
        {
            await Task.Delay(100);
            MessageBox.Show("Test Appointment Scheduled Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void _AddTestAppointment()
        {
            _testAppointmentDTO = new TestAppointmentDTO();

            _testAppointmentDTO.LocalDrivingLicenseApplicationID = _lDLApplicationDTO.LocalDrivingLicenseApplicationID;
            _testAppointmentDTO.TestTypeID = Convert.ToInt32(_TestType);
            _testAppointmentDTO.AppointmentDate = dtpTestDate.Value;
            _testAppointmentDTO.PaidFees = _fees;
            _testAppointmentDTO.CreatedByUserID = CurrentUser.LoggedInUser.UserID;
            _testAppointmentDTO.IsLocked = false;
            _testAppointmentDTO.RetakeTestApplicationID = -1;

            ApplicationDTO applicationDTO = new ApplicationDTO();
            applicationDTO.PaidFees = _AppointmentFees;
            int RetakeTestApplicationID = _testAppointmentServices.Add(_testAppointmentDTO , applicationDTO );
            if (RetakeTestApplicationID == -1)
            {
                lblRetakeTestAppID.Text = "[??]";
            }
            else
            {
                lblRetakeTestAppID.Text = RetakeTestApplicationID.ToString();
            }

            ShowMessage();
            IsDataEnabled = false;
            IsSaveEnabled = false;
        }
        private void _UpdateTestAppointment()
        {
            _testAppointmentDTO.AppointmentDate = dtpTestDate.Value;

            if (_testAppointmentServices.Update(_testAppointmentDTO))
            {
                MessageBox.Show("Successfully to update Test Appointment.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
   
            }
            else
            {
                MessageBox.Show("Failed to update Test Appointment.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            IsDataEnabled = false;
            IsSaveEnabled = false;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {

            if (_mode == Mode.Add)
            {
                _AddTestAppointment();
            }
            else if (_mode == Mode.Update)
            {
                _UpdateTestAppointment();
            }

        }
     
    }
}
