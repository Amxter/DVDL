using BusinessDVLD;
using DatabaseDVLD;
using DrivingVehicleLicenseDepartment.Applications.Tests.User_Controls;
using DrivingVehicleLicenseDepartment.Properties;

using PresentationDVLD;
using System;
using System.Windows.Forms;
using Unity;
using Unity.Resolution;
using static DrivingVehicleLicenseDepartment.Applications.Tests.ListTestsAppointments;

namespace DrivingVehicleLicenseDepartment.Applications.Tests
{
    public partial class ListTestsAppointments : GeneralForm
    {
        readonly ITestAppointmentServices _testAppointmentServices;
        public enum TestType
        {
            Vision = 1 , Written, Practical 
        }
        TestType _testType;
        int _lDLApplicationID; 
        private void LoadAppointmentDetails()
        {
            
            switch (_testType)
            {
                case TestType.Vision:
                    dgvLicenseTestAppointments.DataSource = _testAppointmentServices.GetAllVisionTestByLDLApplication(_lDLApplicationID);
                    break;
                case TestType.Written:
                    dgvLicenseTestAppointments.DataSource = _testAppointmentServices.GetAllWrittenTestByLDLApplication(_lDLApplicationID);
                    break;
                case TestType.Practical:
                    dgvLicenseTestAppointments.DataSource = _testAppointmentServices.GetAllPracticalTestByLDLApplication(_lDLApplicationID);
                    break;
                default:
                    break;
            }

            lblRecordsCount.Text = dgvLicenseTestAppointments.Rows.Count.ToString() + " Records Found";
        }
        private void LoadVision ()
        {
            this.Text = "List Vision Test Appointments";
            lblTitle.Text = "Vision Test Appointments";
            pbTestTypeImage.Image = Resources.Vision_512;
            localDrivingLicenseApplicationInfo1.LoadData(_lDLApplicationID);

        }
        private int GetSelectedTestAppointmentID()
        {
           return dgvLicenseTestAppointments.CurrentRow != null ? Convert.ToInt32(dgvLicenseTestAppointments.CurrentRow.Cells["TestAppointmentID"].Value) : -1;
        }
        private void LoadWritten()
        {
            this.Text = "List Written Test Appointments";
            lblTitle.Text = "Written Test Appointments";
            pbTestTypeImage.Image = Resources.Written_Test_512 ;
            localDrivingLicenseApplicationInfo1.LoadData(_lDLApplicationID);
        }
        private void LoadPractical()
        {
            this.Text = "List Practical Test Appointments";
            lblTitle.Text = "Practical Test Appointments";
            pbTestTypeImage.Image = Resources.Schedule_Test_512 ;
            localDrivingLicenseApplicationInfo1.LoadData(_lDLApplicationID);
        }
        private void LoadForm(TestType testType)
        {

            switch (testType)
            {
                case TestType.Vision:
                    LoadVision();
                    break;
                case TestType.Written:
                    LoadWritten();
                    break;
                case TestType.Practical:
                    LoadPractical();
                    break;
                default:
                    break;
            }
        }
        public ListTestsAppointments(TestType testType , int LDLApplicationID  , ITestAppointmentServices  testAppointmentServices)
        {
            
            InitializeComponent();
            _testType = testType;
            _lDLApplicationID = LDLApplicationID;
            _testAppointmentServices = testAppointmentServices;
            LoadForm(testType);
            LoadAppointmentDetails();
        }
        private void btnAddNewAppointment_Click(object sender, EventArgs e)
        {
            if (_testAppointmentServices.IsActiveAppointment(_lDLApplicationID , Convert.ToInt32(_testType) )  )
            {
                MessageBox.Show("An active Vision Test Appointment already exists for this application. You cannot schedule another one until the existing appointment is completed or canceled.", "Active Appointment Exists", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else 
           {
                if (_testAppointmentServices.IsPassedTest(_lDLApplicationID, Convert.ToInt32(_testType) ))
                {
                    MessageBox.Show("The applicant has already passed this test. Scheduling a new appointment is not allowed.", "Test Already Passed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var frm = Program.Container.Resolve<ScheduleTestForm>(
                new ParameterOverride("mode", ScheduleTest.Mode.Add),
                new ParameterOverride("testType", _testType ),
                new ParameterOverride("LDLApplication", _lDLApplicationID));
                frm.ShowDialog();
 
                LoadAppointmentDetails();
            }
            

        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {

            var frm = Program.Container.Resolve<ScheduleTestForm>(
                new ParameterOverride("mode", ScheduleTest.Mode.Update),
                new ParameterOverride("testType", _testType),
                new ParameterOverride("LDLApplication", _lDLApplicationID),
                new ParameterOverride("testAppointment", GetSelectedTestAppointmentID())
                  );
 
            frm.ShowDialog();

            LoadAppointmentDetails();
        }
        private TakeTest.Mode IsLocked ()
        {
            bool isLocked = dgvLicenseTestAppointments.CurrentRow != null ? Convert.ToBoolean(dgvLicenseTestAppointments.CurrentRow.Cells["IsLocked"].Value) : false ; ;
            if (isLocked)
            {
                return TakeTest.Mode.Show;
            }
            else
            {
                return TakeTest.Mode.Add;
            }

        }
        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            

               var frm = Program.Container.Resolve<TakeTest>(
                new ParameterOverride("TestID", _testType ),
                new ParameterOverride("AppointmentID", GetSelectedTestAppointmentID() ),
                 new ParameterOverride("mode", IsLocked() )
                  );

            frm.ShowDialog();

            LoadAppointmentDetails();
        }
    }
}
