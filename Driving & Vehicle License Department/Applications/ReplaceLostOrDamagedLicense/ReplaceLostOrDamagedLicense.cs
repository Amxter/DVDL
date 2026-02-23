using BusinessDVLD;
using DatabaseDVLD;
using Driving___Vehicle_License_Department.Licenses;
using Driving___Vehicle_License_Department.Licenses.Local_Licenses;
using PresentationDVLD;
using System;
using System.Windows.Forms;
using Unity;
using Unity.Resolution;

namespace Driving___Vehicle_License_Department.Applications.ReplaceLostOrDamagedLicense
{
    public partial class ReplaceLostOrDamagedLicense : GeneralForm
    {
        class LicenseInfo
        {
            public static double DamagedFees { get; set; }
            public static double LostFees { get; set; }

        }

        int _licenseID;
        int _newLicenseID;
        int _applicationTypeID;
        ILicenseService _licenseService;
        IApplicationServices _applicationServices;
        ILicenseClassServices _licenseClassServices;
        public ReplaceLostOrDamagedLicense(IApplicationTypesServices applicationTypesServices,
           ILicenseService licenseService,
           IApplicationServices applicationServices,
           ILicenseClassServices licenseClassServices)
        {
            InitializeComponent();
            _licenseService = licenseService;
            LicenseInfo.DamagedFees = applicationTypesServices.GetApplication(4).Fees;
            LicenseInfo.LostFees = applicationTypesServices.GetApplication(3).Fees;
            _applicationServices = applicationServices;
            _licenseClassServices = licenseClassServices;
        }
        private void _Initialize()
        {
            lblApplicationID.Text = "[???]";
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblRreplacedLicenseID.Text = "[???]";
            lblOldLicenseID.Text = "[???]";
            lblCreatedByUser.Text = CurrentUser.LoggedInUser.UserName;

            llShowLicenseHistory.Enabled = false;
            llShowLicenseInfo.Enabled = false;
            btnIssueReplacement.Enabled = false;
        }
        private void LoadDamagedLicenseInfo()
        {
            lblTitle.Text = $"Replacement for Damaged License";
            lblApplicationFees.Text = LicenseInfo.DamagedFees.ToString();
            _applicationTypeID = 4;
        }
        private void LoadLostLicenseInfo()
        {
            lblTitle.Text = $"Replacement for Lost License";
            lblApplicationFees.Text = LicenseInfo.LostFees.ToString();
            _applicationTypeID = 3;
        }
        private void LoadData()
        {
            lblOldLicenseID.Text = _licenseID.ToString();
            llShowLicenseHistory.Enabled = true;
            btnIssueReplacement.Enabled = true;
        }
        private void driverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            _licenseID = obj;

            if (_licenseID != -1)
            {
                if (driverLicenseInfoWithFilter1.LicenseDTO.IsActive)
                {
                    LoadData();
                    return;
                }
                else
                {
                    MessageBox.Show("The selected license is not active. Please select an active license.", "Invalid License", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _Initialize();
                    llShowLicenseHistory.Enabled = true;
                    return;
                }
            }
            _Initialize();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var frm = Program.Container.Resolve<ShowPersonLicenseHistory>(
            new ParameterOverride("PersonID", driverLicenseInfoWithFilter1.PersonDTO.PersonID));

            frm.ShowDialog();

        }
        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var frm = Program.Container.Resolve<ShowLicensesInfo>(
            new ParameterOverride("licenseID", _newLicenseID));
            frm.ShowDialog();

        }
        private void ReplaceLostOrDamagedLicense_Load(object sender, EventArgs e)
        {
            rbDamagedLicense.Checked = true;
            _Initialize(); ;
        }
        private void rbDamagedLicense_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDamagedLicense.Checked)
            {
                LoadDamagedLicenseInfo();
            }
        }
        private void rbLostLicense_CheckedChanged(object sender, EventArgs e)
        {
            if (rbLostLicense.Checked)
            {
                LoadLostLicenseInfo();
            }
        }
        private void btnIssueReplacement_Click(object sender, EventArgs e)
        {
            ApplicationDTO applicationDTO = new ApplicationDTO
            {
                ApplicationTypeID = _applicationTypeID,
                ApplicationDate = DateTime.Now,
                LastStatusDate = DateTime.Now,
                CreatedByUserID = CurrentUser.LoggedInUser.UserID,
                ApplicantPersonID = driverLicenseInfoWithFilter1.PersonDTO.PersonID,
                ApplicationStatus = ApplicationStatusIDs.CompletedStatus,
                PaidFees = Convert.ToDouble(lblApplicationFees.Text)
            };
            int ApplicationID = _applicationServices.Add(applicationDTO);

            int IssueReasonID;

            if (rbDamagedLicense.Checked)
                IssueReasonID = DatabaseDVLD.IssueReasonIDs.ReplacementForDamaged;
            else
                IssueReasonID = DatabaseDVLD.IssueReasonIDs.ReplacementForLost;

            LicenseClassDTO licenseClassDTO = _licenseClassServices.GetByID(driverLicenseInfoWithFilter1.LicenseDTO.LicenseClass);
            LicenseDTO licenseDTO = new LicenseDTO
            {
                ApplicationID = ApplicationID,
                DriverID = driverLicenseInfoWithFilter1.LicenseDTO.DriverID,
                ExpirationDate = DateTime.Now.AddYears(licenseClassDTO.DefaultValidityLength),
                IssueDate = DateTime.Now,
                IsActive = true,
                LicenseClass = driverLicenseInfoWithFilter1.LicenseDTO.LicenseClass,
                Notes = driverLicenseInfoWithFilter1.LicenseDTO.Notes,
                PaidFees = Convert.ToDouble(licenseClassDTO.ClassFees),
                IssueReason = IssueReasonID ,
                CreatedByUserID = CurrentUser.LoggedInUser.UserID
            };

            int LicenseID = _licenseService.Add(licenseDTO);
            _newLicenseID = LicenseID;
            if (LicenseID != -1)
            {
                if (_licenseService.DeactivateLicense(_licenseID))
                {

                    lblRreplacedLicenseID.Text = LicenseID.ToString();
                    lblApplicationID.Text = ApplicationID.ToString();
                    btnIssueReplacement.Enabled = false;
                    driverLicenseInfoWithFilter1.FilterEnabled = false;
                    llShowLicenseInfo.Enabled = true;
                    gbReplacementFor.Enabled = false;

                    MessageBox.Show("License renewed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

            }

            MessageBox.Show("An error occurred while deactivating the old license. Please contact support.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            this.Close();
        }
    }

}
