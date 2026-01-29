using BusinessDVLD;
using Driving___Vehicle_License_Department;
using Driving___Vehicle_License_Department.Applications.Local_Driving_License;
using Driving___Vehicle_License_Department.Applications.ManageTestTypes;
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
            PeopleManagement peopleManagement = new PeopleManagement();
            peopleManagement.MdiParent = this;
            peopleManagement.Show();

        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListUsers listUsers = new ListUsers();
            listUsers.MdiParent = this;
            listUsers.Show(); 
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangePassword changePassword = new ChangePassword(CurrentUser.LoggedInUser.UserID);
            changePassword.ShowDialog();

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
            ApplicationTypes applicationTypes = new ApplicationTypes();
            applicationTypes.ShowDialog();
        }

        private void maToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageTestTypes manageTestTypes = new ManageTestTypes();
            manageTestTypes.ShowDialog();
        }

 

        private void localLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddUpdateLocalDrivingLicenseApplication addUpdateLocalDrivingLicenseApplication = new AddUpdateLocalDrivingLicenseApplication(-1);
            addUpdateLocalDrivingLicenseApplication.MdiParent = this;
            addUpdateLocalDrivingLicenseApplication.Show();
        }

        private void localDrivingLicenseApllicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {



            ListLocalDrivingLicenseApplication listLocalDrivingLicenseApplication = new ListLocalDrivingLicenseApplication();
            listLocalDrivingLicenseApplication.MdiParent = this;
            listLocalDrivingLicenseApplication.Show();
        }
    }
}
