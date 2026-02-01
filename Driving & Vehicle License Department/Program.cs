using BusinessDVLD;
using DatabaseDVLD;
using Driving___Vehicle_License_Department;
using Driving___Vehicle_License_Department.Applications.ApplicationTypes;
using Driving___Vehicle_License_Department.Applications.Local_Driving_License;
using Driving___Vehicle_License_Department.Applications.ManageTestTypes;
using Driving___Vehicle_License_Department.Login;
using Driving___Vehicle_License_Department.People;
using Driving___Vehicle_License_Department.People.User_Controls;
using Driving___Vehicle_License_Department.Users;
using System;
using System.Windows.Forms;
using Unity;
using Unity.Resolution;

namespace PresentationDVLD
{
    internal static class Program
    {
        public static IUnityContainer Container { get; private set; }

        [STAThread]
        static void Main()
        {
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);

            Container = new UnityContainer();

            // Repositories
            Container.RegisterType<IPersonRepository, PersonRepository>();
            Container.RegisterType<IUserRepository, UserRepository>();
            Container.RegisterType<IApplicationRepository, ApplicationRepository>();
            Container.RegisterType<IApplicationTypesRepository, ApplicationTypesRepository>();
            Container.RegisterType<ICountryRepository, CountryRepository>();
            Container.RegisterType<ILicenseClassRepository, LicenseClassRepository>();
            Container.RegisterType<IManageTestTypesRepository, ManageTestTypesRepository>();
            Container.RegisterType<ILDLApplicationRepository, LDLApplicationRepository>();
            Container.RegisterType<ILogger, FileLogger>();

            // Services
            Container.RegisterType<IPersonServices, PersonServices>();
            Container.RegisterType<IUserServices, UserServices>();
            Container.RegisterType<IApplicationServices, ApplicationServices>();
            Container.RegisterType<IApplicationTypesServices, ApplicationTypesServices>();
            Container.RegisterType<ICountryServices, CountryServices>();
            Container.RegisterType<ILicenseClassServices, LicenseClassServices>();
            Container.RegisterType<IManageTestTypesServices, ManageTestTypesServices>();
            Container.RegisterType<ILDLApplicationServices, LDlApplicationServices>();

            // Forms
            Container.RegisterType<LoginScreen>();
            Container.RegisterType<PeopleManagement>(); 
            Container.RegisterType<UpdateApplicationTypes>();
            Container.RegisterType<Driving___Vehicle_License_Department.ApplicationTypes>();
            Container.RegisterType<AddUpdateLocalDrivingLicenseApplication>();
            Container.RegisterType<ListLocalDrivingLicenseApplication>();
            Container.RegisterType<ManageTestTypes>();
            Container.RegisterType<AddAndUpdatePerson>();
            Container.RegisterType<PeopleManagement>();
            Container.RegisterType<AddUpdateUser>();
            Container.RegisterType<ChangePassword>();
            Container.RegisterType<ListUsers>();
 
            var login = Container.Resolve<LoginScreen>();
            System.Windows.Forms.Application.Run(login);
           // System.Windows.Forms.Application.Run(new TestForm());

        }
    }
}
