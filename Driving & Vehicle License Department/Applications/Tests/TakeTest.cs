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

namespace Driving___Vehicle_License_Department.Applications.Tests
{
    public partial class TakeTest : GeneralForm
    {

        public enum Mode
        {
            Add,
            Show
        }

        Mode _mode; 
        int _testID;
        int _appointmentID;
        ITestServices _testServices;

        private void SetMode()
        {
            switch (_mode)
            {
                case Mode.Add:
                    btnSave.Enabled = true;
                    txtNotes.ReadOnly = false;
                    rbPass.Enabled = true;
                    rbFail.Enabled = true;
                    break;
                case Mode.Show:
                    btnSave.Enabled = false;
                    txtNotes.ReadOnly = true;
                    rbPass.Enabled = false;
                    rbFail.Enabled = false;
                    lblUserMessage.Visible = true;
                    TestDTO testDTO =  _testServices.GetByTestAppointmentID(_appointmentID );
                    if (testDTO != null)
                    {
                        txtNotes.Text = testDTO.Notes;
                        if (testDTO.TestResult)
                        {
                            rbPass.Checked = true;
                        }
                        else
                        {
                            rbFail.Checked = true;
                        }
                    }



                    break;
                default:
                    break;
            }
        }
        public TakeTest(int TestID , int AppointmentID, Mode mode , ITestServices testServices   )
        {

            InitializeComponent();
            _mode = mode;
            _testID = TestID;
            _appointmentID = AppointmentID;
            _testServices = testServices;
            secheduledTestInto1.LoadData(TestID, AppointmentID);
            SetMode();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            TestDTO testDTO = new TestDTO();
            testDTO.TestAppointmentID = _appointmentID;
            testDTO.TestResult = rbPass.Checked;
            testDTO.Notes = txtNotes.Text;
            testDTO.CreatedByUserID = CurrentUser.LoggedInUser.UserID;


            if (_testServices.Add(testDTO) !=-1 )
            {
                MessageBox.Show("Test Result Saved Successfully");

            }
            else
            {
                MessageBox.Show("Failed to Save Test Result");
            }
            this.Close();
        }
    }
}
