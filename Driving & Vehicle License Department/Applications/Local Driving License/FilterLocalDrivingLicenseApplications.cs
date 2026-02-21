using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Driving___Vehicle_License_Department.Applications.Local_Driving_License
{
    public partial class FilterLocalDrivingLicenseApplications : UserControl
    {
        DataGridView _dataGridView;
        DataTable _dataTable;
        string _selectedColumnName ;
        public FilterLocalDrivingLicenseApplications()
        {

            InitializeComponent();
            _selectedColumnName = ""; 
        }

        public void SendPreparate(DataGridView dataGridView, DataTable dataTable)
        {
            _dataGridView = dataGridView;
            _dataTable = dataTable;
        }
        private void FilterLocalDrivingLicenseApplications_Load(object sender, EventArgs e)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            if (cbFilterBy.Items.Count > 0)
                cbFilterBy.SelectedIndex = 0;
        }
        private void _loadData()
        {
            if (_dataGridView == null || _dataTable == null) return;
            _dataGridView.DataSource = _dataTable.DefaultView;
        }
        private void FilterColumn (string columnName, string filterValue)
        {
            if (string.IsNullOrEmpty(filterValue))
            {
                _loadData();
            }
            else
            {
                    
                DataView dv = new DataView(_dataTable);
                string filterExpression;
                if (_selectedColumnName == "LocalDrivingLicenseApplicationID")
                {

                    if (int.TryParse(filterValue, out int id))
                        filterExpression = $"[{columnName}] = {id}";
                    else
                        filterExpression = "1=0";  
                    
                }
                else
                {
                       filterExpression = string.Format("[{0}] LIKE '%{1}%'", columnName, filterValue.Replace("'", "''"));
                }


                dv.RowFilter = filterExpression;
                _dataGridView.DataSource = dv;
            }
        }
        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {

            txtFilterValue.Clear();
            if (cbFilterBy.Text == "None")
            {
                _loadData();
                _selectedColumnName = "";
                txtFilterValue.Visible = false;
                return; 
            }
            else if (cbFilterBy.Text == "L.D.L.AppID")
            {
                _selectedColumnName = "LocalDrivingLicenseApplicationID";
            }
            else if (cbFilterBy.Text == "National No.")
            {
                _selectedColumnName = "NationalNo";
            }
            else if (cbFilterBy.Text == "Full Name")
            {
                _selectedColumnName = "Full Name";
            }
            else if (cbFilterBy.Text == "Status")
            {
                _selectedColumnName = "ApplicationStatus"; 
            }

            txtFilterValue.Visible = true ;
        }
        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            if (_selectedColumnName != "")
            {
                FilterColumn(_selectedColumnName, txtFilterValue.Text);
            }

        }
        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (_selectedColumnName == "LocalDrivingLicenseApplicationID")
            {
            
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }
    }

}
