using BusinessDVLD;
using DatabaseDVLD;
using Driving___Vehicle_License_Department.Licenses;
using Driving___Vehicle_License_Department.Licenses.Local_Licenses;
using PresentationDVLD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;
using Unity.Resolution;

namespace Driving___Vehicle_License_Department.Applications.Rlease_Detained_License
{
    public partial class ReleaseDetainedLicenseApplication : GeneralForm
    {

        double ApplicationFees;
        int _licenseId;
        DetainedLicenseDTO _detainedLicenseDTO;
        IApplicationTypesServices _applicationTypesServices;
        IApplicationServices _applicationServices;
        IDetainedLicenseServices _detainedLicenseServices;
        ILicenseService _licenseService;
        public ReleaseDetainedLicenseApplication(IApplicationTypesServices applicationTypesServices,
            IDetainedLicenseServices detainedLicenseServices,
            ILicenseService licenseService,
            IApplicationServices applicationServices,
            int LicenseID = -1)
        {
            InitializeComponent();
            _applicationTypesServices = applicationTypesServices;
            _detainedLicenseServices = detainedLicenseServices;
            _licenseService = licenseService;
            ApplicationFees = _applicationTypesServices.GetApplication(5).Fees;
            _applicationServices = applicationServices;
            _Initialize();

            if (LicenseID != -1)
            {
                driverLicenseInfoWithFilter1.SelectLicense(LicenseID);
            }

        }
        private void _Initialize()
        {
            lblDetainID.Text = "[???]";
            lblLicenseID.Text = "[???]";
            lblDetainDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lblTotalFees.Text = "[$$$$]";
            lblApplicationFees.Text = ApplicationFees.ToString();
            lblFineFees.Text = "[$$$$]";
            lblApplicationID.Text = "[????]";
            lblCreatedByUser.Text = CurrentUser.LoggedInUser.UserName;
            llShowLicenseHistory.Enabled = false;
            llShowLicenseInfo.Enabled = false;
            btnRelease.Enabled = false;

        }
        private void _loadData()
        {
            _detainedLicenseDTO = _detainedLicenseServices.GetByLicenseID(driverLicenseInfoWithFilter1.LicenseDTO.LicenseID);
            lblLicenseID.Text   = driverLicenseInfoWithFilter1.LicenseDTO.LicenseID.ToString();
            lblDetainID.Text    = _detainedLicenseDTO.DetainID.ToString();
            lblFineFees.Text    = _detainedLicenseDTO.FineFees.ToString();
            lblTotalFees.Text   = (_detainedLicenseDTO.FineFees + ApplicationFees).ToString();
            llShowLicenseHistory.Enabled = true;
            btnRelease.Enabled = true;
        }
        private void driverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            _licenseId = obj;


            if (_licenseId != -1)
            {
                if (!driverLicenseInfoWithFilter1.LicenseDTO.IsActive)
                {
                    _Initialize();
                    llShowLicenseHistory.Enabled = true;
                    MessageBox.Show("The selected license is not active.");
                    return;
                }
                if (_detainedLicenseServices.IsLicenseDetained(_licenseId))
                {
                    _loadData();
                }
                else
                {
                    _Initialize();
                    llShowLicenseHistory.Enabled = true;
                    MessageBox.Show("The selected license is not detained.");
                    return;
                }

            }
            else
            {
                _Initialize();
            }
        }
        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var frm = Program.Container.Resolve<ShowLicensesInfo>(
           new ParameterOverride("licenseID", _licenseId));
            frm.ShowDialog();
        }
        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            var frm = Program.Container.Resolve<ShowPersonLicenseHistory>(
            new ParameterOverride("PersonID", driverLicenseInfoWithFilter1.PersonDTO.PersonID));

            frm.ShowDialog();
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {



            if (MessageBox.Show("Are you sure you want to release the license?", "Confirm Release", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {


                ApplicationDTO applicationDTO = new ApplicationDTO
                {
                    ApplicationTypeID = 5,
                    CreatedByUserID = CurrentUser.LoggedInUser.UserID,
                    ApplicantPersonID = driverLicenseInfoWithFilter1.PersonDTO.PersonID,
                    PaidFees = ApplicationFees,
                    ApplicationDate = DateTime.Now,
                    ApplicationStatus = ApplicationStatus.CompletedStatus,
                    LastStatusDate = DateTime.Now,


                };

                int applicationID = _applicationServices.Add(applicationDTO);
                if (_detainedLicenseServices.ReleaseLicense(_detainedLicenseDTO.DetainID, CurrentUser.LoggedInUser.UserID, DateTime.Now, applicationID))
                {

                    lblApplicationID.Text = applicationID.ToString();
                    btnRelease.Enabled = false;
                    driverLicenseInfoWithFilter1.FilterEnabled = false;
                    MessageBox.Show("License released successfully.");

                }
                else
                {
                    MessageBox.Show("An error occurred while releasing the license. Please try again.");
                    this.Close();
                }
            }

        }
    }
}
