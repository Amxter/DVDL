using BusinessDVLD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Driving___Vehicle_License_Department.Licenses.International.User_Controls
{
    public partial class InfoInternationalLicense : UserControl
    {
        IInternationalLicenseService _internationalLicenseService;
        IApplicationServices _applicationServices;
        IPersonServices _personServices;
        public InfoInternationalLicense()
        {
            InitializeComponent();
            _applicationServices = ServiceFactory.CreateApplicationServices();
            _internationalLicenseService = ServiceFactory.CreateInternationalLicenseService();
            _personServices = ServiceFactory.CreatePersonServices();
        }

        public void LoadInfo( int InternationalLicenseID) 
        {
            InternationalLicenseDTO internationalLicenseDTO =  _internationalLicenseService.GetByID(InternationalLicenseID);
            lblIssueDate.Text = internationalLicenseDTO.IssueDate.ToShortDateString();
            lblExpirationDate.Text = internationalLicenseDTO.ExpirationDate.ToShortDateString();
            lblInternationalLicenseID.Text = internationalLicenseDTO.InternationalLicenseID.ToString();
            lblLocalLicenseID.Text = internationalLicenseDTO.IssuedUsingLocalLicenseID.ToString();
            lblIsActive.Text = internationalLicenseDTO.IsActive ? "Yes" : "No";
            lblDriverID.Text = internationalLicenseDTO.DriverID.ToString();
            lblApplicationID.Text = internationalLicenseDTO.ApplicationID.ToString();

            PersonDTO personDTO = _personServices.GetByID(_applicationServices.GetByApplicationID(internationalLicenseDTO.ApplicationID).ApplicantPersonID);

            lblFullName.Text = personDTO.FullName;
            lblNationalNo.Text = personDTO.NationalNo;
            lblGendor.Text = personDTO.Gendor == 1 ? " Male " : " Female ";
            lblDateOfBirth.Text = personDTO.DateOfBirth.ToShortDateString();

        }
    }
}
