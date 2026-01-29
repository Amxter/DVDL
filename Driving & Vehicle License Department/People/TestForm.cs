using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Driving___Vehicle_License_Department.People
{
    public partial class FindPerson : GeneralForm
    {

        public delegate void DataBackEventHandler(object sender , int personID );

        public event DataBackEventHandler DataBack;
        public FindPerson()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataBack?.Invoke(this, filterPerson1.PersonID);
            this.Close();
        }
    }
}
