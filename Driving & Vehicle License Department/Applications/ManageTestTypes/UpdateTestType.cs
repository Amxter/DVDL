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

namespace Driving___Vehicle_License_Department.Applications.ManageTestTypes
{
    public partial class UpdateTestType : GeneralForm
    {
        int _ID;

        IManageTestTypesServices _manageTestTypesServices;

        private void Initial (DataRow dataRow )
        {
            _ID = Convert.ToInt32( dataRow["TestTypeID"] );
            lblTestTypeID.Text = _ID.ToString() ;
            txtDescription.Text = dataRow["TestTypeDescription"].ToString();
            txtTitle.Text = dataRow["TestTypeTitle"].ToString();
            txtFees.Text = dataRow["TestTypeFees"].ToString() ;

        }
        public UpdateTestType(DataRow dataRow )
        {
            InitializeComponent();
            _manageTestTypesServices = ServiceFactory.CreateManageTestTypesServices();
          
            Initial(dataRow);
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
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


                TestDTO dTO = new TestDTO
                {
                    ID = _ID,
                    Fees = Convert.ToDouble(txtFees.Text),
                    Title = txtTitle.Text,
                    Description = txtDescription.Text

                };
                if (_manageTestTypesServices.Upgrade(dTO))
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
