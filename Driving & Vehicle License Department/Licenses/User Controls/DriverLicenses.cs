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

namespace Driving___Vehicle_License_Department.Licenses
{
    public partial class DriverLicenses : UserControl
    {

        ILicenseService _licenseService;
        public DriverLicenses( )
        {
            InitializeComponent();
            _licenseService = ServiceFactory.CreateLicenseServices() ;

        }

        public void LoadData (int personID )
        {
            dgvLocalLicensesHistory.DataSource = _licenseService.GetAllLocalLicensesByPersonID(personID);
            lblLocalLicensesRecords.Text = dgvLocalLicensesHistory.RowCount.ToString();
            dgvInternationalLicensesHistory.DataSource = _licenseService.GetAllInternationalLicensesByPersonID(personID);
            lblInternationalLicensesRecords.Text = dgvInternationalLicensesHistory.RowCount.ToString();

        }
    }
}
