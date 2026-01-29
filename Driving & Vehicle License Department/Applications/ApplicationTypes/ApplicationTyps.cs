using BusinessDVLD;
using Driving___Vehicle_License_Department.Applications.ApplicationTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Driving___Vehicle_License_Department
{
    public partial class ApplicationTypes : GeneralForm
    {

        IApplicationTypesServices _services;
        public ApplicationTypes()
        {
            InitializeComponent();

            _services = ServiceFactory.CreateApplicationTypesServices();
            LoadApplications();
        }

        private void LoadApplications()
        {

            dgvApplicationTypes.DataSource = _services.GetAll();

            dgvApplicationTypes.Columns[0].Width = 100;
            dgvApplicationTypes.Columns[1].Width = 320;
            dgvApplicationTypes.Columns[2].Width = 100;

            lblRecordsCount.Text = dgvApplicationTypes.RowCount.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {

            DataGridViewRow dgvRow = dgvApplicationTypes.SelectedRows[0];
            DataRow dataRow = ((DataRowView)dgvRow.DataBoundItem).Row;

            UpdateApplicationTypes updateApplicationTypes = new UpdateApplicationTypes(dataRow);
                updateApplicationTypes.ShowDialog();
            LoadApplications();
        }
    }
}
