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
    public partial class ShowLicensesInfo : GeneralForm
    {
        public ShowLicensesInfo(int licenseID)
        {
            InitializeComponent();
            localLicensesInfo1.LoadData(licenseID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
