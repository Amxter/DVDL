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

namespace Driving___Vehicle_License_Department.Applications
{
    public partial class ApplicationBasicInfo : UserControl
    {
        IApplicationServices applicationServices;
        IUserServices userServices;
        IPersonServices personServices;
        IApplicationTypesServices applicationTypesServices;
        int _personID ;

        public int PersonID
        {
            get { return _personID; }
        }
        public void loadData(int ApplicationID )
        {

           ApplicationDTO applicationDTO =  applicationServices.GetByApplicationID(ApplicationID);


            if (applicationDTO != null)
            {
                lblApplicationID.Text = applicationDTO.ApplicationID.ToString();
                lblCreatedByUser.Text = userServices.GetByID( applicationDTO.CreatedByUserID).UserName.ToString();
                lblDate.Text = applicationDTO.ApplicationDate.ToString("dd/MM/yyyy");
                lblStatusDate.Text = applicationDTO.LastStatusDate.ToString("dd/MM/yyyy");
                lblFees.Text = applicationDTO.PaidFees.ToString("C2");
                lblApplicant.Text = personServices.GetByID (applicationDTO.ApplicantPersonID ).FullName.ToString()  ;
                _personID = applicationDTO.ApplicantPersonID;

                if (applicationDTO.ApplicationTypeID == 1)
                    lblStatus.Text = "New";
                else if (applicationDTO.ApplicationTypeID == 2)
                    lblStatus.Text = "Canceled";
                else if (applicationDTO.ApplicationTypeID == 3)
                    lblStatus.Text = "Completed";
                else
                    lblStatus.Text = "Other";

                lblType.Text = applicationTypesServices.GetApplication (applicationDTO.ApplicationTypeID).Title.ToString();
              
            }
            else
            {
                MessageBox.Show("No data found for the given Application ID.");
            }
             
        }
        public ApplicationBasicInfo()
        {
            InitializeComponent();
            applicationServices = ServiceFactory.CreateApplicationServices();
            userServices = ServiceFactory.CreateUserServices();
            personServices = ServiceFactory.CreatePersonServices();
            applicationTypesServices = ServiceFactory.CreateApplicationTypesServices();
        }

        private void llViewPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowDetailsPerson showDetailsPerson = new ShowDetailsPerson (_personID);
            showDetailsPerson.ShowDialog();
        }
    }
}
