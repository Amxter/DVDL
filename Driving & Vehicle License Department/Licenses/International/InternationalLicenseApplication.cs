using BusinessDVLD;
using DatabaseDVLD;
using DrivingVehicleLicenseDepartment.Licenses.Local_Licenses;
using PresentationDVLD;
using System;
using System.Windows.Forms;
using Unity;
using Unity.Resolution;

namespace DrivingVehicleLicenseDepartment.Licenses.International
{
    public partial class InternationalLicenseApplication : GeneralForm
    {

       readonly IApplicationTypesServices _applicationTypesServices;
       readonly IInternationalLicenseService _internationalLicenseService;
       readonly IApplicationServices _applicationServices;
        int _internationalLicenseID  ;
        public InternationalLicenseApplication(IApplicationTypesServices applicationTypesServices,
            IInternationalLicenseService internationalLicenseService ,
            IApplicationServices applicationServices)
        {
            InitializeComponent();
            _applicationTypesServices = applicationTypesServices;
            _internationalLicenseService = internationalLicenseService;
            _applicationServices = applicationServices;
            _internationalLicenseID = -1;
        }
        int _licenseId  ;
        private void Initialize()
        {
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblIssueDate.Text = DateTime.Now.ToShortDateString();
            lblExpirationDate.Text = DateTime.Now.AddYears(1).ToShortDateString();
            lblCreatedByUser.Text = CurrentUser.LoggedInUser.UserName.ToString();
            lblLocalLicenseID.Text = "[???]";
            lblFees.Text = _applicationTypesServices.GetApplication(6).Fees.ToString("C2");
            btnIssueLicense.Enabled = false;
            llShowLicenseHistory.Enabled = false;

        }
        private void LoadApplicationInfo()
        {
            lblLocalLicenseID.Text = _licenseId.ToString();
            llShowLicenseHistory.Enabled = true ;
            btnIssueLicense.Enabled = true ;
        }
        private void driverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            _licenseId = obj;
            if (obj == -1)
            {
                Initialize();
            }
            else
            {
                LoadApplicationInfo();
                int inter = _internationalLicenseService.DoesHaveActiveInternationalLicense(obj) ;
                if (inter >  0)
                {
                    MessageBox.Show("This license already has an active international license.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _internationalLicenseID = inter;
                    llShowLicenseInfo.Enabled = true;
                    btnIssueLicense.Enabled = false ;
                    return;
                }
                _internationalLicenseID = -1;


            }
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
        private void InternationalLicenseApplication_Load(object sender, EventArgs e)
        {
            Initialize();
        }
        private void btnIssueLicense_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to issue this international license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    ApplicationTypesDTO applicationTypesDTO = _applicationTypesServices.GetApplication(ApplicationTypesIDs.ReplacementInternationalDrivingLicenseService);

                    ApplicationDTO applicationDTO = new ApplicationDTO
                    {
                        ApplicantPersonID = driverLicenseInfoWithFilter1.PersonDTO.PersonID,
                        ApplicationDate = DateTime.Now,
                        ApplicationTypeID = applicationTypesDTO.ID,
                        ApplicationStatus = ApplicationStatusIDs.CompletedStatus,
                        LastStatusDate = DateTime.Now,
                        PaidFees = applicationTypesDTO.Fees,
                        CreatedByUserID = CurrentUser.LoggedInUser.UserID
                    };

                    int ApplicationID = _applicationServices.Add(applicationDTO);

                    if (ApplicationID == -1)
                    {
                        MessageBox.Show("Failed to create the application. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    int InternationalLicenseID =  _internationalLicenseService.Add(this._licenseId , ApplicationID);
                    if (InternationalLicenseID != -1)
                    {

                        _internationalLicenseID = InternationalLicenseID;
                        driverLicenseInfoWithFilter1.FilterEnabled = false;
                        btnIssueLicense.Enabled = false;
                        llShowLicenseInfo.Enabled = true;
                        llShowLicenseHistory.Enabled = true;
                        lblInternationalLicenseID.Text = InternationalLicenseID.ToString() ;
                        lblApplicationID.Text = ApplicationID.ToString();
                        MessageBox.Show($"International license issued successfully. InternationalLicenseID = {InternationalLicenseID}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to issue the international license. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    } 
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while issuing the international license: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            var frm = Program.Container.Resolve<ShowInternationalLicense>(
           new ParameterOverride("InternationalLicenseID", _internationalLicenseID));

            frm.ShowDialog();
        }
    }
}
