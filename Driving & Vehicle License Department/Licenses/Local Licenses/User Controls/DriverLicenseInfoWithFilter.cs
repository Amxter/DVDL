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

namespace Driving___Vehicle_License_Department.Licenses.Local_Licenses
{
    public partial class DriverLicenseInfoWithFilter : UserControl
    {

        ILicenseService _licenseService;

        public event Action<int> OnLicenseSelected;
        protected virtual void LicenseSelected(int licenseID)
        {
            Action<int> handler = OnLicenseSelected;

            if (handler != null)
            {
                handler(licenseID); // Fire the event and pass the parameter
            }
        }
        public LicenseDTO LicenseDTO { get { return localLicensesInfo1.LicenseDTO; } }
        public PersonDTO PersonDTO { get { return localLicensesInfo1.PersonDTO; } }
        public bool FilterEnabled { get { return gbFilters.Enabled; } set { gbFilters.Enabled = value; } }
        public DriverLicenseInfoWithFilter()
        {
            InitializeComponent();
            _licenseService = ServiceFactory.CreateLicenseServices();
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

            if ( _licenseService.IsExists(Convert.ToInt32(txtLicenseID.Text)))
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
                MessageBox.Show("License ID not found");

            }
             
        }
    }
}
