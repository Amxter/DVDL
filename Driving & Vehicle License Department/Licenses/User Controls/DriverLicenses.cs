using BusinessDVLD;
using System.Windows.Forms;

namespace DrivingVehicleLicenseDepartment.Licenses
{
    public partial class DriverLicenses : UserControl
    {

       readonly ILicenseService _licenseService;
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
