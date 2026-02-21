using BusinessDVLD;
using PresentationDVLD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;


namespace Driving___Vehicle_License_Department.Login
{
    public partial class LoginScreen : GeneralForm
    {

        IUserServices _userServices;
        public LoginScreen(IUserServices  userServices)
        {
            InitializeComponent();
            _userServices = userServices ;
        }
        private void SavePasswordAndUserNameInFile(bool isSave)
        {
            string filePath = "login.txt"; 

            if (isSave)
            {
                 
                string content =
                    txtUserName.Text + Environment.NewLine +
                    txtPassword.Text;

                File.WriteAllText(filePath, content);
            }
            else
            {
                
                if (File.Exists(filePath))
                {
                    File.WriteAllText(filePath, string.Empty);
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
                    SavePasswordAndUserNameInFile(chkRememberMe.Checked);
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
        private void LoadUserNameAndPasswordFromFile()
        {
            string filePath = "login.txt";

           
            if (!File.Exists(filePath))
                return;

            string[] lines = File.ReadAllLines(filePath);

            
            if (lines.Length < 2)
                return;

            txtUserName.Text = lines[0];
            txtPassword.Text = lines[1];
        }
        private void LoginScreen_Load(object sender, EventArgs e)
        {
            LoadUserNameAndPasswordFromFile();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
