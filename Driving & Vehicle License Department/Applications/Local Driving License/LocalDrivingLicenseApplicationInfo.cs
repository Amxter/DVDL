using BusinessDVLD;
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

namespace Driving___Vehicle_License_Department.Applications.Application
{
    public partial class LocalDrivingLicenseApplicationInfo : UserControl
    {

      readonly ILDLApplicationServices _dlApplicationServices;
      readonly ILicenseClassServices _licenseClassServices;
        readonly ILicenseService _licenseService;
         

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
            _licenseService = ServiceFactory.CreateLicenseServices();
        }
        public void LoadData(int localApplicationID)
        {
             _lDLApplicationDTO =  _dlApplicationServices.GetByID(localApplicationID);
            if (_lDLApplicationDTO != null)
            {
                _localDrivingLicenseApplicationID = _lDLApplicationDTO.LocalDrivingLicenseApplicationID;
                lblLocalDrivingLicenseApplicationID.Text = _lDLApplicationDTO.LocalDrivingLicenseApplicationID.ToString();
                lblAppliedFor.Text = _licenseClassServices.GetByID(_lDLApplicationDTO.LicenseClassID ).ClassName.ToString() ;
                lblPassedTests.Text = _dlApplicationServices.GetPassedTestCount(localApplicationID).ToString() + "/3";
                applicationBasicInfo1.loadData(_lDLApplicationDTO.Application.ApplicationID);

            }
            else
            {
                MessageBox.Show("No data found for the given Application ID.");
            }


            if (_licenseService.IsExistsLicenseByLDLApplication(_localDrivingLicenseApplicationID))
            {
                llShowLicenceInfo.Enabled = true  ;
            }
            else
            {
                llShowLicenceInfo.Enabled = false ;
            }
        }
        private void llShowLicenceInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LDLApplicationDTO lDLApplicationDTO = _dlApplicationServices.GetByID(_localDrivingLicenseApplicationID);
            var frm = Program.Container.Resolve<ShowLicensesInfo>(
            new ParameterOverride("licenseID", _licenseService.GetByApplicationID(lDLApplicationDTO.Application.ApplicationID).LicenseID));
            frm.ShowDialog();
        }
    
    }
}
