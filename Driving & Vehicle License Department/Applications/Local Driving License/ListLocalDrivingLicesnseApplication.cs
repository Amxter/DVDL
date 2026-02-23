using BusinessDVLD;
using DatabaseDVLD;
using Driving___Vehicle_License_Department.Applications.Tests;
using Driving___Vehicle_License_Department.Licenses;
using Driving___Vehicle_License_Department.Licenses.Local_Licenses;
using Driving___Vehicle_License_Department.Login;
using PresentationDVLD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;
using Unity.Resolution;

namespace Driving___Vehicle_License_Department.Applications.Local_Driving_License
{
    public partial class ListLocalDrivingLicenseApplication : GeneralForm
    {

        ILDLApplicationServices _ldlApplicationServices;
        ITestAppointmentServices _testAppointmentServices;
        ILicenseService _licenseService;
        DataTable _dataTable;
        public ListLocalDrivingLicenseApplication(ILDLApplicationServices ldlApplicationServices,
            ITestAppointmentServices testAppointmentServices ,
            ILicenseService licenseService)
        {
            InitializeComponent();
            _ldlApplicationServices = ldlApplicationServices;
            _testAppointmentServices = testAppointmentServices;
            _loadData();
            _licenseService = licenseService;
        }
        private int GetSelectedLocalDrivingLicenseApplicationID()
        {
            if (dgvLocalDrivingLicenseApplications.CurrentRow == null)
                return -1;

            return Convert.ToInt32(
                dgvLocalDrivingLicenseApplications.CurrentRow
                .Cells["LocalDrivingLicenseApplicationID"].Value
            );
        }
        private string GetSelectedStatus()
        {
            if (dgvLocalDrivingLicenseApplications.CurrentRow == null)
                return "No Selection";

            return  dgvLocalDrivingLicenseApplications
                            .CurrentRow
                            .Cells["ApplicationStatus"]
                            .Value.ToString();
 
        }
        private void _loadData()
        {
            _dataTable = _ldlApplicationServices.GetAll();
            dgvLocalDrivingLicenseApplications.DataSource = _dataTable.DefaultView;
            dgvLocalDrivingLicenseApplications.Columns[0].Width = 100;
            dgvLocalDrivingLicenseApplications.Columns[1].Width = 300;
            dgvLocalDrivingLicenseApplications.Columns[2].Width = 100;
            dgvLocalDrivingLicenseApplications.Columns[3].Width = 300;
            dgvLocalDrivingLicenseApplications.Columns[4].Width = 150;
            dgvLocalDrivingLicenseApplications.Columns[5].Width = 100;
            dgvLocalDrivingLicenseApplications.Columns[6].Width = 100;
            lblRecordsCount.Text = dgvLocalDrivingLicenseApplications.Rows.Count.ToString();


        }
        private void btnAddNewApplication_Click(object sender, EventArgs e)
        {
            var frm = Program.Container.Resolve<AddUpdateLocalDrivingLicenseApplication>(
new ParameterOverride("LDLApplicationID", -1)
);
            frm.ShowDialog();

            _loadData();
        }
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int SelectedColumn = GetSelectedLocalDrivingLicenseApplicationID();

            if (SelectedColumn == -1)
            {
                MessageBox.Show("Please select a record to edit.");
                return;
            }

            var frm = Program.Container.Resolve<AddUpdateLocalDrivingLicenseApplication>(
new ParameterOverride("LDLApplicationID", SelectedColumn)
);
            frm.ShowDialog();
            _loadData();
        }
        private void DeleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int SelectedColumn = GetSelectedLocalDrivingLicenseApplicationID();

            if (SelectedColumn == -1)
            {
                MessageBox.Show("Please select a record to Deleted.");
                return;
            }

           ;

