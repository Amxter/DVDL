using BusinessDVLD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrivingVehicleLicenseDepartment.Applications.ApplicationTypes
{
    public partial class UpdateApplicationTypes : GeneralForm
    {
        readonly IApplicationTypesServices _services;
        readonly int _applicationTypeID;
        private void LoadApplicationType(int applicationTypeID)
        {

            ApplicationTypesDTO applicationTypesDTO = _services.GetApplication(applicationTypeID);
            lblApplicationTypeID.Text = applicationTypesDTO.ID.ToString();
            txtTitle.Text = applicationTypesDTO.Title.ToString();
            txtFees.Text = applicationTypesDTO.Fees.ToString();

        }
        public UpdateApplicationTypes(int applicationTypeID, IApplicationTypesServices services )
        {
            InitializeComponent();
            _services = services ;
            _applicationTypeID = applicationTypeID;
            LoadApplicationType(_applicationTypeID);
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ValidateEmptyTextBox(object sender, CancelEventArgs e)
        {
            if (sender is TextBox Temp)
            {
                if (string.IsNullOrWhiteSpace(Temp.Text))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(Temp, "This field is required!");
                }
                else
                {
                    e.Cancel = false;
                    errorProvider1.SetError(Temp, null);
                }
            }

        }
        private void btnSave_Click(object sender, EventArgs e)
        {


            if (!this.ValidateChildren())
            {

                MessageBox.Show("Please correct the errors before saving.");
                return;
            }
            else
            {


                ApplicationTypesDTO dTO = new ApplicationTypesDTO
                {
                    ID = _applicationTypeID,
                    Fees = Convert.ToDouble(txtFees.Text),
                    Title = txtTitle.Text

                };
                if (_services.Upgrade(dTO))
                {

                    MessageBox.Show("Update added successfully.");

                }
                else
                {

                    MessageBox.Show("Failed to Update user.");
                }

            }
        }
    }
}
