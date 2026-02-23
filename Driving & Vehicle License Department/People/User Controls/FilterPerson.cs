
using PresentationDVLD;
using System;
using System.Windows.Forms;
using Unity;

namespace DrivingVehicleLicenseDepartment.People.User_Controls
{
    public partial class FilterPerson : UserControl
    {
 
        public int PersonID
        {
            get
            {

                return ucDetailsPerson1.PersonID;
            }
        }
        public bool FilterEnabled
        {
            get { return _filterEnabled; }
            set
            {
                _filterEnabled = value;
                gbFilter.Enabled = value;
            }
        }
        public bool ShowAddPerson
        {
            get { return _showAddPerson; }
            set
            {
                _showAddPerson = value;
                button2.Enabled = value;
            }
        }

        bool _filterEnabled = true;
        bool _showAddPerson = true;

        public event Action<int> OnPersonSelected;
        protected virtual void PersonSelected(int personID)
        {
            Action<int> handler = OnPersonSelected;

            if (handler != null)
            {
                handler(personID);
            }
        }

        private void InitialForm()
        {
            cbFilter.SelectedIndex = 0;

        }
        public FilterPerson()
        {
            InitializeComponent();
            InitialForm();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (cbFilter.Text == "PersonID" && txbSearch.Text != "")
            {
                ucDetailsPerson1.LoadPersonDetails(Convert.ToInt32(txbSearch.Text.Trim()));
                if (OnPersonSelected != null && _filterEnabled)
                    PersonSelected(ucDetailsPerson1.PersonID);

            }
            else if (cbFilter.Text == "NationalNo" && txbSearch.Text != "")
            {

                ucDetailsPerson1.LoadPersonDetails(txbSearch.Text.Trim());
                if (OnPersonSelected != null && _filterEnabled)
                    PersonSelected(ucDetailsPerson1.PersonID);
            }
        }
        private void LoadInfoAfterAddPerson(int personID)
        {
            ucDetailsPerson1.LoadPersonDetails(personID);
            cbFilter.SelectedIndex = 0;
            txbSearch.Text = personID.ToString();

        }
        public void FilterPersonByID(int personID)
        {
            ucDetailsPerson1.LoadPersonDetails(personID);
            cbFilter.SelectedIndex = 0;
            txbSearch.Text = personID.ToString();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            var frm = Program.Container.Resolve<AddAndUpdatePerson>();
            frm.OnDataSent += LoadInfoAfterAddPerson;
            frm.ShowDialog();
            txbSearch.Focus();
        }
        private void FilterPerson_Load(object sender, EventArgs e)
        {
            txbSearch.Focus();
        }
        private void txbSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                button1.PerformClick();
            }
            if (cbFilter.Text == "PersonID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }
    }
}
