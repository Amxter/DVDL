using BusinessDVLD;
using Driving___Vehicle_License_Department.Applications.ApplicationTypes;
using PresentationDVLD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;
using Unity.Resolution;

namespace Driving___Vehicle_License_Department
{
    public partial class ApplicationTypes : GeneralForm
    {

        IApplicationTypesServices _services;
        public ApplicationTypes(IApplicationTypesServices services )
        {
            InitializeComponent();

            _services = services ;
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
            int id = (int)dgvApplicationTypes.CurrentRow
              .Cells["ApplicationTypeID"].Value;


            var frm = Program.Container.Resolve<UpdateApplicationTypes>(
                new ParameterOverride("applicationTypeId", id)
            );
            frm.ShowDialog();
            LoadApplications();
        }
    }
}
