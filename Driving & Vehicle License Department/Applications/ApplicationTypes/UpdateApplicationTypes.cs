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

namespace Driving___Vehicle_License_Department.Applications.ApplicationTypes
{
    public partial class UpdateApplicationTypes : GeneralForm
    {
        IApplicationTypesServices _services;

        int _ID;
        private void Initialize(DataRow dataRow)
        {
            _ID = Convert.ToInt32(dataRow["ApplicationTypeID"]);
            lblApplicationTypeID.Text = _ID.ToString();
            txtTitle.Text = dataRow["ApplicationTypeTitle"].ToString();
            txtFees.Text = dataRow["ApplicationFees"].ToString();

        }
        public UpdateApplicationTypes(DataRow dataRow)
        {
            InitializeComponent();
            Initialize(dataRow);
            _services = ServiceFactory.CreateApplicationTypesServices();
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
                    ID = _ID,
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
