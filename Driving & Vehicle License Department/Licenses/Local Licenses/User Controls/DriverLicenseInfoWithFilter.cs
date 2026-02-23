using BusinessDVLD;
using System;
using System.Windows.Forms;

namespace DrivingVehicleLicenseDepartment.Licenses.Local_Licenses
{
    public partial class DriverLicenseInfoWithFilter : UserControl
    {

       readonly ILicenseService _licenseService;

        public event Action<int> OnLicenseSelected;
        protected virtual void LicenseSelected(int licenseID)
        {
            Action<int> handler = OnLicenseSelected;

            if (handler != null)
            {
                handler(licenseID); 
            }
        }
        public LicenseDTO LicenseDTO { get { return localLicensesInfo1.LicenseDTO; } }
        public PersonDTO PersonDTO { get { return localLicensesInfo1.PersonDTO; } }
        public bool FilterEnabled { get { return gbFilters.Enabled; } set { gbFilters.Enabled = value; } }
        public DriverLicenseInfoWithFilter()
        {
            InitializeComponent();
            _licenseService = ServiceFactory.CreateLicenseServices();
            txtLicenseID.Focus();
        }
        private void txtLicenseID_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }

        }
        private void btnFind_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLicenseID.Text))
            {
                MessageBox.Show("Please enter a license ID");
                return;
            }

            if (_licenseService.IsExists(Convert.ToInt32(txtLicenseID.Text)))
            {
                localLicensesInfo1.LoadData(Convert.ToInt32(txtLicenseID.Text));

                if (OnLicenseSelected != null && LicenseDTO.LicenseID != -1)
                    OnLicenseSelected(LicenseDTO.LicenseID);
                return;
            }
            else
            {
                localLicensesInfo1.LoadData(-1);
                if (OnLicenseSelected != null)
                    OnLicenseSelected(-1);
                MessageBox.Show($"License ID {Convert.ToInt32(txtLicenseID.Text)} not found");

            }

        }
        public void SelectLicense(int licenseID)
        {
            if (licenseID != -1)
            {
                txtLicenseID.Text = licenseID.ToString();
                btnFind_Click(null, null);
            }
            else
            {
 
            }
        }

        private void txtLicenseID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnFind.PerformClick();
        }
    }
}
