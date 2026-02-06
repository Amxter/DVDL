using BusinessDVLD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Driving___Vehicle_License_Department.Licenses.Local_Licenses
{
    public partial class LocalLicensesInfo : UserControl
    {

        ILicenseService _licenseService;
        ILicenseClassServices _licenseClassServices;
        IApplicationServices _applicationServices;
        IPersonServices _personServices;
        public LocalLicensesInfo()
        {
            InitializeComponent();
            _licenseService = ServiceFactory.CreateLicenseServices();
            _licenseClassServices = ServiceFactory.CreateLicenseClassServices();
            _applicationServices = ServiceFactory.CreateApplicationServices();
            _personServices = ServiceFactory.CreatePersonServices();
        }




        private void LoadPersonImage(string imagePath)
        {
            // صورة افتراضية
            pbPersonImage.Image = Properties.Resources.Male_512 ; // أو خليها null

            if (string.IsNullOrWhiteSpace(imagePath))
                return;

            if (!File.Exists(imagePath))
                return;

            // مهم: حتى ما يضل ماسك الملف (file lock)
            using (var fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
            {
                pbPersonImage.Image = Image.FromStream(fs);
            }
        }

        public void LoadData(int licenseID)
        {
            LicenseDTO licenseDTO = _licenseService.GetByID(licenseID);
            PersonDTO personDTO = _personServices.GetByID(_applicationServices.GetByApplicationID(licenseDTO.ApplicationID).ApplicantPersonID);


            lblClass.Text = _licenseClassServices.GetByID(licenseDTO.LicenseClass).ClassName.ToString();
            lblFullName.Text = personDTO.FullName.ToString();
            lblLicenseID.Text = licenseDTO.LicenseID.ToString();
            lblNationalNo.Text = personDTO.NationalNo.ToString();
            lblGendor.Text = personDTO.Gendor == 1 ? " Male " : " Female ";
            lblIssueDate.Text = licenseDTO.IssueDate.ToShortDateString();
            lblIssueReason.Text = licenseDTO.IssueReason.ToString();
            lblNotes.Text = licenseDTO.Notes;
            lblIsActive.Text = licenseDTO.IsActive ? "Active" : "Inactive";
            lblDateOfBirth.Text = personDTO.DateOfBirth.ToShortDateString();
            lblDriverID.Text = licenseDTO.DriverID.ToString();
            lblExpirationDate.Text = licenseDTO.ExpirationDate.ToShortDateString();

            LoadPersonImage(personDTO.ImagePath);

        }


    }
}
