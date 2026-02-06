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

namespace Driving___Vehicle_License_Department.Applications.Application
{
    public partial class LocalDrivingLicenseApplicationInfo : UserControl
    {

        ILDLApplicationServices _dlApplicationServices;
        ILicenseClassServices _licenseClassServices;
        int _localDrivingLicenseApplicationID;
        public int LDLApplicationID         {
            get { return _localDrivingLicenseApplicationID; }
        }
        LDLApplicationDTO _lDLApplicationDTO; 
        public LDLApplicationDTO LDLApplicationDTO 
        {
            get { return _lDLApplicationDTO; }  
        }
        public bool IsShowLicenseInfoEnabled
        {
            get { return llShowLicenceInfo.Enabled; }
            set { llShowLicenceInfo.Enabled = value; }
        }

        public LocalDrivingLicenseApplicationInfo()
        {
            InitializeComponent();
            _dlApplicationServices =  ServiceFactory.CreateLDLApplicationServices() ;
            _licenseClassServices = ServiceFactory.CreateLicenseClassServices();
        }

        public void LoadData(int LocalApplicationID)
        {
             _lDLApplicationDTO =  _dlApplicationServices.GetByID(LocalApplicationID);
            if (_lDLApplicationDTO != null)
            {
                _localDrivingLicenseApplicationID = _lDLApplicationDTO.LocalDrivingLicenseApplicationID;
                lblLocalDrivingLicenseApplicationID.Text = _lDLApplicationDTO.LocalDrivingLicenseApplicationID.ToString();
                lblAppliedFor.Text = _licenseClassServices.GetByID(_lDLApplicationDTO.LicenseClassID ).ClassName.ToString() ;
                lblPassedTests.Text = _dlApplicationServices.GetPassedTestCount(LocalApplicationID).ToString() + "/3";
                applicationBasicInfo1.loadData(_lDLApplicationDTO.Application.ApplicationID);

            }
            else
            {
                MessageBox.Show("No data found for the given Application ID.");
            }
        }
    }
}
