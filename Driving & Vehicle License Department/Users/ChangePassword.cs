using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessDVLD;

namespace Driving___Vehicle_License_Department.Users
{
    public partial class ChangePassword : GeneralForm
    {

        bool _IsCurrentUserCorrect;
        bool _IsNewPasswordMatch;

        IUserServices _userServices;
        public ChangePassword(int user)
        {
            InitializeComponent();
            ucUserCard1.LoaderData(user);
            _IsCurrentUserCorrect = false;
            _IsNewPasswordMatch = false;
            _userServices = ServiceFactory.CreateUserServices();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void txtCurrentPassword_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCurrentPassword.Text))
            {
                errorProvider1.SetError(txtCurrentPassword, "Current password cannot be empty.");
            }
            else
            {

                if (!PasswordHelper.VerifyPassword(txtCurrentPassword.Text, ucUserCard1.User.Password))
                {
                    errorProvider1.SetError(txtCurrentPassword, "Current password is incorrect.");
                    _IsCurrentUserCorrect = false;
                    return;
                }
                else
                {
                    _IsCurrentUserCorrect = true;
                    errorProvider1.SetError(txtCurrentPassword, null);
                }
            }



        }
        private void _IsMatchPasswords()
        {
            if (txtNewPassword.Text.Trim() != txtConfirmPassword.Text.Trim())
            {
                errorProvider1.SetError(txtConfirmPassword, "Passwords do not match.");
                _IsNewPasswordMatch = false;
            }
            else
            {
                errorProvider1.SetError(txtConfirmPassword, null);
                _IsNewPasswordMatch = true;
            }
        }
        private void ValidateConfirmPassword()
        {
            if (txtNewPassword.Text != txtConfirmPassword.Text)
            {
                errorProvider1.SetError(txtConfirmPassword, "Passwords do not match.");
                _IsNewPasswordMatch = false;
            }
            else
            {
                errorProvider1.SetError(txtConfirmPassword, null);
                _IsMatchPasswords();
            }
        }

        private void txtNewPassword_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNewPassword.Text))
            {
                errorProvider1.SetError(txtNewPassword, "New password cannot be empty.");
                _IsNewPasswordMatch = false;
            }
            else
            {
                errorProvider1.SetError(txtNewPassword, null);
                _IsMatchPasswords();
            }


        }


        private void txtConfirmPassword_Leave(object sender, EventArgs e)
        {
            ValidateConfirmPassword();


        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_IsCurrentUserCorrect && _IsNewPasswordMatch)
            {
                ucUserCard1.User.Password = txtConfirmPassword.Text;
                if (_userServices.Update(ucUserCard1.User))
                {
                    MessageBox.Show("Password changed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("An error occurred while changing the password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                this.Close();
            }
            else
            {
                ValidateConfirmPassword();
                MessageBox.Show("Please correct the errors before saving.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }
        }
    }
}
