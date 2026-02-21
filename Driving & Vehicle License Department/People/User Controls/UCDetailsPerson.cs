using BusinessDVLD;
using DatabaseDVLD;
using Driving___Vehicle_License_Department.Properties;
using PresentationDVLD;
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
using Unity;
using Unity.Resolution;

namespace Driving___Vehicle_License_Department
{

    public partial class UCDetailsPerson : UserControl
    {

       public event EventHandler<PersonDTO> OnSelectedPerson ; 

        IPersonServices _personServices;
        ICountryServices _countryServices;
        int _personID;

        public int PersonID
        {
            get { return _personID; }
        }
        public UCDetailsPerson()
        {
            InitializeComponent();
            _personServices = ServiceFactory.CreatePersonServices();
            _countryServices = ServiceFactory.CreateCountryServices();
            _personID = -1;


        }
        private void loadPictureBoxImage(string Path , string Gendor )
        {
            pbPersonImage.ImageLocation = Path ;
       
            if (Path == null)
            {
                if (Gendor == "Male")
                    pbPersonImage.Image = Resources.Male_512;
                else
                    pbPersonImage.Image = Resources.Female_512 ;

            }

            pbPersonImage.ImageLocation = Path; 
        }
        private void _loadInfo (PersonDTO person)
        {

            lblPersonID.Text = person.PersonID.ToString();
            lblFullName.Text = person.FullName;
            lblNationalNo.Text = person.NationalNo.ToString();
            lblDateOfBirth.Text = person.DateOfBirth.ToShortDateString();
            if (person.Gendor == 0)
                lblGendor.Text = "Male";
            else
                lblGendor.Text = "Female";
            lblAddress.Text = person.Address;
            lblPhone.Text = person.Phone;
            lblEmail.Text = person.Email;
            lblCountry.Text = _countryServices.GetCountryNameByID(person.Nationality);


            loadPictureBoxImage(person.ImagePath, lblGendor.Text);
        }
        private void initLoadInfo()
        {
            lblPersonID.Text =  "[????]";
            lblFullName.Text = "[????]";
            lblNationalNo.Text = "[????]";
            lblDateOfBirth.Text = "[????]";
            lblGendor.Text = "[????]";
            lblAddress.Text = "[????]";
            lblPhone.Text = "[????]";
            lblEmail.Text = "[????]";
            lblCountry.Text = "[????]" ;

            loadPictureBoxImage(null, "Male");
        }
        public void LoadPersonDetails(int personID)
        {
            _personID = personID;
            PersonDTO person = _personServices.GetByID(personID);
            if (person != null)
            {
                _loadInfo(person);
            }
            else
            {
                _personID = -1;
                initLoadInfo();
              
            }
            OnSelectedPerson?.Invoke(this, person);
        }
        public void LoadPersonDetails(string nationalNo)
        {
   
            PersonDTO person = _personServices.GetByNationalNo(nationalNo);
            if (person != null)
            {
                _personID = person.PersonID;
                _loadInfo(person);
            }
            else
            {
                _personID = -1;
                initLoadInfo();
            }
            OnSelectedPerson?.Invoke(this, person);
        }
        private void llEditPersonInfo_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var frm = Program.Container.Resolve<AddAndUpdatePerson>(
               new ParameterOverride("ID", _personID));

            frm.ShowDialog();
      
  
            LoadPersonDetails(_personID);
        }
    }

}
