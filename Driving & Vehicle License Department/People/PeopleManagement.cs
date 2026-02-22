using BusinessDVLD;
using DatabaseDVLD;
using Driving___Vehicle_License_Department;
using System;
using System.Data;
using System.Windows.Forms;
using Unity;
using Unity.Resolution;


namespace PresentationDVLD
{
    public partial class PeopleManagement : GeneralForm
    {
        private IPersonServices _personServices;
        private ICountryServices _countryServices;
        DataTable dt;
        enum FilterOptions
        {
            None,
            PersonID,
            FullName,
            NationalNo,
            Nationality,
            Phone,
            Email,
            Gendor
        }
        FilterOptions _FilterOptions;

        private void _LoadPeopleData()
        {
            dt = _personServices.GetAll();
            dgvPeople.DataSource = dt.DefaultView;
            labCountRecords.Text = $"Records # {dgvPeople.Rows.Count}";
            
        }
        public PeopleManagement(IPersonServices personServices , ICountryServices countryServices )
        {
            InitializeComponent();
            _personServices = personServices;
            _countryServices = countryServices ;
            _LoadPeopleData();
            dgvPeople.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }
        private void _ToggleFilterInput(bool isShow, FilterOptions filterOptions)
        {

            txbFilterBy.Visible = isShow;
            cbSearch.Visible = !isShow;
            _FilterOptions = filterOptions;
        }
        private void _loadFilterOptions(string option)
        {
            if (option == "None")
            {

                txbFilterBy.Visible = false;
                cbSearch.Visible = false;
                _FilterOptions = FilterOptions.None;
                dt.DefaultView.RowFilter = string.Empty;    
                dt.DefaultView.Sort = string.Empty;        


            }
            else if (option == "PersonID")
            {
                _ToggleFilterInput(true, FilterOptions.PersonID);
            }
            else if (option == "FullName")
            {
                _ToggleFilterInput(true, FilterOptions.FullName);
            }
            else if (option == "NationalNo")
            {
                _ToggleFilterInput(true, FilterOptions.NationalNo);
            }
            else if (option == "Email")
            {
                _ToggleFilterInput(true, FilterOptions.Email);
            }
            else if (option == "Phone")
            {
                _ToggleFilterInput(true, FilterOptions.Phone);
            }
            else if (option == "Gendor")
            {
                _ToggleFilterInput(false, FilterOptions.Gendor);
                cbSearch.DataSource = null;
                cbSearch.Items.Clear();
                cbSearch.Items.Add("Male");
                cbSearch.Items.Add("Female");
            }
            else if (option == "Nationality")
            {
                _ToggleFilterInput(false, FilterOptions.Nationality);
                cbSearch.Items.Clear();
                cbSearch.DataSource = _countryServices.GetAll();
                cbSearch.DisplayMember = "CountryName";
                cbSearch.ValueMember = "CountryID";
                cbSearch.SelectedIndex = cbSearch.FindStringExact("Jordan");

            }


        }
        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {

            _loadFilterOptions(cbFilter.Text);

        }
        private void _LoadDataBeforeSearch()
        {

        
            
            switch (_FilterOptions)
            {



                case FilterOptions.PersonID:
                    {
                        int personID;
                        if (int.TryParse(txbFilterBy.Text, out personID))
                        {

                            dt.DefaultView.RowFilter = $"PersonID = {personID}";
                            
                        }
                        else if (string.IsNullOrEmpty(txbFilterBy.Text))
                        {
                            dt.DefaultView.RowFilter = string.Empty;

                        }
                        break;
                    }
                case FilterOptions.FullName:
                    {

                        var safe = txbFilterBy.Text.Replace("'", "''");
                        dt.DefaultView.RowFilter = $"FullName LIKE '%{safe}%'";


                        break;
                    }
                case FilterOptions.NationalNo:
                    {

                        dt.DefaultView.RowFilter = $"NationalNo LIKE '%{txbFilterBy.Text}%'";
                       
                        break;
                    }
                case FilterOptions.Phone:
                    {

                        dt.DefaultView.RowFilter = $"Phone LIKE '%{txbFilterBy.Text}%'";
                       
                        break;
                    }
                case FilterOptions.Email:
                    {
                       
                         dt.DefaultView.RowFilter = $"Email LIKE '%{txbFilterBy.Text}%'";
                         
                        break;
                    }


                    
            }

       
        }
        private void txbFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (_FilterOptions == FilterOptions.PersonID || _FilterOptions == FilterOptions.Phone)
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }
        private void txbFilterBy_TextChanged(object sender, EventArgs e)
        {
            _LoadDataBeforeSearch();
        }
        private void cbSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
          
            switch (_FilterOptions)
            {
                case FilterOptions.Gendor:
                    {
                       
                        dt.DefaultView.RowFilter = $"Gendor = '{cbSearch.Text}'";
                        
                        break;
                    }
                case FilterOptions.Nationality:
                    {

                        dt.DefaultView.RowFilter = $"Nationality = '{cbSearch.Text}'";
                        
                        break;
                    }
            }

          
        }
        private void LoadPeopleData(int id )
        {
            _LoadPeopleData();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            var frm = Program.Container.Resolve<AddAndUpdatePerson>();
            frm.OnDataSent += LoadPeopleData;
            frm.ShowDialog();
 
           _LoadPeopleData();
        }
        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = Program.Container.Resolve<AddAndUpdatePerson>();
           
            frm.ShowDialog();
 
            _LoadPeopleData();
        }
        private int GetSelectedPersonID()
        {
     
            return (int)dgvPeople.CurrentRow.Cells[0].Value; 
        }
        private void editPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int personID = GetSelectedPersonID(); 
            if (personID != -1)
            {

                var frm = Program.Container.Resolve<AddAndUpdatePerson>(
                    new ParameterOverride("ID", personID) );
               
                frm.ShowDialog();
               
                
                _LoadPeopleData();
            }

            

        }
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_personServices.Delete(GetSelectedPersonID()))
            {
                MessageBox.Show("Person deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _LoadPeopleData();
            }
            else
            {
                MessageBox.Show("Failed to delete person.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int personID = GetSelectedPersonID();
            if (personID != -1)
            {
                var frm = Program.Container.Resolve<ShowDetailsPerson>(
                   new ParameterOverride("id", personID));

                frm.ShowDialog();
                _LoadPeopleData(); 
            }
            else
            {
                MessageBox.Show("Please select a person to view details.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void dgvPeople_DoubleClick(object sender, EventArgs e)
        {
            var frm = Program.Container.Resolve<ShowDetailsPerson>(
   new ParameterOverride("ID", GetSelectedPersonID()));

            frm.ShowDialog();
        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature is not implemented yet.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void phoneCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature is not implemented yet.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}