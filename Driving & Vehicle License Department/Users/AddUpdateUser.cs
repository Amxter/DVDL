using BusinessDVLD;
using Driving___Vehicle_License_Department.Applications;
using Driving___Vehicle_License_Department.People.User_Controls;
using PresentationDVLD;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using Unity;

namespace Driving___Vehicle_License_Department.Users
{
    public partial class AddUpdateUser : GeneralForm
    {
        enum UserMode
        {
            Add = -1,
            Update
        }

        bool _isValidUserName;
        bool _isValidPassword;
        UserDTO _user;
        UserMode _userMode;

        IUserServices _userServices;


        private void _InitialAddMode()
        {
            tpLoginInfo.Enabled = false;
            btnSave.Enabled = false;

        }
        private void _InitialUpdateMode()
        {
            lblTitle.Text = "Update User";


            if (_user != null)
            {
                filterPerson1.FilterEnabled = false;
                _userMode = UserMode.Update;
                filterPerson1.FilterPersonByID(_user.PersonID);
                txtUserName.Text = _user.UserName;
                chkIsActive.Checked = _user.IsActive;
                tpLoginInfo.Enabled = true;
                btnSave.Enabled = true;
                _isValidUserName = true;
            }
            else
            {
                MessageBox.Show("User not found.");
                this.Close();
            }
        }
        public AddUpdateUser(IUserServices userServices , IPersonServices personServices,int userId)
        {
            InitializeComponent();
            _userServices = userServices ;

  
            _isValidUserName = false;
            _isValidPassword = false;
            if (userId == (int)UserMode.Add)
            {
                _userMode = UserMode.Add;
                _user = new UserDTO();
                _user.PersonID = -1;
                _user.UserID = -1;
                this.Text = "Add User";
                _InitialAddMode();
            }
            else
            {
                _userMode = UserMode.Update;
                
                _user = _userServices.GetByID(userId);
                _user.UserID = userId;
                this.Text = "Update User";
                _InitialUpdateMode();
            }
            
        }
        private void ValidateEmptyTextBox(object sender, CancelEventArgs e)
        {
            if (sender is TextBox Temp)
            {
                if (string.IsNullOrWhiteSpace(Temp.Text))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(Temp, "This field is required!");
                }
                else
                {
                    e.Cancel = false;
                    errorProvider1.SetError(Temp, null);
                }
            }

        }
         private void InsertsLoginInfo()
        {
            tpLoginInfo.Enabled = true;
            btnSave.Enabled = true;

        }
        private void btnPersonInfoNext_Click(object sender, EventArgs e)
        {

            if (_userMode == UserMode.Add)
            {
                if (_user.PersonID != -1)
                {
                    if (!_userServices.IsExistsByPersonID(_user.PersonID))
                    {
                        tcUserInfo.SelectedTab = tpLoginInfo;
                        InsertsLoginInfo();
                    }
                    else
                    {
                        MessageBox.Show("This person already has a user account.");
                        _InitialAddMode();
                    }
                }
                else
                {
                    MessageBox.Show("Please select a person first.");
                    _InitialAddMode();
                }
            }
            else if (_userMode == UserMode.Update)
            {
                tcUserInfo.SelectedTab = tpLoginInfo;
            }
        }
        private void filterPerson1_OnPersonSelected(int obj)
        {
            _user.PersonID = obj;

            if (_user.PersonID != -1)
            {
                if (!_userServices.IsExistsByPersonID(_user.PersonID))
                {
                    tcUserInfo.SelectedTab = tpLoginInfo;
                    InsertsLoginInfo();
                }
                else
                {
                    _InitialAddMode();
                }
            }
            else
            {

                _InitialAddMode();
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void matchPasswords()
        {
            if (txtPassword.Text.Trim() != txtConfirmPassword.Text.Trim())
            {
                errorProvider1.SetError(txtConfirmPassword, "Passwords do not match.");
                _isValidPassword = false;
            }
            else
            {
                errorProvider1.SetError(txtConfirmPassword, null);
                _isValidPassword = true;
            }
        }
        private void txtConfirmPassword_Leave(object sender, EventArgs e)
        {
            matchPasswords();
        }
        private void AddUser()
        {


            _user.UserName = txtUserName.Text.Trim();
            _user.Password = txtPassword.Text.Trim();
            _user.IsActive = chkIsActive.Checked;

            _user.UserID = _userServices.Add(_user);

            if (_user.UserID > 0)
            {
                _InitialUpdateMode();
                MessageBox.Show("User added successfully.");

            }
            else
            {
                MessageBox.Show("Failed to add user.");
                this.Close();
            }
        }
        private void UpdateUser()
        {

            _user.UserName = txtUserName.Text.Trim();
            _user.Password = txtPassword.Text.Trim();
            _user.IsActive = chkIsActive.Checked;

            if (_userServices.Update(_user))
            {
                MessageBox.Show("Update added successfully.");

            }
            else
            {
                MessageBox.Show("Failed to Update user.");
                this.Close();
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.ValidateChildren() && _isValidUserName && _isValidPassword)
            {
                if (_userMode == UserMode.Add)
                {
                    AddUser();
                }
                else if (_userMode == UserMode.Update)
                {
                    UpdateUser();
                }
            }
            else
            {
                MessageBox.Show("Please correct the errors before saving.");


            }
        }
        private void txtUserName_Leave(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtUserName.Text))
            {
                errorProvider1.SetError(txtUserName, "This field is required!");
                _isValidUserName = false;
                return;
            }


            if (_userMode == UserMode.Add)
            {

                UserDTO existingUser = _userServices.GetByUserName(txtUserName.Text.Trim());

                if (existingUser != null)
                {
                    errorProvider1.SetError(txtUserName, "This user name is already taken.");
                    _isValidUserName = false;
                }
                else
                {
                    errorProvider1.SetError(txtUserName, null);
                    _isValidUserName = true;
                }
            }
            else if (_userMode == UserMode.Update)
            {
                if (_userServices.IsExistsByUserNameExceptUserID(txtUserName.Text.Trim(), _user.UserID))
                {
                    errorProvider1.SetError(txtUserName, "This user name is already taken.");

                }
                else
                {
                    errorProvider1.SetError(txtUserName, null);
                    btnSave.Enabled = true;
                }
            }
        }
        private void txtPassword_Leave(object sender, EventArgs e)
        {
           
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {

                errorProvider1.SetError(txtPassword, "This field is required!");
            }
            else
            {
                errorProvider1.SetError(txtPassword, null);

            }
            matchPasswords();
        }

        private void AddUpdateUser_Load(object sender, EventArgs e)
        {
 
        }
    }
}
