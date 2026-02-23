using BusinessDVLD;
using DatabaseDVLD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrivingVehicleLicenseDepartment.Licenses.Local_Licenses
{
    public partial class IssueDriverLicenseFirstTime : GeneralForm
    {

       readonly ILicenseService _licenseService;
       readonly IDriverServices _driverServices;
       readonly ILicenseClassServices _licenseClassServices;
       readonly IApplicationServices _applicationServices;
        public IssueDriverLicenseFirstTime(int lDLApplicationID , 
            ILicenseService  licenseService ,
            ILicenseClassServices licenseClassServices ,
            IDriverServices driverServices ,
            IApplicationServices applicationServices )
        {
            InitializeComponent();
            localDrivingLicenseApplicationInfo1.LoadData(lDLApplicationID);
             _licenseService = licenseService;
            _licenseClassServices = licenseClassServices;
            _driverServices = driverServices;
            _applicationServices = applicationServices;

        }

        private void btnIssueLicense_Click(object sender, EventArgs e)
        {
            DriverDTO driverDTO;
            int driverID = _driverServices.IsExistByPersonID(localDrivingLicenseApplicationInfo1.LDLApplicationDTO.Application.ApplicantPersonID) ;
           if (driverID <= 0 ) 
            {
                driverDTO = new DriverDTO
                {
                    CreatedByUserID = CurrentUser.LoggedInUser.UserID,
                    CreatedDate = DateTime.Now,
                    PersonID = localDrivingLicenseApplicationInfo1.LDLApplicationDTO.Application.ApplicantPersonID
                };

                driverID = _driverServices.Add(driverDTO);

                if (driverID == -1)
                {
                    MessageBox.Show("Error while creating driver record");
                    this.Close();
                    return;

                }
            }

           LicenseClassDTO licenseClassDTO = _licenseClassServices.GetByID(localDrivingLicenseApplicationInfo1.LDLApplicationDTO.LicenseClassID);
           int licenseID = _licenseService.Add(new LicenseDTO
            {
                ApplicationID = localDrivingLicenseApplicationInfo1.LDLApplicationDTO.Application.ApplicationID ,
                DriverID = driverID ,
                LicenseClass = localDrivingLicenseApplicationInfo1.LDLApplicationDTO.LicenseClassID ,
                ExpirationDate = DateTime.Now.AddYears(licenseClassDTO.DefaultValidityLength) ,
                Notes = txtNotes.Text ,
                PaidFees = Convert.ToDouble(  licenseClassDTO.ClassFees ),
                IsActive = true,
                IssueReason = IssueReasonIDs.FirstTime ,  
                CreatedByUserID = CurrentUser.LoggedInUser.UserID ,
                IssueDate = DateTime.Now

            });

            if (licenseID == -1)
            {
                MessageBox.Show("Error while issuing license");
 
            }
            else
            {
                if (_applicationServices.UpdateApplicationStatus(localDrivingLicenseApplicationInfo1.LDLApplicationDTO.Application.ApplicationID, 3 , DateTime.Now )) 
                MessageBox.Show($"License issued successfully \n LicenseID = {licenseID}");
                else
                    MessageBox.Show("License issued but failed to update application status");
            }
            this.Close();
            return;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }
    }
}
