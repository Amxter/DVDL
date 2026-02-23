using DrivingVehicleLicenseDepartment.Applications.Tests.User_Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrivingVehicleLicenseDepartment.Applications.Tests
{
    public partial class ScheduleTestForm : GeneralForm
    {

        public ScheduleTestForm(ScheduleTest.Mode mode,
                                ScheduleTest.TestType testType,
                                int LDLApplication,
                                int? testAppointment = null)
        {
            InitializeComponent();

            if (testAppointment.HasValue)
            {
                 scheduleTest1.LoadScheduleTest(mode, testType, LDLApplication, testAppointment.Value);
            }
            else
            scheduleTest1.LoadScheduleTest(mode, testType, LDLApplication);
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
