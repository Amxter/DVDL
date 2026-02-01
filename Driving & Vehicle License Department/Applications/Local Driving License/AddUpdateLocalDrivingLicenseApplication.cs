using BusinessDVLD;
using Driving___Vehicle_License_Department.Applications.ApplicationTypes;
using PresentationDVLD;
using System;
using System.Windows.Forms;
using Unity;
using Unity.Resolution;

namespace Driving___Vehicle_License_Department.Applications.Local_Driving_License
{
    public partial class AddUpdateLocalDrivingLicenseApplication : GeneralForm
    {

        ILicenseClassServices _licenseClassServices;
        ILDLApplicationServices _ldlApplicationServices;
        IApplicationTypesServices _applicationTypesServices;
        IUserServices _userServices;

        enum Mode { Add  , Update  }
        Mode _mode;
        ApplicationDTO _applicationDTO;
        int _LDLApplicationID;

        public AddUpdateLocalDrivingLicenseApplication(int LDLApplicationID , 
            ILicenseClassServices licenseClassServices ,
            ILDLApplicationServices  ldlApplicationServices ,
           IApplicationTypesServices applicationTypesServices ,
           IUserServices userServices ,
           IPersonServices personServices ) 
        {
            InitializeComponent();
           
            _licenseClassServices = licenseClassServices;
            _ldlApplicationServices = ldlApplicationServices;
            _applicationTypesServices = applicationTypesServices;
            _userServices = userServices;
            _LDLApplicationID = LDLApplicationID;
            _LoadLicenseClasses();
            cbLicenseClass.SelectedIndex = 2;

            if (_LDLApplicationID == -1)
            {
               
                _AddMode();
            }
            else
            {
                
                 _UpdateMode();
            }

                 

        }
        private void _UpdateMode()
        {
            this.Text = "Update Local Driving License Application";
            btnSave.Enabled = true ;
            tpApplicationInfo.Enabled = true;
            tpPersonalInfo.Enabled = false; 
            _mode = Mode.Update;

            LDLApplicationDTO lDLApplicationDTO =  _ldlApplicationServices.GetByID(_LDLApplicationID);
            _applicationDTO = lDLApplicationDTO.Application;
            lblLocalDrivingLicebseApplicationID.Text = _LDLApplicationID.ToString() ;
            filterPerson1.FilterPersonByID(lDLApplicationDTO.Application.ApplicantPersonID) ;
            cbLicenseClass.SelectedValue = lDLApplicationDTO.LicenseClassID;
            lblApplicationDate.Text = lDLApplicationDTO.Application.ApplicationDate.ToString("yyyy-MM-dd HH:mm:ss");
            lblCreatedByUser.Text = _userServices.GetByID( Convert.ToInt32 (lDLApplicationDTO.Application.CreatedByUserID ) ).UserName  ;
            lblFees.Text = _applicationTypesServices.GetApplication("New Local Driving License Service").Fees.ToString()  ;


        }
        private void _AddMode()
        {
            this.Text = "Add Local Driving License Application";
            btnSave.Enabled = false;
           
          
            tpApplicationInfo.Enabled = false;
            tpPersonalInfo.Enabled = true ;
            _mode = Mode.Add; 

        }
        private void _LoadLicenseClasses()
        {

            cbLicenseClass.DataSource = _licenseClassServices.GetAll();
            cbLicenseClass.DisplayMember = "ClassName";
            cbLicenseClass.ValueMember = "LicenseClassID";
        }
        private void AddUpdateLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
        
        }
        private void _Update()
        {
            bool isUpdate = _ldlApplicationServices.Update(new LDLApplicationDTO
            {
                LocalDrivingLicenseApplicationID = _LDLApplicationID,
                LicenseClassID = Convert.ToInt32(cbLicenseClass.SelectedValue),
                Application = _applicationDTO
            });

            if (isUpdate)
            {
                MessageBox.Show("Local driving license application has been updated successfully . ", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _UpdateMode();
            }
            else
            {
                MessageBox.Show("An error occurred while updating the local driving license application . ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }
        private void Add ()
        {

            int LDLID = _ldlApplicationServices.Add(Convert.ToInt32(cbLicenseClass.SelectedValue), filterPerson1.PersonID);
            if (LDLID > 0)
            {
                lblLocalDrivingLicebseApplicationID.Text = LDLID.ToString();
                _LDLApplicationID = LDLID;
                _UpdateMode();
                MessageBox.Show("Local driving license application has been added successfully . ", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            else
            {
                MessageBox.Show("An error occurred while adding the local driving license application . ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void btnSave_Click(object sender, EventArgs e)
        {
             if (filterPerson1.PersonID == -1)
            {
                MessageBox.Show("Please select applicant person . ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (cbLicenseClass.SelectedIndex == -1)
            {
                MessageBox.Show("Please select license class . ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_ldlApplicationServices.IsExistsApplicationByUsernameAndLicenseClass(filterPerson1.PersonID, Convert.ToInt32(cbLicenseClass.SelectedValue)))
            {
                MessageBox.Show("The applicant person already has an application for the selected license class . ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {

                if (_mode == Mode.Add)
                {
                    Add();
                }
                else if (_mode == Mode.Update)
                {
                     _Update();
                }

            }

        }
        private void filterPerson1_OnPersonSelected(int obj)
        {
            if (obj <= 0 )
            {

                btnSave.Enabled = false;
                tpApplicationInfo.Enabled = false; 
                return;
            }


            tpApplicationInfo.Enabled = true ;
            btnSave.Enabled = true ;
        }
    }
}
