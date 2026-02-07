using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Driving___Vehicle_License_Department.Licenses.International
{
    public partial class ShowInternationalLicense : GeneralForm
    {
        public ShowInternationalLicense(int InternationalLicenseID)
        {
            InitializeComponent();
            infoInternationalLicense1.LoadInfo(InternationalLicenseID);
        }
    }
}
