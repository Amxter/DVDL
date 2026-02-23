using System;


namespace DrivingVehicleLicenseDepartment.Licenses.Local_Licenses
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
