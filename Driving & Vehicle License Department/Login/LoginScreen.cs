using BusinessDVLD;
using DrivingVehicleLicenseDepartment.Ather_File;
using DrivingVehicleLicenseDepartment.Other_File;
using Microsoft.Win32;
using PresentationDVLD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
 
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;


namespace DrivingVehicleLicenseDepartment.Login
{
    public partial class LoginScreen : DrivingVehicleLicenseDepartment.GeneralForm
    {

      readonly  IUserServices _userServices;
        public LoginScreen(IUserServices userServices)
        {
            InitializeComponent();
            _userServices = userServices;
            LoadTheme(); 
        }
        private void LoadTheme()
        {

            try
            {
                var theme = Properties.Settings.Default.Theme; 
            }
            catch (ConfigurationErrorsException ex)
            {

                try
                {
                    if (!string.IsNullOrWhiteSpace(ex.Filename) && File.Exists(ex.Filename))
                        File.Delete(ex.Filename);
                }
                catch {  }

              
                Properties.Settings.Default.Reload();
            }

            var themeValue = Properties.Settings.Default.Theme;

            if (themeValue == "Dark")
                ThemeManager.SetTheme(AppTheme.Dark);
            else
                ThemeManager.SetTheme(AppTheme.Light);
        }

        private void ValidateInputs(object sender, CancelEventArgs e)
        {
            TextBox txtBox = sender as TextBox;

            if (string.IsNullOrWhiteSpace(txtBox.Text))
            {
                errorProvider1.SetError(txtBox, $"{txtBox.Tag} cannot be empty.");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtBox, null);
                e.Cancel = false;
            }


        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (this.ValidateChildren())
            {
                UserDTO userDTO = _userServices.GetByUserName(txtUserName.Text.Trim());
                if (userDTO != null && PasswordHelper.VerifyPassword(txtPassword.Text.Trim(), userDTO.Password))
                {
                    if (!userDTO.IsActive)
                    {
                        MessageBox.Show("Your account is inactive. Please contact the administrator.", "Account Inactive", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    CurrentUser.LoggedInUser = userDTO;

                    MainForm mainForm = new MainForm(this);
                    WindowsRegistry.SavePasswordAndUserNameInWindowsRegistry(chkRememberMe.Checked , txtUserName.Text.Trim(),  txtPassword.Text.Trim()  );
                    this.Hide();
                    mainForm.ShowDialog();



                }
                else
                {
                    MessageBox.Show("Invalid username or password. Please try again.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }



            }
            else
            {
                MessageBox.Show("Please correct the input errors before proceeding.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
       
        private void LoginScreen_Load(object sender, EventArgs e)
        {

            string UserName = string.Empty;
            string Password = string.Empty;
            WindowsRegistry.LoadUserNameAndPasswordFromWindowsRegistry(ref UserName ,ref Password );
            if (!string.IsNullOrEmpty(UserName))
            {
                txtUserName.Text = UserName;
            }
            if (!string.IsNullOrEmpty(Password))
            {
                txtPassword.Text = Password;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
