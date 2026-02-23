using DrivingVehicleLicenseDepartment.People.User_Controls;
using System;

namespace DrivingVehicleLicenseDepartment.Licenses
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
