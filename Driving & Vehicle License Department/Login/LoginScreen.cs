using BusinessDVLD;
using Driving___Vehicle_License_Department.Ather_File;
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
using Microsoft.Win32;


namespace Driving___Vehicle_License_Department.Login
{
    public partial class LoginScreen : GeneralForm
    {

        IUserServices _userServices;
        public LoginScreen(IUserServices userServices)
        {
            InitializeComponent();
            _userServices = userServices;
            LoadTheme(); 
        }
        private void LoadTheme()
        {
            var theme = Driving___Vehicle_License_Department.Properties.Settings.Default.Theme;
            if (theme == "Dark")
                ThemeManager.SetTheme(AppTheme.Dark);
            else
                ThemeManager.SetTheme(AppTheme.Light);
        }
        private void SavePasswordAndUserNameInWindowsRegistry(bool isSave)
        {
            // Specify the Registry key and path
            string keyPath = @"HKEY_CURRENT_USER\SOFTWARE\DVDLApplication";


            if (isSave)
            {
                try
                {
                    // Write the value to the Registry
                    Registry.SetValue(keyPath, "UserName", txtUserName.Text, RegistryValueKind.String);
                    Registry.SetValue(keyPath, "Password", txtPassword.Text, RegistryValueKind.String);

                }
                catch
                {

                }
                
            }
            else
            {
                try
                {
                    // Write the value to the Registry
                    Registry.SetValue(keyPath, "UserName",  "", RegistryValueKind.String);
                    Registry.SetValue(keyPath, "Password",  "", RegistryValueKind.String);

                }
                catch
                {

                }

            }



            
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
                    SavePasswordAndUserNameInWindowsRegistry(chkRememberMe.Checked);
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
        private void LoadUserNameAndPasswordFromWindowsRegistry()
        {
            string keyPath = @"HKEY_CURRENT_USER\SOFTWARE\DVDLApplication";
            


            try
            {

                string UserName = Registry.GetValue(keyPath, "UserName", null) as string;
                string Password = Registry.GetValue(keyPath, "Password" , null) as string;

                if ( !string.IsNullOrEmpty(UserName))
                {
                     txtUserName.Text = UserName;
                }

                if (!string.IsNullOrEmpty(Password))
                {
                    txtPassword.Text = Password;
                }

            }
            catch (Exception ex)
            {
               
            }
        }
        private void LoginScreen_Load(object sender, EventArgs e)
        {
            LoadUserNameAndPasswordFromWindowsRegistry();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
