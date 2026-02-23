using BusinessDVLD;
using DatabaseDVLD;
using DrivingVehicleLicenseDepartment.Licenses;
using DrivingVehicleLicenseDepartment.Licenses.Local_Licenses;
using PresentationDVLD;
using System;
using System.Windows.Forms;
using Unity;
using Unity.Resolution;

namespace DrivingVehicleLicenseDepartment.Applications.Renew_Local_Driving_license
{
    public partial class RenewLocalDrivingLicenseApplication : GeneralForm
    {

        int _licenseID = -1;
        int _newLicenseID = -1;
        readonly  ILicenseService _licenseService;
        readonly  ILicenseClassServices _licenseClassServices;
        readonly  IApplicationTypesServices _applicationTypesServices;
        readonly IApplicationServices _ApplicationServices;
        public RenewLocalDrivingLicenseApplication(ILicenseService licenseService,
            ILicenseClassServices licenseClassServices,
            IApplicationTypesServices applicationTypesServices,
            IApplicationServices applicationServices)
        {
            InitializeComponent();
            _licenseService = licenseService;
            _licenseClassServices = licenseClassServices;
            _applicationTypesServices = applicationTypesServices;
            _ApplicationServices = applicationServices;
            Initialize();
        }
        private void Initialize()
        {
            lblApplicationID.Text = "[???]";
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblIssueDate.Text = DateTime.Now.ToShortDateString();
            lblCreatedByUser.Text = CurrentUser.LoggedInUser.UserName;
            lblApplicationFees.Text = _applicationTypesServices.GetApplication(2).Fees.ToString();
            lblLicenseFees.Text = "[$$$]";
            lblRenewedLicenseID.Text = "[???]";
            lblOldLicenseID.Text = "[???]";
            lblExpirationDate.Text = "[??/??/????]";
            lblTotalFees.Text = "[$$$]";
            txtNotes.Clear();
            llShowLicenseHistory.Enabled = false;
            llShowLicenseInfo.Enabled = false;
            btnRenewLicense.Enabled = false;

        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void LoadLicenseInfo(int licenseID)
        {

            LicenseDTO licenseDTO = driverLicenseInfoWithFilter1.LicenseDTO;
            lblOldLicenseID.Text = licenseID.ToString();
            lblExpirationDate.Text = licenseDTO.ExpirationDate.ToShortDateString();
            ApplicationTypesDTO applicationTypesDTO = _applicationTypesServices.GetApplication(_ApplicationServices.GetByApplicationID(licenseDTO.ApplicationID).ApplicationTypeID);
            lblLicenseFees.Text = applicationTypesDTO.Fees.ToString();
            lblTotalFees.Text = (Convert.ToDouble(lblApplicationFees.Text) + Convert.ToDouble(lblLicenseFees.Text)).ToString();
            llShowLicenseHistory.Enabled = true;
            btnRenewLicense.Enabled = true;

        }
        private void driverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            _licenseID = obj;

            if (_licenseID != -1)
            {
                if (!driverLicenseInfoWithFilter1.LicenseDTO.IsActive)
                {
                    MessageBox.Show("The selected license is not eligible for renewal as it is not active.", "Ineligible for Renewal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Initialize();
                    llShowLicenseHistory.Enabled = true;
                    return;
                }

                if (!_licenseService.IsExpirationDateLicense(_licenseID))
                {


                    MessageBox.Show("The selected license is not eligible for renewal as it has not yet reached its expiration date.", "Ineligible for Renewal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Initialize();
                    llShowLicenseHistory.Enabled = true;
                    return;
                }
                LoadLicenseInfo(_licenseID);

            }
            else
            {
                Initialize();
            }
        }
        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var frm = Program.Container.Resolve<ShowPersonLicenseHistory>(
            new ParameterOverride("PersonID", driverLicenseInfoWithFilter1.PersonDTO.PersonID));

            frm.ShowDialog();
        }
        private void btnRenewLicense_Click(object sender, EventArgs e)
        {
            ApplicationDTO applicationDTO = new ApplicationDTO
            {
                ApplicationTypeID = 2,
                ApplicationDate = DateTime.Now,
                LastStatusDate = DateTime.Now,
                CreatedByUserID = CurrentUser.LoggedInUser.UserID,
                ApplicantPersonID = driverLicenseInfoWithFilter1.PersonDTO.PersonID,
                ApplicationStatus = ApplicationStatusIDs.CompletedStatus,
                PaidFees = Convert.ToDouble(lblApplicationFees.Text)
            };
            int ApplicationID = _ApplicationServices.Add(applicationDTO);

            LicenseClassDTO licenseClassDTO = _licenseClassServices.GetByID(driverLicenseInfoWithFilter1.LicenseDTO.LicenseClass);
            LicenseDTO licenseDTO = new LicenseDTO
            {
                ApplicationID = ApplicationID,
                DriverID = driverLicenseInfoWithFilter1.LicenseDTO.DriverID,
                ExpirationDate = DateTime.Now.AddYears(licenseClassDTO.DefaultValidityLength),
                IssueDate = DateTime.Now,
                IsActive = true,
                LicenseClass = driverLicenseInfoWithFilter1.LicenseDTO.LicenseClass,
                Notes = txtNotes.Text,
                PaidFees = Convert.ToDouble(lblLicenseFees.Text),
                IssueReason = IssueReasonIDs.Renew,
                CreatedByUserID = CurrentUser.LoggedInUser.UserID
            };

            int LicenseID = _licenseService.Add(licenseDTO);
            _newLicenseID = LicenseID;
            if (LicenseID != -1)
            {
                if (_licenseService.DeactivateLicense(_licenseID))
                {
                    
                    lblRenewedLicenseID.Text = LicenseID.ToString();
                    lblApplicationID.Text = ApplicationID.ToString();
                    btnRenewLicense.Enabled = false;
                    driverLicenseInfoWithFilter1.FilterEnabled = false;
                    llShowLicenseInfo.Enabled = true;
                    txtNotes.Enabled = false;
                    MessageBox.Show("License renewed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return; 
                }

            }

            MessageBox.Show("An error occurred while deactivating the old license. Please contact support.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            this.Close();
        }
        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            var frm = Program.Container.Resolve<ShowLicensesInfo>(
            new ParameterOverride("licenseID", _newLicenseID ));
            frm.ShowDialog();
            
        }
    }
}
