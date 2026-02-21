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
using System.IO;
using Driving___Vehicle_License_Department.Properties;

namespace Driving___Vehicle_License_Department
{


    public partial class AddAndUpdatePerson : GeneralForm
    {
        enum Mode
        {
            Update = 1, Add = -1
        }

        string _imagesFolder;
        string _ImagePath;
        string _oldImagePath;

        public delegate void SendDataDelegate(int personID);

        public event SendDataDelegate OnDataSent;

        Mode _mode;
        int _personID;
        private IPersonServices _personServices;
        private ICountryServices _countryServices;
        public AddAndUpdatePerson(IPersonServices personServices, ICountryServices countryServices, int ID = -1)
        {
            InitializeComponent();
            _imagesFolder = @"C:\\Programing with Mohamed abu Hadhod\\Course 19\\Image Person";
            _ImagePath = null;
            _oldImagePath = null;

            _personServices = personServices;
            _countryServices = countryServices;
            _loadNationality();
            dtpDateOfBirth.MaxDate = DateTime.Now.AddYears(-18);
            dtpDateOfBirth.MaxDate = DateTime.Now.AddHours(1);
            _personID = ID;
            if (_personID == -1)
            {
                this.Text = "Add Person";
                _mode = Mode.Add;
            }
            else
            {
                this.Text = "Edit Person";
                _mode = Mode.Update;
                InitialUpdateForm();
            }


        }
        private void DeleteOldImage(string oldImagePath)
        {
            if (string.IsNullOrWhiteSpace(oldImagePath))
                return;

            if (File.Exists(oldImagePath))
            {
                try
                {
                    File.Delete(oldImagePath);
                }
                catch
                {

                }
            }
        }
        private void InitialUpdateForm()
        {
            this.Text = "Edit Person";
            labMain.Text = "Edit Person";
            labPersonID.Text = "Person ID : " + _personID.ToString();

            PersonDTO person = _personServices.GetByID(_personID);

            if (person == null)
            {
                MessageBox.Show("Person not found.");
                this.Close();
                return;
            }

            txbNationalNo.Text = person.NationalNo;
            txbFirstName.Text = person.FirstName;
            txbSecondName.Text = person.SecondName;
            txbThirdName.Text = person.ThirdName;
            txbLastName.Text = person.LastName;

            dtpDateOfBirth.Value = person.DateOfBirth;
          
            if (person.Gendor == 0)
                rbMale.Checked = true;
            else if (person.Gendor == 1)
                rbFemale.Checked = true;
           
            txbAddress.Text = person.Address;
            txbPhone.Text = person.Phone;
            txtEmail.Text = person.Email;
            cbNationality.SelectedValue = person.Nationality;
            _oldImagePath = _ImagePath = person.ImagePath;
           
            if (_ImagePath != null)
                linkLabel2.Visible = true;
            pictureBox1.ImageLocation = _ImagePath;
        }
        private void UpdatePerson()
        {
            PersonDTO person = new PersonDTO
            {
                PersonID = _personID,
                NationalNo = txbNationalNo.Text,
                FirstName = txbFirstName.Text,
                SecondName = txbSecondName.Text,
                ThirdName = txbThirdName.Text,
                LastName = txbLastName.Text,
                DateOfBirth = dtpDateOfBirth.Value,
                Gendor = (short)(rbMale.Checked ? 0 : 1),
                Address = txbAddress.Text,
                Phone = txbPhone.Text,
                Email = txtEmail.Text,
                Nationality = (int)cbNationality.SelectedValue,
                ImagePath = CopyImageWithGuid(_ImagePath)

            };

            if (_personServices.Update(person))
            {
                MessageBox.Show("Person updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Failed to update person.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void _loadNationality()
        {
            DataTable dtCountries = _countryServices.GetAll();
            cbNationality.DataSource = dtCountries;
            cbNationality.DisplayMember = "CountryName";
            cbNationality.ValueMember = "CountryID";
            cbNationality.SelectedIndex = cbNationality.FindStringExact("Jordan");
        }
        private void AddPerson()
        {
            short gendor = -1;
            if (rbMale.Checked)
                gendor = 0;
            else if (rbFemale.Checked)
                gendor = 1;

            PersonDTO person = new PersonDTO
            {
                NationalNo = txbNationalNo.Text,
                FirstName = txbFirstName.Text,
                SecondName = txbSecondName.Text,
                ThirdName = txbThirdName.Text,
                LastName = txbLastName.Text,
                DateOfBirth = dtpDateOfBirth.Value,
                Gendor = gendor,
                Address = txbAddress.Text,
                Phone = txbPhone.Text,
                Email = txtEmail.Text,
                Nationality = (int)cbNationality.SelectedValue,
                ImagePath = CopyImageWithGuid(_ImagePath)

            };

            _personID = _personServices.Add(person);

            if (_personID > 0)
            {
                MessageBox.Show("Person added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _mode = Mode.Update;
                InitialUpdateForm();
                OnDataSent?.Invoke(_personID);
            }
            else
            {
                MessageBox.Show("Failed to add person.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }

        }
        private string SelectImagePath()
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Select Image";
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                ofd.Multiselect = false;

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    return ofd.FileName;
                }
            }

            return null;
        }
        private void btnSave_Click_1(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {

                MessageBox.Show("Some fields are not veiled!, put the mouse over the red icon(s) to see the error", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }


            if (_mode == Mode.Add)
            {
                AddPerson();
            }
            else if (_mode == Mode.Update)
            {
                UpdatePerson();
            }

        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private string CopyImageWithGuid(string sourceImagePath)
        {
            if (_oldImagePath != null)
            {
                if (sourceImagePath != _oldImagePath)
                    DeleteOldImage(_oldImagePath);
            }

            if (string.IsNullOrWhiteSpace(sourceImagePath))
                return null;

            if (!Directory.Exists(_imagesFolder))
                Directory.CreateDirectory(_imagesFolder);

            string extension = Path.GetExtension(sourceImagePath);
            string newFileName = $"{Guid.NewGuid()}{extension}";
            string destinationPath = Path.Combine(_imagesFolder, newFileName);

            File.Copy(sourceImagePath, destinationPath, true);

            return destinationPath;
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _ImagePath = SelectImagePath();
            if (_ImagePath != null)
            {
                pictureBox1.ImageLocation = _ImagePath;
                linkLabel2.Visible = true;
            }
        }
        private void ValidateEmptyTextBox(object sender, CancelEventArgs e)
        {
            TextBox Temp = ((TextBox)sender);
            if (string.IsNullOrEmpty(Temp.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(Temp, "This field is required!");
            }
            else
            {

                errorProvider1.SetError(Temp, null);
            }

        }
        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            if (_ImagePath == null)
                pictureBox1.Image = Resources.Male_512;
        }
        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            if (_ImagePath == null)
                pictureBox1.Image = Resources.Female_512;
        }
        private void txbNationalNo_Leave(object sender, EventArgs e)
        {
            if (_personServices.IsExistsByNationalNo(txbNationalNo.Text))
            {
                errorProvider1.SetError(txbNationalNo, "National No already exists.");
            }
            else
            {
                errorProvider1.Clear();
            }
        }
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _ImagePath = null;


            if (rbMale.Checked)
                pictureBox1.Image = Resources.Male_512;
            else
                pictureBox1.Image = Resources.Female_512;

            linkLabel2.Visible = false;
        }
        private void txbEmail_Validating(object sender, CancelEventArgs e)
        {
            if (txtEmail.Text.Trim() == "")
                return;

            if (!clsValidatoin.ValidateEmail(txtEmail.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtEmail, "Invalid Email Address Format!");
            }
            else
            {
                errorProvider1.SetError(txtEmail, null);
            }
            ;
        }
    }


}
