using BusinessDVLD;
using DatabaseDVLD;
using DrivingVehicleLicenseDepartment.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;

using System.Windows.Forms;

namespace DrivingVehicleLicenseDepartment.Licenses.Local_Licenses
{
    public partial class LocalLicensesInfo : UserControl
    {

        readonly ILicenseService _licenseService;
        readonly ILicenseClassServices _licenseClassServices;
        readonly IApplicationServices _applicationServices;
        readonly IPersonServices _personServices;
        readonly IDetainedLicenseServices _detainedLicenseServices;

        LicenseDTO _licenseDTO;
        PersonDTO _personDTO;
        public LicenseDTO LicenseDTO { get { return _licenseDTO; } }
        public PersonDTO PersonDTO { get { return _personDTO; } }
        public LocalLicensesInfo()
        {
            InitializeComponent();
            _licenseService = ServiceFactory.CreateLicenseServices();
            _licenseClassServices = ServiceFactory.CreateLicenseClassServices();
            _applicationServices = ServiceFactory.CreateApplicationServices();
            _personServices = ServiceFactory.CreatePersonServices();
            _detainedLicenseServices = ServiceFactory.CreateDetainedLicenseServices();
        }
        private void LoadPersonImage(string imagePath)
        {

            pbPersonImage.Image = Properties.Resources.Male_512;

            if (string.IsNullOrWhiteSpace(imagePath))
                return;

            if (!File.Exists(imagePath))
                return;


            using (var fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
            {
                pbPersonImage.Image = Image.FromStream(fs);
            }
        }
        private void Initialize()
        {

            _licenseDTO = null;
            _personDTO = null;


            lblClass.Text = "[???]";
            lblFullName.Text = "[????]";
            lblLicenseID.Text = "[????]";
            lblNationalNo.Text = "[????]";
            lblGendor.Text = "[????]";
            lblIssueDate.Text = "[????]";
            lblIssueReason.Text = "[????]";
            lblNotes.Text = "[????]";
            lblIsActive.Text = "[????]";
            lblDateOfBirth.Text = "[????]";
            lblDriverID.Text = "[????]";
            lblExpirationDate.Text = "[????]";

            pbPersonImage.Image = Resources.Male_512;

        }
        public void LoadData(int licenseID)
        {
            if (licenseID == -1)
            {
                Initialize();
                return;
            }
            _licenseDTO = _licenseService.GetByID(licenseID);
            _personDTO = _personServices.GetByID(_applicationServices.GetByApplicationID(_licenseDTO.ApplicationID).ApplicantPersonID);


            lblClass.Text = _licenseClassServices.GetByID(_licenseDTO.LicenseClass).ClassName.ToString();
            lblFullName.Text = _personDTO.FullName.ToString();
            lblLicenseID.Text = _licenseDTO.LicenseID.ToString();
            lblNationalNo.Text = _personDTO.NationalNo.ToString();
            lblGendor.Text = _personDTO.Gendor == 1 ? " Male " : " Female ";
            lblIssueDate.Text = _licenseDTO.IssueDate.ToShortDateString();
            switch (_licenseDTO.IssueReason)
            {     case 1 :
                    lblIssueReason.Text = "First Time";
                    break;
                case 2 :
                    lblIssueReason.Text = "Renew";
                    break;
                case 3 :
                    lblIssueReason.Text = "Replacement For Damaged";
                    break;
                case 4 :
                    lblIssueReason.Text = "Replacement For Lost";
                    break;
                default:
                    lblIssueReason.Text = "Unknown";
                    break;
            }
            lblNotes.Text = _licenseDTO.Notes == null || _licenseDTO.Notes == "" ? "No Notes" : _licenseDTO.Notes;
            lblIsActive.Text = _licenseDTO.IsActive ? "Active" : "Inactive";
            lblDateOfBirth.Text = _personDTO.DateOfBirth.ToShortDateString();
            lblDriverID.Text = _licenseDTO.DriverID.ToString();
            lblExpirationDate.Text = _licenseDTO.ExpirationDate.ToShortDateString();
            lblIsDetained.Text = _detainedLicenseServices.IsLicenseDetained(_licenseDTO.LicenseID) ? "Yes" : "No";

            LoadPersonImage(_personDTO.ImagePath);

        }
    }
}
