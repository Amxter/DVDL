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

namespace Driving___Vehicle_License_Department.Applications.ManageTestTypes
{
    public partial class ManageTestTypes : GeneralForm
    {
        IManageTestTypesServices _manageTestTypes;
        private void loadData()
        {

            dgvTestTypes.DataSource = _manageTestTypes.GetAll();

            dgvTestTypes.Columns[0].Width = 100;
            dgvTestTypes.Columns[1].Width = 150;
            dgvTestTypes.Columns[2].Width = 400;
            dgvTestTypes.Columns[3].Width = 100;

            lblRecordsCount.Text = dgvTestTypes.RowCount.ToString();

        }
        public ManageTestTypes(IManageTestTypesServices manageTestTypes)
        {
            InitializeComponent();
            _manageTestTypes = manageTestTypes ;
            loadData();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {


            DataGridViewRow dgvRow = dgvTestTypes.SelectedRows[0];
            DataRow dataRow = ((DataRowView)dgvRow.DataBoundItem).Row;
            UpdateTestType updateTestType = new UpdateTestType(dataRow);
            updateTestType.ShowDialog();
            loadData();

        }
    }
}
