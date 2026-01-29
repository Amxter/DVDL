using BusinessDVLD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Driving___Vehicle_License_Department.Applications.Local_Driving_License
{
    public partial class ListLocalDrivingLicenseApplication : GeneralForm
    {

        ILDLApplicationServices _ldlApplicationServices;
        DataTable _dataTable;
        public ListLocalDrivingLicenseApplication()
        {
            InitializeComponent();
            _ldlApplicationServices = ServiceFactory.CreateLDLApplicationServices();
            _loadData();
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


            AddUpdateLocalDrivingLicenseApplication addUpdateLocalDrivingLicenseApplication = new AddUpdateLocalDrivingLicenseApplication(-1);
            addUpdateLocalDrivingLicenseApplication.ShowDialog();
            _loadData();
        }
        private void ScheduleTestsMenue_Click(object sender, EventArgs e)
        {

        }
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int SelectedColumn = GetSelectedLocalDrivingLicenseApplicationID();

            if (SelectedColumn == -1)
            {
                MessageBox.Show("Please select a record to edit.");
                return;
            }

            AddUpdateLocalDrivingLicenseApplication addUpdateLocalDrivingLicenseApplication = new AddUpdateLocalDrivingLicenseApplication(SelectedColumn);
            addUpdateLocalDrivingLicenseApplication.ShowDialog();
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
    }
}
