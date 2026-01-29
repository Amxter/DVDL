using Driving___Vehicle_License_Department;
using Driving___Vehicle_License_Department.Login;
using Driving___Vehicle_License_Department.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresentationDVLD
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginScreen());
            // Application.Run(new GeneralForm());
        }
    }
}
