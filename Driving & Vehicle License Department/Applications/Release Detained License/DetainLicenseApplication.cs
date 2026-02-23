using BusinessDVLD;
using DrivingVehicleLicenseDepartment.Licenses;
using DrivingVehicleLicenseDepartment.Licenses.Local_Licenses;
using PresentationDVLD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Unity;
using Unity.Resolution;

namespace DrivingVehicleLicenseDepartment.Applications.Release_Detained_License
{
    public partial class DetainLicenseApplication : GeneralForm
    {

        int _licenseId;

       readonly IDetainedLicenseServices _detainedLicenseServices;
    
        public DetainLicenseApplication(IDetainedLicenseServices detainedLicenseServices )
        {
            InitializeComponent();
            Initialize();
            _detainedLicenseServices = detainedLicenseServices;
           
        }

        private void Initialize()
        {

            lblLicenseID.Text = "[???]";
            lblDetainID.Text = "[???]";
            lblDetainDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lblCreatedByUser.Text = CurrentUser.LoggedInUser.UserName;
            llShowLicenseHistory.Enabled = false;
            llShowLicenseInfo.Enabled = false;
            btnDetain.Enabled = false;

        }
        private void LoadLicenseInfo()
        {
            lblLicenseID.Text = _licenseId.ToString();
            llShowLicenseHistory.Enabled = true;
            btnDetain.Enabled = true;
            // llShowLicenseInfo.Enabled = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void driverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            _licenseId = obj;
            if (_licenseId != -1)
            {
                if (!driverLicenseInfoWithFilter1.LicenseDTO.IsActive)
                {
                    MessageBox.Show("This license is not active.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Initialize();
                    llShowLicenseHistory.Enabled = true;
                    return;

                }

                if (_detainedLicenseServices.IsLicenseDetained(_licenseId))
                {
                    MessageBox.Show("This license is already detained.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Initialize();
                    llShowLicenseHistory.Enabled = true;
                    return;
                }

                LoadLicenseInfo();
            }
            else
            {
                Initialize();
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

        private void btnDetain_Click(object sender, EventArgs e)
        {
            DetainedLicenseDTO detainedLicenseDTO = new DetainedLicenseDTO
            {
                LicenseID = _licenseId,
                DetainDate = DateTime.Now,
                CreatedByUserID = CurrentUser.LoggedInUser.UserID,
                DetainID = driverLicenseInfoWithFilter1.LicenseDTO.DriverID,
                FineFees = Convert.ToDouble(txtFineFees.Text),
                ReleaseApplicationID = null,
                ReleasedByUserID = null,
                IsReleased = false

            };

            try
            {
                int detainedID = _detainedLicenseServices.Add(detainedLicenseDTO);
                if (detainedID != 0)
                {
                    lblDetainID.Text = detainedID.ToString();
                    txtFineFees.Enabled = false;
                    btnDetain.Enabled = false;
                    llShowLicenseInfo.Enabled = true;
                    driverLicenseInfoWithFilter1.FilterEnabled = false; 
                    MessageBox.Show("License detained successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return; 
                }
                else
                {
                    MessageBox.Show("Failed to detain the license.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while detaining the license: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }
        private void txtFineFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFineFees.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFineFees, "Fees cannot be empty!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtFineFees, null);

            }
            ;


            if (!clsValidations.IsNumber(txtFineFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFineFees, "Invalid Number.");
            }
            else
            {
                errorProvider1.SetError(txtFineFees, null);
            }
            ;
        }
    }
}
