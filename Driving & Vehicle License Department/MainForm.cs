using BusinessDVLD;
using Driving___Vehicle_License_Department;
using Driving___Vehicle_License_Department.Applications.ApplicationTypes;
using Driving___Vehicle_License_Department.Applications.Local_Driving_License;
using Driving___Vehicle_License_Department.Applications.ManageTestTypes;
using Driving___Vehicle_License_Department.Applications.Renew_Local_Driving_license;
using Driving___Vehicle_License_Department.Applications.ReplaceLostOrDamagedLicense;
using Driving___Vehicle_License_Department.Applications.Rlease_Detained_License;
using Driving___Vehicle_License_Department.Drivers;
using Driving___Vehicle_License_Department.Licenses.International;
using Driving___Vehicle_License_Department.Users;
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

namespace PresentationDVLD
{
    public partial class MainForm : Form
    {
        Form _loginForm;
        private bool _suppressFormClosed  ; 
        public MainForm(Form loginForm)
        {
            InitializeComponent();
            _loginForm = loginForm;
            _suppressFormClosed = false;
        }

        private void peToolStripMenuItem_Click(object sender, EventArgs e)
        {

            var frm = Program.Container.Resolve<PeopleManagement>();
      
            frm.ShowDialog();
 

        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = Program.Container.Resolve<ListUsers>();


            frm.Show();
 
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = Program.Container.Resolve<ChangePassword>(
new ParameterOverride("user", CurrentUser.LoggedInUser.UserID));

            frm.ShowDialog();
 

        }

        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserInfo userInfo = new UserInfo(CurrentUser.LoggedInUser.UserID);
            userInfo.ShowDialog();
        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentUser.LoggedInUser = null;
            _loginForm.Visible = true;

            _suppressFormClosed = true;
            this.Close();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_suppressFormClosed)
                return;

            _loginForm.Close();
        }

        private void applicationTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var applicationTypes = Program.Container.Resolve<ApplicationTypes>() ;
            applicationTypes.ShowDialog();
        }

        private void maToolStripMenuItem_Click(object sender, EventArgs e)
        {

            var frm = Program.Container.Resolve<ManageTestTypes>();


            frm.ShowDialog();
 
        }

 

        private void localLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = Program.Container.Resolve<AddUpdateLocalDrivingLicenseApplication>(
           new ParameterOverride("LDLApplicationID", -1)
             );
            frm.ShowDialog();

           
     
        }

        private void localDrivingLicenseApllicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {


            var frm = Program.Container.Resolve<ListLocalDrivingLicenseApplication>();
            frm.ShowDialog();


        }

        private void driversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = Program.Container.Resolve<ListDrivers>();
            frm.ShowDialog();
            
        }

        private void intrnationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = Program.Container.Resolve<InternationalLicenseApplication>();



            frm.ShowDialog();
        }

        private void applicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void retakeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = Program.Container.Resolve<ListLocalDrivingLicenseApplication>();
            frm.ShowDialog();
        }

        private void internationaLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            var frm = Program.Container.Resolve<ListInternationalLicenses>();
            frm.ShowDialog();
        }

        private void renewDrivingLicensToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            var frm = Program.Container.Resolve<RenewLocalDrivingLicenseApplication>();
            frm.ShowDialog();
        }

        private void replacementForLostOrDamagedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = Program.Container.Resolve<ReplaceLostOrDamagedLicense>();
            frm.ShowDialog();
            
        }

        private void detainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        var frm = Program.Container.Resolve<DetainLicenseApplication>();
            frm.ShowDialog();
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = Program.Container.Resolve<ReleaseDetainedLicenseApplication>();
            frm.ShowDialog();
            
        }

        private void manToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = Program.Container.Resolve<ListDetainedLicenses>();
            frm.ShowDialog();
            
        }
    }
}