            if (MessageBox.Show($"Are you sure you want to delete the selected record? LDLApplicationID = {SelectedColumn} ", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                bool isDeleted = _ldlApplicationServices.Delete(SelectedColumn);
                if (isDeleted)
                {
                    MessageBox.Show("The selected record has been deleted successfully.");
                    _loadData();
                }
                else
                {
                    MessageBox.Show("Failed to delete the selected record.");
                }
            }



        }
        private void ListLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            filterLocalDrivingLicenseApplications1.SendPreparate(dgvLocalDrivingLicenseApplications, _dataTable);
        }
        private void scheduleVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = Program.Container.Resolve<ListTestsAppointments>(
                 new ParameterOverride("testType", ListTestsAppointments.TestType.Vision),
                 new ParameterOverride("LDLApplicationID", GetSelectedLocalDrivingLicenseApplicationID())

                   );

            frm.ShowDialog();

            _loadData();
        }
        private void scheduleWrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = Program.Container.Resolve<ListTestsAppointments>(
     new ParameterOverride("testType", ListTestsAppointments.TestType.Written),
     new ParameterOverride("LDLApplicationID", GetSelectedLocalDrivingLicenseApplicationID())

       );

            frm.ShowDialog();
            _loadData(); 
        }
        private void scheduleStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = Program.Container.Resolve<ListTestsAppointments>(
     new ParameterOverride("testType", ListTestsAppointments.TestType.Practical),
     new ParameterOverride("LDLApplicationID", GetSelectedLocalDrivingLicenseApplicationID())

       );

            frm.ShowDialog();
            _loadData();
        }
        private void _LoadStatusCompleted()
        {

            showDetailsToolStripMenuItem.Enabled = true;
            editToolStripMenuItem.Enabled = false;
            DeleteApplicationToolStripMenuItem.Enabled = false;
            CancelApplicaitonToolStripMenuItem.Enabled = false;
            ScheduleTestsMenue.Enabled = false;
            issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;
            showLicenseToolStripMenuItem.Enabled = true;
            showPersonLicenseHistoryToolStripMenuItem.Enabled = true;

        }
        private void _LoadStatusNew()
        {
            int appId = GetSelectedLocalDrivingLicenseApplicationID();

             
            showDetailsToolStripMenuItem.Enabled = true;
            editToolStripMenuItem.Enabled = true;
            DeleteApplicationToolStripMenuItem.Enabled = true;
            CancelApplicaitonToolStripMenuItem.Enabled = true;
            showLicenseToolStripMenuItem.Enabled = false;
            showPersonLicenseHistoryToolStripMenuItem.Enabled = true;

            
            bool passedVision = _testAppointmentServices.IsPassedTest(appId, TestTypesIDs.VisionTestID);
            bool passedWritten = _testAppointmentServices.IsPassedTest(appId, TestTypesIDs.WrittenTestID);
            bool passedPractical = _testAppointmentServices.IsPassedTest(appId, TestTypesIDs.PracticalTestID);

          
            scheduleVisionTestToolStripMenuItem.Enabled = !passedVision;
            scheduleWrittenTestToolStripMenuItem.Enabled = passedVision && !passedWritten;
            scheduleStreetTestToolStripMenuItem.Enabled = passedVision && passedWritten && !passedPractical;

          
            bool allPassed = passedVision && passedWritten && passedPractical;

            ScheduleTestsMenue.Enabled = !allPassed;
            issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = allPassed;
        }
        private void _LoadStatusCancelled()
        {

            showDetailsToolStripMenuItem.Enabled = true;
            editToolStripMenuItem.Enabled = false;
            DeleteApplicationToolStripMenuItem.Enabled = false;
            CancelApplicaitonToolStripMenuItem.Enabled = false;
            ScheduleTestsMenue.Enabled = false;
            issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;
            showLicenseToolStripMenuItem.Enabled = false;
            showPersonLicenseHistoryToolStripMenuItem.Enabled = true;

        }
        private void cmsApplications_Opening(object sender, CancelEventArgs e)
        {

            string status = GetSelectedStatus();
            if (status == "New")
            {
                _LoadStatusNew();
            }
            else if (status == "Completed")
            {
                _LoadStatusCompleted();
            }
            else if (status == "Cancelled")
            {
                _LoadStatusCancelled();
            }

        }
        private void issueDrivingLicenseFirstTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = Program.Container.Resolve<IssueDriverLicenseFirstTime>(
               
               new ParameterOverride("lDLApplicationID", GetSelectedLocalDrivingLicenseApplicationID()));



            frm.ShowDialog();
            _loadData(); 
        }
        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = Program.Container.Resolve<LDLApplicationInfo>(

           new ParameterOverride("lDLApplicationID", GetSelectedLocalDrivingLicenseApplicationID()));



            frm.ShowDialog();
        }
        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LDLApplicationDTO lDLApplicationDTO = _ldlApplicationServices.GetByID(GetSelectedLocalDrivingLicenseApplicationID());

            var frm = Program.Container.Resolve<ShowPersonLicenseHistory>(
                  new ParameterOverride("PersonID", lDLApplicationDTO.Application.ApplicantPersonID ));

            frm.ShowDialog();
        }
        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {

            LDLApplicationDTO lDLApplicationDTO = _ldlApplicationServices.GetByID(GetSelectedLocalDrivingLicenseApplicationID());


            var frm = Program.Container.Resolve<ShowLicensesInfo>(
            new ParameterOverride("licenseID",   _licenseService.GetByApplicationID(lDLApplicationDTO.Application.ApplicationID).LicenseID) );

            frm.ShowDialog();
           
        }
    }
}
