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

namespace Driving___Vehicle_License_Department.People.User_Controls
{
    public partial class FilterPerson : UserControl
    {

        IPersonServices _personServices;

        public int PersonID
        {
            get
            {

                return ucDetailsPerson1.PersonID;
            }
        }

        bool _filterEnabled = true;
        public bool FilterEnabled
        {
            get { return _filterEnabled; }
            set
            {
                _filterEnabled = value;
                gbFilter.Enabled = value;
            }
        }

        bool _showAddPerson = true;
        public bool ShowAddPerson
        {
            get { return _showAddPerson; }
            set
            {
                _showAddPerson = value;
                button2.Enabled = value;
            }
        }

        public event Action<int> OnPersonSelected;

        // Protected method to raise (fire) the event
        protected virtual void PersonSelected(int personID)
        {
            Action<int> handler = OnPersonSelected;

            if (handler != null)
            {
                handler(personID); // Fire the event and pass the parameter
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
            _personServices = ServiceFactory.CreatePersonServices();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (cbFilter.Text == "PersonID" && txbSearch.Text != "")
            {
                ucDetailsPerson1.LoadPersonDetails(Convert.ToInt32(txbSearch.Text.Trim()));
                if (OnPersonSelected != null && _filterEnabled)
                    OnPersonSelected(ucDetailsPerson1.PersonID);

            }
            else if (cbFilter.Text == "NationalNo"&& txbSearch.Text != "")
            {

                ucDetailsPerson1.LoadPersonDetails(txbSearch.Text.Trim());
                if (OnPersonSelected != null && _filterEnabled)
                    OnPersonSelected(ucDetailsPerson1.PersonID);
            }
        }
        private void _LoadInfoAfterAddPerson(int personID)
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
            AddAndUpdatePerson addPerson = new AddAndUpdatePerson();
            addPerson.OnDataSent += _LoadInfoAfterAddPerson;
            addPerson.ShowDialog();
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

            // Allow only digits if "Person ID" is selected
            if (cbFilter.Text == "PersonID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }
    }
}
