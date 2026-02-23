using BusinessDVLD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace DrivingVehicleLicenseDepartment.Drivers
{
    public partial class ListDrivers : GeneralForm
    {

       readonly IDriverServices _driverServices;
        enum FilterBy
        {
            None,
            DriverID,
            PersonID,
            NationalNo,
            FullName,
        }
        FilterBy _filterBy = FilterBy.None;

        DataTable _driversTable  ;
        public void LoadData()
        {
            _driversTable = _driverServices.GetAll(); 
            dgvDrivers.DataSource = _driversTable.DefaultView ;
            dgvDrivers.Columns[3].Width = 350;
            lblRecordsCount.Text = dgvDrivers.Rows.Count.ToString();
        }
        public ListDrivers(IDriverServices driverServices)
        {
            InitializeComponent();
            _driverServices = driverServices;
            LoadData();
        }
        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cbFilterBy.Text == "None")
            {
                _filterBy = FilterBy.None;
            }
            else if (cbFilterBy.Text == "Driver ID")
            {
                _filterBy = FilterBy.DriverID;
            }
            else if (cbFilterBy.Text == "Person ID")
            {
                _filterBy = FilterBy.PersonID;
            }
            else if (cbFilterBy.Text == "National No.")
            {
                _filterBy = FilterBy.NationalNo;
            }
            else if (cbFilterBy.Text == "Full Name")
            {
                _filterBy = FilterBy.FullName;

            }


            if (_filterBy == FilterBy.None)
            {
                txtFilterValue.Visible = false;
                LoadData();
            }
            else
            {
                txtFilterValue.Visible = true;
                txtFilterValue.Focus();
            }

            txtFilterValue.Clear();
        }
        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (FilterBy.PersonID == _filterBy || FilterBy.DriverID == _filterBy)
            {

                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }
        private void FilterByID(int Value)
        {
 

            _driversTable.DefaultView.RowFilter = $"[{_filterBy}] = {Value}";
        }
        private string GetColumnName(FilterBy filterBy)
        {
            switch (filterBy)
            {
                case FilterBy.FullName: return "Full Name";
                case FilterBy.NationalNo: return "NationalNo";
                case FilterBy.PersonID: return "PersonID";
                default: return "Full Name"; 
            }
        }
        private void FilterByText(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                LoadData();
                return;
            }

            string columnName = GetColumnName(_filterBy);
            string safeValue = value.Replace("'", "''");

            _driversTable.DefaultView.RowFilter =
                $"[{columnName}] LIKE '%{safeValue}%'";
        }
        private void Filter(string Value )
        {
            if (_filterBy == FilterBy.None)
            {
                LoadData();
                return;
            }

            if (_filterBy == FilterBy.DriverID || _filterBy == FilterBy.PersonID)
            {

                int intValue;
                if (int.TryParse(Value, out intValue))
                {
                    FilterByID(intValue);
                }
                else
                {
                    LoadData();
                }
            }
            else if (_filterBy == FilterBy.NationalNo || _filterBy == FilterBy.FullName)
            {
                FilterByText(Value);
            }
            }
        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            Filter(txtFilterValue.Text.Trim());
        }
    }
}
