using BusinessDVLD;
using DatabaseDVLD;
using Driving___Vehicle_License_Department.Users;
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
using Unity;
using Unity.Resolution;

namespace Driving___Vehicle_License_Department
{
    public partial class ListUsers : GeneralForm
    {
        IUserServices _userServices;
        DataTable dt; 
        private void _loadUsers()
        {
            dt = _userServices.GetAll(); 
            dgvUsers.DataSource = dt.DefaultView ;
            dgvUsers.Columns[0].Width = 100;
            dgvUsers.Columns[1].Width = 100;
            dgvUsers.Columns[2].Width = 300;
            dgvUsers.Columns[3].Width = 100;
            dgvUsers.Columns[4].Width = 100;
        }
        public ListUsers(IUserServices userServices)
        {
            InitializeComponent();

            _userServices = userServices;
            _loadUsers();
         
        }
        private void FilterUsersBy(string filterBy , string value )
        {

          

            if (filterBy== "UserID" || filterBy == "PersonID" )
            {

                if (int.TryParse(value, out int intValue))
                    dt.DefaultView.RowFilter = string.Format("{0} = {1}", filterBy, intValue);
                else
                    dt.DefaultView.RowFilter = null ; 
            } 
            else if (filterBy == "IsActive")
            {
                if (bool.TryParse(value, out bool boolValue))
                    dt.DefaultView.RowFilter = string.Format("{0} = {1}", filterBy, boolValue ? "true" : "false");
                else
                    dt.DefaultView.RowFilter = null; 
            }
            else
                dt.DefaultView.RowFilter = string.Format("{0} LIKE '%{1}%'", filterBy, value );
 
        }
        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            string isActiveValue = cbIsActive.Text == "Yes" ? "true" : "false";
            if ("All"!= cbIsActive.Text)
            FilterUsersBy("IsActive", isActiveValue);
            else
                dt.DefaultView.RowFilter = null; 
        }
        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.Text == "None")
            {
                cbIsActive.Visible = false;
                txtFilterValue.Visible = false;
             
                _loadUsers();

            }
            else if (cbFilterBy.Text == "IsActive")
            {
                cbIsActive.Visible = true;
                txtFilterValue.Visible = false;
                cbIsActive.SelectedIndex = 0 ;
            }
            else
            {
                cbIsActive.Visible = false;
                txtFilterValue.Visible = true;

            }

            txtFilterValue.Clear();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            FilterUsersBy(cbFilterBy.Text.Trim() , txtFilterValue.Text );
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            var frm = Program.Container.Resolve<AddUpdateUser>(
    new ParameterOverride("userId", -1 ));

            frm.ShowDialog();
 
            _loadUsers();
        }

        private void deleteUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int selectedUserID = _getSelectedUserID();


            if (selectedUserID == CurrentUser.LoggedInUser.UserID)
            {
                MessageBox.Show("You don't delete current user.");
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete User with ID " + selectedUserID + "?", "Delete User", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                bool isDeleted = _userServices.Delete(selectedUserID);

                if (isDeleted)
                {
                    MessageBox.Show("User deleted successfully.");
                    _loadUsers();
                }
                else
                {
                    MessageBox.Show("Failed to delete user.");
                }
            }


        }
        private int _getSelectedUserID()
        {
            return (int)dgvUsers.SelectedRows[0].Cells["UserID"].Value;
        }
        private void addUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = Program.Container.Resolve<AddUpdateUser>(
    new ParameterOverride("userId", -1));

            frm.ShowDialog();
            _loadUsers(); 
        }

        private void editUserToolStripMenuItem_Click(object sender, EventArgs e)
        {

            var frm = Program.Container.Resolve<AddUpdateUser>(
new ParameterOverride("userId", _getSelectedUserID()));

            frm.ShowDialog();

            _loadUsers();
        }

        private void showInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserInfo userInfo = new UserInfo(_getSelectedUserID());
            //userInfo.MdiParent = this.MdiParent;
            userInfo.ShowDialog();

        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {

            var frm = Program.Container.Resolve<ChangePassword>(
            new ParameterOverride("user", _getSelectedUserID() ));

            frm.ShowDialog();
 

        }

        private void callPhoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature is not implemented yet.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void sendeEmailToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature is not implemented yet.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy .Text == "PersonID" || cbFilterBy.Text == "UserID")
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }
    }
}
