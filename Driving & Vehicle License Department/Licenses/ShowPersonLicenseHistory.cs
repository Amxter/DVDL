using Driving___Vehicle_License_Department.People.User_Controls;
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
    public partial class ShowPersonLicenseHistory : GeneralForm
    {
        public ShowPersonLicenseHistory(int PersonID)
        {
            InitializeComponent();
            filterPerson1.FilterPersonByID(PersonID);
            filterPerson1.FilterEnabled = false; 
            driverLicenses1.LoadData(PersonID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
